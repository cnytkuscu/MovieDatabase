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
using System.Data.Sql;
using System.Data.SqlClient;

namespace MovieDatabase
{
    public partial class UserLoginForm : Form
    {
        MovieDetailForm frm;
        public UserLoginForm()
        {
            InitializeComponent();
        }
        public UserLoginForm(MovieDetailForm frm)
        {
            InitializeComponent();
            this.frm = frm;
        }


        private void login()
        {    
            SqlConnection cnn = new SqlConnection();
            //cnn.ConnectionString = @"Data Source=DESKTOP-ADUMA73;Initial Catalog=MovieDatabase;Integrated Security=true";
            cnn.ConnectionString = @"server=.\\mssqlserver;Data Source=DESKTOP-50DANOA;user id=necati;password=123456;Initial Catalog=MovieDatabase;Integrated Security=true";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = "select USER_ID from USERINFO   where USER_NAME = '" + textBox1.Text + "' and USER_PASSWORD = '" + textBox2.Text + "'";

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;

            int value = dataGridView1.RowCount;
            if (value == 0)
            {
                MessageBox.Show("You've entered wrong ID or Password. Please re-enter.");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            else
            {
                frm.button1.Enabled = true;
                frm.button2.Enabled=true;
                frm.Focus();
                this.Close();
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            login();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm.Close();
        }

        private void UserLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void UserLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }
    }
}
