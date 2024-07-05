using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GroupCurves
{
    public partial class ProgressReport : Form
    {
        public ProgressReport()
        {
            InitializeComponent();
        }
        public string FormCaption
        {
            get { return label1.Text; }
            set { label1.Text = value; }

        }
    }
}
