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
    public partial class MovieDetailForm : Form
    {
        public MovieDetailForm()
        {
            InitializeComponent();
        }

        string fullLink;
        string str;

        SqlCommand com;
        public Movie movie;

        public void fillGaps(int id)
        {

            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = @"Data Source=DESKTOP-ADUMA73;Initial Catalog=MovieDatabase;Integrated Security=true";
            cnn.Open();
            str = @"select ID, ORIGINAL_TITLE,
                    RUNTIME,
                    (select G.NAME from GENRE as G inner join MOVIE as M ON M.GENRE_ID_1 = G.ID and M.ID = " + id + @") as GENRE_NAME_1,
                    (select G.NAME from GENRE as G inner join MOVIE as M ON M.GENRE_ID_2 = G.ID and M.ID = " + id + @") as GENRE_NAME_2,
                    (select G.NAME from GENRE as G inner join MOVIE as M ON M.GENRE_ID_3 = G.ID and M.ID = " + id + @") as GENRE_NAME_3,
                    RELEASE_DATE,
                    ORIGINAL_LANGUAGE,
                    BUDGET,
                    REVENUE,
                    (select PC.NAME from PRODUCTION_COMPANY as PC inner join MOVIE as M on PC.ID = M.PRODUCTION_COMPANY_ID_1 and M.ID = " + id + @") as PRODUCTION_COMPANY_NAME_1,
                    (select PC.NAME from PRODUCTION_COMPANY as PC inner join MOVIE as M on PC.ID = M.PRODUCTION_COMPANY_ID_2 and M.ID = " + id + @") as PRODUCTION_COMPANY_NAME_2,
                    (select PC.NAME from PRODUCTION_COMPANY as PC inner join MOVIE as M on PC.ID = M.PRODUCTION_COMPANY_ID_3 and M.ID = " + id + @") as PRODUCTION_COMPANY_NAME_3, 
                    (select C.NAME from COUNTRY as C inner join MOVIE as M on C.ID = M.PRODUCTION_COUNTRY_ID_1 and M.ID = " + id + @") as PRODUCTION_COUNTRY_NAME_1,
                    (select C.NAME from COUNTRY as C inner join MOVIE as M on C.ID = M.PRODUCTION_COUNTRY_ID_2 and M.ID = " + id + @") as PRODUCTION_COUNTRY_NAME_2,
                    (select C.NAME from COUNTRY as C inner join MOVIE as M on C.ID = M.PRODUCTION_COUNTRY_ID_3 and M.ID = " + id + @") as PRODUCTION_COUNTRY_NAME_3,
                    IMDB_ID,
                    POPULARITY,
                    OVERVIEW,
                    VOTE_AVERAGE,
                    VOTE_COUNT,
                    POSTER_PATH,
                    HOMEPAGE 
                    from MOVIE as M  where M.ID = " + id;
            com = new SqlCommand(str, cnn);

            SqlDataReader reader = com.ExecuteReader();
            reader.Read();

            movie = new Movie();
            movie.id = Convert.ToInt32(reader["ID"]);
            movie.original_title = reader["ORIGINAL_TITLE"].ToString();

            movie.runtime = Convert.ToInt32(reader["RUNTIME"]);

            movie.genres.Add(new Genre() { name = reader["GENRE_NAME_1"].ToString() });
            movie.genres.Add(new Genre() { name = reader["GENRE_NAME_2"].ToString() });
            movie.genres.Add(new Genre() { name = reader["GENRE_NAME_3"].ToString() });

            movie.release_date = reader["RELEASE_DATE"].ToString();
            movie.original_language = reader["ORIGINAL_LANGUAGE"].ToString();
            movie.budget = Convert.ToInt32(reader["BUDGET"]);
            movie.revenue = Convert.ToInt32(reader["REVENUE"]);

            movie.production_companies.Add(new ProductionCompany() { name = reader["PRODUCTION_COMPANY_NAME_1"].ToString() });
            movie.production_companies.Add(new ProductionCompany() { name = reader["PRODUCTION_COMPANY_NAME_2"].ToString() });
            movie.production_companies.Add(new ProductionCompany() { name = reader["PRODUCTION_COMPANY_NAME_3"].ToString() });

            movie.production_countries.Add(new ProductionCountry() { name = reader["PRODUCTION_COUNTRY_NAME_1"].ToString() });
            movie.production_countries.Add(new ProductionCountry() { name = reader["PRODUCTION_COUNTRY_NAME_2"].ToString() });
            movie.production_countries.Add(new ProductionCountry() { name = reader["PRODUCTION_COUNTRY_NAME_3"].ToString() });

            movie.imdb_id = reader["IMDB_ID"].ToString();
            movie.popularity = Convert.ToDouble(reader["POPULARITY"]);
            movie.overview = reader["OVERVIEW"].ToString();
            movie.vote_average = Convert.ToDouble(reader["VOTE_AVERAGE"]);
            movie.vote_count = Convert.ToInt32(reader["VOTE_COUNT"]);            
            movie.poster_path =  reader["POSTER_PATH"].ToString();
            movie.homepage = reader["HOMEPAGE"].ToString();

            label1.Text = movie.original_title;


            label7.Text = (movie.runtime / 60 + " h" + movie.runtime % 60 + " min | " + movie.genres[0].name + " , " + movie.genres[1].name + " , " + movie.genres[2].name + " | " + movie.release_date.Substring(0, 10)).ToString();


            label2.Text = "Original Language : " + movie.original_language;
            label3.Text = "Production Budget :" + movie.budget + "$";
            label8.Text = "Revenue :" + movie.revenue + "$";
            label9.Text = movie.production_companies[0].name + " , " + movie.production_companies[1].name + " , " + movie.production_companies[2].name;
            label10.Text = "Production Country: " + movie.production_countries[0].name + " , " + movie.production_countries[1].name + " , " + movie.production_countries[2].name;
            fullLink = "http://www.imdb.com/title/" + movie.imdb_id;
            linkLabel1.Text = fullLink.ToString();
            label12.Text = movie.popularity.ToString();
            textBox1.Text = movie.overview.ToString();
            label4.Text = movie.vote_average.ToString();
            label6.Text = movie.vote_count.ToString();                  
            if(movie.poster_path != "" && movie.poster_path != null)
            {
              pictureBox1.Load("https://image.tmdb.org/t/p/w300" + movie.poster_path);
            }
            else
            {
                //pictureBox1.Image = Image.FromFile(@"Images\a.bmp");
                pictureBox1.Image = Properties.Resources.nomovie;
                
            }
            
            


            reader.Close();
            cnn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UserLoginForm user_login = new UserLoginForm(this);
            user_login.ShowDialog();

        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(fullLink);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MovieSearchForm SM = new MovieSearchForm(this);
            SM.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (movie != null)
            {
                MovieEditForm edit = new MovieEditForm(this, this.movie);
                edit.Show();
            }





        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
