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
    public partial class GroupName : Form
    {
        public GroupName()
        {
            InitializeComponent();
        }
        public string Groupname = "";
        private void button1_Click(object sender, EventArgs e)
        {
            Groupname = textBox1.Text;
            this.Close();
        }
    }
}
