namespace GroupCurves
{
    partial class SelectableGraph
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LX2 = new System.Windows.Forms.Label();
            this.LX0 = new System.Windows.Forms.Label();
            this.LX1 = new System.Windows.Forms.Label();
            this.LY2 = new System.Windows.Forms.Label();
            this.LY1 = new System.Windows.Forms.Label();
            this.LY0 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(44, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(794, 433);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // LX2
            // 
            this.LX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LX2.AutoSize = true;
            this.LX2.Location = new System.Drawing.Point(803, 436);
            this.LX2.Name = "LX2";
            this.LX2.Size = new System.Drawing.Size(35, 13);
            this.LX2.TabIndex = 1;
            this.LX2.Text = "label1";
            // 
            // LX0
            // 
            this.LX0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LX0.AutoSize = true;
            this.LX0.Location = new System.Drawing.Point(41, 436);
            this.LX0.Name = "LX0";
            this.LX0.Size = new System.Drawing.Size(35, 13);
            this.LX0.TabIndex = 2;
            this.LX0.Text = "label2";
            // 
            // LX1
            // 
            this.LX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LX1.AutoSize = true;
            this.LX1.Location = new System.Drawing.Point(491, 436);
            this.LX1.Name = "LX1";
            this.LX1.Size = new System.Drawing.Size(35, 13);
            this.LX1.TabIndex = 3;
            this.LX1.Text = "label3";
            // 
            // LY2
            // 
            this.LY2.AutoSize = true;
            this.LY2.Location = new System.Drawing.Point(3, 0);
            this.LY2.Name = "LY2";
            this.LY2.Size = new System.Drawing.Size(35, 13);
            this.LY2.TabIndex = 4;
            this.LY2.Text = "label1";
            // 
            // LY1
            // 
            this.LY1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LY1.AutoSize = true;
            this.LY1.Location = new System.Drawing.Point(3, 189);
            this.LY1.Name = "LY1";
            this.LY1.Size = new System.Drawing.Size(35, 13);
            this.LY1.TabIndex = 5;
            this.LY1.Text = "label2";
            // 
            // LY0
            // 
            this.LY0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LY0.AutoSize = true;
            this.LY0.Location = new System.Drawing.Point(3, 420);
            this.LY0.Name = "LY0";
            this.LY0.Size = new System.Drawing.Size(35, 13);
            this.LY0.TabIndex = 6;
            this.LY0.Text = "label3";
            // 
            // SelectableGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LY0);
            this.Controls.Add(this.LY1);
            this.Controls.Add(this.LY2);
            this.Controls.Add(this.LX1);
            this.Controls.Add(this.LX0);
            this.Controls.Add(this.LX2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "SelectableGraph";
            this.Size = new System.Drawing.Size(841, 449);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SelectableGraph_KeyUp);
            this.Resize += new System.EventHandler(this.SelectableGraph_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LX2;
        private System.Windows.Forms.Label LX0;
        private System.Windows.Forms.Label LX1;
        private System.Windows.Forms.Label LY2;
        private System.Windows.Forms.Label LY1;
        private System.Windows.Forms.Label LY0;
    }
}
