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
    public partial class MovieEditForm : Form
    {
        Movie movie;
        public MovieEditForm()
        {
            InitializeComponent();
        }
        private MovieDetailForm anaform = null;
        public MovieEditForm(Form callingForm,Movie movie)
        {
            anaform = callingForm as MovieDetailForm;
            this.movie = movie;
            InitializeComponent();
        }

        
        private void FillGaps()
        {
            int ID = movie.id;
           
            textBox1.Text = movie.original_title;
            comboBox1.SelectedItem = movie.genres[0].name;
            comboBox2.SelectedItem = movie.genres[1].name;
            comboBox3.SelectedItem = movie.genres[2].name;
            textBox2.Text = movie.budget.ToString();
            textBox3.Text = movie.revenue.ToString();
            comboBox4.SelectedItem = movie.production_companies[0].name;
            comboBox5.SelectedItem = movie.production_companies[1].name;
            comboBox6.SelectedItem = movie.production_companies[2].name;
            textBox4.Text = movie.overview;
        }
        private int UpdateMovie()
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-ADUMA73;Initial Catalog=MovieDatabase;Integrated Security=true";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandText = @"update MOVIE set ORIGINAL_TITLE = @ORIGINAL_TITLE,
                                GENRE_ID_1 = (select ID from GENRE where NAME = '" + comboBox1.SelectedItem + @"') , 
                                GENRE_ID_2 = (select ID from GENRE where NAME = '" + comboBox2.SelectedItem + @"') ,
                                GENRE_ID_3 = (select ID from GENRE where NAME = '" + comboBox3.SelectedItem + @"') ,                                
                                BUDGET = @BUDGET,
                                REVENUE = @REVENUE,
                                PRODUCTION_COMPANY_ID_1 =  (select ID from PRODUCTION_COMPANY where NAME = '" + comboBox4.SelectedItem + @"'),  
                                PRODUCTION_COMPANY_ID_2 = (select ID from PRODUCTION_COMPANY where NAME = '" + comboBox5.SelectedItem + @"'), 
                                PRODUCTION_COMPANY_ID_3 = (select ID from PRODUCTION_COMPANY where NAME = '" + comboBox6.SelectedItem + @"'), 
                                OVERVIEW = @OVERVIEW
                                where ID = @ID";

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@ORIGINAL_TITLE", SqlDbType.NVarChar, 50);
           
            cmd.Parameters.Add("@BUDGET", SqlDbType.Money);
            cmd.Parameters.Add("@REVENUE", SqlDbType.Money);
           
            cmd.Parameters.Add("@OVERVIEW", SqlDbType.NVarChar, 1000);


            cmd.Parameters["@ID"].Value = movie.id;
            cmd.Parameters["@ORIGINAL_TITLE"].Value = textBox1.Text;
            //if (comboBox1.SelectedValue == "") { cmd.Parameters["@GENRE_ID_1"].Value = "NULL"; }
            //else { cmd.Parameters["@GENRE_ID_1"].Value = comboBox1.SelectedValue; }
            //if (comboBox2.SelectedValue == "") { cmd.Parameters["@GENRE_ID_2"].Value = "NULL"; }
            //else { cmd.Parameters["@GENRE_ID_2"].Value = comboBox2.SelectedValue; }
            //if (comboBox3.SelectedValue == "") { cmd.Parameters["@GENRE_ID_3"].Value = "NULL"; }
            //else { cmd.Parameters["@GENRE_ID_3"].Value = comboBox3.SelectedValue; }

           
            cmd.Parameters["@BUDGET"].Value = textBox2.Text;
            cmd.Parameters["@REVENUE"].Value = textBox3.Text;

            //if (comboBox4.SelectedValue == "") { cmd.Parameters["@PRODUCTION_COMPANY_ID_1"].Value = "NULL"; }
            //else { cmd.Parameters["@PRODUCTION_COMPANY_ID_1"].Value = comboBox4.SelectedValue; }
            //if (comboBox5.SelectedValue == "") { cmd.Parameters["@PRODUCTION_COMPANY_ID_2"].Value = "NULL"; }
            //else { cmd.Parameters["@PRODUCTION_COMPANY_ID_2"].Value = comboBox5.SelectedValue; }
            //if (comboBox6.SelectedValue == "") { cmd.Parameters["@PRODUCTION_COMPANY_ID_3"].Value = "NULL"; }
            //else { cmd.Parameters["@PRODUCTION_COMPANY_ID_3"].Value = comboBox6.SelectedValue; }
            
            cmd.Parameters["@OVERVIEW"].Value = textBox4.Text;

            


            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            int a = Convert.ToInt32(cmd.ExecuteNonQuery().ToString());

            if (cnn.State == ConnectionState.Open)
                cnn.Close();
            return a;
        }



        private void EditMovie_Load(object sender, EventArgs e)
        {
            FillGaps();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateMovie();
           
            this.anaform.fillGaps(movie.id);
            this.Close();
        }
        
    }
}
