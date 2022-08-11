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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sorin\OneDrive\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From PAROLA where PASSWORD ='" + bunifuTextBox1.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {

                this.Hide();

                Form4 ss = new Form4();
                ss.Show();
            }
            else
            {
                MessageBox.Show("parola gresita");
            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            bunifuTextBox1.UseSystemPasswordChar = true;
        }
    }
}
