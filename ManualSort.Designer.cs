namespace GroupCurves
{
    partial class ManualSort
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
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.bTrash = new System.Windows.Forms.Button();
            this.bSkip = new System.Windows.Forms.Button();
            this.lbGroupFiles = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lGroupNames = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bHint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(664, 533);
            this.zedGraphControl1.TabIndex = 0;
            this.zedGraphControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.zedGraphControl1_KeyUp);
            // 
            // bTrash
            // 
            this.bTrash.AllowDrop = true;
            this.bTrash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bTrash.Location = new System.Drawing.Point(811, 252);
            this.bTrash.Name = "bTrash";
            this.bTrash.Size = new System.Drawing.Size(79, 22);
            this.bTrash.TabIndex = 1;
            this.bTrash.Text = "Trash";
            this.bTrash.UseVisualStyleBackColor = true;
            this.bTrash.Click += new System.EventHandler(this.bTrash_Click);
            this.bTrash.DragDrop += new System.Windows.Forms.DragEventHandler(this.bTrash_DragDrop);
            this.bTrash.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bTrash_KeyUp);
            // 
            // bSkip
            // 
            this.bSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSkip.Location = new System.Drawing.Point(670, 252);
            this.bSkip.Name = "bSkip";
            this.bSkip.Size = new System.Drawing.Size(79, 22);
            this.bSkip.TabIndex = 5;
            this.bSkip.Text = "Skip";
            this.bSkip.UseVisualStyleBackColor = true;
            this.bSkip.Click += new System.EventHandler(this.bSkip_Click);
            this.bSkip.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bSkip_KeyUp);
            // 
            // lbGroupFiles
            // 
            this.lbGroupFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroupFiles.FormattingEnabled = true;
            this.lbGroupFiles.Location = new System.Drawing.Point(670, 280);
            this.lbGroupFiles.Name = "lbGroupFiles";
            this.lbGroupFiles.Size = new System.Drawing.Size(225, 225);
            this.lbGroupFiles.TabIndex = 6;
            this.lbGroupFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbGroupFiles_KeyUp);
            this.lbGroupFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbGroupFiles_MouseUp);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(832, 511);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 22);
            this.button1.TabIndex = 7;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.button1_KeyUp);
            // 
            // lGroupNames
            // 
            this.lGroupNames.AllowDrop = true;
            this.lGroupNames.FormattingEnabled = true;
            this.lGroupNames.Location = new System.Drawing.Point(670, 4);
            this.lGroupNames.Name = "lGroupNames";
            this.lGroupNames.Size = new System.Drawing.Size(157, 238);
            this.lGroupNames.TabIndex = 8;
            this.lGroupNames.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox1_DragDrop);
            this.lGroupNames.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lGroupNames_KeyUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(833, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 42);
            this.button2.TabIndex = 9;
            this.button2.Text = "Create Group";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.button2_KeyUp);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(337, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 252);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // bHint
            // 
            this.bHint.Location = new System.Drawing.Point(485, 356);
            this.bHint.Name = "bHint";
            this.bHint.Size = new System.Drawing.Size(96, 23);
            this.bHint.TabIndex = 11;
            this.bHint.Text = "Close Hint";
            this.bHint.UseVisualStyleBackColor = true;
            this.bHint.Click += new System.EventHandler(this.bHint_Click);
            // 
            // ManualSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 536);
            this.Controls.Add(this.bHint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lGroupNames);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbGroupFiles);
            this.Controls.Add(this.bSkip);
            this.Controls.Add(this.bTrash);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "ManualSort";
            this.Text = "ManualSort";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ManualSort_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button bTrash;
        private System.Windows.Forms.Button bSkip;
        private System.Windows.Forms.ListBox lbGroupFiles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lGroupNames;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bHint;
    }
}