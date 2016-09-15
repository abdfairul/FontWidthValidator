using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using AeroWizard;
using FontValidator.Class;
using Microsoft.Office.Interop.Excel;
using Action = System.Action;
using Application = Microsoft.Office.Interop.Excel.Application;
using CheckBox = System.Windows.Forms.CheckBox;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;

namespace FontValidator
{
    public partial class UIWizard : Form
    {
        private DRMForm _drmForm;
        private IDData _idDrm = new IDData();
        private readonly ListViewColumnSorter _lvwColumnSorter;
        private IEnumerable<Control> _mCheckboxControls;
        private Checkboxes _mCheckboxes;

        // this flag to indicate should we update or not
        private bool _mReportGenerated;
        private readonly ReportMakerParameters _mReportParam = new ReportMakerParameters();

        private List<IDData> _mResultDatas;

        private List<DRMData> _mResultDrmData = new List<DRMData>();
        private string _mSelectedString = "";

        private string _mSelectedTabReport;
        private IEnumerable<Control> _mTabChildrenControls;
        private SearchItem _searchItem;
        private bool _validContextmenuLocation;
        private ReportMaker _reportMaker;
        private System.Windows.Forms.TextBox txtEditor = 
            new System.Windows.Forms.TextBox { BorderStyle = BorderStyle.FixedSingle, Visible = false };

        public struct SearchItem
        {
            public int currentSearchIndexListViewItem;
            public int currentSearchIndexListViewSubItem;
            public string currentSearchString;
            public ListViewItem.ListViewSubItem previousFoundSubItem;
            public Color previousFoundSubItemColor;
        }

        public UIWizard()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(800, 600);
            WindowState = FormWindowState.Maximized;
            TopLevel = true;

            
            _lvwColumnSorter = new ListViewColumnSorter();

            Action<object, EventArgs> leaveAction = (s, a) =>
            {
                txtEditor.Visible = false;
                var info = txtEditor.Tag as ListViewHitTestInfo;
                var subitem = info.SubItem;
                subitem.Text = txtEditor.Text;

                var idData = _mResultDatas[Convert.ToInt16(info.Item.Tag)];
                var textValues = idData.text_values;
                StringResult textValue = null;
                foreach (var i in textValues)
                {
                    if (i.label.Equals(_mSelectedTabReport))
                    {
                        textValue = i;
                        break;
                    }
                }
                if (textValue == null)
                    return;

                textValue.value_string = info.SubItem.Text;

                // compute new string
                _reportMaker.ComputeSubValue(idData, ref textValue);

                // assign back values to listview
                info.Item.SubItems[14].Text = textValue.value_string;
                info.Item.SubItems[15].Text = textValue.calc_base_width.ToString();
                info.Item.SubItems[16].Text = textValue.calc_w_tolerance_width.ToString();
                info.Item.SubItems[17].Text = textValue.calc_base_height.ToString();
                info.Item.SubItems[18].Text = textValue.calc_w_tolerance_height.ToString();
                info.Item.SubItems[19].Text = textValue.calc_row_count.ToString();

                info.Item.SubItems[20].Text = textValue.is_ok_width ? "OK" : "NOT OK";
                info.Item.SubItems[20].BackColor = textValue.is_ok_width ? info.Item.SubItems[20].BackColor : Color.Red;
                info.Item.SubItems[21].Text = textValue.is_ok_width ? "OK" : "NOT OK";
                info.Item.SubItems[21].BackColor = textValue.is_ok_width ? info.Item.SubItems[21].BackColor : Color.Red;

                // recolor to white if ok, red if not
                foreach (var k in info.Item.SubItems)
                {
                    var m = (ListViewItem.ListViewSubItem)k;

                    if (!textValue.is_ok_height || !textValue.is_ok_width)
                    {
                        if (m.BackColor != Color.Red)
                            m.BackColor = Color.MistyRose;
                    }
                    else
                        m.BackColor = default(Color);
                }

            };

            txtEditor.Leave += leaveAction.Invoke;
        }

        public sealed override Size MinimumSize
        {
            get { return base.MinimumSize; }
            set { base.MinimumSize = value; }
        }

