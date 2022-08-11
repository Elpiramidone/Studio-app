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
    public partial class Form4 : Form
    {
        SqlDataAdapter sda;
        SqlCommandBuilder scb;
        DataTable dt;
        public Form4()
        {
            InitializeComponent();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sorin\OneDrive\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
            sda = new SqlDataAdapter(@"SELECT USERNAME, PASSWORD FROM LOGIN", con);
            dt = new DataTable();
            sda.Fill(dt);
            bunifuDataGridView1.DataSource = dt;
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            scb = new SqlCommandBuilder(sda);
            sda.Update(dt);
        }
    }
}
