namespace FontValidator
{
    partial class UIWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "(editable)",
            "(can add multiple files here, example below)"}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "EN",
            ".../rc_magnavox/rc_EN/Str.rc"}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "ES",
            ".../rc_magnavox/rc_ES/Str.rc"}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "FR",
            ".../rc_magnavox/rc_FR/Str.rc"}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "0",
            "(can add multiple files here, example below)",
            "0 KB"}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "1",
            "../src/Mapp_4KDemoModeApp.cpp",
            "100 KB"}, -1);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
            "2",
            "../src/Mapp_4KDemoModeFrame.cpp",
            "200 KB"}, -1);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "3"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "../src/Mapp_AgingMenuApp.cpp"),
            new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "300 KB", System.Drawing.SystemColors.ControlDarkDark, System.Drawing.SystemColors.Window, new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World))}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIWizard));
            this.wizardControl = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.DRMFileTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DRMFileBrowse = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.FontFileTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FontFileBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ResourceFilesBrowseNum = new System.Windows.Forms.Button();
            this.ResourceFileNumTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ResourceFilesAddStr = new System.Windows.Forms.Button();
            this.ResourceFilesStrListView = new System.Windows.Forms.ListView();
            this.Num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.CPPFilesListView = new System.Windows.Forms.ListView();
            this.num_cpp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filepath_cpp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size_cpp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CPPFilesAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.filepath = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.Item2IncludePresetComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.value_multiline = new System.Windows.Forms.CheckBox();
            this.value_fontsize = new System.Windows.Forms.CheckBox();
            this.id_fontsize = new System.Windows.Forms.CheckBox();
            this.value_padding = new System.Windows.Forms.CheckBox();
            this.value_height = new System.Windows.Forms.CheckBox();
            this.value_width = new System.Windows.Forms.CheckBox();
            this.id_width = new System.Windows.Forms.CheckBox();
            this.id_height = new System.Windows.Forms.CheckBox();
            this.id_padding = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.id_string = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Item2IncludeTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ResultGroupBox = new System.Windows.Forms.GroupBox();
            this.tab_widthtest = new System.Windows.Forms.CheckBox();
            this.tab_heighttest = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tab_value_width = new System.Windows.Forms.CheckBox();
            this.tab_value_height = new System.Windows.Forms.CheckBox();
            this.tab_tolerance_height = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tab_rowcount = new System.Windows.Forms.CheckBox();
            this.tab_tolerance_width = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tab_stringvalue = new System.Windows.Forms.CheckBox();
            this.wizardPage4 = new AeroWizard.WizardPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.viewDRMReport = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.show_fail_combobox = new System.Windows.Forms.ComboBox();
            this.saveAs = new System.Windows.Forms.Button();
            this.Generate = new System.Windows.Forms.Button();
            this.reporttab = new System.Windows.Forms.TabControl();
            this.reporttabpage1 = new System.Windows.Forms.TabPage();
            this.ReportListView_1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.update_tolerance = new System.Windows.Forms.Button();
            this.tolerance_height = new System.Windows.Forms.TextBox();
            this.tolerance_width = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.table.SuspendLayout();
            this.Item2IncludeTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ResultGroupBox.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.reporttab.SuspendLayout();
            this.reporttabpage1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            this.wizardControl.BackColor = System.Drawing.Color.White;
            this.wizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl.ForeColor = System.Drawing.Color.Black;
            this.wizardControl.Location = new System.Drawing.Point(0, 0);
            this.wizardControl.MinimumSize = new System.Drawing.Size(574, 530);
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.Pages.Add(this.wizardPage1);
            this.wizardControl.Pages.Add(this.wizardPage2);
            this.wizardControl.Pages.Add(this.wizardPage3);
            this.wizardControl.Pages.Add(this.wizardPage4);
            this.wizardControl.Size = new System.Drawing.Size(784, 561);
            this.wizardControl.TabIndex = 0;
            this.wizardControl.Title = "Font Validator";
            this.wizardControl.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl.TitleIcon")));
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.groupBox4);
            this.wizardPage1.Controls.Add(this.groupBox2);
            this.wizardPage1.Controls.Add(this.groupBox1);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(737, 407);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.Text = "Step 1 of 4 : Add resource, font and drm files";
            this.wizardPage1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage1_Commit);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.DRMFileTextBox);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.DRMFileBrowse);
            this.groupBox4.Location = new System.Drawing.Point(374, 142);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(349, 124);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "(optional) DRM file";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.label23.ForeColor = System.Drawing.Color.DimGray;
            this.label23.Location = new System.Drawing.Point(14, 49);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(305, 26);
            this.label23.TabIndex = 9;
            this.label23.Text = "Example: \"...\\Supernova\\projects\\ui\\nebula\\Funai_LC13\\include\\\r\nLC13.0_IPTV_OSS_D" +
    "RM.h\"";
            // 
            // DRMFileTextBox
            // 
            this.DRMFileTextBox.Enabled = false;
            this.DRMFileTextBox.Location = new System.Drawing.Point(16, 84);
            this.DRMFileTextBox.Name = "DRMFileTextBox";
            this.DRMFileTextBox.ReadOnly = true;
            this.DRMFileTextBox.Size = new System.Drawing.Size(310, 23);
            this.DRMFileTextBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Select *drm.h file:";
            // 
            // DRMFileBrowse
            // 
            this.DRMFileBrowse.Location = new System.Drawing.Point(275, 27);
            this.DRMFileBrowse.Name = "DRMFileBrowse";
            this.DRMFileBrowse.Size = new System.Drawing.Size(54, 23);
            this.DRMFileBrowse.TabIndex = 6;
            this.DRMFileBrowse.Text = "Browse";
            this.DRMFileBrowse.UseVisualStyleBackColor = true;
            this.DRMFileBrowse.Click += new System.EventHandler(this.DRMFileBrowse_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.FontFileTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.FontFileBrowse);
            this.groupBox2.Location = new System.Drawing.Point(374, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 121);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Font file";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.label22.ForeColor = System.Drawing.Color.DimGray;
            this.label22.Location = new System.Drawing.Point(14, 49);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(263, 26);
            this.label22.TabIndex = 8;
            this.label22.Text = "Example: \"...Supernova\\projects\\ui\\nebula\\Funai_LC13\\\r\nrc_magnavox\\rc\\font\\DejaVu" +
    "Sans_88_1.ttf\"";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(16, 47);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(0, 15);
            this.label21.TabIndex = 7;
            // 
            // FontFileTextBox
            // 
            this.FontFileTextBox.Enabled = false;
            this.FontFileTextBox.Location = new System.Drawing.Point(16, 83);
            this.FontFileTextBox.Name = "FontFileTextBox";
            this.FontFileTextBox.ReadOnly = true;
            this.FontFileTextBox.Size = new System.Drawing.Size(313, 23);
            this.FontFileTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Select *.ttf file:";
            // 
            // FontFileBrowse
            // 
            this.FontFileBrowse.Location = new System.Drawing.Point(276, 22);
            this.FontFileBrowse.Name = "FontFileBrowse";
            this.FontFileBrowse.Size = new System.Drawing.Size(54, 23);
            this.FontFileBrowse.TabIndex = 6;
            this.FontFileBrowse.Text = "Browse";
            this.FontFileBrowse.UseVisualStyleBackColor = true;
            this.FontFileBrowse.Click += new System.EventHandler(this.FontFileBrowse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.ResourceFilesBrowseNum);
            this.groupBox1.Controls.Add(this.ResourceFileNumTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ResourceFilesAddStr);
            this.groupBox1.Controls.Add(this.ResourceFilesStrListView);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 409);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resource files";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.label24.ForeColor = System.Drawing.Color.DimGray;
            this.label24.Location = new System.Drawing.Point(12, 334);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(267, 26);
            this.label24.TabIndex = 10;
            this.label24.Text = "Example: \"...\\Supernova\\projects\\ui\\nebula\\Funai_LC13\\\r\nrc_magnavox\\rc\\Num.rc\"";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.label15.ForeColor = System.Drawing.Color.DimGray;
            this.label15.Location = new System.Drawing.Point(14, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(263, 26);
            this.label15.TabIndex = 6;
            this.label15.Text = "Example: \"...Supernova\\projects\\ui\\nebula\\Funai_LC13\\\r\nrc_magnavox\\rc_EN\\Str.rc\"";
            // 
            // ResourceFilesBrowseNum
            // 
            this.ResourceFilesBrowseNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceFilesBrowseNum.Location = new System.Drawing.Point(280, 313);
            this.ResourceFilesBrowseNum.Name = "ResourceFilesBrowseNum";
            this.ResourceFilesBrowseNum.Size = new System.Drawing.Size(54, 23);
            this.ResourceFilesBrowseNum.TabIndex = 5;
            this.ResourceFilesBrowseNum.Text = "Browse";
            this.ResourceFilesBrowseNum.UseVisualStyleBackColor = true;
            this.ResourceFilesBrowseNum.Click += new System.EventHandler(this.ResourceFilesBrowseNum_Click);
            // 
            // ResourceFileNumTextBox
            // 
            this.ResourceFileNumTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceFileNumTextBox.Enabled = false;
            this.ResourceFileNumTextBox.Location = new System.Drawing.Point(16, 369);
            this.ResourceFileNumTextBox.Name = "ResourceFileNumTextBox";
            this.ResourceFileNumTextBox.ReadOnly = true;
            this.ResourceFileNumTextBox.Size = new System.Drawing.Size(318, 23);
            this.ResourceFileNumTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Num.rc file:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Add Str.rc file(s) : ";
            // 
            // ResourceFilesAddStr
            // 
            this.ResourceFilesAddStr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceFilesAddStr.Location = new System.Drawing.Point(280, 22);
            this.ResourceFilesAddStr.Name = "ResourceFilesAddStr";
            this.ResourceFilesAddStr.Size = new System.Drawing.Size(54, 23);
            this.ResourceFilesAddStr.TabIndex = 1;
            this.ResourceFilesAddStr.Text = "Add";
            this.ResourceFilesAddStr.UseVisualStyleBackColor = true;
            this.ResourceFilesAddStr.Click += new System.EventHandler(this.ResourceFilesAddStr_Click);
            // 
            // ResourceFilesStrListView
            // 
            this.ResourceFilesStrListView.AllowDrop = true;
            this.ResourceFilesStrListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResourceFilesStrListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResourceFilesStrListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Num,
            this.Filename});
            this.ResourceFilesStrListView.Enabled = false;
            this.ResourceFilesStrListView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.ResourceFilesStrListView.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ResourceFilesStrListView.FullRowSelect = true;
            this.ResourceFilesStrListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12});
            this.ResourceFilesStrListView.LabelEdit = true;
            this.ResourceFilesStrListView.Location = new System.Drawing.Point(16, 82);
            this.ResourceFilesStrListView.Name = "ResourceFilesStrListView";
            this.ResourceFilesStrListView.ShowGroups = false;
            this.ResourceFilesStrListView.ShowItemToolTips = true;
            this.ResourceFilesStrListView.Size = new System.Drawing.Size(318, 220);
            this.ResourceFilesStrListView.TabIndex = 0;
            this.ResourceFilesStrListView.UseCompatibleStateImageBehavior = false;
            this.ResourceFilesStrListView.View = System.Windows.Forms.View.Details;
            this.ResourceFilesStrListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.ResourceFilesStrListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
            this.ResourceFilesStrListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            // 
            // Num
            // 
            this.Num.Text = "Label";
            this.Num.Width = 82;
            // 
            // Filename
            // 
            this.Filename.Text = "Filepath";
            this.Filename.Width = 348;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.groupBox3);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(737, 407);
            this.wizardPage2.TabIndex = 1;
            this.wizardPage2.Text = "Step 2 of 4 : Add CPP files";
            this.wizardPage2.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage2_Commit);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.CPPFilesListView);
            this.groupBox3.Controls.Add(this.CPPFilesAdd);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(15, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(704, 371);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CPP files";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.label25.ForeColor = System.Drawing.Color.DimGray;
            this.label25.Location = new System.Drawing.Point(15, 48);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(399, 13);
            this.label25.TabIndex = 7;
            this.label25.Text = "Example: \"...\\Supernova\\projects\\ui\\nebula\\Funai_LC13\\src\\Mapp_4KDemoApp.cpp\"";
            // 
            // CPPFilesListView
            // 
            this.CPPFilesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CPPFilesListView.AutoArrange = false;
            this.CPPFilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.num_cpp,
            this.filepath_cpp,
            this.size_cpp});
            this.CPPFilesListView.Enabled = false;
            this.CPPFilesListView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.CPPFilesListView.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.CPPFilesListView.FullRowSelect = true;
            this.CPPFilesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16});
            this.CPPFilesListView.Location = new System.Drawing.Point(16, 69);
            this.CPPFilesListView.Margin = new System.Windows.Forms.Padding(0);
            this.CPPFilesListView.Name = "CPPFilesListView";
            this.CPPFilesListView.Size = new System.Drawing.Size(669, 286);
            this.CPPFilesListView.TabIndex = 4;
            this.CPPFilesListView.UseCompatibleStateImageBehavior = false;
            this.CPPFilesListView.View = System.Windows.Forms.View.Details;
            this.CPPFilesListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.CPPFilesListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
            // 
            // num_cpp
            // 
            this.num_cpp.Text = "#";
            this.num_cpp.Width = 34;
            // 
            // filepath_cpp
            // 
            this.filepath_cpp.Text = "Filepath";
            this.filepath_cpp.Width = 332;
            // 
            // size_cpp
            // 
            this.size_cpp.Text = "Size";
            this.size_cpp.Width = 70;
            // 
            // CPPFilesAdd
            // 
            this.CPPFilesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CPPFilesAdd.Location = new System.Drawing.Point(610, 27);
            this.CPPFilesAdd.Name = "CPPFilesAdd";
            this.CPPFilesAdd.Size = new System.Drawing.Size(75, 23);
            this.CPPFilesAdd.TabIndex = 2;
            this.CPPFilesAdd.Text = "Add";
            this.CPPFilesAdd.UseVisualStyleBackColor = true;
            this.CPPFilesAdd.Click += new System.EventHandler(this.CPPFilesAdd_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Add *.cpp file(s) :";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.groupBox5);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Size = new System.Drawing.Size(737, 407);
            this.wizardPage3.TabIndex = 2;
            this.wizardPage3.Text = "Step 3 of 4 : Customize the report";
            this.wizardPage3.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage3_Commit);
            this.wizardPage3.Enter += new System.EventHandler(this.wizardPage3_Enter);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.filepath);
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.Item2IncludePresetComboBox);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.Item2IncludeTabs);
            this.groupBox5.Location = new System.Drawing.Point(15, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(494, 341);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Item to include:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(117, 84);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(81, 19);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Tag = "001_Textbox ID";
            this.checkBox1.Text = "Textbox ID";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // filepath
            // 
            this.filepath.AutoSize = true;
            this.filepath.Location = new System.Drawing.Point(30, 84);
            this.filepath.Name = "filepath";
            this.filepath.Size = new System.Drawing.Size(68, 19);
            this.filepath.TabIndex = 9;
            this.filepath.Tag = "00_Filepath";
            this.filepath.Text = "Filepath";
            this.filepath.UseVisualStyleBackColor = true;
            this.filepath.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(13, 22);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(45, 15);
            this.label34.TabIndex = 25;
            this.label34.Text = "Preset: ";
            // 
            // Item2IncludePresetComboBox
            // 
            this.Item2IncludePresetComboBox.FormattingEnabled = true;
            this.Item2IncludePresetComboBox.Items.AddRange(new object[] {
            "Basic",
            "Detailed"});
            this.Item2IncludePresetComboBox.Location = new System.Drawing.Point(30, 46);
            this.Item2IncludePresetComboBox.Name = "Item2IncludePresetComboBox";
            this.Item2IncludePresetComboBox.Size = new System.Drawing.Size(151, 23);
            this.Item2IncludePresetComboBox.TabIndex = 24;
            this.Item2IncludePresetComboBox.SelectedIndexChanged += new System.EventHandler(this.Item2IncludePresetComboBox_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.table);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Location = new System.Drawing.Point(13, 113);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(194, 194);
            this.groupBox7.TabIndex = 23;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Layout properties";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(74, 143);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 15);
            this.label14.TabIndex = 31;
            this.label14.Text = "Scrollable";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(75, 162);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 15);
            this.label13.TabIndex = 30;
            this.label13.Text = "String";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(74, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 15);
            this.label12.TabIndex = 29;
            this.label12.Text = "Multiline flag";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(74, 102);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 15);
            this.label11.TabIndex = 28;
            this.label11.Text = "Font size";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 15);
            this.label10.TabIndex = 27;
            this.label10.Text = "Padding internal";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(74, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 15);
            this.label9.TabIndex = 26;
            this.label9.Text = "Height";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(74, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Width";
            // 
            // table
            // 
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.table.Controls.Add(this.value_multiline, 1, 4);
            this.table.Controls.Add(this.value_fontsize, 1, 3);
            this.table.Controls.Add(this.id_fontsize, 0, 3);
            this.table.Controls.Add(this.value_padding, 1, 2);
            this.table.Controls.Add(this.value_height, 1, 1);
            this.table.Controls.Add(this.value_width, 1, 0);
            this.table.Controls.Add(this.id_width, 0, 0);
            this.table.Controls.Add(this.id_height, 0, 1);
            this.table.Controls.Add(this.id_padding, 0, 2);
            this.table.Controls.Add(this.checkBox2, 1, 5);
            this.table.Controls.Add(this.id_string, 0, 6);
            this.table.Location = new System.Drawing.Point(17, 40);
            this.table.Name = "table";
            this.table.RowCount = 7;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.table.Size = new System.Drawing.Size(57, 138);
            this.table.TabIndex = 24;
            // 
            // value_multiline
            // 
            this.value_multiline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.value_multiline.Location = new System.Drawing.Point(31, 83);
            this.value_multiline.Name = "value_multiline";
            this.value_multiline.Size = new System.Drawing.Size(23, 14);
            this.value_multiline.TabIndex = 13;
            this.value_multiline.Tag = "09_Multiline";
            this.value_multiline.UseVisualStyleBackColor = true;
            this.value_multiline.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // value_fontsize
            // 
            this.value_fontsize.AccessibleName = "";
            this.value_fontsize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.value_fontsize.Location = new System.Drawing.Point(31, 63);
            this.value_fontsize.Name = "value_fontsize";
            this.value_fontsize.Size = new System.Drawing.Size(23, 14);
            this.value_fontsize.TabIndex = 10;
            this.value_fontsize.Tag = "08_Fontsize";
            this.value_fontsize.UseVisualStyleBackColor = true;
            this.value_fontsize.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // id_fontsize
            // 
            this.id_fontsize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.id_fontsize.Location = new System.Drawing.Point(3, 63);
            this.id_fontsize.Name = "id_fontsize";
            this.id_fontsize.Size = new System.Drawing.Size(22, 14);
            this.id_fontsize.TabIndex = 9;
            this.id_fontsize.Tag = "07_Fontsize ID";
            this.id_fontsize.UseVisualStyleBackColor = true;
            this.id_fontsize.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // value_padding
            // 
            this.value_padding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.value_padding.Location = new System.Drawing.Point(31, 43);
            this.value_padding.Name = "value_padding";
            this.value_padding.Size = new System.Drawing.Size(23, 14);
            this.value_padding.TabIndex = 7;
            this.value_padding.Tag = "06_Padding";
            this.value_padding.UseVisualStyleBackColor = true;
            this.value_padding.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // value_height
            // 
            this.value_height.Dock = System.Windows.Forms.DockStyle.Fill;
            this.value_height.Location = new System.Drawing.Point(31, 23);
            this.value_height.Name = "value_height";
            this.value_height.Size = new System.Drawing.Size(23, 14);
            this.value_height.TabIndex = 5;
            this.value_height.Tag = "04_Height";
            this.value_height.UseVisualStyleBackColor = true;
            this.value_height.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // value_width
            // 
            this.value_width.Dock = System.Windows.Forms.DockStyle.Fill;
            this.value_width.Location = new System.Drawing.Point(31, 3);
            this.value_width.Name = "value_width";
            this.value_width.Size = new System.Drawing.Size(23, 14);
            this.value_width.TabIndex = 3;
            this.value_width.Tag = "02_Width";
            this.value_width.UseVisualStyleBackColor = true;
            this.value_width.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // id_width
            // 
            this.id_width.Dock = System.Windows.Forms.DockStyle.Fill;
            this.id_width.Location = new System.Drawing.Point(3, 3);
            this.id_width.Name = "id_width";
            this.id_width.Size = new System.Drawing.Size(22, 14);
            this.id_width.TabIndex = 1;
            this.id_width.Tag = "01_Width ID";
            this.id_width.UseVisualStyleBackColor = true;
            this.id_width.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // id_height
            // 
            this.id_height.Dock = System.Windows.Forms.DockStyle.Fill;
            this.id_height.Location = new System.Drawing.Point(3, 23);
            this.id_height.Name = "id_height";
            this.id_height.Size = new System.Drawing.Size(22, 14);
            this.id_height.TabIndex = 0;
            this.id_height.Tag = "03_Height ID";
            this.id_height.UseVisualStyleBackColor = true;
            this.id_height.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // id_padding
            // 
            this.id_padding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.id_padding.Location = new System.Drawing.Point(3, 43);
            this.id_padding.Name = "id_padding";
            this.id_padding.Size = new System.Drawing.Size(22, 14);
            this.id_padding.TabIndex = 2;
            this.id_padding.Tag = "05_Padding ID";
            this.id_padding.UseVisualStyleBackColor = true;
            this.id_padding.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(31, 103);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(16, 14);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.Tag = "10_Scrollable";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // id_string
            // 
            this.id_string.Location = new System.Drawing.Point(3, 123);
            this.id_string.Name = "id_string";
            this.id_string.Size = new System.Drawing.Size(16, 14);
            this.id_string.TabIndex = 16;
            this.id_string.Tag = "101_String ID";
            this.id_string.UseVisualStyleBackColor = true;
            this.id_string.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 15);
            this.label8.TabIndex = 22;
            this.label8.Text = "Value";
            // 
            // Item2IncludeTabs
            // 
            this.Item2IncludeTabs.Controls.Add(this.tabPage1);
            this.Item2IncludeTabs.Enabled = false;
            this.Item2IncludeTabs.Location = new System.Drawing.Point(227, 22);
            this.Item2IncludeTabs.Multiline = true;
            this.Item2IncludeTabs.Name = "Item2IncludeTabs";
            this.Item2IncludeTabs.SelectedIndex = 0;
            this.Item2IncludeTabs.Size = new System.Drawing.Size(243, 304);
            this.Item2IncludeTabs.TabIndex = 22;
            this.Item2IncludeTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.Item2IncludeTabs_Selected);
            this.Item2IncludeTabs.Deselected += new System.Windows.Forms.TabControlEventHandler(this.Item2IncludeTabs_Deselected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ResultGroupBox);
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Controls.Add(this.tab_stringvalue);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(235, 276);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "[Disable]";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ResultGroupBox
            // 
            this.ResultGroupBox.Controls.Add(this.tab_widthtest);
            this.ResultGroupBox.Controls.Add(this.tab_heighttest);
            this.ResultGroupBox.Location = new System.Drawing.Point(13, 215);
            this.ResultGroupBox.Name = "ResultGroupBox";
            this.ResultGroupBox.Size = new System.Drawing.Size(209, 53);
            this.ResultGroupBox.TabIndex = 8;
            this.ResultGroupBox.TabStop = false;
            this.ResultGroupBox.Text = "Result";
            // 
            // tab_widthtest
            // 
            this.tab_widthtest.Location = new System.Drawing.Point(16, 22);
            this.tab_widthtest.Name = "tab_widthtest";
            this.tab_widthtest.Size = new System.Drawing.Size(80, 24);
            this.tab_widthtest.TabIndex = 26;
            this.tab_widthtest.Tag = "17_Width Test";
            this.tab_widthtest.Text = "Width test";
            this.tab_widthtest.UseVisualStyleBackColor = true;
            this.tab_widthtest.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // tab_heighttest
            // 
            this.tab_heighttest.Location = new System.Drawing.Point(106, 22);
            this.tab_heighttest.Name = "tab_heighttest";
            this.tab_heighttest.Size = new System.Drawing.Size(87, 24);
            this.tab_heighttest.TabIndex = 27;
            this.tab_heighttest.Tag = "18_Height Test";
            this.tab_heighttest.Text = "Height test";
            this.tab_heighttest.UseVisualStyleBackColor = true;
            this.tab_heighttest.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.tab_value_width);
            this.groupBox8.Controls.Add(this.tab_value_height);
            this.groupBox8.Controls.Add(this.tab_tolerance_height);
            this.groupBox8.Controls.Add(this.groupBox6);
            this.groupBox8.Controls.Add(this.tab_tolerance_width);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Location = new System.Drawing.Point(13, 31);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(209, 167);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Calculation";
            // 
            // tab_value_width
            // 
            this.tab_value_width.Location = new System.Drawing.Point(16, 61);
            this.tab_value_width.Name = "tab_value_width";
            this.tab_value_width.Size = new System.Drawing.Size(90, 20);
            this.tab_value_width.TabIndex = 1;
            this.tab_value_width.Tag = "12_Text Width";
            this.tab_value_width.Text = "Text height";
            this.tab_value_width.UseVisualStyleBackColor = true;
            this.tab_value_width.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // tab_value_height
            // 
            this.tab_value_height.Location = new System.Drawing.Point(16, 40);
            this.tab_value_height.Name = "tab_value_height";
            this.tab_value_height.Size = new System.Drawing.Size(90, 20);
            this.tab_value_height.TabIndex = 0;
            this.tab_value_height.Tag = "14_Text Height";
            this.tab_value_height.Text = "Text width";
            this.tab_value_height.UseVisualStyleBackColor = true;
            this.tab_value_height.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // tab_tolerance_height
            // 
            this.tab_tolerance_height.Location = new System.Drawing.Point(112, 64);
            this.tab_tolerance_height.Name = "tab_tolerance_height";
            this.tab_tolerance_height.Size = new System.Drawing.Size(14, 15);
            this.tab_tolerance_height.TabIndex = 5;
            this.tab_tolerance_height.Tag = "15_Text Height (+tol)";
            this.tab_tolerance_height.UseVisualStyleBackColor = true;
            this.tab_tolerance_height.Visible = false;
            this.tab_tolerance_height.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tab_rowcount);
            this.groupBox6.Location = new System.Drawing.Point(12, 93);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(181, 54);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Multiline";
            // 
            // tab_rowcount
            // 
            this.tab_rowcount.AutoSize = true;
            this.tab_rowcount.Location = new System.Drawing.Point(16, 20);
            this.tab_rowcount.Name = "tab_rowcount";
            this.tab_rowcount.Size = new System.Drawing.Size(83, 19);
            this.tab_rowcount.TabIndex = 21;
            this.tab_rowcount.Tag = "16_Row count";
            this.tab_rowcount.Text = "Row count";
            this.tab_rowcount.UseVisualStyleBackColor = true;
            this.tab_rowcount.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // tab_tolerance_width
            // 
            this.tab_tolerance_width.Location = new System.Drawing.Point(112, 43);
            this.tab_tolerance_width.Name = "tab_tolerance_width";
            this.tab_tolerance_width.Size = new System.Drawing.Size(14, 14);
            this.tab_tolerance_width.TabIndex = 3;
            this.tab_tolerance_width.Tag = "13_Text Width (+tol)";
            this.tab_tolerance_width.UseVisualStyleBackColor = true;
            this.tab_tolerance_width.Visible = false;
            this.tab_tolerance_width.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 15);
            this.label19.TabIndex = 17;
            this.label19.Text = "Value";
            // 
            // tab_stringvalue
            // 
            this.tab_stringvalue.AutoSize = true;
            this.tab_stringvalue.Location = new System.Drawing.Point(25, 9);
            this.tab_stringvalue.Name = "tab_stringvalue";
            this.tab_stringvalue.Size = new System.Drawing.Size(88, 19);
            this.tab_stringvalue.TabIndex = 7;
            this.tab_stringvalue.Tag = "11_String";
            this.tab_stringvalue.Text = "String value";
            this.tab_stringvalue.UseVisualStyleBackColor = true;
            this.tab_stringvalue.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.pictureBox1);
            this.wizardPage4.Controls.Add(this.searchButton);
            this.wizardPage4.Controls.Add(this.searchTextBox);
            this.wizardPage4.Controls.Add(this.groupBox9);
            this.wizardPage4.Controls.Add(this.label26);
            this.wizardPage4.Controls.Add(this.show_fail_combobox);
            this.wizardPage4.Controls.Add(this.saveAs);
            this.wizardPage4.Controls.Add(this.Generate);
            this.wizardPage4.Controls.Add(this.reporttab);
            this.wizardPage4.Controls.Add(this.groupBox13);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.Size = new System.Drawing.Size(737, 407);
            this.wizardPage4.TabIndex = 3;
            this.wizardPage4.Text = "Step 4 of 4 : Apply tolerance and view report";
            this.wizardPage4.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage4_Commit);
            this.wizardPage4.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPage4_Initialize);
            this.wizardPage4.Enter += new System.EventHandler(this.wizardPage4_Enter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::FontValidator.Properties.Resources.show;
            this.pictureBox1.Location = new System.Drawing.Point(559, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.FlatAppearance.BorderSize = 0;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Image = ((System.Drawing.Image)(resources.GetObject("searchButton.Image")));
            this.searchButton.Location = new System.Drawing.Point(697, 14);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(25, 25);
            this.searchButton.TabIndex = 10;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(596, 16);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(100, 23);
            this.searchTextBox.TabIndex = 9;
            this.searchTextBox.Text = "Search item..";
            this.searchTextBox.Click += new System.EventHandler(this.searchTextBox_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox9.Controls.Add(this.viewDRMReport);
            this.groupBox9.Location = new System.Drawing.Point(321, 328);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(156, 66);
            this.groupBox9.TabIndex = 8;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "(optional) DRM report";
            // 
            // viewDRMReport
            // 
            this.viewDRMReport.Enabled = false;
            this.viewDRMReport.Location = new System.Drawing.Point(18, 25);
            this.viewDRMReport.Name = "viewDRMReport";
            this.viewDRMReport.Size = new System.Drawing.Size(116, 23);
            this.viewDRMReport.TabIndex = 0;
            this.viewDRMReport.Text = "View DRM Report";
            this.viewDRMReport.UseVisualStyleBackColor = true;
            this.viewDRMReport.Click += new System.EventHandler(this.viewDRMReport_Click);
            // 
            // label26
            // 
            this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World);
            this.label26.Location = new System.Drawing.Point(608, 382);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(124, 12);
            this.label26.TabIndex = 7;
            this.label26.Text = "(*need Excel 2010 installed)";
            // 
            // show_fail_combobox
            // 
            this.show_fail_combobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.show_fail_combobox.FormattingEnabled = true;
            this.show_fail_combobox.Items.AddRange(new object[] {
            "All",
            "Failed \"WidthTest\" only",
            "Failed \"Height Test\" only",
            "Failed \"Width & Height Test\" only"});
            this.show_fail_combobox.Location = new System.Drawing.Point(432, 16);
            this.show_fail_combobox.Name = "show_fail_combobox";
            this.show_fail_combobox.Size = new System.Drawing.Size(121, 23);
            this.show_fail_combobox.TabIndex = 5;
            this.show_fail_combobox.Text = "Select NG items..";
            this.show_fail_combobox.SelectedIndexChanged += new System.EventHandler(this.show_fail_combobox_SelectedIndexChanged);
            this.show_fail_combobox.Click += new System.EventHandler(this.show_fail_combobox_Click);
            // 
            // saveAs
            // 
            this.saveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveAs.Location = new System.Drawing.Point(627, 353);
            this.saveAs.Name = "saveAs";
            this.saveAs.Size = new System.Drawing.Size(85, 23);
            this.saveAs.TabIndex = 3;
            this.saveAs.Text = "Save as XLS*";
            this.saveAs.UseVisualStyleBackColor = true;
            this.saveAs.Click += new System.EventHandler(this.saveAs_Click);
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.Location = new System.Drawing.Point(536, 353);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(85, 23);
            this.Generate.TabIndex = 2;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // reporttab
            // 
            this.reporttab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reporttab.Controls.Add(this.reporttabpage1);
            this.reporttab.Location = new System.Drawing.Point(19, 37);
            this.reporttab.Name = "reporttab";
            this.reporttab.SelectedIndex = 0;
            this.reporttab.Size = new System.Drawing.Size(707, 285);
            this.reporttab.TabIndex = 1;
            this.reporttab.Selected += new System.Windows.Forms.TabControlEventHandler(this.reportTab_Selected);
            // 
            // reporttabpage1
            // 
            this.reporttabpage1.Controls.Add(this.ReportListView_1);
            this.reporttabpage1.Location = new System.Drawing.Point(4, 24);
            this.reporttabpage1.Name = "reporttabpage1";
            this.reporttabpage1.Padding = new System.Windows.Forms.Padding(3);
            this.reporttabpage1.Size = new System.Drawing.Size(699, 257);
            this.reporttabpage1.TabIndex = 0;
            this.reporttabpage1.Text = "[Disable]";
            this.reporttabpage1.UseVisualStyleBackColor = true;
            // 
            // ReportListView_1
            // 
            this.ReportListView_1.ContextMenuStrip = this.contextMenuStrip1;
            this.ReportListView_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportListView_1.FullRowSelect = true;
            this.ReportListView_1.Location = new System.Drawing.Point(3, 3);
            this.ReportListView_1.Name = "ReportListView_1";
            this.ReportListView_1.ShowGroups = false;
            this.ReportListView_1.ShowItemToolTips = true;
            this.ReportListView_1.Size = new System.Drawing.Size(693, 251);
            this.ReportListView_1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ReportListView_1.TabIndex = 0;
            this.ReportListView_1.UseCompatibleStateImageBehavior = false;
            this.ReportListView_1.View = System.Windows.Forms.View.Details;
            this.ReportListView_1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            this.ReportListView_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyDown);
            this.ReportListView_1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox13.Controls.Add(this.update_tolerance);
            this.groupBox13.Controls.Add(this.tolerance_height);
            this.groupBox13.Controls.Add(this.tolerance_width);
            this.groupBox13.Controls.Add(this.label33);
            this.groupBox13.Controls.Add(this.label32);
            this.groupBox13.Location = new System.Drawing.Point(26, 328);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(275, 66);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Tolerance (%)";
            // 
            // update_tolerance
            // 
            this.update_tolerance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.update_tolerance.Location = new System.Drawing.Point(201, 25);
            this.update_tolerance.Name = "update_tolerance";
            this.update_tolerance.Size = new System.Drawing.Size(58, 23);
            this.update_tolerance.TabIndex = 4;
            this.update_tolerance.Text = "Update";
            this.update_tolerance.UseVisualStyleBackColor = true;
            this.update_tolerance.Click += new System.EventHandler(this.update_tolerance_Click);
            // 
            // tolerance_height
            // 
            this.tolerance_height.Location = new System.Drawing.Point(144, 25);
            this.tolerance_height.Name = "tolerance_height";
            this.tolerance_height.Size = new System.Drawing.Size(44, 23);
            this.tolerance_height.TabIndex = 4;
            this.tolerance_height.Text = "5";
            // 
            // tolerance_width
            // 
            this.tolerance_width.Location = new System.Drawing.Point(53, 25);
            this.tolerance_width.Name = "tolerance_width";
            this.tolerance_width.Size = new System.Drawing.Size(44, 23);
            this.tolerance_width.TabIndex = 3;
            this.tolerance_width.Text = "5";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(13, 30);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(39, 15);
            this.label33.TabIndex = 2;
            this.label33.Text = "Width";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(100, 29);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(43, 15);
            this.label32.TabIndex = 1;
            this.label32.Text = "Height";
            // 
            // UIWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.wizardControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "UIWizard";
            this.Load += new System.EventHandler(this.UIWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.table.ResumeLayout(false);
            this.Item2IncludeTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResultGroupBox.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.reporttab.ResumeLayout(false);
            this.reporttabpage1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage3;
        private AeroWizard.WizardPage wizardPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView ResourceFilesStrListView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox FontFileTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button FontFileBrowse;
        private System.Windows.Forms.Button ResourceFilesBrowseNum;
        private System.Windows.Forms.TextBox ResourceFileNumTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ResourceFilesAddStr;
        private System.Windows.Forms.ColumnHeader Num;
        private System.Windows.Forms.ColumnHeader Filename;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox DRMFileTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button DRMFileBrowse;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button CPPFilesAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView CPPFilesListView;
        private System.Windows.Forms.ColumnHeader num_cpp;
        private System.Windows.Forms.ColumnHeader filepath_cpp;
        private System.Windows.Forms.ColumnHeader size_cpp;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel table;
        private System.Windows.Forms.CheckBox value_multiline;
        private System.Windows.Forms.CheckBox value_fontsize;
        private System.Windows.Forms.CheckBox id_fontsize;
        private System.Windows.Forms.CheckBox value_padding;
        private System.Windows.Forms.CheckBox value_height;
        private System.Windows.Forms.CheckBox value_width;
        private System.Windows.Forms.CheckBox id_width;
        private System.Windows.Forms.CheckBox id_height;
        private System.Windows.Forms.CheckBox id_padding;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl Item2IncludeTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox ResultGroupBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox tab_rowcount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox tab_value_width;
        private System.Windows.Forms.CheckBox tab_value_height;
        private System.Windows.Forms.CheckBox tab_stringvalue;
        private AeroWizard.WizardPage wizardPage4;
        private System.Windows.Forms.CheckBox id_string;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button saveAs;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox Item2IncludePresetComboBox;
        private System.Windows.Forms.TextBox tolerance_height;
        private System.Windows.Forms.TextBox tolerance_width;
        private System.Windows.Forms.CheckBox tab_widthtest;
        private System.Windows.Forms.CheckBox tab_heighttest;
        private System.Windows.Forms.CheckBox tab_tolerance_width;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox tab_tolerance_height;
        private System.Windows.Forms.TabControl reporttab;
        private System.Windows.Forms.TabPage reporttabpage1;
        private System.Windows.Forms.ListView ReportListView_1;
        private System.Windows.Forms.CheckBox filepath;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button update_tolerance;
        private System.Windows.Forms.ComboBox show_fail_combobox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button viewDRMReport;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}