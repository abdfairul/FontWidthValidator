using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using FontValidator.Class;
using Excel = Microsoft.Office.Interop.Excel;

namespace FontValidator
{
    public partial class UIWizard : Form
    {
        private ReportMakerParameters m_report_param = new ReportMakerParameters();
        private IEnumerable<Control> m_tab_children_controls;
        private IEnumerable<Control> m_checkbox_controls;
        private ListViewColumnSorter lvwColumnSorter;
        private Checkboxes m_checkboxes;

        private List<IDData> m_result_datas;
        private List<ListView> m_report_listviews = new List<ListView>();

        private List<DRMData> m_result_drm_data = new List<DRMData>();
        private IDData id_drm = new IDData();

        private string m_selected_tab_report;
        private string m_selected_string = "";
        private bool valid_contextmenu_location = false;

        private DRMForm drmForm;

        // this flag to indicate should we update or not
        private bool m_report_generated = false;

        private Dictionary<string, List<int>> m_tab_enabler =
            new Dictionary<string, List<int>>();

        ProgressForm progressForm = new ProgressForm();

        public UIWizard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.WindowState = FormWindowState.Maximized;
            this.TopLevel = true;

            lvwColumnSorter = new ListViewColumnSorter();

        }

        private void ResourceFilesAddStr_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = true;
            op.Filter = "Resource files|Str.rc";

            if (!ResourceFilesStrListView.Enabled)
            {
                ResourceFilesStrListView.Enabled = true;
                ResourceFilesStrListView.Items.Clear();
                ResourceFilesStrListView.ForeColor = SystemColors.WindowText;
                ResourceFilesStrListView.Font = new Font("Segoe UI", 12,
                    FontStyle.Regular, GraphicsUnit.World, ((byte)(0)));
            }

