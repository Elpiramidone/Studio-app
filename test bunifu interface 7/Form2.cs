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
using System.IO;
using System.Diagnostics;

namespace test_bunifu_interface
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sorin\OneDrive\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;

        private bool isCollapsed;



        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Filter = "image files(*.png; *.jpg; *jpeg; *gif;)|*.png; *jpg; *jpeg; *gif;";
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportedImage.Image = new Bitmap(FileDialog.FileName);

            }



        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            display_datagrid();
            combo1();
            username.Text = ("Bine ai venit, " + Properties.Settings.Default.UserName);
            load_data();
            LoadData();
            dgridpath.Columns[0].Width = 20;
            dgridpath.Columns[2].Width = 100;

           
        }
        private void LoadData()
        {
            

            using (SqlConnection cn = GetConnection())
            {
                
                string query = "SELECT id,FILENAME, EXTENSION FROM PATH";
                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgridpath.DataSource = dt;

                }
          
            }
        }

        private void combo1()
        {
            string query = "Select STADIU, NAME FROM IMAGE ";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            conn.Open();
            DataSet dt = new DataSet();
            da.Fill(dt, "STADIU");
            filtru1.DisplayMember = "STADIU";
            filtru1.ValueMember = "STADIU";
            filtru1.DataSource = dt.Tables["STADIU"];
            conn.Close();
            if (filtru1.Items.Count>1)
            {
                filtru1.SelectedIndex = -1;
            }

        
             }

        private void display_datagrid()
        {
            SqlCommand quer2 = new SqlCommand("Select id, NAME,IMAGE,DATA,  STADIU FROM IMAGE WHERE STADIU LIKE '%" + filtru1.Text + "'", conn);
            SqlDataAdapter da2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            da2.SelectCommand = quer2;
            dt2.Clear();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }
        ///-------------------------------------------------------------------------------------------modifica jos pentru filtru