        private void ResourceFilesAddStr_Click(object sender, EventArgs e)
        {
            var op = new OpenFileDialog();
            op.Multiselect = true;
            op.Filter = "Resource files|Str.rc";

            if (!ResourceFilesStrListView.Enabled)
            {
                ResourceFilesStrListView.Enabled = true;
                ResourceFilesStrListView.Items.Clear();
                ResourceFilesStrListView.ForeColor = SystemColors.WindowText;
                ResourceFilesStrListView.Font = new Font("Segoe UI", 12,
                    FontStyle.Regular, GraphicsUnit.World, 0);
            }

            var count = ResourceFilesStrListView.Items.Count + 1;

            if (op.ShowDialog() == DialogResult.OK)
            {
                foreach (var filename in op.FileNames)
                {
                    string[] row = { count.ToString(), filename };
                    var item = new ListViewItem(row);

                    var exist = false;
                    foreach (ListViewItem it in ResourceFilesStrListView.Items)
                    {
                        if (it.SubItems[1].Text.Contains(filename))
                        {
                            exist = true;
                            break;
                        }
                    }

                    if (!exist)
                        ResourceFilesStrListView.Items.Add(item);
                    else
                        MessageBox.Show(@"Item already added.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _mReportGenerated = false;
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var list = (ListView)sender;
            list.SelectedItems[0].BeginEdit();
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            var list = (ListView)sender;

            if (e.KeyData == Keys.F2)
                list.SelectedItems[0].BeginEdit();

            if (e.KeyData == Keys.Delete)
            {
                for (var i = list.SelectedItems.Count - 1; i >= 0; i--)
                    list.SelectedItems[i].Remove();

                // update count number
                int count = 1;
                foreach (var i in list.Items)
                {
                    var item = i as ListViewItem;
                    item.Text = count.ToString();
                    count++;
                }
            }

            if (e.KeyCode == Keys.A && e.Control)
            {
                for (var i = 0; i < list.Items.Count; ++i)
                    list.Items[i].Selected = true;
            }
        }

        private void ResourceFilesBrowseNum_Click(object sender, EventArgs e)
        {
            var op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Resource file|Num.rc";

            if (!ResourceFileNumTextBox.Enabled)
                ResourceFileNumTextBox.Enabled = true;

            if (op.ShowDialog() == DialogResult.OK)
            {
                _mReportParam.m_num_file = op.FileName;
                _mReportGenerated = false;
            }

            ResourceFileNumTextBox.Text = _mReportParam.m_num_file;
        }

        private void FontFileBrowse_Click(object sender, EventArgs e)
        {
            var op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Font file|*.ttf";

            if (!FontFileTextBox.Enabled)
                FontFileTextBox.Enabled = true;

            if (op.ShowDialog() == DialogResult.OK)
            {
                _mReportParam.m_ttf_file = op.FileName;
                _mReportGenerated = false;
            }


            FontFileTextBox.Text = _mReportParam.m_ttf_file;
        }

        private void DRMFileBrowse_Click(object sender, EventArgs e)
        {
            var op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Drm file|*drm.h";

            if (!DRMFileTextBox.Enabled)
                DRMFileTextBox.Enabled = true;

            if (op.ShowDialog() == DialogResult.OK)
            {
                _mReportParam.m_drm_file = op.FileName;
                _mReportGenerated = false;
            }

            DRMFileTextBox.Text = _mReportParam.m_drm_file;
        }

        private void CPPFilesAdd_Click(object sender, EventArgs e)
        {
            var op = new OpenFileDialog();
            op.Multiselect = true;
            op.Filter = "CPP files|*.cpp";

            if (!CPPFilesListView.Enabled)
            {
                CPPFilesListView.Enabled = true;
                CPPFilesListView.Items.Clear();
                CPPFilesListView.ForeColor = SystemColors.WindowText;
                CPPFilesListView.Font = new Font("Segoe UI", 12,
                    FontStyle.Regular, GraphicsUnit.World, 0);
            }

            var count = CPPFilesListView.Items.Count + 1;

            if (op.ShowDialog() == DialogResult.OK)
            {
                foreach (var filename in op.FileNames)
                {
                    var f = new FileInfo(filename);
                    string[] row = { count.ToString(), filename, f.Length / 1024 + " KB" };

                    var exist = false;
                    foreach (ListViewItem it in CPPFilesListView.Items)
                    {
                        if (it.SubItems[1].Text.Contains(filename))
                        {
                            exist = true;
                            break;
                        }
                    }

                    if (!exist)
                    {
                        CPPFilesListView.Items.Add(new ListViewItem(row));
                        count++;
                    }
                    else
                        MessageBox.Show("Item already added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _mReportGenerated = false;
            }
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            var chxbox = (CheckBox)sender;

            if (!chxbox.Name.Contains("tab_"))
            {
                _mCheckboxes.m_layout_checkboxes[chxbox.Tag.ToString()] = chxbox.Checked;
            }
            else
            {
                if (_mCheckboxes.m_tab_checkboxes.ContainsKey(_mCheckboxes.current_selected_tab_name))
                {
                    var checkboxes = _mCheckboxes.m_tab_checkboxes[_mCheckboxes.current_selected_tab_name];
                    checkboxes.m_tab_checkbox[chxbox.Tag.ToString()] = chxbox.Checked;
                }
            }
        }

        private void Item2IncludeTabs_Selected(object sender, TabControlEventArgs e)
        {
            if (!_mTabChildrenControls.Any())
                return;

            for (var i = _mTabChildrenControls.Count() - 1; i >= 0; i--)
                _mTabChildrenControls.ElementAt(i).Parent = e.TabPage;

            _mCheckboxes.current_selected_tab_name = e.TabPage.Text;

            // reset checkbox depend on param
            foreach (var control in _mCheckboxControls)
            {
                var button = (CheckBox)control;

                if (button.Name.Contains("tab_"))
                {
                    //for (int i = 0; i < m_checkboxes.m_tab_checkboxes.Count; ++i)
                    //{
                    //    var k = m_checkboxes.m_tab_checkboxes.ElementAt(i);
                    //    if (k.tab_name == m_checkboxes.current_selected_tab_name)
                    //    {
                    //        button.Checked = k.m_tab_checkbox[button.Tag.ToString()];
                    //        break;
                    //    }
                    //}

                    if (_mCheckboxes.m_tab_checkboxes.ContainsKey(_mCheckboxes.current_selected_tab_name))
                    {
                        var checkboxes = _mCheckboxes.m_tab_checkboxes[_mCheckboxes.current_selected_tab_name];
                        button.Checked = checkboxes.m_tab_checkbox[button.Tag.ToString()];
                    }
                }
                else
                    button.Checked = _mCheckboxes.m_layout_checkboxes[button.Tag.ToString()];
            }
        }

        private void Item2IncludeTabs_Deselected(object sender, TabControlEventArgs e)
        {
            _mTabChildrenControls = e.TabPage.Controls.OfType<Control>();
        }

        private void populate_report(ProgressForm pForm, bool hide_width_test = false, bool hide_height_test = false)
        {
            var tab = 0;

            var totalCount = _mCheckboxes.m_tab_checkboxes.Count * _mResultDatas.Count;
            var progressCount = 0;
            var currentProgressBar = pForm.ProgressBar.Value;

            foreach (var i in _mCheckboxes.m_tab_checkboxes)
            {
                var count = 1;

                var tabpage = reporttab.TabPages[tab];
                var listviews = tabpage.GetAll(typeof(ListView));
                var listview = (ListView)listviews.ElementAt(0);

                listview.InvokeIfRequired(() =>
                {
                    listview.Items.Clear();
                    listview.BeginUpdate();
                });

                int index = 0;
                foreach (var j in _mResultDatas)
                {
                    index++;
                    progressCount++;
                    if (j.Equals(_idDrm))
                        continue;

                    var item = new ListViewItem(count.ToString());
                    item.UseItemStyleForSubItems = false;

                    // hack: counter for tracking mresultdata
                    item.Tag = index - 1;

                    var needAddItem = true;
                    var needRecolor = false;

                    if (j.text_values.Count < 1)
                        continue;

                    item.SubItems.Add(j.filepath);
                    item.SubItems.Add(j.textbox_id);
                    item.SubItems.Add(j.width.Key);

                    item.SubItems.Add(j.width.Value);

                    item.SubItems.Add(j.height.Key);
                    item.SubItems.Add(j.height.Value);

                    item.SubItems.Add(j.padding.Key);
                    item.SubItems.Add(j.padding.Value);

                    item.SubItems.Add(j.fontsize.Key);
                    item.SubItems.Add(j.fontsize.Value);

                    item.SubItems.Add(j.multiline);
                    item.SubItems.Add(j.scrollable);
                    item.SubItems.Add(j.text);

                    foreach (var k in j.text_values)
                    {
                        if (k.label == i.Key)
                        {
                            if ((hide_height_test && k.is_ok_height) ||
                                (hide_width_test && k.is_ok_width))
                            {
                                needAddItem = false;
                                break;
                            }


                            if (hide_height_test && hide_width_test)
                            {
                                if (k.is_ok_height || k.is_ok_width)
                                {
                                    needAddItem = false;
                                    break;
                                }
                            }
                            else if (hide_width_test)
                            {
                                if (k.is_ok_width)
                                {
                                    needAddItem = false;
                                    break;
                                }
                            }
                            else if (hide_height_test)
                            {
                                if (k.is_ok_height)
                                {
                                    needAddItem = false;
                                    break;
                                }
                            }

                            if (k.value_string.Equals(""))
                            {
                                needAddItem = false;
                                break;
                            }

                            item.SubItems.Add(k.value_string);
                            item.SubItems.Add(k.calc_base_width.ToString());
                            item.SubItems.Add(k.calc_w_tolerance_width.ToString());
                            item.SubItems.Add(k.calc_base_height.ToString());
                            item.SubItems.Add(k.calc_w_tolerance_height.ToString());

                            item.SubItems.Add(k.calc_row_count.ToString());
                            var sub_width = item.SubItems.Add(k.is_ok_width ? "OK" : "NOT OK");
                            sub_width.BackColor = k.is_ok_width ? sub_width.BackColor : Color.Red;
                            var sub_height = item.SubItems.Add(k.is_ok_height ? "OK" : "NOT OK");
                            sub_height.BackColor = k.is_ok_height ? sub_height.BackColor : Color.Red;

                            needRecolor = !k.is_ok_height || !k.is_ok_width;
                            break;
                        }
                    }

                    if (needRecolor)
                    {
                        foreach (var k in item.SubItems)
                        {
                            var m = (ListViewItem.ListViewSubItem)k;
                            if (m.BackColor != Color.Red)
                                m.BackColor = Color.MistyRose;
                        }
                    }

                    if (needAddItem)
                    {
                        listview.InvokeIfRequired(() =>
                        {
                            listview.Items.Add(item);
                        });

                        ++count;
                    }

                    if (currentProgressBar == 95)
                    {
                        var percent = (double)progressCount / totalCount;
                        percent = percent * 4;

                        pForm.SetProgress(95 + (int)percent);
                    }
                    else if (currentProgressBar == 0)
                    {
                        var percent = (double)progressCount / totalCount * 100;
                        pForm.SetProgress((int)percent);
                    }
                }

                listview.InvokeIfRequired(() => { listview.EndUpdate(); });

                ++tab;
            }
        }

        private void populate_report_drm(ProgressForm pForm)
        {
            var listview = _drmForm.DRMListView;
            listview.InvokeIfRequired(() =>
            {
                listview.Items.Clear();
                listview.BeginUpdate();
            });

            var total = _mResultDrmData.Count;
            var count = 1;
            var currentProgressBar = pForm.ProgressBar.Value;

            foreach (var i in _mResultDrmData)
            {
                var item = new ListViewItem(count.ToString());
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(i.lineNumber.ToString());
                item.SubItems.Add(i.stringValue);
                item.SubItems.Add(i.widthValue.ToString());
                var sub_width = item.SubItems.Add(i.widthTest ? "OK" : "NOT OK");
                sub_width.BackColor = i.widthTest ? sub_width.BackColor : Color.Red;

                if (!i.widthTest)
                {
                    foreach (var k in item.SubItems)
                    {
                        var m = (ListViewItem.ListViewSubItem)k;
                        if (m.BackColor != Color.Red)
                            m.BackColor = Color.MistyRose;
                    }
                }

                listview.InvokeIfRequired(() => { listview.Items.Add(item); });

                count++;

                if (currentProgressBar == 0)
                {
                    var percent = (double)count / total * 100;
                    pForm.SetProgress((int)percent);
                }
            }
            listview.InvokeIfRequired(() => { listview.EndUpdate(); });

            _drmForm.InvokeIfRequired(() =>
            {
                _drmForm.DRMSummary.Text = "TextBox ID: " + _idDrm.textbox_id +
                                          "; Width: " + _idDrm.width.Value + "; Fontsize: " + 18;
            });
        }

        private void show_relevant_columns()
        {
            Action action = () =>
            {
                foreach (var tab in reporttab.TabPages)
                {
                    var tabpage = tab as TabPage;
                    var listviews = tabpage.GetAll(typeof(ListView));
                    var listview = (ListView)listviews.ElementAt(0);

                    var rgx = new Regex(@"[\d]+?_");

                    for (var i = 1; i < listview.Columns.Count; ++i)
                    {
                        var column = listview.Columns[i];
                        column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                        // search throu dictionary. because key is no longer the same so need extra step   
                        var is_in_layout = false;

                        foreach (var j in _mCheckboxes.m_layout_checkboxes)
                        {
                            var key = j.Key;
                            var key_wo_prefix = rgx.Replace(key, string.Empty);

                            if (key_wo_prefix.Equals(column.Text))
                            {
                                if (!j.Value)
                                    column.Width = 0;
                                is_in_layout = true;
                                break;
                            }
                        }

                        if (is_in_layout) // skip unnecessary loop
                            continue;

                        var m = _mCheckboxes.m_tab_checkboxes[tabpage.Text];

                        foreach (var k in m.m_tab_checkbox)
                        {
                            var key = k.Key;
                            var keyWoPrefix = rgx.Replace(key, string.Empty);

                            if (keyWoPrefix.Equals(column.Text))
                            {
                                if (!k.Value)
                                    column.Width = 0;
                                is_in_layout = true;
                                break;
                            }
                        }
                    }
                }
            };

            reporttab.Invoke(action);
        }

        private void add_header()
        {
            var sort_layout_key = _mCheckboxes.m_layout_checkboxes.Keys.ToList();
            sort_layout_key.Sort();

            var sort_tab_key = _mCheckboxes.m_tab_checkboxes.Values.ToList()[0].m_tab_checkbox.Keys.ToList();
            sort_tab_key.Sort();

            var sort_all_key = new List<string>();
            sort_all_key.AddRange(sort_layout_key);
            sort_all_key.AddRange(sort_tab_key);
            sort_all_key.Sort();

            // cleanup the list
            var rgx = new Regex(@"[\d]+?_");
            var sort_layout_key_wo_prefix = sort_layout_key.Select(x => x = rgx.Replace(x, string.Empty)).ToList();
            var sort_tab_key_wo_prefix = sort_tab_key.Select(x => x = rgx.Replace(x, string.Empty)).ToList();
            var sort_all_key_wo_prefix = sort_all_key.Select(x => x = rgx.Replace(x, string.Empty)).ToList();

            // create column header LOL      
            var tab = 0;
            foreach (var j in _mCheckboxes.m_tab_checkboxes)
            {
                Action action = () =>
                {
                    var tabpage = reporttab.TabPages[tab];
                    var listviews = tabpage.GetAll(typeof(ListView));
                    var listview = (ListView)listviews.ElementAt(0);

                    // clear all header
                    listview.Columns.Clear();

                    {
                        var k = listview.Columns.Add(new ColumnHeader());
                        listview.Columns[k].Text = "#";
                    }

                    for (var i = 0; i < sort_layout_key_wo_prefix.Count; ++i)
                    {
                        var k = listview.Columns.Add(new ColumnHeader());
                        listview.Columns[k].Text = sort_layout_key_wo_prefix.ElementAt(i);
                    }


                    for (var i = 0; i < sort_tab_key_wo_prefix.Count; ++i)
                    {
                        var k = listview.Columns.Add(new ColumnHeader());
                        listview.Columns[k].Text = sort_tab_key_wo_prefix.ElementAt(i);
                    }
                };

                reporttab.Invoke(action);
                ++tab;
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (!_getTolerance())
                return;

            _reportMaker = new ReportMaker(_mReportParam);

            var form = new ProgressForm();
            form.Text = "Generating report...";
            form.DoWork += form_DoWork;
            form.Argument = _reportMaker;

            //if(m_report_param.m_cpp_files.Count < 40)
            //form.SetReportProgress(false);

            var result = form.ShowDialog();

            if (form.Result.Result.ToString() == "ABORT")
            {
                MessageBox.Show(form, "No relevant IDs found in cpp files.\nPlease add correct CPP files", 
                    "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw (new Exception("ABORT"));
            }
            
            form.ProgressBar.MarqueeAnimationSpeed = 1;
            form.ProgressBar.Style = ProgressBarStyle.Marquee;

            if (result == DialogResult.Cancel)
            {
                //the user clicked cancel
            }
            else if (result == DialogResult.Abort)
            {
                MessageBox.Show(form.Result.Error.Message);
            }
            else if (result == DialogResult.OK)
            {
                //var results = (KeyValuePair<List<IDData>, List<DRMData>>)form.Result.Result ;
                //m_result_datas = results.Key;
                //m_result_drm_data = results.Value;
            }

            _mReportGenerated = true;
            viewDRMReport.Enabled = _mResultDrmData.Count > 0;
        }

        public void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.

            var list = (ListView)sender;
            //ReportListView_1.Sort();
            list.ListViewItemSorter = _lvwColumnSorter;

            if (e.Column == _lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    _lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _lvwColumnSorter.SortColumn = e.Column;
                _lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.

            list.Sort();
        }

        private void _createReportTabs()
        {
            if (_mReportGenerated)
                return;

            var count = _mCheckboxes.m_tab_checkboxes.Count;

            // clean up added tabs
            for (var i = reporttab.TabCount - 1; i >= 1; i--)
                reporttab.TabPages.RemoveAt(i);

            var str_files_names = new List<string>();
            for (var i = 0; i < _mCheckboxes.m_tab_checkboxes.Count; ++i)
            {
                var title = _mCheckboxes.m_tab_checkboxes.Keys.ToList()[i];
                //string title = m_result_datas[0].text_values.ElementAt(i).label;
                if (i == 0)
                {
                    reporttab.TabPages[0].Text = title;

                    var tabpage = reporttab.TabPages[0];
                    tabpage.Text = title;
                    var listviews = tabpage.GetAll(typeof(ListView));
                    var listview = (ListView)listviews.ElementAt(0);
                }
                else if (i > 0) // create tab if more than 1
                {
                    var myTabPage = new TabPage(title);
                    myTabPage.Location = new Point(4, 24);
                    myTabPage.Padding = new Padding(3);
                    myTabPage.Size = new Size(489, 246);
                    myTabPage.UseVisualStyleBackColor = true;

                    var myListView = new ListView();

                    myListView.ContextMenuStrip = contextMenuStrip1;
                    myListView.Dock = DockStyle.Fill;
                    myListView.FullRowSelect = true;
                    myListView.Location = new Point(3, 3);
                    myListView.Text = "ReportListView_" + (i + 1);
                    myListView.Margin = new Padding(3, 3, 3, 3);
                    myListView.ShowGroups = false;
                    myListView.ShowItemToolTips = true;
                    myListView.Size = new Size(483, 240);
                    myListView.Sorting = SortOrder.Ascending;
                    myListView.UseCompatibleStateImageBehavior = false;
                    myListView.View = View.Details;
                    myListView.ColumnClick += ListView_ColumnClick;
                    myListView.KeyDown += ListView_KeyDown;
                    myListView.MouseClick += ListView_MouseClick;
                    myListView.MouseDoubleClick += ReportListView_MouseDoubleClick;

                    myTabPage.Controls.Add(myListView);
                    myTabPage.BackColor = Color.White;
                    reporttab.TabPages.Add(myTabPage);
                }
            }

            _mSelectedTabReport = reporttab.TabPages[0].Text;
        }

        private bool _getTolerance()
        {
            double height, width;
            if (!double.TryParse(tolerance_height.Text, out height) ||
                !double.TryParse(tolerance_width.Text, out width) ||
                height < 0 || width < 0)
            {
                MessageBox.Show("Not a valid input." + Environment.NewLine +
                                "Please use positive integer", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            _mReportParam.m_tolerance_height = height;
            _mReportParam.m_tolerance_width = width;

            return true;
        }

        private void update_tolerance_Click(object sender, EventArgs e)
        {
            if (!_getTolerance())
                return;

            foreach (var i in _mResultDatas)
            {
                foreach (var j in i.text_values)
                {
                    if (i.multiline == "TRUE")
                        j.is_ok_width = true;

                    if (i.scrollable == "TRUE")
                        j.is_ok_width = true;
                    else if (i.multiline == "FALSE")
                    {
                        var total_width = Convert.ToDouble(i.width.Value);

                        j.is_ok_width = j.calc_base_width < total_width;
                        j.calc_w_tolerance_width = j.calc_base_width - total_width;

                        if (!j.is_ok_width)
                            j.is_ok_width = Math.Abs(j.calc_base_width - total_width) < _mReportParam.m_tolerance_width;
                    }

                    var total_height = Convert.ToDouble(i.height.Value);

                    j.is_ok_height = j.calc_base_height < total_height;
                    j.calc_w_tolerance_height = j.calc_base_height - total_height;

                    if (!j.is_ok_height)
                        j.is_ok_height = Math.Abs(j.calc_base_height - total_height) < _mReportParam.m_tolerance_height;
                }
            }

            add_header();


            var formx = new ProgressForm();
            formx.Text = "Updating tolerance..";

            formx.DoWork += (sender1, args) => { populate_report(sender1); };

            formx.ShowDialog();
            show_relevant_columns();
            _mReportGenerated = true;
            searchTextBox.Text = "";
        }

        public void update_tolerance_drm(double tolerance)
        {
            var formx = new ProgressForm();
            formx.Text = "Updating tolerance..";

            formx.DoWork += (sender, args) =>
            {
                foreach (var i in _mResultDrmData)
                {
                    i.widthTest = i.widthValue < i.layoutwidthtotal;

                    if (!i.widthTest)
                        i.widthTest = Math.Abs(i.widthValue - i.layoutwidthtotal) < tolerance;
                }

                populate_report_drm(sender);
            };

            formx.ShowDialog();
        }

        private void show_fail_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _searchItem.currentSearchIndexListViewItem = 0;
            _searchItem.currentSearchIndexListViewSubItem = 0;
            _searchItem.currentSearchString = string.Empty;
            _searchItem.previousFoundSubItem = new ListViewItem.ListViewSubItem();
            _searchItem.previousFoundSubItemColor = default(Color);
            searchTextBox.Text = "";

            var formx = new ProgressForm();
            formx.Text = "Updating selection..";

            formx.DoWork += (sender1, args) =>
            {
                var selectedItem = "";
                show_fail_combobox.InvokeIfRequired(
                    () => { selectedItem = show_fail_combobox.SelectedItem?.ToString(); });

                if (selectedItem == "All")
                    populate_report(formx);
                else if (selectedItem == @"Failed ""WidthTest"" only")
                    populate_report(formx, true, false);
                else if (selectedItem == @"Failed ""Height Test"" only")
                    populate_report(formx, false, true);
                else if (selectedItem == @"Failed ""Width & Height Test"" only")
                    populate_report(formx, true, true);
            };

            formx.ShowDialog();
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                var list = (ListView)sender;
                var info = list.HitTest(e.X, e.Y);
                _mSelectedString = info.SubItem.Text;

                _validContextmenuLocation = true;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mSelectedString != "")
            {
                try
                {
                    Clipboard.SetText(_mSelectedString);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    //throw;
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!_validContextmenuLocation)
                e.Cancel = true;

            _validContextmenuLocation = false;
        }

        private void Item2IncludePresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = Item2IncludePresetComboBox.SelectedItem.ToString();

            var listOfButtons = new List<string>();

            if (selectedItem == "Basic")
            {
                listOfButtons = new List<string>
                {
                    "00_Filepath",
                    "01_Width ID",
                    "17_Width Test",
                    "18_Height Test",
                    "11_String"
                };
            }
            else if (selectedItem == "Detailed")
            {
                var layout_key = _mCheckboxes.m_layout_checkboxes.Keys.ToList();
                var tab_key = _mCheckboxes.m_tab_checkboxes.Values.ToList()[0].m_tab_checkbox.Keys.ToList();
                listOfButtons.AddRange(layout_key);
                listOfButtons.AddRange(tab_key);
            }

            foreach (var control in _mCheckboxControls)
            {
                var button = (CheckBox)control;
                var tagname = button.Tag.ToString();
                button.Checked = false;

                foreach (var i in listOfButtons)
                {
                    if (tagname.Equals(i))
                    {
                        button.Checked = true;

                        if (_mCheckboxes.m_layout_checkboxes.ContainsKey(tagname))
                        {
                            _mCheckboxes.m_layout_checkboxes[tagname] = !tagname.Contains("Padding");
                        }

                        
                        foreach (var j in _mCheckboxes.m_tab_checkboxes)
                        {
                            if (j.Value.m_tab_checkbox.ContainsKey(tagname))
                            {
                                //hack: remove tolerance columm
                                j.Value.m_tab_checkbox[tagname] = !tagname.Contains("(+tol)");

                            }
                        }
                    }
                }
            }
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            if (!_mReportGenerated)
            {
                MessageBox.Show("Please generate report first");
                return;
            }


            var xlApp = new Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel 2010 is not properly installed!!");
                return;
            }

            Workbook xlWorkBook;

            object misValue = Missing.Value;

            try
            {
                xlWorkBook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }


            var formx = new ProgressForm();
            formx.Text = "Saving report as XLS..";


            var totalCount = 0;
            foreach (var i in reporttab.TabPages)
            {
                var tabpage = i as TabPage;
                var listviews = tabpage.GetAll(typeof(ListView));
                var listview = (ListView)listviews.ElementAt(0);
                foreach (var j in listview.Columns)
                    foreach (var k in listview.Items)
                    {
                        ++totalCount;
                    }
            }

            formx.DoWork += (sender1, args) =>
            {
                // add column header
                var tab_idx = 1;


                var progressCount = 0;
                foreach (var i in reporttab.TabPages)
                {
                    try
                    {
                        var tabpage = i as TabPage;

                        var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(tab_idx);
                        xlWorkSheet.Name = tabpage.Text;

                        var listviews = tabpage.GetAll(typeof(ListView));
                        var listview = (ListView)listviews.ElementAt(0);

                        var column_idx = 1;

                        var columnlist = listview.Columns;

                        for (var j = 0; j < columnlist.Count; ++j)
                        {
                            // first row is header  
                            var columheader = new ColumnHeader();

                            listview.InvokeIfRequired(() => { columheader = columnlist[j]; });


                            xlWorkSheet.Cells[1, column_idx] = columheader.Text;

                            var row_idx = 2;

                            var listviewItems = listview.Items;

                            for (var k = 0; k < listviewItems.Count; ++k)
                            {
                                var item = new ListViewItem();

                                listview.InvokeIfRequired(() => { item = listviewItems[k]; });

                                xlWorkSheet.Cells[row_idx, column_idx] = item.SubItems[column_idx - 1].Text;
                                row_idx++;
                                progressCount++;

                                if (progressCount % 1000 == 0)
                                {
                                    var percent = (double)progressCount / totalCount * 100;

                                    sender1.InvokeIfRequired(() =>
                                    {
                                        sender1.DefaultStatusText = "Populate report into Excel cells..";
                                        sender1.SetProgress((int)percent);
                                        sender1.ProgressBar.Update();
                                    });
                                }
                            }

                            column_idx++;
                        }

                        tab_idx++;
                        releaseObject(xlWorkSheet);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
            };
            formx.ProgressBar.MarqueeAnimationSpeed = 1;
            var result = formx.ShowDialog();


            var savefile = new SaveFileDialog();
            savefile.FileName = "TWVReport.xls";
            savefile.Filter = "Excel files (*.xls)|*.xls";

            var tryAgain = true;
            while (tryAgain)
            {
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        xlApp.DisplayAlerts = false;
                        xlWorkBook.SaveAs(savefile.FileName, XlFileFormat.xlWorkbookNormal,
                            misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive,
                            misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();

                        releaseObject(xlWorkBook);
                        releaseObject(xlApp);

                        if (MessageBox.Show("Report successfully saved.\nShow report?", "Font batch checker",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (File.Exists(savefile.FileName))
                            {
                                var argument = @"/select, " + savefile.FileName;
                                Process.Start("explorer.exe", argument);
                                tryAgain = false;
                            }
                        }
                        else
                            tryAgain = false;
                    }
                    catch (Exception exp)
                    {
                        if (MessageBox.Show(exp.Message + ". Please try again.", "Font batch checker",
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Cancel)
                            tryAgain = false;
                    }
                }
                else
                    tryAgain = false;
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void form_DoWork(ProgressForm sender, DoWorkEventArgs e)
        {
            //_createReportTabs();

            var reportMaker = e.Argument as ReportMaker;
            if (!reportMaker.Generate(sender))
            {
                e.Result = "ABORT";
                return;
            }

            _mResultDatas = reportMaker.GetResult();
            _mResultDrmData = reportMaker.GetResultDRM();

            _idDrm = reportMaker.GetIDDRM();

            //e.Result = new KeyValuePair<List<IDData>, List<DRMData>>(result_datas, result_drm_data);

            sender.SetProgress(95, "Populating list..");
            add_header();
            populate_report(sender);
            populate_report_drm(sender);
            show_relevant_columns();
            Thread.Sleep(500);
            sender.SetProgress(100);
            e.Result = "SUCCESS";
        }

        private void viewDRMReport_Click(object sender, EventArgs e)
        {
            if (_mReportGenerated)
                _drmForm.Show();
        }

        private void reportTab_Selected(object sender, TabControlEventArgs e)
        {
            _mSelectedTabReport = e.TabPage.Text;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Equals(string.Empty))
                return;

            if (_searchItem.previousFoundSubItem != null)
                _searchItem.previousFoundSubItem.BackColor = _searchItem.previousFoundSubItemColor;

            var activeTab = new TabPage();

            foreach (var i in reporttab.TabPages)
            {
                var tabPage = i as TabPage;

                if (!tabPage.Text.Equals(_mSelectedTabReport)) continue;
                activeTab = tabPage;
                break;
            }

            if (activeTab.Text.Equals(string.Empty))
                return;

            var listviews = activeTab.GetAll(typeof(ListView));
            var listview = (ListView)listviews.ElementAt(0);

            if (_searchItem.currentSearchIndexListViewItem >= listview.Items.Count)
                return;

            if (_searchItem.currentSearchString == null || !_searchItem.currentSearchString.Equals(searchTextBox.Text))
            {
                _searchItem.currentSearchIndexListViewItem = 0;
                _searchItem.currentSearchIndexListViewSubItem = 0;
            }

            _searchItem.currentSearchString = searchTextBox.Text;

            var foundItem = listview.FindItemWithText(_searchItem.currentSearchString, true,
                _searchItem.currentSearchIndexListViewItem, true);

            if (foundItem == null && _searchItem.currentSearchIndexListViewItem == 0)
            {
                MessageBox.Show("Item not found");
                return;
            }

            if (foundItem == null && _searchItem.currentSearchIndexListViewItem != 0)
            {
                _searchItem.currentSearchIndexListViewItem = 0;
                _searchItem.currentSearchIndexListViewSubItem = 0;
                foundItem = listview.FindItemWithText(_searchItem.currentSearchString, true,
                    _searchItem.currentSearchIndexListViewItem, true);
            }

            if (foundItem == null)
                return;

            var subitemindexfound = new List<int>();

            listview.TopItem = foundItem;
            foundItem.EnsureVisible();

            for (var j = 0; j < foundItem.SubItems.Count; j++)
            {
                if (listview.Columns[j].Width == 0)
                    continue; //skip hidden column

                //foundItem.SubItems[j].BackColor = default(Color);

                var subtext = foundItem.SubItems[j].Text;

                var subtext_low = subtext.ToLower();
                var searchtext_low = searchTextBox.Text.ToLower();

                if (subtext_low.Contains(searchtext_low))
                {
                    subitemindexfound.Add(j);
                }
            }

            if (subitemindexfound.Count == 1)
            {
                listview.EnsureVisible(foundItem, subitemindexfound[0]);
                _searchItem.previousFoundSubItem = foundItem.SubItems[subitemindexfound[0]];
                _searchItem.previousFoundSubItemColor = foundItem.SubItems[subitemindexfound[0]].BackColor;
                foundItem.SubItems[subitemindexfound[0]].BackColor = Color.Yellow;

                _searchItem.currentSearchIndexListViewSubItem = 0;
                _searchItem.currentSearchIndexListViewItem = foundItem.Index + 1;
            }
            else if (subitemindexfound.Count > 1)
            {
                var subitemIndex = subitemindexfound[_searchItem.currentSearchIndexListViewSubItem];

                listview.EnsureVisible(foundItem, subitemIndex);
                _searchItem.previousFoundSubItem = foundItem.SubItems[subitemIndex];
                _searchItem.previousFoundSubItemColor = foundItem.SubItems[subitemIndex].BackColor;
                foundItem.SubItems[subitemIndex].BackColor = Color.Yellow;

                _searchItem.currentSearchIndexListViewSubItem++;

                if (_searchItem.currentSearchIndexListViewSubItem >= subitemindexfound.Count)
                {
                    _searchItem.currentSearchIndexListViewSubItem = 0;
                    _searchItem.currentSearchIndexListViewItem = foundItem.Index + 1;
                }
            }
        }

        private void UIWizard_Load(object sender, EventArgs e)
        {
            var title = new Font("Segoe UI", 12F,
                FontStyle.Regular, GraphicsUnit.World, 0);
            var header = new Font("Segoe UI", 30,
                FontStyle.Bold, GraphicsUnit.World, 0);
            var button = new Font("Segoe UI", 12F,
                FontStyle.Regular, GraphicsUnit.World, 0);

            wizardControl.OverrideThemeFonts(title, header, button);

            _drmForm = new DRMForm(this);
            _drmForm.DRMListView.ColumnClick +=
                ListView_ColumnClick;
        }

        private void wizardPage1_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            var strRcCount = ResourceFilesStrListView.Items.Count;

            if (!ResourceFilesStrListView.Enabled || !ResourceFileNumTextBox.Enabled || !FontFileTextBox.Enabled)
            {
                MessageBox.Show("All fields except \"DRM File\" are compulsory.\nPlease fill them.");
                e.Cancel = true;
                return;
            }

            if (strRcCount < 1 || _mReportParam.m_num_file.Equals(string.Empty) ||
                _mReportParam.m_ttf_file.Equals(string.Empty))
            {
                MessageBox.Show("All fields except \"DRM File\" are compulsory.\nPlease fill them.");
                e.Cancel = true;
                return;
            }
            
            Item2IncludeTabs.Enabled = strRcCount > 0 ? true : false;

            // clean up added tabs
            for (var i = Item2IncludeTabs.TabCount - 1; i >= 1; i--)
                Item2IncludeTabs.TabPages.RemoveAt(i);

            _mReportParam.m_str_files.Clear();
            var strFilesNames = new List<string>();

            for (var i = 0; i < strRcCount; ++i)
            {
                var title = ResourceFilesStrListView.Items[i].Text;
                if (i == 0)
                    Item2IncludeTabs.TabPages[0].Text = title;
                else if (i > 0) // create tab if more than 1
                {
                    var myTabPage = new TabPage(title);
                    myTabPage.BackColor = Color.White;
                    Item2IncludeTabs.TabPages.Add(myTabPage);
                }

                _mReportParam.m_str_files.Add(title,
                    ResourceFilesStrListView.Items[i].SubItems[1].Text);

                strFilesNames.Add(title);
            }

            _mCheckboxes = new Checkboxes(strFilesNames);
            _mCheckboxes.current_selected_tab_name = Item2IncludeTabs.TabPages[0].Text;
        }

        private void wizardPage2_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (!CPPFilesListView.Enabled)
            {
                MessageBox.Show("No file added.\nPlease add some before proceeding.");
                e.Cancel = true;
                return;
            }

            _mReportParam.m_cpp_files.Clear();

            for (var i = 0; i < CPPFilesListView.Items.Count; ++i)
            {
                var title = CPPFilesListView.Items[i].Text;
                _mReportParam.m_cpp_files.Add(title,
                    CPPFilesListView.Items[i].SubItems[1].Text);
            }

            if (_mReportParam.m_cpp_files.Count == 0)
            {
                MessageBox.Show("No file added.\nPlease add some before proceeding.");
                e.Cancel = true;
            }
        }

        private void wizardPage3_Initialize(object sender, EventArgs e)
        {
            if (_mReportGenerated)
                return;

            // clear old stuff
            {
                _mCheckboxes.m_layout_checkboxes.Clear();
                var tab_checkbox = _mCheckboxes.m_tab_checkboxes;
                foreach (var tab in tab_checkbox)
                    tab.Value.m_tab_checkbox.Clear();
            }

            var df = (WizardPage)sender;

            var all_checkbox = df.GetAll(typeof(CheckBox));
            _mCheckboxControls = all_checkbox;

            foreach (var chxbx in all_checkbox)
            {
                var item = (CheckBox)chxbx;
                var chkbx_name = item.Name;
                var chxbx_tag = item.Tag.ToString();
                var chxbx_state = item.Checked;

                if (chkbx_name.Contains("tab_"))
                {
                    var tab_checkbox = _mCheckboxes.m_tab_checkboxes;
                    foreach (var tab in tab_checkbox)
                    {
                        tab.Value.m_tab_checkbox.Add(chxbx_tag, chxbx_state);
                    }
                }
                else
                    _mCheckboxes.m_layout_checkboxes.Add(chxbx_tag, chxbx_state);
            }

            Item2IncludePresetComboBox.SelectedIndex = 0;
        }

        private void wizardPage3_Commit(object sender, WizardPageConfirmEventArgs e) => _createReportTabs();

        private void wizardPage4_Initialize(object sender, WizardPageInitEventArgs e)
        {
            if (_mReportGenerated)
                show_relevant_columns();
            else
            {
                try
                {
                    Generate_Click(sender, e);
                }
                catch (Exception b)
                {
                    if (b.Message == "ABORT")
                    {
                        wizardControl.PreviousPage();
                        wizardControl.PreviousPage();
                    }
                        
                }
            }

            viewDRMReport.Enabled = _mReportGenerated && _mResultDrmData.Count > 0;
        }

        private void wizardPage4_Commit(object sender, WizardPageConfirmEventArgs e)
        {
            if (MessageBox.Show("Exiting...\nAre you sure?", "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void searchTextBox_Click(object sender, EventArgs e) => searchTextBox.Clear();

        private void show_fail_combobox_Click(object sender, EventArgs e)
        {
            show_fail_combobox.Text = "";
            show_fail_combobox.DroppedDown = true;
        }
        
        private void ReportListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var list = (ListView)sender;
            var hitInfo = list.HitTest(e.X, e.Y);

            if(!hitInfo.Item.SubItems[14].Equals(hitInfo.SubItem))
                return;

            list.Controls.Add(txtEditor);

            txtEditor.Bounds = hitInfo.SubItem.Bounds;
            txtEditor.Text = hitInfo.SubItem.Text;
            txtEditor.SelectAll();
            txtEditor.Visible = true;
            
            txtEditor.Focus();
            txtEditor.Tag = hitInfo;
        }

        private void ResourceFilesStrListView_AfterLabelEdit(object sender, LabelEditEventArgs e) => _mReportGenerated = false;
    }

    public static class Extender
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SCROLL = LVM_FIRST + 20;
        private const int MARGIN = 0;

        [DllImport("user32")]
        private static extern IntPtr SendMessage(IntPtr Handle, int msg, IntPtr wParam,
            IntPtr lParam);

        private static void ScrollHorizontal(this IntPtr handle, int pixelsToScroll)
        {
            SendMessage(handle, LVM_SCROLL, (IntPtr)pixelsToScroll,
                IntPtr.Zero);
        }

        // invokeifrequired shorthand extension
        public static void InvokeIfRequired(this Control control, Action action)
        {
            // wait for 2 sec max
            //var count = 40;
            //while (!control.Visible && count > 0)
            //{
            //    System.Threading.Thread.Sleep(50);
            //    --count;
            //}

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void EnsureVisible(this ListView listView, ListViewItem listViewItem, int subItemIndex)
        {
            if (listViewItem == null || subItemIndex > listViewItem.SubItems.Count - 1)
            {
                throw new ArgumentException();
            }

            // scroll to the item row.

            var bounds = listViewItem.SubItems[subItemIndex].Bounds;

            // need to set width from columnheader, first subitem includes
            // all subitems.
            bounds.Width = listView.Columns[subItemIndex].Width;

            var scrollToLeft = bounds.X + bounds.Width + MARGIN;
            //if (scrollToLeft > listView.Bounds.Width)
            //{
            //    var s1 = scrollToLeft - listView.Bounds.Width;
            //    var s2 = s1 - bounds.Width;
            //    listView.Handle.ScrollHorizontal(s2);
            //}
            //else
            {
                var scrollToRight = bounds.X - MARGIN;
                //if (scrollToRight < 0)
                //{
                listView.Handle.ScrollHorizontal(scrollToRight);
                //}
            }
        }

        public static IEnumerable<Control> GetAll(this Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            var controlList = controls as IList<Control> ?? controls.ToList();
            return controlList.SelectMany(ctrl => ctrl.GetAll(type))
                .Concat(controlList)
                .Where(c => c.GetType() == type);
        }
    }
}