            var count = ResourceFilesStrListView.Items.Count + 1;

            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var filename in op.FileNames)
                {
                    string[] row = { count.ToString(), filename };
                    var item = new ListViewItem(row);

                    bool exist = false;
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
                        MessageBox.Show("Item already added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                m_report_generated = false;
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
                for (int i = list.SelectedItems.Count - 1; i >= 0; i--)
                    list.SelectedItems[i].Remove();
            }

            if (e.KeyCode == Keys.A && e.Control)
            {
                for (int i = 0; i < list.Items.Count; ++i)
                    list.Items[i].Selected = true;
            }
        }

        private void ResourceFilesBrowseNum_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Resource file|Num.rc";

            if (!ResourceFileNumTextBox.Enabled)
                ResourceFileNumTextBox.Enabled = true;

            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_report_param.m_num_file = op.FileName;
                m_report_generated = false;
            }

            ResourceFileNumTextBox.Text = m_report_param.m_num_file;
        }

        private void FontFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Font file|*.ttf";

            if (!FontFileTextBox.Enabled)
                FontFileTextBox.Enabled = true;

            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_report_param.m_ttf_file = op.FileName;
                m_report_generated = false;
            }


            FontFileTextBox.Text = m_report_param.m_ttf_file;
        }

        private void DRMFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "Drm file|*drm.h";

            if (!DRMFileTextBox.Enabled)
                DRMFileTextBox.Enabled = true;

            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_report_param.m_drm_file = op.FileName;
                m_report_generated = false;
            }

            DRMFileTextBox.Text = m_report_param.m_drm_file;
        }

        private void CPPFilesAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = true;
            op.Filter = "CPP files|*.cpp";

            if (!CPPFilesListView.Enabled)
            {
                CPPFilesListView.Enabled = true;
                CPPFilesListView.Items.Clear();
                CPPFilesListView.ForeColor = SystemColors.WindowText;
                CPPFilesListView.Font = new Font("Segoe UI", 12,
                    FontStyle.Regular, GraphicsUnit.World, ((byte)(0)));
            }

            var count = CPPFilesListView.Items.Count + 1;

            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var filename in op.FileNames)
                {
                    FileInfo f = new FileInfo(filename);
                    string[] row = { count.ToString(), filename, (f.Length / 1024).ToString() + " KB" };

                    bool exist = false;
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

                m_report_generated = false;
            }
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            var chxbox = (CheckBox)sender;

            if (!chxbox.Name.Contains("tab_"))
            {
                m_checkboxes.m_layout_checkboxes[chxbox.Tag.ToString()] = chxbox.Checked;
            }
            else
            {
                if (m_checkboxes.m_tab_checkboxes.ContainsKey(m_checkboxes.current_selected_tab_name))
                {
                    var checkboxes = m_checkboxes.m_tab_checkboxes[m_checkboxes.current_selected_tab_name];
                    checkboxes.m_tab_checkbox[chxbox.Tag.ToString()] = chxbox.Checked;
                }
            }
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            var count = ResourceFilesStrListView.Items.Count;

            Item2IncludeTabs.Enabled = count > 0 ? true : false;

            // clean up added tabs
            for (int i = Item2IncludeTabs.TabCount - 1; i >= 1; i--)
                Item2IncludeTabs.TabPages.RemoveAt(i);

            m_report_param.m_str_files.Clear();
            var str_files_names = new List<string>();

            for (int i = 0; i < ResourceFilesStrListView.Items.Count; ++i)
            {
                string title = ResourceFilesStrListView.Items[i].Text;
                if (i == 0)
                    Item2IncludeTabs.TabPages[0].Text = title;
                else if (i > 0) // create tab if more than 1
                {
                    var myTabPage = new TabPage(title);
                    myTabPage.BackColor = System.Drawing.Color.White;
                    Item2IncludeTabs.TabPages.Add(myTabPage);
                }

                m_report_param.m_str_files.Add(title,
                    ResourceFilesStrListView.Items[i].SubItems[1].Text);

                str_files_names.Add(title);
            }

            m_checkboxes = new Checkboxes(str_files_names);
            m_checkboxes.current_selected_tab_name = Item2IncludeTabs.TabPages[0].Text;
        }

        private void Item2IncludeTabs_Selected(object sender, TabControlEventArgs e)
        {
            if (m_tab_children_controls.Cast<Control>().Count() < 1)
                return;

            for (int i = m_tab_children_controls.Count<Control>() - 1; i >= 0; i--)
                m_tab_children_controls.ElementAt(i).Parent = e.TabPage;

            m_checkboxes.current_selected_tab_name = e.TabPage.Text;

            // reset checkbox depend on param
            foreach (var control in m_checkbox_controls)
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

                    if (m_checkboxes.m_tab_checkboxes.ContainsKey(m_checkboxes.current_selected_tab_name))
                    {
                        var checkboxes = m_checkboxes.m_tab_checkboxes[m_checkboxes.current_selected_tab_name];
                        button.Checked = checkboxes.m_tab_checkbox[button.Tag.ToString()];
                    }
                }
                else
                    button.Checked = m_checkboxes.m_layout_checkboxes[button.Tag.ToString()];
            }
        }

        private void Item2IncludeTabs_Deselected(object sender, TabControlEventArgs e)
        {
            m_tab_children_controls = e.TabPage.Controls.OfType<Control>();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void wizardPage2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            m_report_param.m_cpp_files.Clear();

            for (int i = 0; i < CPPFilesListView.Items.Count; ++i)
            {
                string title = CPPFilesListView.Items[i].Text;
                m_report_param.m_cpp_files.Add(title,
                    CPPFilesListView.Items[i].SubItems[1].Text);
            }
        }

        private void wizardPage3_Enter(object sender, EventArgs e)
        {
            if (m_report_generated)
                return;

            // clear old stuff
            {
                m_checkboxes.m_layout_checkboxes.Clear();
                var tab_checkbox = m_checkboxes.m_tab_checkboxes;
                foreach (var tab in tab_checkbox)
                    tab.Value.m_tab_checkbox.Clear();
            }

            var df = (AeroWizard.WizardPage)sender;

            var all_checkbox = GetAll(df, typeof(CheckBox));
            m_checkbox_controls = all_checkbox;

            foreach (var chxbx in all_checkbox)
            {
                var item = (CheckBox)chxbx;
                var chkbx_name = item.Name;
                var chxbx_tag = item.Tag.ToString();
                var chxbx_state = item.Checked;

                if (chkbx_name.Contains("tab_"))
                {
                    var tab_checkbox = m_checkboxes.m_tab_checkboxes;
                    foreach (var tab in tab_checkbox)
                    {
                        tab.Value.m_tab_checkbox.Add(chxbx_tag, chxbx_state);
                    }
                }
                else
                    m_checkboxes.m_layout_checkboxes.Add(chxbx_tag, chxbx_state);
            }

            Item2IncludePresetComboBox.SelectedIndex = 0;
        }

        private void populate_report(ProgressForm pForm, bool hide_width_test = false, bool hide_height_test = false)
        {
            int tab = 0;

            var totalCount = m_checkboxes.m_tab_checkboxes.Count * m_result_datas.Count;
            var progressCount = 0;
            var currentProgressBar = pForm.ProgressBar.Value;

            foreach (var i in m_checkboxes.m_tab_checkboxes)
            {
                int count = 1;

                var tabpage = reporttab.TabPages[tab];
                var listviews = GetAll(tabpage, typeof(ListView));
                var listview = (ListView)listviews.ElementAt(0);

                listview.InvokeIfRequired(new Action(() =>
                {
                    listview.Items.Clear();
                    listview.BeginUpdate();
                }));


                foreach (var j in m_result_datas)
                {
                    progressCount++;
                    if (j.Equals(id_drm))
                        continue;

                    var item = new ListViewItem(count.ToString());
                    item.UseItemStyleForSubItems = false;

                    var need_add_item = true;
                    bool need_recolor = false;

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
                                need_add_item = false;
                                break;
                            }


                            if (hide_height_test && hide_width_test)
                            {
                                if (k.is_ok_height || k.is_ok_width)
                                {
                                    need_add_item = false;
                                    break;
                                }
                            }
                            else if (hide_width_test)
                            {
                                if (k.is_ok_width)
                                {
                                    need_add_item = false;
                                    break;
                                }
                            }
                            else if (hide_height_test)
                            {
                                if (k.is_ok_height)
                                {
                                    need_add_item = false;
                                    break;
                                }
                            }

                            if (k.value_string.Equals(""))
                            {
                                need_add_item = false;
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

                            need_recolor = (!k.is_ok_height || !k.is_ok_width);
                            break;
                        }
                    }

                    if (need_recolor)
                    {
                        foreach (var k in item.SubItems)
                        {
                            var m = (ListViewItem.ListViewSubItem)k;
                            if ((m.BackColor != Color.Red))
                                m.BackColor = Color.MistyRose;
                        }
                    }

                    if (need_add_item)
                    {
                        listview.InvokeIfRequired(new Action(() =>
                        {
                            listview.Items.Add(item);
                        }));

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

                listview.InvokeIfRequired(new Action(() =>
                {
                    listview.EndUpdate();
                }));

                ++tab;
            }
        }

        private void populate_report_drm(ProgressForm pForm)
        {
            var listview = drmForm.DRMListView;
            listview.InvokeIfRequired(new Action(() =>
            {
                listview.Items.Clear();
                listview.BeginUpdate();
            }));

            int total = m_result_drm_data.Count;
            int count = 1;
            var currentProgressBar = pForm.ProgressBar.Value;

            foreach (var i in m_result_drm_data)
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
                        if ((m.BackColor != Color.Red))
                            m.BackColor = Color.MistyRose;
                    }

                }

                listview.InvokeIfRequired(new Action(() =>
                {
                    listview.Items.Add(item);
                }));

                count++;

                if (currentProgressBar == 0)
                {
                    var percent = (double)count / total * 100;
                    pForm.SetProgress((int)percent);
                }
            }
            listview.InvokeIfRequired(new Action(() =>
            {
                listview.EndUpdate();
            }));

            drmForm.InvokeIfRequired(new Action(() =>
            {
                drmForm.DRMSummary.Text = "TextBox ID: " + id_drm.textbox_id +
                          "; Width: " + id_drm.width.Value + "; Fontsize: " + 18;
            }));


        }

        private void show_relevant_columns()
        {
            Action action = () =>
            {
                foreach (var tabpage in reporttab.TabPages)
                {
                    var listviews = GetAll((TabPage)tabpage, typeof(ListView));
                    var listview = (ListView)listviews.ElementAt(0);

                    var rgx = new Regex(@"[\d]+?_");

                    for (int i = 1; i < listview.Columns.Count; ++i)
                    {
                        var column = listview.Columns[i];
                        column.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                        // search throu dictionary. because key is no longer the same so need extra step   
                        bool is_in_layout = false;

                        foreach (var j in m_checkboxes.m_layout_checkboxes)
                        {
                            var key = j.Key;
                            var key_wo_prefix = rgx.Replace(key, string.Empty);

                            if (key_wo_prefix.Equals(column.Text))
                            {
                                if (!j.Value)
                                {
                                    column.Width = 0;
                                }
                                is_in_layout = true;
                                break;
                            }
                        }

                        if (is_in_layout) // skip unnecessary loop
                            continue;

                        var m = m_checkboxes.m_tab_checkboxes[((TabPage)tabpage).Text];

                        foreach (var k in m.m_tab_checkbox)
                        {
                            var key = k.Key;
                            var key_wo_prefix = rgx.Replace(key, string.Empty);

                            if (key_wo_prefix.Equals(column.Text))
                            {
                                if (!k.Value)
                                {
                                    column.Width = 0;
                                }
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
            var sort_layout_key = m_checkboxes.m_layout_checkboxes.Keys.ToList();
            sort_layout_key.Sort();

            var sort_tab_key = m_checkboxes.m_tab_checkboxes.Values.ToList()[0].m_tab_checkbox.Keys.ToList();
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
            int tab = 0;
            foreach (var j in m_checkboxes.m_tab_checkboxes)
            {
                Action action = () =>
                {
                    var tabpage = reporttab.TabPages[tab];
                    var listviews = GetAll(tabpage, typeof(ListView));
                    var listview = (ListView)listviews.ElementAt(0);

                    // clear all header
                    listview.Columns.Clear();

                    {
                        int k = listview.Columns.Add(new ColumnHeader());
                        listview.Columns[k].Text = "#";
                    }

                    for (int i = 0; i < sort_layout_key_wo_prefix.Count; ++i)
                    {
                        int k = listview.Columns.Add(new ColumnHeader());
                        listview.Columns[k].Text = sort_layout_key_wo_prefix.ElementAt(i);
                    }


                    for (int i = 0; i < sort_tab_key_wo_prefix.Count; ++i)
                    {
                        int k = listview.Columns.Add(new ColumnHeader());
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

            var report_maker = new ReportMaker(m_report_param);

            ProgressForm form = new ProgressForm();
            form.Text = "Generating report...";
            form.DoWork += new ProgressForm.DoWorkEventHandler(form_DoWork);
            form.Argument = report_maker;

            //if(m_report_param.m_cpp_files.Count < 40)
            //form.SetReportProgress(false);

            DialogResult result = form.ShowDialog();
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

            m_report_generated = true;
            viewDRMReport.Enabled = (m_result_drm_data.Count > 0);
        }

        public void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.

            var list = (ListView)sender;
            //ReportListView_1.Sort();
            list.ListViewItemSorter = lvwColumnSorter;

            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.

            list.Sort();
        }

        private void _createReportTabs()
        {
            if (m_report_generated)
                return;

            var count = m_checkboxes.m_tab_checkboxes.Count;

            // clean up added tabs
            for (int i = reporttab.TabCount - 1; i >= 1; i--)
                reporttab.TabPages.RemoveAt(i);

            var str_files_names = new List<string>();
            for (int i = 0; i < m_checkboxes.m_tab_checkboxes.Count; ++i)
            {
                string title = m_checkboxes.m_tab_checkboxes.Keys.ToList()[i];
                //string title = m_result_datas[0].text_values.ElementAt(i).label;
                if (i == 0)
                    reporttab.TabPages[0].Text = title;
                else if (i > 0) // create tab if more than 1
                {
                    var myTabPage = new TabPage(title);
                    myTabPage.Location = new System.Drawing.Point(4, 24);
                    myTabPage.Padding = new Padding(3);
                    myTabPage.Size = new System.Drawing.Size(489, 246);
                    myTabPage.UseVisualStyleBackColor = true;

                    var myListView = new ListView();
                    myListView.ContextMenuStrip = this.contextMenuStrip1;
                    myListView.Dock = System.Windows.Forms.DockStyle.Fill;
                    myListView.FullRowSelect = true;
                    myListView.Location = new System.Drawing.Point(3, 3);
                    myListView.Text = "ReportListView_" + (i + 1).ToString();
                    myListView.Margin = new Padding(3, 3, 3, 3);
                    myListView.ShowGroups = false;
                    myListView.ShowItemToolTips = true;
                    myListView.Size = new System.Drawing.Size(483, 240);
                    myListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
                    myListView.UseCompatibleStateImageBehavior = false;
                    myListView.View = System.Windows.Forms.View.Details;
                    myListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
                    myListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
                    myListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);

                    myTabPage.Controls.Add(myListView);
                    myTabPage.BackColor = System.Drawing.Color.White;
                    reporttab.TabPages.Add(myTabPage);
                }
            }

            m_selected_tab_report = reporttab.TabPages[0].Text;
        }

        private void wizardPage3_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            _createReportTabs();
        }

        private void wizardPage4_Enter(object sender, EventArgs e)
        {
            if (m_report_generated)
                show_relevant_columns();

            viewDRMReport.Enabled = m_report_generated && m_result_drm_data.Count > 0;
        }

        private bool _getTolerance()
        {
            double height, width;
            if (!double.TryParse(tolerance_height.Text, out height) ||
                !double.TryParse(tolerance_width.Text, out width) ||
                height < 0 || height > 100 || width < 0 || width > 100)
            {
                MessageBox.Show("Not a valid input." + Environment.NewLine +
                    "Please use an integer within 0-100 for input", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            m_report_param.m_tolerance_height = height / 100;
            m_report_param.m_tolerance_width = width / 100;

            return true;
        }

        private void update_tolerance_Click(object sender, EventArgs e)
        {
            if (!_getTolerance())
                return;

            foreach (var i in m_result_datas)
            {
                foreach (var j in i.text_values)
                {
                    j.calc_w_tolerance_width = j.calc_base_width * (1 + m_report_param.m_tolerance_width / 100);
                    // tolerance_height
                    j.calc_w_tolerance_height = j.calc_base_height * (1 + m_report_param.m_tolerance_height / 100);
                    // tolerance

                    if (i.multiline == "TRUE")
                        j.is_ok_width = true;

                    if (i.scrollable == "TRUE")
                        j.is_ok_width = true;
                    else if (i.multiline == "FALSE")
                    {
                        var total_width = Convert.ToDouble(i.width.Value)
                            /*- 2 * Convert.ToDouble(i.padding.Value)*/;
                        //j.is_ok_width = j.calc_w_tolerance_width < total_width;

                        j.is_ok_width = (j.calc_base_width < total_width);

                        if (!j.is_ok_width)
                            j.is_ok_width = (Math.Abs(j.calc_base_width - total_width) <
                                             (j.calc_base_width * m_report_param.m_tolerance_width));
                    }

                    var total_height = Convert.ToDouble(i.height.Value) /*- 2 * Convert.ToDouble(i.padding.Value)*/;
                    //j.is_ok_height = (j.calc_w_tolerance_height * j.calc_row_count) < total_height;

                    j.is_ok_height = (j.calc_base_height < total_height);

                    if (!j.is_ok_height)
                        j.is_ok_height = (Math.Abs(j.calc_base_height - total_height) <
                                          (j.calc_base_height * m_report_param.m_tolerance_height));
                }
            }

            add_header();


            ProgressForm formx = new ProgressForm();
            formx.Text = "Updating tolerance..";

            formx.DoWork += (sender1, args) =>
            {
                populate_report(sender1);
            };

            formx.ShowDialog();
            show_relevant_columns();
            m_report_generated = true;
        }

        public void update_tolerance_drm(double tolerance)
        {
            ProgressForm formx = new ProgressForm();
            formx.Text = "Updating tolerance..";

            formx.DoWork += (sender, args) =>
            {
                foreach (var i in m_result_drm_data)
                {
                    i.widthTest = (i.widthValue < i.layoutwidthtotal);

                    if (!i.widthTest)
                        i.widthTest = (Math.Abs(i.widthValue - i.layoutwidthtotal) < (i.widthValue * tolerance / 100));
                }

                populate_report_drm(sender);
            };

            formx.ShowDialog();
        }

        private void show_fail_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProgressForm formx = new ProgressForm();
            formx.Text = "Updating selection..";

            formx.DoWork += (sender1, args) =>
            {
                var selectedItem = "";
                show_fail_combobox.InvokeIfRequired(() =>
                {
                    selectedItem = show_fail_combobox.SelectedItem?.ToString();
                });

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
                ListViewHitTestInfo info = list.HitTest(e.X, e.Y);
                m_selected_string = info.SubItem.Text;

                valid_contextmenu_location = true;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_selected_string != "")
            {
                try
                {
                    Clipboard.SetText(m_selected_string);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                    //throw;
                }
            }

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!valid_contextmenu_location)
                e.Cancel = true;

            valid_contextmenu_location = false;
        }

        private void UIWizard_Load(object sender, EventArgs e)
        {
            var title = new Font("Segoe UI", 12F,
                FontStyle.Regular, GraphicsUnit.World, ((byte)(0)));
            var header = new Font("Segoe UI", 30,
                FontStyle.Bold, GraphicsUnit.World, ((byte)(0)));
            var button = new Font("Segoe UI", 12F,
                FontStyle.Regular, GraphicsUnit.World, ((byte)(0)));

            wizardControl.OverrideThemeFonts(title, header, button);

            drmForm = new DRMForm(this);
            drmForm.DRMListView.ColumnClick +=
                new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
        }

        private void Item2IncludePresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = Item2IncludePresetComboBox.SelectedItem.ToString();

            List<string> listOfButtons = new List<string>();

            if (selectedItem == "Basic")
            {
                listOfButtons = new List<string>{"00_Filepath",
                    "01_Width ID", "17_Width Test", "18_Height Test",
                    "11_String" };
            }
            else if (selectedItem == "Detailed")
            {
                var layout_key = m_checkboxes.m_layout_checkboxes.Keys.ToList();
                var tab_key = m_checkboxes.m_tab_checkboxes.Values.ToList()[0].m_tab_checkbox.Keys.ToList();
                listOfButtons.AddRange(layout_key);
                listOfButtons.AddRange(tab_key);
            }

            foreach (var control in m_checkbox_controls)
            {
                var button = (CheckBox)control;
                var tagname = button.Tag.ToString();
                button.Checked = false;

                foreach (var i in listOfButtons)
                {
                    if (tagname.Equals(i))
                    {
                        button.Checked = true;
                        foreach (var j in m_checkboxes.m_tab_checkboxes)
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
            if (!m_report_generated)
            {
                MessageBox.Show("Please generate report first");
                return;
            }


            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel 2010 is not properly installed!!");
                return;
            }

            Excel.Workbook xlWorkBook;

            object misValue = System.Reflection.Missing.Value;

            try
            {
                xlWorkBook = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return;
            }


            ProgressForm formx = new ProgressForm();
            formx.Text = "Saving report as XLS..";


            int totalCount = 0;
            foreach (var i in reporttab.TabPages)
            {
                var listviews = GetAll(i as TabPage, typeof(ListView));
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
                int tab_idx = 1;


                int progressCount = 0;
                foreach (var i in reporttab.TabPages)
                {
                    try
                    {
                        var tabpage = i as TabPage;

                        Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(tab_idx);
                        xlWorkSheet.Name = tabpage.Text;

                        var listviews = GetAll(tabpage, typeof(ListView));
                        var listview = (ListView)listviews.ElementAt(0);

                        int column_idx = 1;

                        var columnlist = listview.Columns;

                        for (int j = 0; j < columnlist.Count; ++j)
                        {
                            // first row is header  
                            ColumnHeader columheader = new ColumnHeader();

                            listview.InvokeIfRequired(new Action(() =>
                            {
                                columheader = columnlist[j];
                            }));


                            xlWorkSheet.Cells[1, column_idx] = columheader.Text;

                            int row_idx = 2;

                            var listviewItems = listview.Items;

                            for (int k = 0; k < listviewItems.Count; ++k)
                            {
                                ListViewItem item = new ListViewItem();

                                listview.InvokeIfRequired(new Action(() =>
                                {
                                    item = listviewItems[k];
                                }));

                                xlWorkSheet.Cells[row_idx, column_idx] = item.SubItems[column_idx - 1].Text;
                                row_idx++;
                                progressCount++;

                                if (progressCount % 1000 == 0)
                                {
                                    var percent = (double)progressCount / totalCount * 100;

                                    sender1.InvokeIfRequired(new Action(() =>
                                    {
                                        sender1.DefaultStatusText = "Populate report into Excel cells..";
                                        sender1.SetProgress((int)percent);
                                        sender1.ProgressBar.Update();
                                    }));


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
            DialogResult result = formx.ShowDialog();



            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = "TWVReport.xls";
            savefile.Filter = "Excel files (*.xls)|*.xls";

            bool tryAgain = true;
            while (tryAgain)
            {
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        xlApp.DisplayAlerts = false;
                        xlWorkBook.SaveAs(savefile.FileName, Excel.XlFileFormat.xlWorkbookNormal,
                            misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive,
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
                                string argument = @"/select, " + savefile.FileName;
                                System.Diagnostics.Process.Start("explorer.exe", argument);
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
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
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
            reportMaker.Generate(sender);

            m_result_datas = reportMaker.GetResult();
            m_result_drm_data = reportMaker.GetResultDRM();

            id_drm = reportMaker.GetIDDRM();

            //e.Result = new KeyValuePair<List<IDData>, List<DRMData>>(result_datas, result_drm_data);

            sender.SetProgress(95, "Populating list..");
            add_header();
            populate_report(sender);
            populate_report_drm(sender);
            show_relevant_columns();
            Thread.Sleep(500);
            sender.SetProgress(100);
        }

        private void viewDRMReport_Click(object sender, EventArgs e)
        {
            if (m_report_generated)
                drmForm.Show();
        }

        private void reportTab_Selected(object sender, TabControlEventArgs e)
        {
            m_selected_tab_report = e.TabPage.Text;
        }

        private int currentSearchIndexListViewItem = 0;
        private int currentSearchIndexListViewSubItem = 0;
        private string currentSearchString = "";

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Equals(string.Empty))
                return;


            TabPage activeTab = new TabPage();

            foreach (var i in reporttab.TabPages)
            {
                var tabPage = i as TabPage;

                if (!tabPage.Text.Equals(m_selected_tab_report)) continue;
                activeTab = tabPage;
                break;
            }

            if (activeTab.Text.Equals(String.Empty))
                return;

            var listviews = GetAll(activeTab, typeof(ListView));
            var listview = (ListView)listviews.ElementAt(0);

            bool notFound = true;
            while (currentSearchIndexListViewItem < listview.Items.Count && notFound)
            {
                if (!currentSearchString.Equals(searchTextBox.Text))
                {
                    currentSearchIndexListViewItem = 0;
                    currentSearchIndexListViewSubItem = 0;
                }

                currentSearchString = searchTextBox.Text;

                var foundItem = listview.FindItemWithText(currentSearchString, true, currentSearchIndexListViewItem, true);
                if (foundItem == null)
                    break;

                for (int j = 0; j < foundItem.SubItems.Count; j++)
                {
                    foundItem.SubItems[j].BackColor = default(Color);

                    var subtext = foundItem.SubItems[j].Text;

                    if (subtext.Contains(searchTextBox.Text))
                    {
                        if (currentSearchIndexListViewSubItem < j)
                        {
                            foundItem.SubItems[j].BackColor = Color.AliceBlue;
                            listview.EnsureVisible(foundItem, j);
                            currentSearchIndexListViewSubItem = j;
                            currentSearchIndexListViewItem = foundItem.Index + 1;
                            notFound = false;
                            break;
                        }

                    }
                }

                
            }


        }
    }

    public static class Extender
    {
        const Int32 LVM_FIRST = 0x1000;
        const Int32 LVM_SCROLL = LVM_FIRST + 20;
        const int MARGIN = 20;

        [DllImport("user32")]
        static extern IntPtr SendMessage(IntPtr Handle, Int32 msg, IntPtr wParam,
        IntPtr lParam);

        private static void ScrollHorizontal(this IntPtr handle, int pixelsToScroll)
        {
            SendMessage(handle, LVM_SCROLL, (IntPtr)pixelsToScroll,
            IntPtr.Zero);
        }

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
            listView.TopItem = listViewItem;
            listViewItem.EnsureVisible();
            Rectangle bounds = listViewItem.SubItems[subItemIndex].Bounds;

            // need to set width from columnheader, first subitem includes
            // all subitems.
            bounds.Width = listView.Columns[subItemIndex].Width;

            int scrollToLeft = bounds.X + bounds.Width + MARGIN;
            if (scrollToLeft > listView.Bounds.Width)
            {
                listView.Handle.ScrollHorizontal(scrollToLeft - listView.Bounds.Width);
            }
            else
            {
                int scrollToRight = bounds.X - MARGIN;
                if (scrollToRight < 0)
                {
                    listView.Handle.ScrollHorizontal(scrollToRight);
                }
            }
        }
    }
}