/*        private void combo2()
        {
            string query = "Select STADIU, NAME FROM IMAGE ";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            conn.Open();
            DataSet dt = new DataSet();
            da.Fill(dt, "STADIU");
            filtru1.DisplayMember = "STADIU";
            filtru1.ValueMember = "STADIU";
            filtru1.DataSource = dt.Tables["STADIU"];
            conn.Close();
            if (filtru1.Items.Count > 1)
            {
                filtru1.SelectedIndex = -1;
            }


        }

        private void display_datagrid2()
        {
            SqlCommand quer2 = new SqlCommand("Select id, NAME,IMAGE,DATA,  STADIU FROM IMAGE WHERE STADIU LIKE '%" + filtru1.Text + "'", conn);
            SqlDataAdapter da2 = new SqlDataAdapter();
            DataTable dt2 = new DataTable();
            da2.SelectCommand = quer2;
            dt2.Clear();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }*/
        //// ------------------------------------------------------------modifica sus pentru filtru |||Ctrl + Shift + /  sa unccoment
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Escape)
            {
                bunifuButton1.PerformClick();
            }
        }
        private void SaveFile(string filepath)
        {
            using (Stream stream = File.OpenRead(filepath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                string extn = new FileInfo(filepath).Extension;
                string name = new FileInfo(filepath).Name;
                string query = "INSERT INTO PATH(FILENAME, PATH, EXTENSION)VALUES(@FILENAME, @PATH, @EXTENSION)";

                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@PATH", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@EXTENSION", SqlDbType.Char).Value = extn;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }


            }
        }
        private SqlConnection GetConnection()

        {
            return new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Sorin\OneDrive\Documents\Data.mdf;Integrated Security=True;");
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {

            cmd = new SqlCommand("Insert into IMAGE(IMAGE, NAME, DATA, STADIU, POTENTIAL, THE_KEY, PROTOOLS_PROJECT, PRODUS)Values(@IMAGE, @NAME, @DATA, @STADIU, @POTENTIAL, @THE_KEY, @PROTOOLS_PROJECT, @PRODUS)", conn);
            cmd.Parameters.AddWithValue("NAME", txtboxnameimg.Text);

            cmd.Parameters.AddWithValue("DATA", txtboxpret.Value);
            cmd.Parameters.AddWithValue("STADIU", txtboxcantitate.Text);
            cmd.Parameters.AddWithValue("POTENTIAL", txtboxpot.Text);
            cmd.Parameters.AddWithValue("THE_KEY", txtboxkey.Text);
            cmd.Parameters.AddWithValue("PROTOOLS_PROJECT", protools.Text);
        
            cmd.Parameters.AddWithValue("id", txtboxid.Text);
            cmd.Parameters.AddWithValue("PRODUS", txtboxprod.Text);
            MemoryStream memostr = new MemoryStream();
            ImportedImage.Image.Save(memostr, ImportedImage.Image.RawFormat);
            cmd.Parameters.AddWithValue("IMAGE", memostr.ToArray());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Poza incarcata cu succes");

            load_data();
        }
        private void load_data()
        {
            cmd = new SqlCommand("Select * from IMAGE order by NAME", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView2.RowTemplate.Height = 75;
            dataGridView2.DataSource = dt;
            DataGridViewImageColumn pic1 = new DataGridViewImageColumn();
            pic1 = (DataGridViewImageColumn)dataGridView2.Columns[0];
            pic1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView2.Columns[0].Width = 75;
            dataGridView2.Columns[1].Width = 230;
            dataGridView2.Columns[4].Width = 40;
            dataGridView2.Columns[5].Width = 100;
            this.dataGridView2.Columns["POTENTIAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[6].Width = 80;

            int numberOfRecords = dt.Select("STADIU = 'Ready <3'").Length;
            bunifuLabel9.Text = "Piese gata : " + numberOfRecords.ToString();
            int numberOfRecords2 = dt.Select("STADIU = 'INREGISTRATA'").Length;
            bunifuLabel10.Text = "Piese trase : " + numberOfRecords2.ToString();
            int numberOfRecords3 = dt.Select("STADIU = 'DEMO'").Length;
            bunifuLabel15.Text = "Demo-uri : " + numberOfRecords3.ToString();
            int numberOfRecords4 = dt.Select("POTENTIAL = 'DA'").Length;
            bunifuLabel16.Text = "Piese potentiale de album : " + numberOfRecords4.ToString();
            int numberOfRecords5 = dt.Select("STADIU = 'MIX/MASTER PENDING'").Length;
            bunifuLabel17.Text = "Mix/Master pending : " + numberOfRecords5.ToString();


        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {


        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {


            cmd = new SqlCommand("Update IMAGE Set DATA = @DATA ,POTENTIAL = @POTENTIAL, THE_KEY = @THE_KEY, PROTOOLS_PROJECT = @PROTOOLS_PROJECT, PRODUS = @PRODUS, IMAGE = @IMAGE,STADIU = @STADIU, NAME = @NAME Where id = @id", conn);
            cmd.Parameters.AddWithValue("NAME", txtboxnameimg.Text);
            MemoryStream memostr = new MemoryStream();
            ImportedImage.Image.Save(memostr, ImportedImage.Image.RawFormat);
            cmd.Parameters.AddWithValue("IMAGE", memostr.ToArray());
            cmd.Parameters.AddWithValue("DATA", txtboxpret.Value);
            cmd.Parameters.AddWithValue("POTENTIAL", txtboxpot.Text);
            cmd.Parameters.AddWithValue("THE_KEY", txtboxkey.Text);
            cmd.Parameters.AddWithValue("PROTOOLS_PROJECT", protools.Text);
            cmd.Parameters.AddWithValue("PRODUS", txtboxprod.Text);
            cmd.Parameters.AddWithValue("STADIU", txtboxcantitate.Text);
            cmd.Parameters.AddWithValue("id", txtboxid.Text);



            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            load_data();
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            string message = "Esti sigur ca vrei sa stergi asta??";
            string title = "Vrei sa stergi?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                cmd = new SqlCommand("Delete from IMAGE where id = @id", conn);
                cmd.Parameters.AddWithValue("id", txtboxid.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                load_data();
                ImportedImage.Image = null;
                txtboxnameimg.Text = "";
                txtboxcantitate.Text = "";
                txtboxpret.Text = "";
            }
            else
            {
                // Do something  
            }




        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            id1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtboxnameimg.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            numeprj.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtboxpret.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtboxcantitate.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            txtboxid.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            txtboxpot.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            txtboxkey.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            protools.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            txtboxprod.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();

            MemoryStream ms = new MemoryStream((byte[])dataGridView2.CurrentRow.Cells[0].Value);
            ImportedImage.Image = Image.FromStream(ms);
        }

        private void txtboxnameimg_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox currentContainer = ((TextBox)sender);
            int caretPosition = currentContainer.SelectionStart;

            currentContainer.Text = currentContainer.Text.ToUpper();
            currentContainer.SelectionStart = caretPosition++;
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            txtpath.Text = dlg.FileName;

        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            SaveFile(txtpath.Text);
            LoadData();
        }

        private void bunifuButton7_Click_1(object sender, EventArgs e)
        {
            try
            { 

            var selectedRow = dgridpath.SelectedRows;
            foreach (var row in selectedRow)
            {
                int id = (int)((DataGridViewRow)row).Cells[0].Value;
                OpenFile(id);
            }
             }
          catch
            {
                MessageBox.Show("S-a deschis cu succes!");
            }
            }
        private void OpenFile(int id)
        {
            using (SqlConnection cn = GetConnection())
            {
                string query = "SELECT PATH,FILENAME, EXTENSION FROM PATH WHERE id=@id";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cn.Open();
                var reader=cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    var name = reader["FILENAME"].ToString();
                    var path = (byte[])reader["PATH"];
                    var extn = reader["EXTENSION"].ToString();
                    var newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss")) + extn;
                    File.WriteAllBytes(newFileName, path);
                    System.Diagnostics.Process.Start(newFileName);
                }

                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
            }
        }

        private void dgridpath_Click(object sender, EventArgs e)
        {
            txtpath.Text = dgridpath.CurrentRow.Cells[1].Value.ToString();
            txtexst.Text = dgridpath.CurrentRow.Cells[2].Value.ToString();
            txtboxidpath.Text = dgridpath.CurrentRow.Cells[0].Value.ToString();
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void bunifuButton11_Click(object sender, EventArgs e)
        {
            string message = "Esti sigur ca vrei sa stergi asta??";
            string title = "Vrei sa stergi?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                cmd = new SqlCommand("Delete from PATH where id = @id", conn);
                cmd.Parameters.AddWithValue("id", txtboxidpath.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
             
            }
            else
            {
                // Do something  
            }
        }

        private void bunifuButton12_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuDropdown1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display_datagrid();
        }

        private void search1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void bunifuButton13_Click(object sender, EventArgs e)
        {
            load_data();
        }

    

        private void filtru2_KeyPress(object sender, KeyPressEventArgs e)
        {
       
        }

        private void bunifuLabel9_Click(object sender, EventArgs e)
        {

        }

        private void txtboxkey_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtboxnameimg_TextChanged(object sender, EventArgs e)
        {

        }

        private void protools_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox currentContainer = ((TextBox)sender);
            int caretPosition = currentContainer.SelectionStart;

            currentContainer.Text = currentContainer.Text.ToUpper();
            currentContainer.SelectionStart = caretPosition++;
        }

        private void configurareToolStripMenuItem_Click(object sender, EventArgs e)
        {
         

        }

        private void configurareToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();

        }

        private void bunifuButton14_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Piese != String.Empty)
            {
                bunifuLabel18.Text = Properties.Settings.Default.Piese;

                Process.Start("explorer.exe", bunifuLabel18.Text);
            }
        }

        private void bunifuLabel18_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton12_Click_1(object sender, EventArgs e)
        {
            try
            {


                if (Properties.Settings.Default.Proiecte != String.Empty)
                {
                    label1.Text = Properties.Settings.Default.Proiecte;

                    Process.Start("explorer.exe", label1.Text);
                }
            }
            catch
                {
                MessageBox.Show("S-a deschis cu succes!");
            }
        }

        private void bunifuButton16_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Beat != String.Empty)
            {
                label2.Text = Properties.Settings.Default.Beat;

                Process.Start("explorer.exe", label2.Text);
            }
        }
    }
}



