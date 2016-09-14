using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing.Text;
using FontValidator.Class;
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;
using System.Threading;
using System.Windows.Forms;
using SharpFont;

namespace FontValidator
{
    class ReportMakerParameters
    {
        /// <summary>
        /// should only handdle parameter form UI to reportmaker
        /// </summary>
        public StringDictionary m_str_files = new StringDictionary();
        public StringDictionary m_cpp_files = new StringDictionary();
        public string m_num_file = "";
        public string m_ttf_file = "";
        public string m_drm_file = "";
        public double m_tolerance_width = 0.0;
        public double m_tolerance_height = 0.0;
    }

    class ReportMaker
    {
        private ReportMakerParameters m_report_maker;
        private Graphics m_graphic_context;
        private PrivateFontCollection m_privateFontCollection = new PrivateFontCollection();
        private List<IDData> m_ID_data = new List<IDData>();
        private List<DRMData> m_drm_data = new List<DRMData>();
        private HashSet<string> m_scrollable_id = new HashSet<string>();
        private Dictionary<int, string> drm_result_line = new Dictionary<int, string>();

        private IDData id_drm = new IDData();

        private int _progresscounter = 0;
        public int ProgressCounter
        {
            get
            {
                return _progresscounter;
            }
            set
            {
                _progresscounter = value;

            }
        }



