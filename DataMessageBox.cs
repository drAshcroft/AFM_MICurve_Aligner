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
    public partial class DataMessageBox : Form
    {
        public DataMessageBox()
        {
            InitializeComponent();
        }

        public void DataMessage(string Message)
        {
            richTextBox1.Text = Message;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
