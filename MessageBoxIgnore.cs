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
    public partial class MessageBoxIgnore : Form
    {
        public MessageBoxIgnore()
        {
            InitializeComponent();
        }
        public MessageBoxIgnore(string Text)
        {
            InitializeComponent();
            label1.Text = Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