        public ReportMaker(ReportMakerParameters i_report_maker)
        {
            m_report_maker = i_report_maker;

            // create graphic
            m_graphic_context = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1));
            m_graphic_context.PageUnit = GraphicsUnit.Pixel;
            m_graphic_context.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // initiate font
            if (m_report_maker.m_ttf_file != "")
            {
                m_privateFontCollection.AddFontFile(m_report_maker.m_ttf_file);
            }
        }

        public List<IDData> GetResult()
        {
            return m_ID_data;
        }

        public List<DRMData> GetResultDRM()
        {
            return m_drm_data;
        }

        public IDData GetIDDRM()
        {
            return id_drm;
        }

        public bool Generate(ProgressForm pForm)
        {
            _process_drm_file();
            _process_cpp_files(pForm);    //10

            if (m_ID_data.Count < 1)
                return false;

            _process_rc_files(pForm);          // 60, 5, 20, 5, 30 ~70
            _create_fonts(pForm);              // 5
            _queryresult(pForm);               // 5
            _queryresultdrm();

            return true;
        }

        public int ProgressReport()
        {
            return ProgressCounter;
        }

        private void _process_drm_file()
        {
            if (m_report_maker.m_drm_file.Equals(string.Empty))
                return;

            var drm = new DRMProcessor(m_report_maker.m_drm_file);
            drm.process();

            drm_result_line = drm.indexLine.ToDictionary(p => p.Key, p => p.Value);
        }

        private void _process_cpp_files(ProgressForm pForm)
        {
            var count = m_report_maker.m_cpp_files.Count;
            var cppprocessors = new List<CPPProcessor>();

            if (count > 0)
                pForm.SetProgress("Collecting IDs... ");

            int counter = 0;
            Parallel.ForEach(m_report_maker.m_cpp_files, keyValuePair =>
            {
                var cpp = new CPPProcessor(keyValuePair.Value);
                cpp.process_cpp_file();
                cppprocessors.Add(cpp);

                Interlocked.Increment(ref counter);
                pForm.SetProgress((int)(double)counter * 30 / count, "Collecting IDs... ");
            });

            var alltextrotatelines = string.Empty;

            foreach (var cpp in cppprocessors)
            {
                foreach (var i in cpp.textRotate_lines)
                {
                    alltextrotatelines += i + Environment.NewLine;
                }

                m_ID_data.AddRange(cpp.cpp_layout_IDs);
                foreach (var i in cpp.scrollable_textbox_id)
                    m_scrollable_id.Add(i);
            }

            foreach (var id in m_ID_data)
                id.rotated = alltextrotatelines.Contains(id.textbox_id).ToString().ToUpper();
        }

        private void _process_rc_files(ProgressForm pForm)
        {
            var numIDs = new HashSet<string>();   // ids for num.rc
            var textIDs = new HashSet<string>();   // ids for str.rc

            foreach (var id in m_ID_data)
            {
                textIDs.Add(id.text);
                numIDs.Add(id.width.Key);
                numIDs.Add(id.height.Key);
                numIDs.Add(id.fontsize.Key);
                numIDs.Add(id.padding.Key);

            }

            // build relevant regex            
            Func<HashSet<string>, Regex> regexBuilder = list =>
            {
                string numIdMatcher = "^(" + list.ElementAt(0);
                for (int i = 1; i < list.Count; ++i)
                {
                    numIdMatcher += "|" + list.ElementAt(i);
                }
                numIdMatcher += ")";
                numIdMatcher += @";,.([\s\S]+?)""$";
                return new Regex(numIdMatcher, RegexOptions.Multiline); ;
            };

            Regex numIdRgx = regexBuilder(numIDs);
            Regex textIdRgx = regexBuilder(textIDs);
            var rc = new RCProcessor(m_report_maker.m_num_file, m_report_maker.m_str_files,
                numIdRgx, textIdRgx);
            rc.process(pForm);

            Parallel.For(0, m_ID_data.Count, i =>
            {
                var num_pair = rc.id_numdictionary;
                var text_pair = rc.id_stringdictionary;
                var id = m_ID_data[i];
                id.numfiller(num_pair);
                id.textfiller(text_pair);
            });

        }

        private void _create_fonts(ProgressForm pForm)
        {
            HashSet<string> filter_font_size = new HashSet<string>();

            foreach (var ID in m_ID_data)
            {
                filter_font_size.Add(ID.fontsize.Value);
            }

            foreach (string font_size in filter_font_size)
            {
                var ij = Convert.ToInt16(font_size);
                Font loaded_font = new Font(m_privateFontCollection.Families[0],
                    ij, FontStyle.Regular, GraphicsUnit.Point);

                foreach (var ID in m_ID_data)
                {
                    if (ID.fontsize.Value == font_size)
                        ID.fontface = loaded_font;
                }
            }
        }

        private SizeF _measure_string_gdiplus(String str, Font fnt)
        {
            var str_temp = str;

            StringFormat drawFormat = new System.Drawing.StringFormat(StringFormat.GenericTypographic);
            SizeF gdiplussize = m_graphic_context.MeasureString(str_temp, fnt, new PointF(0, 0), drawFormat);
            return gdiplussize;
        }

        private void _queryresult(ProgressForm pForm)
        {
            Parallel.For(0, m_ID_data.Count, i =>
            {
                var id = m_ID_data[i];

                if (id.textbox_id == "TextBox_OSS_DRM_Scroll")
                {
                    id_drm = id;
                }
                else
                {
                    id.scrollable = m_scrollable_id.Contains(id.textbox_id) ? "TRUE" : "FALSE";

                    for (int j = 0; j < id.text_values.Count; ++j)
                    {
                        var strResult = id.text_values.ElementAt(j);
                        ComputeSubValue(id, ref strResult);
                    }
                }
            });




            //foreach (var i in m_ID_data)
            //{
            //    if (i.textbox_id == "TextBox_OSS_DRM_Scroll")
            //    {
            //        id_drm = i;
            //        continue;
            //    }

            //    i.scrollable = m_scrollable_id.Contains(i.textbox_id) ? "TRUE" : "FALSE";

            //    for (int j = 0; j < i.text_values.Count; ++j)
            //    {
            //        var strResult = i.text_values.ElementAt(j);
            //        ComputeSubValue(i, ref strResult);
            //    }
            //}

            //m_ID_data.Remove(id_drm);
        }

        public void ComputeSubValue(IDData id, ref StringResult strResult)
        {
            var fontService = new FontService();
            fontService.SetFont(m_report_maker.m_ttf_file);

            strResult.calc_base_width = fontService.MeasureString(strResult.value_string, id.fontface.Size).Key;
            strResult.calc_base_height = fontService.MeasureString(strResult.value_string, id.fontface.Size).Value;

            fontService.Dispose();


            // swap values between height and width for rotated textbox
            if (id.rotated == "TRUE")
            {
                var temp = strResult.calc_base_width;
                strResult.calc_base_width = strResult.calc_base_height;
                strResult.calc_base_height = temp;
            }

            if (id.multiline == "TRUE")
            {
                var raw_value = strResult.calc_base_width / Convert.ToDouble(id.width.Value);
                strResult.calc_row_count = Math.Ceiling(raw_value);
                strResult.is_ok_width = true;
            }
            else
            {
                strResult.calc_row_count = 1;
            }

            if (id.scrollable == "TRUE")
            {
                strResult.is_ok_width = true;
            }
            else if (id.multiline == "FALSE")
            {
                var total_width = Convert.ToDouble(id.width.Value);
                strResult.is_ok_width = (strResult.calc_base_width < total_width);
                strResult.calc_w_tolerance_width = strResult.calc_base_width - total_width;

                if (!strResult.is_ok_width)
                    strResult.is_ok_width = (Math.Abs(strResult.calc_base_width - total_width) < m_report_maker.m_tolerance_width);
            }

            var total_height = Convert.ToDouble(id.height.Value);

            strResult.is_ok_height = (strResult.calc_base_height < total_height);
            strResult.calc_w_tolerance_height = strResult.calc_base_height - total_height;

            if (!strResult.is_ok_height)
                strResult.is_ok_height = (Math.Abs(strResult.calc_base_height - total_height) < m_report_maker.m_tolerance_height);
        }

        private void _queryresultdrm()
        {
            if (m_report_maker.m_drm_file.Equals(""))
                return;

            if (!m_report_maker.m_drm_file.Equals("") && id_drm.textbox_id == null)
            {
                MessageBox.Show("DRM layout ID not found from selected CPP files." + Environment.NewLine +
                    "Therefore, DRM data will be excluded from the report", "Warning!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // font size. 
            // id layout width and value
            // tolerance

            // line number
            // string value
            // width
            // width test

            Font font = new Font(m_privateFontCollection.Families[0], 18
                /*Convert.ToInt16(id_drm.fontsize.Value)*/, FontStyle.Regular, GraphicsUnit.Point);

            foreach (var i in drm_result_line)
            {
                var drmData = new DRMData();
                drmData.lineNumber = i.Key;

                // should get rid of escaped characters here
                drmData.stringValue = Regex.Unescape(i.Value);
                //drmData.stringValue = i.Value;

                //var base_width = _measure_string_gdiplus(i.Value, font).Width;
                var fontService = new FontService();
                fontService.SetFont(m_report_maker.m_ttf_file);
                var base_width = fontService.MeasureString(drmData.stringValue, font.Size).Key;

                fontService.Dispose();

                if (base_width == 0.0)
                    continue;

                drmData.widthValue = base_width;

                //var w_tolerance_width = Convert.ToDouble(base_width) * (1 + m_report_maker.m_tolerance_width / 100);

                drmData.layoutwidthtotal = Convert.ToDouble(id_drm.width.Value) /*- 2 * Convert.ToDouble(id_drm.padding.Value)*/;
                //drmData.widthTest = w_tolerance_width < drmData.layoutwidthtotal;

                //NOTE: check twice to prevent case where widthValue is small enough that it failed the test 
                //
                drmData.widthTest = (drmData.widthValue < drmData.layoutwidthtotal);

                if (!drmData.widthTest)
                    drmData.widthTest = (Math.Abs(drmData.widthValue - drmData.layoutwidthtotal) < 5);

                m_drm_data.Add(drmData);
            }

        }
    }
}
