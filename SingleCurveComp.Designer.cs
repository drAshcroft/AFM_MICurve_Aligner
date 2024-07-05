namespace GroupCurves
{
    partial class SingleCurveComp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleCurveComp));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.CurveSelector = new System.Windows.Forms.TabPage();
            this.hsCurveSelector = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bSave1DGroups = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cBCutLines = new System.Windows.Forms.ComboBox();
            this.Autosorts = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.b1DGaussian = new System.Windows.Forms.Button();
            this.b1DkMeans = new System.Windows.Forms.Button();
            this.nNum1DGroups = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rCrossCorrelation = new System.Windows.Forms.RadioButton();
            this.rSquared = new System.Windows.Forms.RadioButton();
            this.rdSquared = new System.Windows.Forms.RadioButton();
            this.rChiSquared = new System.Windows.Forms.RadioButton();
            this.rWeightedDiff = new System.Windows.Forms.RadioButton();
            this.rAbsolute = new System.Windows.Forms.RadioButton();
            this.rRegression = new System.Windows.Forms.RadioButton();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rZoom2D = new System.Windows.Forms.RadioButton();
            this.rGroupHandSelect2D = new System.Windows.Forms.RadioButton();
            this.bSave2DAutos = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bGMM2D = new System.Windows.Forms.Button();
            this.kKmeans2D = new System.Windows.Forms.Button();
            this.nNumberofGroups2D = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.yCross = new System.Windows.Forms.RadioButton();
            this.ySquare = new System.Windows.Forms.RadioButton();
            this.ydSquare = new System.Windows.Forms.RadioButton();
            this.yChi = new System.Windows.Forms.RadioButton();
            this.yWeighted = new System.Windows.Forms.RadioButton();
            this.yAbsolute = new System.Windows.Forms.RadioButton();
            this.yRegression = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.xCross = new System.Windows.Forms.RadioButton();
            this.xSquared = new System.Windows.Forms.RadioButton();
            this.xdSquare = new System.Windows.Forms.RadioButton();
            this.xChi = new System.Windows.Forms.RadioButton();
            this.xWeighted = new System.Windows.Forms.RadioButton();
            this.xAbsolute = new System.Windows.Forms.RadioButton();
            this.xRegression = new System.Windows.Forms.RadioButton();
            this.pInstruct2D = new System.Windows.Forms.PictureBox();
            this.bInstruct2D = new System.Windows.Forms.Button();
            this.bCut1D = new System.Windows.Forms.Button();
            this.selectableGraph1 = new GroupCurves.SelectableGraph();
            this.tabControl1.SuspendLayout();
            this.CurveSelector.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Autosorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nNum1DGroups)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nNumberofGroups2D)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pInstruct2D)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 538);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(964, 18);
            this.progressBar1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.CurveSelector);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(964, 538);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // CurveSelector
            // 
            this.CurveSelector.Controls.Add(this.hsCurveSelector);
            this.CurveSelector.Controls.Add(this.label1);
            this.CurveSelector.Controls.Add(this.zedGraphControl1);
            this.CurveSelector.Location = new System.Drawing.Point(4, 22);
            this.CurveSelector.Name = "CurveSelector";
            this.CurveSelector.Padding = new System.Windows.Forms.Padding(3);
            this.CurveSelector.Size = new System.Drawing.Size(956, 512);
            this.CurveSelector.TabIndex = 0;
            this.CurveSelector.Text = "Compare Curve Selector";
            this.CurveSelector.UseVisualStyleBackColor = true;
            // 
            // hsCurveSelector
            // 
            this.hsCurveSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hsCurveSelector.Location = new System.Drawing.Point(6, 28);
            this.hsCurveSelector.Name = "hsCurveSelector";
            this.hsCurveSelector.Size = new System.Drawing.Size(942, 24);
            this.hsCurveSelector.TabIndex = 3;
            this.hsCurveSelector.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsCurveSelector_Scroll);
            this.hsCurveSelector.ValueChanged += new System.EventHandler(this.hsCurveSelector_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Base Curve";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(6, 55);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(942, 354);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.bSave1DGroups);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.Autosorts);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.zedGraphControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(956, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "1D Selection";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // bSave1DGroups
            // 
            this.bSave1DGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave1DGroups.Location = new System.Drawing.Point(791, 444);
            this.bSave1DGroups.Name = "bSave1DGroups";
            this.bSave1DGroups.Size = new System.Drawing.Size(126, 41);
            this.bSave1DGroups.TabIndex = 49;
            this.bSave1DGroups.Text = "Save Groups";
            this.bSave1DGroups.UseVisualStyleBackColor = true;
            this.bSave1DGroups.Click += new System.EventHandler(this.bSave1DGroups_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.bCut1D);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cBCutLines);
            this.groupBox2.Location = new System.Drawing.Point(381, 414);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 92);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Manual Sort";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Cut Line";
            // 
            // cBCutLines
            // 
            this.cBCutLines.FormattingEnabled = true;
            this.cBCutLines.Location = new System.Drawing.Point(15, 38);
            this.cBCutLines.Name = "cBCutLines";
            this.cBCutLines.Size = new System.Drawing.Size(100, 21);
            this.cBCutLines.TabIndex = 0;
            // 
            // Autosorts
            // 
            this.Autosorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Autosorts.Controls.Add(this.label5);
            this.Autosorts.Controls.Add(this.b1DGaussian);
            this.Autosorts.Controls.Add(this.b1DkMeans);
            this.Autosorts.Controls.Add(this.nNum1DGroups);
            this.Autosorts.Controls.Add(this.label2);
            this.Autosorts.Location = new System.Drawing.Point(768, 6);
            this.Autosorts.Name = "Autosorts";
            this.Autosorts.Size = new System.Drawing.Size(164, 293);
            this.Autosorts.TabIndex = 47;
            this.Autosorts.TabStop = false;
            this.Autosorts.Text = "Auto Sorts";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "Not Yet Implimented";
            // 
            // b1DGaussian
            // 
            this.b1DGaussian.Location = new System.Drawing.Point(10, 99);
            this.b1DGaussian.Name = "b1DGaussian";
            this.b1DGaussian.Size = new System.Drawing.Size(125, 32);
            this.b1DGaussian.TabIndex = 49;
            this.b1DGaussian.Text = "Gaussian Mix";
            this.b1DGaussian.UseVisualStyleBackColor = true;
            this.b1DGaussian.Click += new System.EventHandler(this.b1DGaussian_Click);
            // 
            // b1DkMeans
            // 
            this.b1DkMeans.Location = new System.Drawing.Point(10, 61);
            this.b1DkMeans.Name = "b1DkMeans";
            this.b1DkMeans.Size = new System.Drawing.Size(125, 32);
            this.b1DkMeans.TabIndex = 48;
            this.b1DkMeans.Text = "k Means";
            this.b1DkMeans.UseVisualStyleBackColor = true;
            this.b1DkMeans.Click += new System.EventHandler(this.b1DkMeans_Click);
            // 
            // nNum1DGroups
            // 
            this.nNum1DGroups.Location = new System.Drawing.Point(10, 35);
            this.nNum1DGroups.Name = "nNum1DGroups";
            this.nNum1DGroups.Size = new System.Drawing.Size(125, 20);
            this.nNum1DGroups.TabIndex = 47;
            this.nNum1DGroups.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Number of Groups";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.rCrossCorrelation);
            this.groupBox1.Controls.Add(this.rSquared);
            this.groupBox1.Controls.Add(this.rdSquared);
            this.groupBox1.Controls.Add(this.rChiSquared);
            this.groupBox1.Controls.Add(this.rWeightedDiff);
            this.groupBox1.Controls.Add(this.rAbsolute);
            this.groupBox1.Controls.Add(this.rRegression);
            this.groupBox1.Location = new System.Drawing.Point(3, 414);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 92);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comparison Metric";
            // 
            // rCrossCorrelation
            // 
            this.rCrossCorrelation.AutoSize = true;
            this.rCrossCorrelation.Location = new System.Drawing.Point(268, 42);
            this.rCrossCorrelation.Name = "rCrossCorrelation";
            this.rCrossCorrelation.Size = new System.Drawing.Size(104, 17);
            this.rCrossCorrelation.TabIndex = 42;
            this.rCrossCorrelation.TabStop = true;
            this.rCrossCorrelation.Text = "Cross Correlation";
            this.rCrossCorrelation.UseVisualStyleBackColor = true;
            this.rCrossCorrelation.CheckedChanged += new System.EventHandler(this.rCrossCorrelation_CheckedChanged);
            // 
            // rSquared
            // 
            this.rSquared.AutoSize = true;
            this.rSquared.Checked = true;
            this.rSquared.Location = new System.Drawing.Point(6, 19);
            this.rSquared.Name = "rSquared";
            this.rSquared.Size = new System.Drawing.Size(111, 17);
            this.rSquared.TabIndex = 36;
            this.rSquared.TabStop = true;
            this.rSquared.Text = "Square Difference";
            this.rSquared.UseVisualStyleBackColor = true;
            this.rSquared.CheckedChanged += new System.EventHandler(this.rSquared_CheckedChanged);
            // 
            // rdSquared
            // 
            this.rdSquared.AutoSize = true;
            this.rdSquared.Location = new System.Drawing.Point(123, 19);
            this.rdSquared.Name = "rdSquared";
            this.rdSquared.Size = new System.Drawing.Size(139, 17);
            this.rdSquared.TabIndex = 37;
            this.rdSquared.Text = "Deriv Square Difference";
            this.rdSquared.UseVisualStyleBackColor = true;
            this.rdSquared.CheckedChanged += new System.EventHandler(this.rdSquared_CheckedChanged);
            // 
            // rChiSquared
            // 
            this.rChiSquared.AutoSize = true;
            this.rChiSquared.Location = new System.Drawing.Point(6, 42);
            this.rChiSquared.Name = "rChiSquared";
            this.rChiSquared.Size = new System.Drawing.Size(80, 17);
            this.rChiSquared.TabIndex = 39;
            this.rChiSquared.Text = "ChiSquared";
            this.rChiSquared.UseVisualStyleBackColor = true;
            this.rChiSquared.CheckedChanged += new System.EventHandler(this.rChiSquared_CheckedChanged);
            // 
            // rWeightedDiff
            // 
            this.rWeightedDiff.AutoSize = true;
            this.rWeightedDiff.Location = new System.Drawing.Point(123, 42);
            this.rWeightedDiff.Name = "rWeightedDiff";
            this.rWeightedDiff.Size = new System.Drawing.Size(123, 17);
            this.rWeightedDiff.TabIndex = 41;
            this.rWeightedDiff.TabStop = true;
            this.rWeightedDiff.Text = "Weighted Difference";
            this.rWeightedDiff.UseVisualStyleBackColor = true;
            this.rWeightedDiff.CheckedChanged += new System.EventHandler(this.rWeightedDiff_CheckedChanged);
            // 
            // rAbsolute
            // 
            this.rAbsolute.AutoSize = true;
            this.rAbsolute.Location = new System.Drawing.Point(6, 65);
            this.rAbsolute.Name = "rAbsolute";
            this.rAbsolute.Size = new System.Drawing.Size(118, 17);
            this.rAbsolute.TabIndex = 38;
            this.rAbsolute.Text = "Absolute Difference";
            this.rAbsolute.UseVisualStyleBackColor = true;
            this.rAbsolute.CheckedChanged += new System.EventHandler(this.rAbsolute_CheckedChanged);
            // 
            // rRegression
            // 
            this.rRegression.AutoSize = true;
            this.rRegression.Location = new System.Drawing.Point(268, 19);
            this.rRegression.Name = "rRegression";
            this.rRegression.Size = new System.Drawing.Size(78, 17);
            this.rRegression.TabIndex = 40;
            this.rRegression.Text = "Regression";
            this.rRegression.UseVisualStyleBackColor = true;
            this.rRegression.CheckedChanged += new System.EventHandler(this.rRegression_CheckedChanged);
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl2.Location = new System.Drawing.Point(2, 6);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.ScrollGrace = 0D;
            this.zedGraphControl2.ScrollMaxX = 0D;
            this.zedGraphControl2.ScrollMaxY = 0D;
            this.zedGraphControl2.ScrollMaxY2 = 0D;
            this.zedGraphControl2.ScrollMinX = 0D;
            this.zedGraphControl2.ScrollMinY = 0D;
            this.zedGraphControl2.ScrollMinY2 = 0D;
            this.zedGraphControl2.Size = new System.Drawing.Size(760, 405);
            this.zedGraphControl2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.bInstruct2D);
            this.tabPage3.Controls.Add(this.pInstruct2D);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.bSave2DAutos);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.selectableGraph1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(956, 512);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "2D Hand Selection";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rZoom2D);
            this.groupBox4.Controls.Add(this.rGroupHandSelect2D);
            this.groupBox4.Location = new System.Drawing.Point(784, 164);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(163, 136);
            this.groupBox4.TabIndex = 54;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mouse Mode";
            // 
            // rZoom2D
            // 
            this.rZoom2D.AutoSize = true;
            this.rZoom2D.Location = new System.Drawing.Point(20, 50);
            this.rZoom2D.Name = "rZoom2D";
            this.rZoom2D.Size = new System.Drawing.Size(52, 17);
            this.rZoom2D.TabIndex = 1;
            this.rZoom2D.Text = "Zoom";
            this.rZoom2D.UseVisualStyleBackColor = true;
            this.rZoom2D.CheckedChanged += new System.EventHandler(this.rZoom2D_CheckedChanged);
            // 
            // rGroupHandSelect2D
            // 
            this.rGroupHandSelect2D.AutoSize = true;
            this.rGroupHandSelect2D.Checked = true;
            this.rGroupHandSelect2D.Location = new System.Drawing.Point(20, 27);
            this.rGroupHandSelect2D.Name = "rGroupHandSelect2D";
            this.rGroupHandSelect2D.Size = new System.Drawing.Size(116, 17);
            this.rGroupHandSelect2D.TabIndex = 0;
            this.rGroupHandSelect2D.TabStop = true;
            this.rGroupHandSelect2D.Text = "Group Hand Select";
            this.rGroupHandSelect2D.UseVisualStyleBackColor = true;
            this.rGroupHandSelect2D.CheckedChanged += new System.EventHandler(this.rGroupHandSelect2D_CheckedChanged);
            // 
            // bSave2DAutos
            // 
            this.bSave2DAutos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave2DAutos.Location = new System.Drawing.Point(794, 444);
            this.bSave2DAutos.Name = "bSave2DAutos";
            this.bSave2DAutos.Size = new System.Drawing.Size(107, 32);
            this.bSave2DAutos.TabIndex = 53;
            this.bSave2DAutos.Text = "Save2DAutos";
            this.bSave2DAutos.UseVisualStyleBackColor = true;
            this.bSave2DAutos.Click += new System.EventHandler(this.bSave2DAutos_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.bGMM2D);
            this.groupBox3.Controls.Add(this.kKmeans2D);
            this.groupBox3.Controls.Add(this.nNumberofGroups2D);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(784, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(164, 243);
            this.groupBox3.TabIndex = 52;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Auto Sorts";
            // 
            // bGMM2D
            // 
            this.bGMM2D.Location = new System.Drawing.Point(10, 99);
            this.bGMM2D.Name = "bGMM2D";
            this.bGMM2D.Size = new System.Drawing.Size(125, 32);
            this.bGMM2D.TabIndex = 49;
            this.bGMM2D.Text = "Gaussian Mix";
            this.bGMM2D.UseVisualStyleBackColor = true;
            this.bGMM2D.Click += new System.EventHandler(this.bGMM2D_Click);
            // 
            // kKmeans2D
            // 
            this.kKmeans2D.Location = new System.Drawing.Point(10, 61);
            this.kKmeans2D.Name = "kKmeans2D";
            this.kKmeans2D.Size = new System.Drawing.Size(125, 32);
            this.kKmeans2D.TabIndex = 48;
            this.kKmeans2D.Text = "k Means";
            this.kKmeans2D.UseVisualStyleBackColor = true;
            this.kKmeans2D.Click += new System.EventHandler(this.kKmeans2D_Click);
            // 
            // nNumberofGroups2D
            // 
            this.nNumberofGroups2D.Location = new System.Drawing.Point(10, 35);
            this.nNumberofGroups2D.Name = "nNumberofGroups2D";
            this.nNumberofGroups2D.Size = new System.Drawing.Size(125, 20);
            this.nNumberofGroups2D.TabIndex = 47;
            this.nNumberofGroups2D.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Number of Groups";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.yCross);
            this.groupBox6.Controls.Add(this.ySquare);
            this.groupBox6.Controls.Add(this.ydSquare);
            this.groupBox6.Controls.Add(this.yChi);
            this.groupBox6.Controls.Add(this.yWeighted);
            this.groupBox6.Controls.Add(this.yAbsolute);
            this.groupBox6.Controls.Add(this.yRegression);
            this.groupBox6.Location = new System.Drawing.Point(385, 417);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(376, 92);
            this.groupBox6.TabIndex = 50;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Y Axis";
            // 
            // yCross
            // 
            this.yCross.AutoSize = true;
            this.yCross.Location = new System.Drawing.Point(268, 42);
            this.yCross.Name = "yCross";
            this.yCross.Size = new System.Drawing.Size(104, 17);
            this.yCross.TabIndex = 42;
            this.yCross.Text = "Cross Correlation";
            this.yCross.UseVisualStyleBackColor = true;
            this.yCross.CheckedChanged += new System.EventHandler(this.yCross_CheckedChanged);
            // 
            // ySquare
            // 
            this.ySquare.AutoSize = true;
            this.ySquare.Location = new System.Drawing.Point(6, 19);
            this.ySquare.Name = "ySquare";
            this.ySquare.Size = new System.Drawing.Size(111, 17);
            this.ySquare.TabIndex = 36;
            this.ySquare.Text = "Square Difference";
            this.ySquare.UseVisualStyleBackColor = true;
            this.ySquare.CheckedChanged += new System.EventHandler(this.ySquare_CheckedChanged);
            // 
            // ydSquare
            // 
            this.ydSquare.AutoSize = true;
            this.ydSquare.Location = new System.Drawing.Point(123, 19);
            this.ydSquare.Name = "ydSquare";
            this.ydSquare.Size = new System.Drawing.Size(139, 17);
            this.ydSquare.TabIndex = 37;
            this.ydSquare.Text = "Deriv Square Difference";
            this.ydSquare.UseVisualStyleBackColor = true;
            this.ydSquare.CheckedChanged += new System.EventHandler(this.ydSquare_CheckedChanged);
            // 
            // yChi
            // 
            this.yChi.AutoSize = true;
            this.yChi.Checked = true;
            this.yChi.Location = new System.Drawing.Point(6, 42);
            this.yChi.Name = "yChi";
            this.yChi.Size = new System.Drawing.Size(80, 17);
            this.yChi.TabIndex = 39;
            this.yChi.TabStop = true;
            this.yChi.Text = "ChiSquared";
            this.yChi.UseVisualStyleBackColor = true;
            this.yChi.CheckedChanged += new System.EventHandler(this.yChi_CheckedChanged);
            // 
            // yWeighted
            // 
            this.yWeighted.AutoSize = true;
            this.yWeighted.Location = new System.Drawing.Point(123, 42);
            this.yWeighted.Name = "yWeighted";
            this.yWeighted.Size = new System.Drawing.Size(123, 17);
            this.yWeighted.TabIndex = 41;
            this.yWeighted.Text = "Weighted Difference";
            this.yWeighted.UseVisualStyleBackColor = true;
            this.yWeighted.CheckedChanged += new System.EventHandler(this.yWeighted_CheckedChanged);
            // 
            // yAbsolute
            // 
            this.yAbsolute.AutoSize = true;
            this.yAbsolute.Location = new System.Drawing.Point(6, 65);
            this.yAbsolute.Name = "yAbsolute";
            this.yAbsolute.Size = new System.Drawing.Size(118, 17);
            this.yAbsolute.TabIndex = 38;
            this.yAbsolute.Text = "Absolute Difference";
            this.yAbsolute.UseVisualStyleBackColor = true;
            this.yAbsolute.CheckedChanged += new System.EventHandler(this.yAbsolute_CheckedChanged);
            // 
            // yRegression
            // 
            this.yRegression.AutoSize = true;
            this.yRegression.Location = new System.Drawing.Point(268, 19);
            this.yRegression.Name = "yRegression";
            this.yRegression.Size = new System.Drawing.Size(78, 17);
            this.yRegression.TabIndex = 40;
            this.yRegression.Text = "Regression";
            this.yRegression.UseVisualStyleBackColor = true;
            this.yRegression.CheckedChanged += new System.EventHandler(this.yRegression_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox7.Controls.Add(this.xCross);
            this.groupBox7.Controls.Add(this.xSquared);
            this.groupBox7.Controls.Add(this.xdSquare);
            this.groupBox7.Controls.Add(this.xChi);
            this.groupBox7.Controls.Add(this.xWeighted);
            this.groupBox7.Controls.Add(this.xAbsolute);
            this.groupBox7.Controls.Add(this.xRegression);
            this.groupBox7.Location = new System.Drawing.Point(3, 417);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(376, 92);
            this.groupBox7.TabIndex = 51;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "X Axis";
            // 
            // xCross
            // 
            this.xCross.AutoSize = true;
            this.xCross.Location = new System.Drawing.Point(268, 42);
            this.xCross.Name = "xCross";
            this.xCross.Size = new System.Drawing.Size(104, 17);
            this.xCross.TabIndex = 42;
            this.xCross.TabStop = true;
            this.xCross.Text = "Cross Correlation";
            this.xCross.UseVisualStyleBackColor = true;
            this.xCross.CheckedChanged += new System.EventHandler(this.xCross_CheckedChanged);
            // 
            // xSquared
            // 
            this.xSquared.AutoSize = true;
            this.xSquared.Checked = true;
            this.xSquared.Location = new System.Drawing.Point(6, 19);
            this.xSquared.Name = "xSquared";
            this.xSquared.Size = new System.Drawing.Size(111, 17);
            this.xSquared.TabIndex = 36;
            this.xSquared.TabStop = true;
            this.xSquared.Text = "Square Difference";
            this.xSquared.UseVisualStyleBackColor = true;
            this.xSquared.CheckedChanged += new System.EventHandler(this.xSquared_CheckedChanged);
            // 
            // xdSquare
            // 
            this.xdSquare.AutoSize = true;
            this.xdSquare.Location = new System.Drawing.Point(123, 19);
            this.xdSquare.Name = "xdSquare";
            this.xdSquare.Size = new System.Drawing.Size(139, 17);
            this.xdSquare.TabIndex = 37;
            this.xdSquare.Text = "Deriv Square Difference";
            this.xdSquare.UseVisualStyleBackColor = true;
            this.xdSquare.CheckedChanged += new System.EventHandler(this.xdSquare_CheckedChanged);
            // 
            // xChi
            // 
            this.xChi.AutoSize = true;
            this.xChi.Location = new System.Drawing.Point(6, 42);
            this.xChi.Name = "xChi";
            this.xChi.Size = new System.Drawing.Size(80, 17);
            this.xChi.TabIndex = 39;
            this.xChi.Text = "ChiSquared";
            this.xChi.UseVisualStyleBackColor = true;
            this.xChi.CheckedChanged += new System.EventHandler(this.xChi_CheckedChanged);
            // 
            // xWeighted
            // 
            this.xWeighted.AutoSize = true;
            this.xWeighted.Location = new System.Drawing.Point(123, 42);
            this.xWeighted.Name = "xWeighted";
            this.xWeighted.Size = new System.Drawing.Size(123, 17);
            this.xWeighted.TabIndex = 41;
            this.xWeighted.TabStop = true;
            this.xWeighted.Text = "Weighted Difference";
            this.xWeighted.UseVisualStyleBackColor = true;
            this.xWeighted.CheckedChanged += new System.EventHandler(this.xWeighted_CheckedChanged);
            // 
            // xAbsolute
            // 
            this.xAbsolute.AutoSize = true;
            this.xAbsolute.Location = new System.Drawing.Point(6, 65);
            this.xAbsolute.Name = "xAbsolute";
            this.xAbsolute.Size = new System.Drawing.Size(118, 17);
            this.xAbsolute.TabIndex = 38;
            this.xAbsolute.Text = "Absolute Difference";
            this.xAbsolute.UseVisualStyleBackColor = true;
            this.xAbsolute.CheckedChanged += new System.EventHandler(this.xAbsolute_CheckedChanged);
            // 
            // xRegression
            // 
            this.xRegression.AutoSize = true;
            this.xRegression.Location = new System.Drawing.Point(268, 19);
            this.xRegression.Name = "xRegression";
            this.xRegression.Size = new System.Drawing.Size(78, 17);
            this.xRegression.TabIndex = 40;
            this.xRegression.Text = "Regression";
            this.xRegression.UseVisualStyleBackColor = true;
            this.xRegression.CheckedChanged += new System.EventHandler(this.xRegression_CheckedChanged);
            // 
            // pInstruct2D
            // 
            this.pInstruct2D.Image = ((System.Drawing.Image)(resources.GetObject("pInstruct2D.Image")));
            this.pInstruct2D.Location = new System.Drawing.Point(126, 3);
            this.pInstruct2D.Name = "pInstruct2D";
            this.pInstruct2D.Size = new System.Drawing.Size(569, 427);
            this.pInstruct2D.TabIndex = 55;
            this.pInstruct2D.TabStop = false;
            // 
            // bInstruct2D
            // 
            this.bInstruct2D.Location = new System.Drawing.Point(343, 391);
            this.bInstruct2D.Name = "bInstruct2D";
            this.bInstruct2D.Size = new System.Drawing.Size(128, 20);
            this.bInstruct2D.TabIndex = 56;
            this.bInstruct2D.Text = "Done";
            this.bInstruct2D.UseVisualStyleBackColor = true;
            this.bInstruct2D.Click += new System.EventHandler(this.bInstruct2D_Click);
            // 
            // bCut1D
            // 
            this.bCut1D.Location = new System.Drawing.Point(145, 37);
            this.bCut1D.Name = "bCut1D";
            this.bCut1D.Size = new System.Drawing.Size(113, 22);
            this.bCut1D.TabIndex = 2;
            this.bCut1D.Text = "Cut Groups";
            this.bCut1D.UseVisualStyleBackColor = true;
            this.bCut1D.Click += new System.EventHandler(this.bCut1D_Click);
            // 
            // selectableGraph1
            // 
            this.selectableGraph1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectableGraph1.Location = new System.Drawing.Point(9, 0);
            this.selectableGraph1.Name = "selectableGraph1";
            this.selectableGraph1.Size = new System.Drawing.Size(771, 411);
            this.selectableGraph1.TabIndex = 57;
            // 
            // SingleCurveComp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 556);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Name = "SingleCurveComp";
            this.Text = "SingleCurveComp";
            this.tabControl1.ResumeLayout(false);
            this.CurveSelector.ResumeLayout(false);
            this.CurveSelector.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Autosorts.ResumeLayout(false);
            this.Autosorts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nNum1DGroups)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nNumberofGroups2D)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pInstruct2D)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage CurveSelector;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.HScrollBar hsCurveSelector;
        private System.Windows.Forms.Label label1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rCrossCorrelation;
        public System.Windows.Forms.RadioButton rSquared;
        private System.Windows.Forms.RadioButton rdSquared;
        private System.Windows.Forms.RadioButton rChiSquared;
        private System.Windows.Forms.RadioButton rWeightedDiff;
        private System.Windows.Forms.RadioButton rAbsolute;
        private System.Windows.Forms.RadioButton rRegression;
        private System.Windows.Forms.GroupBox Autosorts;
        private System.Windows.Forms.Button b1DGaussian;
        private System.Windows.Forms.Button b1DkMeans;
        private System.Windows.Forms.NumericUpDown nNum1DGroups;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBCutLines;
        private System.Windows.Forms.Button bSave1DGroups;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton yCross;
        public System.Windows.Forms.RadioButton ySquare;
        private System.Windows.Forms.RadioButton ydSquare;
        private System.Windows.Forms.RadioButton yChi;
        private System.Windows.Forms.RadioButton yWeighted;
        private System.Windows.Forms.RadioButton yAbsolute;
        private System.Windows.Forms.RadioButton yRegression;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton xCross;
        public System.Windows.Forms.RadioButton xSquared;
        private System.Windows.Forms.RadioButton xdSquare;
        private System.Windows.Forms.RadioButton xChi;
        private System.Windows.Forms.RadioButton xWeighted;
        private System.Windows.Forms.RadioButton xAbsolute;
        private System.Windows.Forms.RadioButton xRegression;
        private System.Windows.Forms.Button bSave2DAutos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bGMM2D;
        private System.Windows.Forms.Button kKmeans2D;
        private System.Windows.Forms.NumericUpDown nNumberofGroups2D;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rZoom2D;
        private System.Windows.Forms.RadioButton rGroupHandSelect2D;
        private System.Windows.Forms.Button bInstruct2D;
        private System.Windows.Forms.PictureBox pInstruct2D;
        private SelectableGraph selectableGraph1;
        private System.Windows.Forms.Button bCut1D;
    }
}