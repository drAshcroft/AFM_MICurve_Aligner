using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GroupCurves
{
    public partial class ImportCurves : Form
    {
        private string FilesPath="";
        private Form1 Main;
        public ImportCurves( Form1 MainForm)
        {
            InitializeComponent();
            Main = MainForm;

            Application.DoEvents();
            GetFiles();
        }

        private void GetFiles()
        {
           
            folderBrowserDialog1.ShowDialog(this);
            if (folderBrowserDialog1.SelectedPath == "")
            {
                this.Close();
                return;
            }
            string[] files=   Directory.GetFiles(folderBrowserDialog1.SelectedPath);
            List<string> FormatedFiles = new List<string>();
            foreach (string s in files)
            {
                FormatedFiles.Add(Path.GetFileName(s));
            }

            listBox1.Items.AddRange(FormatedFiles.ToArray() );
            FilesPath = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FilesPath == "")
            {
                MessageBox.Show("Please use the find files button to select your data");
                return;
            }
            CurveSection DesiredCurve;
            CurveSection FlattenCurve;
            if (radioButton1.Checked==true ) 
                DesiredCurve=CurveSection.Approach;
            else 
                DesiredCurve=CurveSection.Withdrawl;

            if (radioButton4.Checked==true )
                FlattenCurve=CurveSection.Approach ;
            else 
                FlattenCurve=CurveSection.Withdrawl;
            int PolyOrder=0;
            if (radioButton5.Checked == true)
                PolyOrder=0;
            if (radioButton6.Checked == true)
                PolyOrder=1;
            if (radioButton7.Checked == true)
                PolyOrder=2;

            Main.ConvertAFMtoCSB(FilesPath,progressBar1,DesiredCurve,FlattenCurve,PolyOrder,false  );
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FilesPath == "")
            {
                MessageBox.Show("Please use the find files button to select your data");
                return;
            }
            CurveSection DesiredCurve;
            CurveSection FlattenCurve;
            if (radioButton1.Checked == true)
                DesiredCurve = CurveSection.Approach;
            else
                DesiredCurve = CurveSection.Withdrawl;

            if (radioButton4.Checked == true)
                FlattenCurve = CurveSection.Approach;
            else
                FlattenCurve = CurveSection.Withdrawl;
            int PolyOrder = 0;
            if (radioButton5.Checked == true)
                PolyOrder = 0;
            if (radioButton6.Checked == true)
                PolyOrder = 1;
            if (radioButton7.Checked == true)
                PolyOrder = 2;

            Main.ConvertAFMtoCSB(FilesPath, progressBar1, DesiredCurve, FlattenCurve, PolyOrder,true );

        /*    Main.button3_Click_2(this, EventArgs.Empty);
            return;*/

            double[][] ConfigurationEnergies = new double[5][];

            Main.ChainSort();
            ConfigurationEnergies[0] = RunRelaxations();
            Main.ReturnAllTo1Group ();

            Main.HistoSort();
            ConfigurationEnergies[1] = RunRelaxations();
            Main.ReturnAllTo1Group();

            Main.BifurificationSort();
            ConfigurationEnergies[2] = RunRelaxations();
            Main.ReturnAllTo1Group();

            Main.ShakeSort();
            ConfigurationEnergies[3] = RunRelaxations();
            Main.ReturnAllTo1Group();

            int BestSort = 0 ;
            int BestRelax = 0;
            double MinEnergy=double.MaxValue ;
            for (int i = 0; i < ConfigurationEnergies.GetLength(0); i++)
            {
                if (ConfigurationEnergies[i] != null)
                {
                    for (int j = 0; j < ConfigurationEnergies[i].GetLength(0); j++)
                    {
                        if (ConfigurationEnergies[i][j] < MinEnergy)
                        {
                            MinEnergy = ConfigurationEnergies[i][j];
                            BestSort = i;
                            BestRelax = j;
                        }
                    }
                }
            }
            switch (BestSort )
            {
                case 0:
                    Main.ChainSort();
                    break;
                case 1:
                    Main.HistoSort();
                    break;
                case 2:
                    Main.BifurificationSort();
                    break;
                case 3:
                    Main.ShakeSort();
                    break;
            }
            switch (BestRelax )
            {
                case 0:
                    break;
                case 1:
                    Main.Shake ();
                    break;
                case 2:
                    Main.Shake();
                    Main.MontoCarlo();
                    Main.MontoCarlo();
                    Main.Shake();
                    break;
            }

            this.Hide();
        }
        private double[] RunRelaxations()
        {
            double[] energies = new double[3];
            energies[0] = Main.GetWholeInteractionEnergy();
            Main.Shake();
            energies[1] = Main.GetWholeInteractionEnergy();
            Main.MontoCarlo();
            Main.MontoCarlo();
            Main.Shake();
            energies[2] = Main.GetWholeInteractionEnergy();
            return energies;
        }
    }
}
