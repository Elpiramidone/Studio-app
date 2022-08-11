using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;

namespace test_bunifu_interface
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }



        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtpath.Text = dialog.FileName;
            }


        }

        private void Form5_Load(object sender, EventArgs e)
        {
            remember.Checked = true;
            if (Properties.Settings.Default.Piese != String.Empty)
            {
                txtpath.Text = Properties.Settings.Default.Piese;

            
            }

            remember2.Checked = true;
            if (Properties.Settings.Default.Proiecte != String.Empty)
            {
                txtpath2.Text = Properties.Settings.Default.Proiecte;


            }
            remember3.Checked = true;
            if (Properties.Settings.Default.Beat != String.Empty)
            {
                txtpath3.Text = Properties.Settings.Default.Beat;


            }

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (remember.Checked == true)
            {
                Properties.Settings.Default.Piese = txtpath.Text;
                Properties.Settings.Default.Save();
            }
            if (remember.Checked == false)
            {
                Properties.Settings.Default.Piese = "";
                Properties.Settings.Default.Save();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (remember2.Checked == true)
            {
                Properties.Settings.Default.Proiecte = txtpath2.Text;
                Properties.Settings.Default.Save();
            }
            if (remember2.Checked == false)
            {
                Properties.Settings.Default.Proiecte = "";
                Properties.Settings.Default.Save();
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtpath2.Text = dialog.FileName;
            }
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if (remember3.Checked == true)
            {
                Properties.Settings.Default.Beat = txtpath3.Text;
                Properties.Settings.Default.Save();
            }
            if (remember3.Checked == false)
            {
                Properties.Settings.Default.Beat = "";
                Properties.Settings.Default.Save();
            }
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtpath3.Text = dialog.FileName;
            }

        }
    }
}
