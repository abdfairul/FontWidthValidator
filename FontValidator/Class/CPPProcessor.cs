using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDValuePair = System.Collections.Generic.KeyValuePair<string, string>;

namespace FontValidator
{
    class CPPProcessor
    {
        public ConcurrentBag<IDData> cpp_layout_IDs = new ConcurrentBag<IDData>();
        public ConcurrentBag<string> scrollable_textbox_id = new ConcurrentBag<string>();
        public String cpp_file;

        public ConcurrentBag<string> textRotate_lines = new ConcurrentBag<string>();

        public CPPProcessor(String file)
        {
            cpp_file = file;
        }

        public void process_cpp_file()
        {
            try
            {
                using (StreamReader sr = new StreamReader(cpp_file))
                {
                    string oneline = "";
                    List<string> alltext = new List<string>();
                    while ((oneline = sr.ReadLine()) != null)
                    {
                        alltext.Add(oneline);
                    }

                    bool begin_collection = false;
                    string bunch_of_lines = "";
                    var bunch_of_lines_collection = new List<string>();

                    for (int i = 0; i < alltext.Count; ++i)
                    {
                        var text = alltext.ElementAt(i);

                        // collect all line with "style.text_rotate".
                        if (Regex.IsMatch(text, @"style.text_rotate"))
                        {
                            textRotate_lines.Add(text);
                        }

                        const string header = @"rc.set";
                        const string footer = @"AddChild";
                        bool header_found = Regex.Match(text, header).Success;
                        bool footer_found = Regex.Match(text, footer).Success;

                        if (header_found)
                        {
                            begin_collection = header_found;
                            bunch_of_lines += text;
                        }
                        else if (footer_found)
                        {
                            begin_collection = !footer_found;
                            bunch_of_lines += text;
                            bunch_of_lines_collection.Add(bunch_of_lines);
                            bunch_of_lines = "";
                        }
                        else if (begin_collection)
                        {
                            bunch_of_lines += text;
                        }

                        // scroll text collection we do it here since it doesnt require block
                        var rgx = new Regex(@"{\sStartTextScroll\(([\s\S]+?)\);");
                        var result = rgx.Match(text);
                        if (result.Success)
                            scrollable_textbox_id.Add(result.Groups[1].Value);
                    }

                    int regex_count = 6;
                    Regex[] rgxs = new Regex[regex_count];

                    rgxs[0] = new Regex(@"(?:[a-zA-Z_0-9]+_X)\)(?:.*?)GetRcEngine::RcGetNum(?:.*?)(?:[a-zA-Z_0-9]+_Y)\)(?:.*?)" +
                        @"GetRcEngine::RcGetNum(?:.*?)([a-zA-Z_0-9]+_W)\)(?:.*?)GetRcEngine::RcGetNum(?:.*?)([a-zA-Z_0-9]+_H)\)");   //find_width_id, height
                    rgxs[1] = new Regex(@"text.Set\((ID\w+)");   //find_text_id
                    rgxs[2] = new Regex(@"(ID_FONT_SIZE\w+)");   //find_fontsize_id
                    rgxs[3] = new Regex(@"SetMultiline\((\w+)");   //find_multiline_flag
                    rgxs[4] = new Regex(@"E_WDG_PADDING_INT(?:.*?)GetRcEngine::RcGetNum(?:.*?)([A-Z_0-9]+)\);");   //find internal padding
                    rgxs[5] = new Regex(@"([\S]+)(?:\s=\snew\s\(std::nothrow\))"); // find textbox id

                    Parallel.For(0, bunch_of_lines_collection.Count, i =>
                    {
                        var bunch_lines = bunch_of_lines_collection.ElementAt(i);

                        bool[] is_valid = new bool[] { false, false, false, false }; // 0, 1, 2, 5 must be valid
                        bool is_empty_string = false;
                        bool is_drm_textbox = false;

                        var text_id_drm = "TextBox_OSS_DRM_Scroll";

                        var default_pair_value = new IDValuePair("ID", "1");
                        var id = new IDData(default_pair_value, default_pair_value, "ID",
                            default_pair_value, default_pair_value, "FILE", "FALSE");

                        for (int j = 0; j < regex_count; ++j)
                        {
                            Match m = rgxs[j].Match(bunch_lines);
                            if (m.Success)
                            {
                                if (j == 0)
                                {
                                    id.width = new IDValuePair(m.Groups[1].Value, id.width.Value);
                                    id.height = new IDValuePair(m.Groups[2].Value, id.height.Value);
                                    is_valid[0] = true;
                                }
                                else if (j == 1)
                                {
                                    //hack: remove ids with empty string
                                    if (m.Groups[1].Value == "ID_STR_EMPTY" || m.Groups[1].Value == "ID_STR_DEFAULT")
                                        is_empty_string = true;

                                    id.text = m.Groups[1].Value;
                                    is_valid[1] = true;
                                }
                                else if (j == 2)
                                {
                                    id.fontsize = new IDValuePair(m.Groups[1].Value, id.fontsize.Value);
                                    is_valid[2] = true;
                                }
                                else if (j == 3)
                                {
                                    id.multiline = m.Groups[1].Value;
                                }
                                else if (j == 4)
                                {
                                    id.padding = new IDValuePair(m.Groups[1].Value, id.padding.Value);
                                }
                                else if (j == 5)
                                {
                                    id.textbox_id = m.Groups[1].Value;
                                    is_valid[3] = true;

                                    if (id.textbox_id == text_id_drm)
                                        is_drm_textbox = true;
                                }
                            }
                        }

                        bool normal_id = !is_empty_string && is_valid[0] && is_valid[1] && is_valid[2] && is_valid[3];
                        bool drm_id = is_drm_textbox && is_empty_string;

                        if (normal_id || is_drm_textbox)
                        {
                            id.filepath = cpp_file;
                            cpp_layout_IDs.Add(id);
                        }
                    });
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
