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
    public partial class MovieSearchForm : Form
    {

        public UserLoginForm userLogin;
        DataTable dtBirimler = new DataTable();
        SqlConnection cnn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public MovieSearchForm()
        {
            InitializeComponent();
        }

        private MovieDetailForm mainForm = null;
        public MovieSearchForm(Form callingForm)
        {
            mainForm = callingForm as MovieDetailForm;
            InitializeComponent();
        }


        
        private void button1_Click(object sender, EventArgs e)
        {
            int numRows = 0;
            cnn.ConnectionString = @"Data Source=DESKTOP-ADUMA73;Initial Catalog=MovieDatabase;Integrated Security=true";

          
            cmd.Connection = cnn;
            cmd.CommandText = @"select ORIGINAL_TITLE ,count(ORIGINAL_TITLE) from MOVIE where ORIGINAL_TITLE like '%"+ textBox1.Text+ "%'group by ORIGINAL_TITLE";
           
            da.SelectCommand = cmd;
           
            da.Fill(dtBirimler);
            numRows = dtBirimler.Rows.Count;

            comboBox1.Items.Clear();
            
            for (int i = 0; i < numRows; i++)
            {
                label2.Text = "Found "+ numRows+" Different Movies";
                comboBox1.Items.Add(dtBirimler.Rows[0]["ORIGINAL_TITLE"].ToString());
            }
            label2.Visible = true;
            dtBirimler.Clear();

        }

        private void SearchMovie_Load(object sender, EventArgs e)
        {               
           // .frm.Hide();
        }           

        private void button2_Click(object sender, EventArgs e)
        {
            string str;
            SqlCommand com;
            cnn.ConnectionString = @"Data Source=DESKTOP-ADUMA73;Initial Catalog=MovieDatabase;Integrated Security=true";
            cnn.Open();
            str = @"select ID from MOVIE where ORIGINAL_TITLE ='" + comboBox1.SelectedItem + "'";                
            com = new SqlCommand(str, cnn);

            SqlDataReader reader = com.ExecuteReader();
            reader.Read();
            
            mainForm.fillGaps(Convert.ToInt32(reader["ID"].ToString()));
            this.Hide();
            mainForm.Show();

            
        }
    }
}
