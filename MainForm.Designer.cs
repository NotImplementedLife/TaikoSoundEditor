namespace TaikoSoundEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OkButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DirSelector = new TaikoSoundEditor.PathSelector();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.WordListPathSelector = new TaikoSoundEditor.PathSelector();
            this.MusicInfoPathSelector = new TaikoSoundEditor.PathSelector();
            this.MusicOrderPathSelector = new TaikoSoundEditor.PathSelector();
            this.MusicAttributePathSelector = new TaikoSoundEditor.PathSelector();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DatatableSpaces = new System.Windows.Forms.CheckBox();
            this.ExportOpenOnFinished = new System.Windows.Forms.CheckBox();
            this.ExportAllButton = new System.Windows.Forms.Button();
            this.ExportSoundBanksButton = new System.Windows.Forms.Button();
            this.ExportSoundFoldersButton = new System.Windows.Forms.Button();
            this.ExportDatatableButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.NewSoundsBox = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.EditorTable = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.MusicAttributesGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.MusicOrderGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.MusicInfoGrid = new System.Windows.Forms.PropertyGrid();
            this.panel2 = new System.Windows.Forms.Panel();
            this.WordDetailGB = new System.Windows.Forms.GroupBox();
            this.WordDetailGrid = new System.Windows.Forms.PropertyGrid();
            this.WordSubGB = new System.Windows.Forms.GroupBox();
            this.WordSubGrid = new System.Windows.Forms.PropertyGrid();
            this.WordsGB = new System.Windows.Forms.GroupBox();
            this.WordsGrid = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LoadedMusicBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.AddSilenceBox = new System.Windows.Forms.CheckBox();
            this.FeedbackBox = new System.Windows.Forms.TextBox();
            this.CreateBackButton = new System.Windows.Forms.Button();
            this.CreateOkButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SongNameBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TJASelector = new TaikoSoundEditor.PathSelector();
            this.AudioFileSelector = new TaikoSoundEditor.PathSelector();
            this.label10 = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.EditorTable.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.WordDetailGB.SuspendLayout();
            this.WordSubGB.SuspendLayout();
            this.WordsGB.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(684, 461);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(676, 452);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.OkButton);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(151, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 229);
            this.panel1.TabIndex = 1;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(262, 203);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(109, 23);
            this.OkButton.TabIndex = 10;
            this.OkButton.Text = "Looks good";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.DirSelector);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(3, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 57);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Or specify the game diretory (/datatable)";
            // 
            // DirSelector
            // 
            this.DirSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirSelector.Filter = "All files(*.*)|*.*";
            this.DirSelector.Location = new System.Drawing.Point(68, 22);
            this.DirSelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DirSelector.Name = "DirSelector";
            this.DirSelector.Path = "";
            this.DirSelector.SelectsFolder = true;
            this.DirSelector.Size = new System.Drawing.Size(293, 23);
            this.DirSelector.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Directory";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.WordListPathSelector);
            this.groupBox1.Controls.Add(this.MusicInfoPathSelector);
            this.groupBox1.Controls.Add(this.MusicOrderPathSelector);
            this.groupBox1.Controls.Add(this.MusicAttributePathSelector);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 132);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select individual files";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "wordlist.bin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "musicinfo.bin";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "music_order.bin";
            // 
            // WordListPathSelector
            // 
            this.WordListPathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WordListPathSelector.Filter = "Binary files(*.bin)|*.bin|All files(*.*)|*.*";
            this.WordListPathSelector.Location = new System.Drawing.Point(118, 102);
            this.WordListPathSelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.WordListPathSelector.Name = "WordListPathSelector";
            this.WordListPathSelector.Path = "";
            this.WordListPathSelector.SelectsFolder = false;
            this.WordListPathSelector.Size = new System.Drawing.Size(243, 23);
            this.WordListPathSelector.TabIndex = 12;
            // 
            // MusicInfoPathSelector
            // 
            this.MusicInfoPathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MusicInfoPathSelector.Filter = "Binary files(*.bin)|*.bin|All files(*.*)|*.*";
            this.MusicInfoPathSelector.Location = new System.Drawing.Point(118, 73);
            this.MusicInfoPathSelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MusicInfoPathSelector.Name = "MusicInfoPathSelector";
            this.MusicInfoPathSelector.Path = "";
            this.MusicInfoPathSelector.SelectsFolder = false;
            this.MusicInfoPathSelector.Size = new System.Drawing.Size(243, 23);
            this.MusicInfoPathSelector.TabIndex = 11;
            // 
            // MusicOrderPathSelector
            // 
            this.MusicOrderPathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MusicOrderPathSelector.Filter = "Binary files(*.bin)|*.bin|All files(*.*)|*.*";
            this.MusicOrderPathSelector.Location = new System.Drawing.Point(118, 44);
            this.MusicOrderPathSelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MusicOrderPathSelector.Name = "MusicOrderPathSelector";
            this.MusicOrderPathSelector.Path = "";
            this.MusicOrderPathSelector.SelectsFolder = false;
            this.MusicOrderPathSelector.Size = new System.Drawing.Size(243, 23);
            this.MusicOrderPathSelector.TabIndex = 10;
            // 
            // MusicAttributePathSelector
            // 
            this.MusicAttributePathSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MusicAttributePathSelector.Filter = "Binary files(*.bin)|*.bin|All files(*.*)|*.*";
            this.MusicAttributePathSelector.Location = new System.Drawing.Point(118, 15);
            this.MusicAttributePathSelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MusicAttributePathSelector.Name = "MusicAttributePathSelector";
            this.MusicAttributePathSelector.Path = "";
            this.MusicAttributePathSelector.SelectsFolder = false;
            this.MusicAttributePathSelector.Size = new System.Drawing.Size(243, 23);
            this.MusicAttributePathSelector.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 15);
            this.label8.TabIndex = 8;
            this.label8.Text = "music_atribute.bin";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DatatableSpaces);
            this.tabPage2.Controls.Add(this.ExportOpenOnFinished);
            this.tabPage2.Controls.Add(this.ExportAllButton);
            this.tabPage2.Controls.Add(this.ExportSoundBanksButton);
            this.tabPage2.Controls.Add(this.ExportSoundFoldersButton);
            this.tabPage2.Controls.Add(this.ExportDatatableButton);
            this.tabPage2.Controls.Add(this.CreateButton);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(676, 452);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DatatableSpaces
            // 
            this.DatatableSpaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DatatableSpaces.AutoSize = true;
            this.DatatableSpaces.Checked = true;
            this.DatatableSpaces.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DatatableSpaces.Location = new System.Drawing.Point(139, 372);
            this.DatatableSpaces.Name = "DatatableSpaces";
            this.DatatableSpaces.Size = new System.Drawing.Size(308, 19);
            this.DatatableSpaces.TabIndex = 11;
            this.DatatableSpaces.Text = "Remove spaces in datatable files (musicinfo, wordlist)";
            this.DatatableSpaces.UseVisualStyleBackColor = true;
            // 
            // ExportOpenOnFinished
            // 
            this.ExportOpenOnFinished.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExportOpenOnFinished.AutoSize = true;
            this.ExportOpenOnFinished.Location = new System.Drawing.Point(139, 395);
            this.ExportOpenOnFinished.Name = "ExportOpenOnFinished";
            this.ExportOpenOnFinished.Size = new System.Drawing.Size(203, 19);
            this.ExportOpenOnFinished.TabIndex = 10;
            this.ExportOpenOnFinished.Text = "Open folder when export finished";
            this.ExportOpenOnFinished.UseVisualStyleBackColor = true;
            // 
            // ExportAllButton
            // 
            this.ExportAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportAllButton.Location = new System.Drawing.Point(528, 423);
            this.ExportAllButton.Name = "ExportAllButton";
            this.ExportAllButton.Size = new System.Drawing.Size(142, 23);
            this.ExportAllButton.TabIndex = 9;
            this.ExportAllButton.Text = "Export All";
            this.ExportAllButton.UseVisualStyleBackColor = true;
            this.ExportAllButton.Click += new System.EventHandler(this.ExportAllButton_Click);
            // 
            // ExportSoundBanksButton
            // 
            this.ExportSoundBanksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportSoundBanksButton.Location = new System.Drawing.Point(528, 395);
            this.ExportSoundBanksButton.Name = "ExportSoundBanksButton";
            this.ExportSoundBanksButton.Size = new System.Drawing.Size(142, 23);
            this.ExportSoundBanksButton.TabIndex = 8;
            this.ExportSoundBanksButton.Text = "Export Sound Banks";
            this.ExportSoundBanksButton.UseVisualStyleBackColor = true;
            this.ExportSoundBanksButton.Click += new System.EventHandler(this.ExportSoundBanksButton_Click);
            // 
            // ExportSoundFoldersButton
            // 
            this.ExportSoundFoldersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportSoundFoldersButton.Location = new System.Drawing.Point(528, 366);
            this.ExportSoundFoldersButton.Name = "ExportSoundFoldersButton";
            this.ExportSoundFoldersButton.Size = new System.Drawing.Size(142, 23);
            this.ExportSoundFoldersButton.TabIndex = 7;
            this.ExportSoundFoldersButton.Text = "Export Sound Folders";
            this.ExportSoundFoldersButton.UseVisualStyleBackColor = true;
            this.ExportSoundFoldersButton.Click += new System.EventHandler(this.ExportSoundFoldersButton_Click);
            // 
            // ExportDatatableButton
            // 
            this.ExportDatatableButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportDatatableButton.Location = new System.Drawing.Point(528, 337);
            this.ExportDatatableButton.Name = "ExportDatatableButton";
            this.ExportDatatableButton.Size = new System.Drawing.Size(142, 23);
            this.ExportDatatableButton.TabIndex = 6;
            this.ExportDatatableButton.Text = "Export Datatable";
            this.ExportDatatableButton.UseVisualStyleBackColor = true;
            this.ExportDatatableButton.Click += new System.EventHandler(this.ExportDatatableButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CreateButton.Location = new System.Drawing.Point(139, 343);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 5;
            this.CreateButton.Text = "Create...";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.NewSoundsBox);
            this.groupBox8.Location = new System.Drawing.Point(8, 212);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(125, 213);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "New Sounds List";
            // 
            // NewSoundsBox
            // 
            this.NewSoundsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewSoundsBox.FormattingEnabled = true;
            this.NewSoundsBox.ItemHeight = 15;
            this.NewSoundsBox.Location = new System.Drawing.Point(3, 19);
            this.NewSoundsBox.Name = "NewSoundsBox";
            this.NewSoundsBox.Size = new System.Drawing.Size(119, 191);
            this.NewSoundsBox.TabIndex = 1;
            this.NewSoundsBox.SelectedIndexChanged += new System.EventHandler(this.NewSoundsBox_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.EditorTable);
            this.groupBox4.Location = new System.Drawing.Point(139, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(531, 331);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sound Data";
            // 
            // EditorTable
            // 
            this.EditorTable.ColumnCount = 3;
            this.EditorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.EditorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.EditorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.EditorTable.Controls.Add(this.panel3, 1, 0);
            this.EditorTable.Controls.Add(this.groupBox5, 0, 0);
            this.EditorTable.Controls.Add(this.panel2, 2, 0);
            this.EditorTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorTable.Location = new System.Drawing.Point(3, 19);
            this.EditorTable.Name = "EditorTable";
            this.EditorTable.RowCount = 1;
            this.EditorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.EditorTable.Size = new System.Drawing.Size(525, 309);
            this.EditorTable.TabIndex = 7;
            this.EditorTable.Resize += new System.EventHandler(this.EditorTable_Resize);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox6);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(176, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 303);
            this.panel3.TabIndex = 5;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.MusicAttributesGrid);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(172, 179);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Music Attributes";
            // 
            // MusicAttributesGrid
            // 
            this.MusicAttributesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MusicAttributesGrid.HelpVisible = false;
            this.MusicAttributesGrid.Location = new System.Drawing.Point(3, 19);
            this.MusicAttributesGrid.Name = "MusicAttributesGrid";
            this.MusicAttributesGrid.Size = new System.Drawing.Size(166, 157);
            this.MusicAttributesGrid.TabIndex = 3;
            this.MusicAttributesGrid.ToolbarVisible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.MusicOrderGrid);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox7.Location = new System.Drawing.Point(0, 179);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(172, 124);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Music Order";
            // 
            // MusicOrderGrid
            // 
            this.MusicOrderGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MusicOrderGrid.HelpVisible = false;
            this.MusicOrderGrid.Location = new System.Drawing.Point(3, 19);
            this.MusicOrderGrid.Name = "MusicOrderGrid";
            this.MusicOrderGrid.Size = new System.Drawing.Size(166, 102);
            this.MusicOrderGrid.TabIndex = 3;
            this.MusicOrderGrid.ToolbarVisible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.MusicInfoGrid);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(167, 303);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Music Info";
            // 
            // MusicInfoGrid
            // 
            this.MusicInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MusicInfoGrid.HelpVisible = false;
            this.MusicInfoGrid.Location = new System.Drawing.Point(3, 19);
            this.MusicInfoGrid.Name = "MusicInfoGrid";
            this.MusicInfoGrid.Size = new System.Drawing.Size(161, 281);
            this.MusicInfoGrid.TabIndex = 3;
            this.MusicInfoGrid.ToolbarVisible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.WordDetailGB);
            this.panel2.Controls.Add(this.WordSubGB);
            this.panel2.Controls.Add(this.WordsGB);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(354, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(168, 303);
            this.panel2.TabIndex = 6;
            // 
            // WordDetailGB
            // 
            this.WordDetailGB.Controls.Add(this.WordDetailGrid);
            this.WordDetailGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.WordDetailGB.Location = new System.Drawing.Point(0, 184);
            this.WordDetailGB.Name = "WordDetailGB";
            this.WordDetailGB.Size = new System.Drawing.Size(168, 96);
            this.WordDetailGB.TabIndex = 8;
            this.WordDetailGB.TabStop = false;
            this.WordDetailGB.Text = "Word Detail";
            // 
            // WordDetailGrid
            // 
            this.WordDetailGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WordDetailGrid.HelpVisible = false;
            this.WordDetailGrid.Location = new System.Drawing.Point(3, 19);
            this.WordDetailGrid.Name = "WordDetailGrid";
            this.WordDetailGrid.Size = new System.Drawing.Size(162, 74);
            this.WordDetailGrid.TabIndex = 3;
            this.WordDetailGrid.ToolbarVisible = false;
            // 
            // WordSubGB
            // 
            this.WordSubGB.Controls.Add(this.WordSubGrid);
            this.WordSubGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.WordSubGB.Location = new System.Drawing.Point(0, 88);
            this.WordSubGB.Name = "WordSubGB";
            this.WordSubGB.Size = new System.Drawing.Size(168, 96);
            this.WordSubGB.TabIndex = 7;
            this.WordSubGB.TabStop = false;
            this.WordSubGB.Text = "Word Sub";
            // 
            // WordSubGrid
            // 
            this.WordSubGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WordSubGrid.HelpVisible = false;
            this.WordSubGrid.Location = new System.Drawing.Point(3, 19);
            this.WordSubGrid.Name = "WordSubGrid";
            this.WordSubGrid.Size = new System.Drawing.Size(162, 74);
            this.WordSubGrid.TabIndex = 3;
            this.WordSubGrid.ToolbarVisible = false;
            // 
            // WordsGB
            // 
            this.WordsGB.Controls.Add(this.WordsGrid);
            this.WordsGB.Dock = System.Windows.Forms.DockStyle.Top;
            this.WordsGB.Location = new System.Drawing.Point(0, 0);
            this.WordsGB.Name = "WordsGB";
            this.WordsGB.Size = new System.Drawing.Size(168, 88);
            this.WordsGB.TabIndex = 6;
            this.WordsGB.TabStop = false;
            this.WordsGB.Text = "Words";
            // 
            // WordsGrid
            // 
            this.WordsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WordsGrid.HelpVisible = false;
            this.WordsGrid.Location = new System.Drawing.Point(3, 19);
            this.WordsGrid.Name = "WordsGrid";
            this.WordsGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.WordsGrid.Size = new System.Drawing.Size(162, 66);
            this.WordsGrid.TabIndex = 3;
            this.WordsGrid.ToolbarVisible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LoadedMusicBox);
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(125, 203);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sounds List";
            // 
            // LoadedMusicBox
            // 
            this.LoadedMusicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadedMusicBox.FormattingEnabled = true;
            this.LoadedMusicBox.ItemHeight = 15;
            this.LoadedMusicBox.Location = new System.Drawing.Point(3, 19);
            this.LoadedMusicBox.Name = "LoadedMusicBox";
            this.LoadedMusicBox.Size = new System.Drawing.Size(119, 181);
            this.LoadedMusicBox.TabIndex = 1;
            this.LoadedMusicBox.SelectedIndexChanged += new System.EventHandler(this.LoadedMusicBox_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(676, 452);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.groupBox10);
            this.panel4.Location = new System.Drawing.Point(151, 102);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(374, 292);
            this.panel4.TabIndex = 2;
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.AddSilenceBox);
            this.groupBox10.Controls.Add(this.FeedbackBox);
            this.groupBox10.Controls.Add(this.CreateBackButton);
            this.groupBox10.Controls.Add(this.CreateOkButton);
            this.groupBox10.Controls.Add(this.label2);
            this.groupBox10.Controls.Add(this.SongNameBox);
            this.groupBox10.Controls.Add(this.label9);
            this.groupBox10.Controls.Add(this.TJASelector);
            this.groupBox10.Controls.Add(this.AudioFileSelector);
            this.groupBox10.Controls.Add(this.label10);
            this.groupBox10.Location = new System.Drawing.Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(368, 247);
            this.groupBox10.TabIndex = 8;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Create new sound";
            // 
            // AddSilenceBox
            // 
            this.AddSilenceBox.AutoSize = true;
            this.AddSilenceBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.AddSilenceBox.Checked = true;
            this.AddSilenceBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AddSilenceBox.Location = new System.Drawing.Point(6, 102);
            this.AddSilenceBox.Name = "AddSilenceBox";
            this.AddSilenceBox.Size = new System.Drawing.Size(143, 19);
            this.AddSilenceBox.TabIndex = 19;
            this.AddSilenceBox.Text = "Delay before song (3s)";
            this.AddSilenceBox.UseVisualStyleBackColor = true;
            // 
            // FeedbackBox
            // 
            this.FeedbackBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FeedbackBox.Location = new System.Drawing.Point(6, 162);
            this.FeedbackBox.Multiline = true;
            this.FeedbackBox.Name = "FeedbackBox";
            this.FeedbackBox.Size = new System.Drawing.Size(356, 78);
            this.FeedbackBox.TabIndex = 18;
            // 
            // CreateBackButton
            // 
            this.CreateBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateBackButton.Location = new System.Drawing.Point(205, 122);
            this.CreateBackButton.Name = "CreateBackButton";
            this.CreateBackButton.Size = new System.Drawing.Size(75, 23);
            this.CreateBackButton.TabIndex = 17;
            this.CreateBackButton.Text = "Back";
            this.CreateBackButton.UseVisualStyleBackColor = true;
            this.CreateBackButton.Click += new System.EventHandler(this.CreateBackButton_Click);
            // 
            // CreateOkButton
            // 
            this.CreateOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateOkButton.Location = new System.Drawing.Point(286, 122);
            this.CreateOkButton.Name = "CreateOkButton";
            this.CreateOkButton.Size = new System.Drawing.Size(75, 23);
            this.CreateOkButton.TabIndex = 16;
            this.CreateOkButton.Text = "Ok";
            this.CreateOkButton.UseVisualStyleBackColor = true;
            this.CreateOkButton.Click += new System.EventHandler(this.CreateOkButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Song name";
            // 
            // SongNameBox
            // 
            this.SongNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SongNameBox.Location = new System.Drawing.Point(79, 73);
            this.SongNameBox.Name = "SongNameBox";
            this.SongNameBox.Size = new System.Drawing.Size(283, 23);
            this.SongNameBox.TabIndex = 14;
            this.SongNameBox.Text = "(6 characters id...)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 15);
            this.label9.TabIndex = 13;
            this.label9.Text = "TJA file";
            // 
            // TJASelector
            // 
            this.TJASelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TJASelector.Filter = ".tja files(*.tja)|*.tja|All files(*.*)|*.*";
            this.TJASelector.Location = new System.Drawing.Point(79, 44);
            this.TJASelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TJASelector.Name = "TJASelector";
            this.TJASelector.Path = "";
            this.TJASelector.SelectsFolder = false;
            this.TJASelector.Size = new System.Drawing.Size(282, 23);
            this.TJASelector.TabIndex = 10;
            this.TJASelector.PathChanged += new TaikoSoundEditor.PathSelector.OnPathChanged(this.TJASelector_PathChanged);
            // 
            // AudioFileSelector
            // 
            this.AudioFileSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AudioFileSelector.Filter = "OGG files(*.ogg)|*.ogg|mp3 files(*.mp3)|*.mp3|WAV files(*.wav)|*.wav|All files(*." +
    "*)|*.*";
            this.AudioFileSelector.Location = new System.Drawing.Point(79, 15);
            this.AudioFileSelector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AudioFileSelector.Name = "AudioFileSelector";
            this.AudioFileSelector.Path = "";
            this.AudioFileSelector.SelectsFolder = false;
            this.AudioFileSelector.Size = new System.Drawing.Size(282, 23);
            this.AudioFileSelector.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 15);
            this.label10.TabIndex = 8;
            this.label10.Text = "Audio file";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.TabControl);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Text = "Taiko Sound Editor";
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.EditorTable.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.WordDetailGB.ResumeLayout(false);
            this.WordSubGB.ResumeLayout(false);
            this.WordsGB.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Panel panel1;
        private GroupBox groupBox1;
        private Label label5;
        private Label label6;
        private Label label7;
        private PathSelector WordListPathSelector;
        private PathSelector MusicInfoPathSelector;
        private PathSelector MusicOrderPathSelector;
        private PathSelector MusicAttributePathSelector;
        private Label label8;
        private GroupBox groupBox2;
        private Button OkButton;
        private PathSelector DirSelector;
        private Label label1;
        private ListBox LoadedMusicBox;
        private GroupBox groupBox3;
        private PropertyGrid MusicInfoGrid;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private PropertyGrid MusicAttributesGrid;
        private GroupBox WordsGB;
        private PropertyGrid WordsGrid;
        private TableLayoutPanel EditorTable;
        private Panel panel2;
        private GroupBox WordDetailGB;
        private PropertyGrid WordDetailGrid;
        private GroupBox WordSubGB;
        private PropertyGrid WordSubGrid;
        private Panel panel3;
        private GroupBox groupBox7;
        private PropertyGrid MusicOrderGrid;
        private GroupBox groupBox8;
        private ListBox NewSoundsBox;
        private Button CreateButton;
        private TabPage tabPage3;
        private Panel panel4;
        private GroupBox groupBox10;
        private Label label9;
        private PathSelector TJASelector;
        private PathSelector AudioFileSelector;
        private Label label10;
        private TextBox SongNameBox;
        private Label label2;
        private Button CreateOkButton;
        private Button CreateBackButton;
        private TextBox FeedbackBox;
        private Button ExportAllButton;
        private Button ExportSoundBanksButton;
        private Button ExportSoundFoldersButton;
        private Button ExportDatatableButton;
        private CheckBox ExportOpenOnFinished;
        private CheckBox AddSilenceBox;
        private CheckBox DatatableSpaces;
    }
}