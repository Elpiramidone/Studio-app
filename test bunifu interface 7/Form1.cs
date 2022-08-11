using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test_bunifu_interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            remember.Checked = true;
            if (Properties.Settings.Default.UserName != String.Empty)
            {
                bunifuTextBox1.Text = Properties.Settings.Default.UserName;
                bunifuTextBox2.Text = Properties.Settings.Default.Password;

            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            if (remember.Checked == true)
            {
                Properties.Settings.Default.UserName = bunifuTextBox1.Text;
                Properties.Settings.Default.Password = bunifuTextBox2.Text;
                Properties.Settings.Default.Save();
            }
            if (remember.Checked == false)
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();

            }

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sorin\OneDrive\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From LOGIN where Username ='" + bunifuTextBox1.Text + "'and password ='" + bunifuTextBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {

                this.Hide();

                Form2 ss = new Form2();
                ss.Show();
            }
            else
            {
                MessageBox.Show("username sau parola gresita");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void remember_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            bunifuTextBox2.UseSystemPasswordChar = true;
        }
    }
}
