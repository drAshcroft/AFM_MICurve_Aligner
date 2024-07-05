namespace GroupCurves
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ChainSortButton = new System.Windows.Forms.Button();
            this.BifurificationButton = new System.Windows.Forms.Button();
            this.ShakeBin = new System.Windows.Forms.Button();
            this.bResetGroup = new System.Windows.Forms.Button();
            this.ShakeSortButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listMain = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bLock = new System.Windows.Forms.Button();
            this.bFixateGroup = new System.Windows.Forms.Button();
            this.bSaveBins = new System.Windows.Forms.Button();
            this.HistoSortButton = new System.Windows.Forms.Button();
            this.MontoCarloButton = new System.Windows.Forms.Button();
            this.bTrash = new System.Windows.Forms.Button();
            this.bMergeGroups = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bShowStats = new System.Windows.Forms.Button();
            this.bAverageGroup = new System.Windows.Forms.Button();
            this.bManualSort = new System.Windows.Forms.Button();
            this.listTrash = new System.Windows.Forms.ListBox();
            this.listLocked = new System.Windows.Forms.ListBox();
            this.bDoYoungs = new System.Windows.Forms.Button();
            this.bReturnToMain = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAFMFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createLSFCubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLSFCubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.arrangeFilesWithGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBinDescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSquared = new System.Windows.Forms.RadioButton();
            this.rdSquared = new System.Windows.Forms.RadioButton();
            this.rAbsolute = new System.Windows.Forms.RadioButton();
            this.rChiSquared = new System.Windows.Forms.RadioButton();
            this.rRegression = new System.Windows.Forms.RadioButton();
            this.rWeightedDiff = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rCrossCorrelation = new System.Windows.Forms.RadioButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.bRunSorts = new System.Windows.Forms.Button();
            this.bUntrashButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lManual = new System.Windows.Forms.ListBox();
            this.UnSort = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SingleCurve = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ChainSortButton
            // 
            this.ChainSortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChainSortButton.Location = new System.Drawing.Point(9, 22);
            this.ChainSortButton.Name = "ChainSortButton";
            this.ChainSortButton.Size = new System.Drawing.Size(121, 23);
            this.ChainSortButton.TabIndex = 0;
            this.ChainSortButton.Text = "ChainSort";
            this.ChainSortButton.UseVisualStyleBackColor = true;
            this.ChainSortButton.Click += new System.EventHandler(this.ChainSort_Click);
            // 
            // BifurificationButton
            // 
            this.BifurificationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BifurificationButton.Location = new System.Drawing.Point(9, 51);
            this.BifurificationButton.Name = "BifurificationButton";
            this.BifurificationButton.Size = new System.Drawing.Size(121, 27);
            this.BifurificationButton.TabIndex = 4;
            this.BifurificationButton.Text = "Bifurication";
            this.BifurificationButton.UseVisualStyleBackColor = true;
            this.BifurificationButton.Click += new System.EventHandler(this.BifurificationButton_click);
            // 
            // ShakeBin
            // 
            this.ShakeBin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShakeBin.Location = new System.Drawing.Point(136, 19);
            this.ShakeBin.Name = "ShakeBin";
            this.ShakeBin.Size = new System.Drawing.Size(125, 25);
            this.ShakeBin.TabIndex = 5;
            this.ShakeBin.Text = "Shake Bin";
            this.ShakeBin.UseVisualStyleBackColor = true;
            this.ShakeBin.Click += new System.EventHandler(this.ShakeBin_Click);
            // 
            // bResetGroup
            // 
            this.bResetGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bResetGroup.Location = new System.Drawing.Point(6, 48);
            this.bResetGroup.Name = "bResetGroup";
            this.bResetGroup.Size = new System.Drawing.Size(125, 36);
            this.bResetGroup.TabIndex = 6;
            this.bResetGroup.Text = "Reset Groups To Original";
            this.bResetGroup.UseVisualStyleBackColor = true;
            this.bResetGroup.Click += new System.EventHandler(this.bResetGroup_Click);
            // 
            // ShakeSortButton
            // 
            this.ShakeSortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShakeSortButton.Location = new System.Drawing.Point(136, 50);
            this.ShakeSortButton.Name = "ShakeSortButton";
            this.ShakeSortButton.Size = new System.Drawing.Size(125, 28);
            this.ShakeSortButton.TabIndex = 7;
            this.ShakeSortButton.Text = "Shake and Divide";
            this.ShakeSortButton.UseVisualStyleBackColor = true;
            this.ShakeSortButton.Click += new System.EventHandler(this.ShakeSort_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(5, 529);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(963, 18);
            this.progressBar1.TabIndex = 9;
            // 
            // listMain
            // 
            this.listMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listMain.FormattingEnabled = true;
            this.listMain.Location = new System.Drawing.Point(10, 19);
            this.listMain.Name = "listMain";
            this.listMain.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listMain.Size = new System.Drawing.Size(108, 303);
            this.listMain.TabIndex = 11;
            this.listMain.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Number Of Curves";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 343);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "0";
            // 
            // bLock
            // 
            this.bLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bLock.Location = new System.Drawing.Point(9, 28);
            this.bLock.Name = "bLock";
            this.bLock.Size = new System.Drawing.Size(69, 25);
            this.bLock.TabIndex = 16;
            this.bLock.Text = "Lock >>";
            this.bLock.UseVisualStyleBackColor = true;
            this.bLock.Click += new System.EventHandler(this.bLock_Click);
            // 
            // bFixateGroup
            // 
            this.bFixateGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bFixateGroup.Location = new System.Drawing.Point(136, 19);
            this.bFixateGroup.Name = "bFixateGroup";
            this.bFixateGroup.Size = new System.Drawing.Size(125, 23);
            this.bFixateGroup.TabIndex = 17;
            this.bFixateGroup.Text = "Fix Curves to Group";
            this.bFixateGroup.UseVisualStyleBackColor = true;
            this.bFixateGroup.Click += new System.EventHandler(this.FixateCurves_Click);
            // 
            // bSaveBins
            // 
            this.bSaveBins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSaveBins.Location = new System.Drawing.Point(9, 24);
            this.bSaveBins.Name = "bSaveBins";
            this.bSaveBins.Size = new System.Drawing.Size(125, 35);
            this.bSaveBins.TabIndex = 18;
            this.bSaveBins.Text = "Save Groups to Text";
            this.bSaveBins.UseVisualStyleBackColor = true;
            this.bSaveBins.Click += new System.EventHandler(this.bSaveBins_Click);
            // 
            // HistoSortButton
            // 
            this.HistoSortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoSortButton.Location = new System.Drawing.Point(136, 22);
            this.HistoSortButton.Name = "HistoSortButton";
            this.HistoSortButton.Size = new System.Drawing.Size(125, 22);
            this.HistoSortButton.TabIndex = 19;
            this.HistoSortButton.Text = "HistoSort";
            this.HistoSortButton.UseVisualStyleBackColor = true;
            this.HistoSortButton.Click += new System.EventHandler(this.HistoSort_Click);
            // 
            // MontoCarloButton
            // 
            this.MontoCarloButton.Location = new System.Drawing.Point(9, 19);
            this.MontoCarloButton.Name = "MontoCarloButton";
            this.MontoCarloButton.Size = new System.Drawing.Size(121, 25);
            this.MontoCarloButton.TabIndex = 20;
            this.MontoCarloButton.Text = "Monto Carlo";
            this.MontoCarloButton.UseVisualStyleBackColor = true;
            this.MontoCarloButton.Click += new System.EventHandler(this.MontoCarlo_Click);
            // 
            // bTrash
            // 
            this.bTrash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bTrash.Location = new System.Drawing.Point(9, 19);
            this.bTrash.Name = "bTrash";
            this.bTrash.Size = new System.Drawing.Size(69, 26);
            this.bTrash.TabIndex = 22;
            this.bTrash.Text = "Trash >>";
            this.bTrash.UseVisualStyleBackColor = true;
            this.bTrash.Click += new System.EventHandler(this.bTrash_Click);
            // 
            // bMergeGroups
            // 
            this.bMergeGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMergeGroups.Location = new System.Drawing.Point(6, 19);
            this.bMergeGroups.Name = "bMergeGroups";
            this.bMergeGroups.Size = new System.Drawing.Size(125, 23);
            this.bMergeGroups.TabIndex = 23;
            this.bMergeGroups.Text = "Merge Groups";
            this.bMergeGroups.UseVisualStyleBackColor = true;
            this.bMergeGroups.Click += new System.EventHandler(this.MergeGroups_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.BifurificationButton);
            this.groupBox1.Controls.Add(this.ChainSortButton);
            this.groupBox1.Controls.Add(this.ShakeSortButton);
            this.groupBox1.Controls.Add(this.HistoSortButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 236);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 88);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sorts";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.ShakeBin);
            this.groupBox2.Controls.Add(this.MontoCarloButton);
            this.groupBox2.Location = new System.Drawing.Point(3, 330);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 56);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relaxations";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.bShowStats);
            this.groupBox3.Controls.Add(this.bAverageGroup);
            this.groupBox3.Controls.Add(this.bFixateGroup);
            this.groupBox3.Controls.Add(this.bMergeGroups);
            this.groupBox3.Controls.Add(this.bResetGroup);
            this.groupBox3.Location = new System.Drawing.Point(3, 85);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 140);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Group Operations";
            // 
            // bShowStats
            // 
            this.bShowStats.Location = new System.Drawing.Point(136, 48);
            this.bShowStats.Name = "bShowStats";
            this.bShowStats.Size = new System.Drawing.Size(125, 36);
            this.bShowStats.TabIndex = 26;
            this.bShowStats.Text = "Show Bin Stats";
            this.bShowStats.UseVisualStyleBackColor = true;
            // 
            // bAverageGroup
            // 
            this.bAverageGroup.Location = new System.Drawing.Point(6, 90);
            this.bAverageGroup.Name = "bAverageGroup";
            this.bAverageGroup.Size = new System.Drawing.Size(125, 35);
            this.bAverageGroup.TabIndex = 24;
            this.bAverageGroup.Text = "Average Group";
            this.bAverageGroup.UseVisualStyleBackColor = true;
            this.bAverageGroup.Click += new System.EventHandler(this.bAverageGroup_Click);
            // 
            // bManualSort
            // 
            this.bManualSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bManualSort.Location = new System.Drawing.Point(9, 19);
            this.bManualSort.Name = "bManualSort";
            this.bManualSort.Size = new System.Drawing.Size(69, 35);
            this.bManualSort.TabIndex = 25;
            this.bManualSort.Text = "Manual Sort";
            this.bManualSort.UseVisualStyleBackColor = true;
            this.bManualSort.Click += new System.EventHandler(this.bManualSort_Click);
            // 
            // listTrash
            // 
            this.listTrash.FormattingEnabled = true;
            this.listTrash.Location = new System.Drawing.Point(84, 15);
            this.listTrash.Name = "listTrash";
            this.listTrash.Size = new System.Drawing.Size(39, 108);
            this.listTrash.TabIndex = 29;
            this.listTrash.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // listLocked
            // 
            this.listLocked.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listLocked.FormattingEnabled = true;
            this.listLocked.Location = new System.Drawing.Point(84, 15);
            this.listLocked.Name = "listLocked";
            this.listLocked.Size = new System.Drawing.Size(39, 108);
            this.listLocked.TabIndex = 32;
            this.listLocked.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // bDoYoungs
            // 
            this.bDoYoungs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDoYoungs.Location = new System.Drawing.Point(288, 553);
            this.bDoYoungs.Name = "bDoYoungs";
            this.bDoYoungs.Size = new System.Drawing.Size(95, 27);
            this.bDoYoungs.TabIndex = 33;
            this.bDoYoungs.Text = "Do Young\'s";
            this.bDoYoungs.UseVisualStyleBackColor = true;
            this.bDoYoungs.Click += new System.EventHandler(this.bDoYoungs_Click);
            // 
            // bReturnToMain
            // 
            this.bReturnToMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bReturnToMain.Location = new System.Drawing.Point(9, 59);
            this.bReturnToMain.Name = "bReturnToMain";
            this.bReturnToMain.Size = new System.Drawing.Size(69, 26);
            this.bReturnToMain.TabIndex = 34;
            this.bReturnToMain.Text = "<<Unlock";
            this.bReturnToMain.UseVisualStyleBackColor = true;
            this.bReturnToMain.Click += new System.EventHandler(this.bReturnToMain_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(997, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importAFMFilesToolStripMenuItem,
            this.createLSFCubeToolStripMenuItem,
            this.openLSFCubeToolStripMenuItem,
            this.toolStripSeparator1,
            this.arrangeFilesWithGroupsToolStripMenuItem,
            this.saveBinDescriptionToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportStatisticsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importAFMFilesToolStripMenuItem
            // 
            this.importAFMFilesToolStripMenuItem.Name = "importAFMFilesToolStripMenuItem";
            this.importAFMFilesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.importAFMFilesToolStripMenuItem.Text = "Import AFM Files";
            this.importAFMFilesToolStripMenuItem.Click += new System.EventHandler(this.importAFMFilesToolStripMenuItem_Click);
            // 
            // createLSFCubeToolStripMenuItem
            // 
            this.createLSFCubeToolStripMenuItem.Name = "createLSFCubeToolStripMenuItem";
            this.createLSFCubeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.createLSFCubeToolStripMenuItem.Text = "Create LSF Cube";
            this.createLSFCubeToolStripMenuItem.Click += new System.EventHandler(this.createLSFCubeToolStripMenuItem_Click);
            // 
            // openLSFCubeToolStripMenuItem
            // 
            this.openLSFCubeToolStripMenuItem.Name = "openLSFCubeToolStripMenuItem";
            this.openLSFCubeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openLSFCubeToolStripMenuItem.Text = "Open LSF Cube";
            this.openLSFCubeToolStripMenuItem.Click += new System.EventHandler(this.openLSFCubeToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // arrangeFilesWithGroupsToolStripMenuItem
            // 
            this.arrangeFilesWithGroupsToolStripMenuItem.Name = "arrangeFilesWithGroupsToolStripMenuItem";
            this.arrangeFilesWithGroupsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.arrangeFilesWithGroupsToolStripMenuItem.Text = "Move Files into Groups";
            this.arrangeFilesWithGroupsToolStripMenuItem.Click += new System.EventHandler(this.arrangeFilesWithGroupsToolStripMenuItem_Click);
            // 
            // saveBinDescriptionToolStripMenuItem
            // 
            this.saveBinDescriptionToolStripMenuItem.Name = "saveBinDescriptionToolStripMenuItem";
            this.saveBinDescriptionToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveBinDescriptionToolStripMenuItem.Text = "Save Groups to Text";
            this.saveBinDescriptionToolStripMenuItem.Click += new System.EventHandler(this.saveBinDescriptionToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // exportStatisticsToolStripMenuItem
            // 
            this.exportStatisticsToolStripMenuItem.Name = "exportStatisticsToolStripMenuItem";
            this.exportStatisticsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exportStatisticsToolStripMenuItem.Text = "Export Statistics";
            this.exportStatisticsToolStripMenuItem.Click += new System.EventHandler(this.exportStatisticsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyGraphToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyGraphToolStripMenuItem
            // 
            this.copyGraphToolStripMenuItem.Name = "copyGraphToolStripMenuItem";
            this.copyGraphToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.copyGraphToolStripMenuItem.Text = "Copy Graph";
            this.copyGraphToolStripMenuItem.Click += new System.EventHandler(this.copyGraphToolStripMenuItem_Click);
            // 
            // rSquared
            // 
            this.rSquared.AutoSize = true;
            this.rSquared.Checked = true;
            this.rSquared.Location = new System.Drawing.Point(3, 5);
            this.rSquared.Name = "rSquared";
            this.rSquared.Size = new System.Drawing.Size(111, 17);
            this.rSquared.TabIndex = 36;
            this.rSquared.TabStop = true;
            this.rSquared.Text = "Square Difference";
            this.rSquared.UseVisualStyleBackColor = true;
            this.rSquared.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rdSquared
            // 
            this.rdSquared.AutoSize = true;
            this.rdSquared.Location = new System.Drawing.Point(120, 5);
            this.rdSquared.Name = "rdSquared";
            this.rdSquared.Size = new System.Drawing.Size(139, 17);
            this.rdSquared.TabIndex = 37;
            this.rdSquared.Text = "Deriv Square Difference";
            this.rdSquared.UseVisualStyleBackColor = true;
            this.rdSquared.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rAbsolute
            // 
            this.rAbsolute.AutoSize = true;
            this.rAbsolute.Location = new System.Drawing.Point(3, 74);
            this.rAbsolute.Name = "rAbsolute";
            this.rAbsolute.Size = new System.Drawing.Size(118, 17);
            this.rAbsolute.TabIndex = 38;
            this.rAbsolute.Text = "Absolute Difference";
            this.rAbsolute.UseVisualStyleBackColor = true;
            this.rAbsolute.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rChiSquared
            // 
            this.rChiSquared.AutoSize = true;
            this.rChiSquared.Location = new System.Drawing.Point(3, 28);
            this.rChiSquared.Name = "rChiSquared";
            this.rChiSquared.Size = new System.Drawing.Size(80, 17);
            this.rChiSquared.TabIndex = 39;
            this.rChiSquared.Text = "ChiSquared";
            this.rChiSquared.UseVisualStyleBackColor = true;
            this.rChiSquared.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // rRegression
            // 
            this.rRegression.AutoSize = true;
            this.rRegression.Location = new System.Drawing.Point(120, 28);
            this.rRegression.Name = "rRegression";
            this.rRegression.Size = new System.Drawing.Size(78, 17);
            this.rRegression.TabIndex = 40;
            this.rRegression.Text = "Regression";
            this.rRegression.UseVisualStyleBackColor = true;
            this.rRegression.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // rWeightedDiff
            // 
            this.rWeightedDiff.AutoSize = true;
            this.rWeightedDiff.Location = new System.Drawing.Point(120, 51);
            this.rWeightedDiff.Name = "rWeightedDiff";
            this.rWeightedDiff.Size = new System.Drawing.Size(123, 17);
            this.rWeightedDiff.TabIndex = 41;
            this.rWeightedDiff.TabStop = true;
            this.rWeightedDiff.Text = "Weighted Difference";
            this.rWeightedDiff.UseVisualStyleBackColor = true;
            this.rWeightedDiff.CheckedChanged += new System.EventHandler(this.rWeightedDiff_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.rCrossCorrelation);
            this.panel1.Controls.Add(this.rdSquared);
            this.panel1.Controls.Add(this.rWeightedDiff);
            this.panel1.Controls.Add(this.rSquared);
            this.panel1.Controls.Add(this.rRegression);
            this.panel1.Controls.Add(this.rAbsolute);
            this.panel1.Controls.Add(this.rChiSquared);
            this.panel1.Location = new System.Drawing.Point(6, 394);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(265, 120);
            this.panel1.TabIndex = 42;
            // 
            // rCrossCorrelation
            // 
            this.rCrossCorrelation.AutoSize = true;
            this.rCrossCorrelation.Location = new System.Drawing.Point(3, 51);
            this.rCrossCorrelation.Name = "rCrossCorrelation";
            this.rCrossCorrelation.Size = new System.Drawing.Size(104, 17);
            this.rCrossCorrelation.TabIndex = 42;
            this.rCrossCorrelation.TabStop = true;
            this.rCrossCorrelation.Text = "Cross Correlation";
            this.rCrossCorrelation.UseVisualStyleBackColor = true;
            this.rCrossCorrelation.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(5, 0);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(546, 366);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // bRunSorts
            // 
            this.bRunSorts.Location = new System.Drawing.Point(388, 553);
            this.bRunSorts.Name = "bRunSorts";
            this.bRunSorts.Size = new System.Drawing.Size(95, 27);
            this.bRunSorts.TabIndex = 43;
            this.bRunSorts.Text = "button3";
            this.bRunSorts.UseVisualStyleBackColor = true;
            this.bRunSorts.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // bUntrashButton
            // 
            this.bUntrashButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bUntrashButton.Location = new System.Drawing.Point(9, 51);
            this.bUntrashButton.Name = "bUntrashButton";
            this.bUntrashButton.Size = new System.Drawing.Size(69, 26);
            this.bUntrashButton.TabIndex = 44;
            this.bUntrashButton.Text = "<< Untrash";
            this.bUntrashButton.UseVisualStyleBackColor = true;
            this.bUntrashButton.Click += new System.EventHandler(this.bUntrashButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listTrash);
            this.groupBox4.Controls.Add(this.bTrash);
            this.groupBox4.Controls.Add(this.bUntrashButton);
            this.groupBox4.Location = new System.Drawing.Point(300, 394);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(129, 129);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trashed";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listMain);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(557, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(124, 366);
            this.groupBox5.TabIndex = 47;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Free Group Bins";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.listLocked);
            this.groupBox6.Controls.Add(this.bLock);
            this.groupBox6.Controls.Add(this.bReturnToMain);
            this.groupBox6.Location = new System.Drawing.Point(571, 394);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(129, 126);
            this.groupBox6.TabIndex = 48;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Locked";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Sorting Metric";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox9);
            this.panel2.Controls.Add(this.groupBox8);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Location = new System.Drawing.Point(702, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 517);
            this.panel2.TabIndex = 50;
            this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lManual);
            this.groupBox7.Controls.Add(this.bManualSort);
            this.groupBox7.Controls.Add(this.UnSort);
            this.groupBox7.Location = new System.Drawing.Point(434, 394);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(129, 128);
            this.groupBox7.TabIndex = 49;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Manual Sorts";
            // 
            // lManual
            // 
            this.lManual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lManual.FormattingEnabled = true;
            this.lManual.Location = new System.Drawing.Point(84, 15);
            this.lManual.Name = "lManual";
            this.lManual.Size = new System.Drawing.Size(39, 108);
            this.lManual.TabIndex = 33;
            this.lManual.SelectedIndexChanged += new System.EventHandler(this.lManual_SelectedIndexChanged);
            // 
            // UnSort
            // 
            this.UnSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UnSort.Location = new System.Drawing.Point(9, 60);
            this.UnSort.Name = "UnSort";
            this.UnSort.Size = new System.Drawing.Size(69, 35);
            this.UnSort.TabIndex = 51;
            this.UnSort.Text = "Unsort";
            this.UnSort.UseVisualStyleBackColor = true;
            this.UnSort.Click += new System.EventHandler(this.UnSort_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox6);
            this.panel3.Controls.Add(this.groupBox7);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.bDoYoungs);
            this.panel3.Controls.Add(this.bRunSorts);
            this.panel3.Controls.Add(this.zedGraphControl1);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Location = new System.Drawing.Point(2, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(995, 586);
            this.panel3.TabIndex = 52;
            // 
            // SingleCurve
            // 
            this.SingleCurve.Location = new System.Drawing.Point(9, 19);
            this.SingleCurve.Name = "SingleCurve";
            this.SingleCurve.Size = new System.Drawing.Size(121, 26);
            this.SingleCurve.TabIndex = 53;
            this.SingleCurve.Text = "Single Curve";
            this.SingleCurve.UseVisualStyleBackColor = true;
            this.SingleCurve.Click += new System.EventHandler(this.SingleCurve_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(489, 553);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 27);
            this.button1.TabIndex = 52;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.button2);
            this.groupBox8.Controls.Add(this.SingleCurve);
            this.groupBox8.Location = new System.Drawing.Point(3, 22);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(270, 57);
            this.groupBox8.TabIndex = 29;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Presorts";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(136, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 26);
            this.button2.TabIndex = 52;
            this.button2.Text = "Manual Sort";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button3);
            this.groupBox9.Controls.Add(this.bSaveBins);
            this.groupBox9.Location = new System.Drawing.Point(3, 400);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(271, 86);
            this.groupBox9.TabIndex = 30;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "File Operations";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(136, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 35);
            this.button3.TabIndex = 19;
            this.button3.Text = "Move Files into Directories";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 614);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Group Curves";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ChainSortButton;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button BifurificationButton;
        private System.Windows.Forms.Button ShakeBin;
        private System.Windows.Forms.Button bResetGroup;
        private System.Windows.Forms.Button ShakeSortButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox listMain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bLock;
        private System.Windows.Forms.Button bFixateGroup;
        private System.Windows.Forms.Button bSaveBins;
        private System.Windows.Forms.Button HistoSortButton;
        private System.Windows.Forms.Button MontoCarloButton;
        private System.Windows.Forms.Button bTrash;
        private System.Windows.Forms.Button bMergeGroups;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listTrash;
        private System.Windows.Forms.ListBox listLocked;
        private System.Windows.Forms.Button bAverageGroup;
        private System.Windows.Forms.Button bDoYoungs;
        private System.Windows.Forms.Button bReturnToMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAFMFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLSFCubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createLSFCubeToolStripMenuItem;
        private System.Windows.Forms.Button bManualSort;
        private System.Windows.Forms.ToolStripMenuItem arrangeFilesWithGroupsToolStripMenuItem;
        public System.Windows.Forms.RadioButton rSquared;
        private System.Windows.Forms.RadioButton rdSquared;
        private System.Windows.Forms.RadioButton rAbsolute;
        private System.Windows.Forms.RadioButton rChiSquared;
        private System.Windows.Forms.RadioButton rRegression;
        private System.Windows.Forms.RadioButton rWeightedDiff;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem exportStatisticsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RadioButton rCrossCorrelation;
        private System.Windows.Forms.Button bRunSorts;
        private System.Windows.Forms.Button bUntrashButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button bShowStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox lManual;
        private System.Windows.Forms.Button UnSort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveBinDescriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SingleCurve;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button3;
    }
}

