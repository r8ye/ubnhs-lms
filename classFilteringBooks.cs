using copyyyy_lms;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ubnhs_lms
{
    internal class classFilteringBooks
    {
  
        public static void LoadFilters(ComboBox cmbFilterGenre)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

              
                DataTable dtGenre = new DataTable();
                dtGenre.Columns.Add("GenreName", typeof(string));
                dtGenre.Rows.Add("All Genre");

                using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT GenreName FROM genres ORDER BY GenreName", conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dtGenre.Rows.Add(reader["GenreName"].ToString());
                    }
                }

                cmbFilterGenre.DataSource = dtGenre;
                cmbFilterGenre.DisplayMember = "GenreName";
                cmbFilterGenre.SelectedIndex = 0;
            }
        }

        
        public static void FilterBooks(Guna2DataGridView dgvBooks, string genre)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

              
                string query = @"SELECT 
                    b.BookID, 
                    b.Title, 
                    b.ISBN,
                    b.Description, 
                    b.AuthorID, a.AuthorName, 
                    b.GenreID, g.GenreName, 
                    b.PublisherID, p.PublisherName, 
                    b.YearPublished
                FROM books b
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                WHERE 1=1";

                
                if (!string.IsNullOrEmpty(genre) && genre != "All Genre")
                {
                    query += " AND g.GenreName = @Genre";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(genre) && genre != "All Genre")
                    {
                        cmd.Parameters.AddWithValue("@Genre", genre);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBooks.Rows.Clear();

                  
                    foreach (DataRow row in dt.Rows)
                    {
                        dgvBooks.Rows.Add(
                            row["BookID"],
                            row["Title"],
                            row["ISBN"],
                            row["Description"],
                            row["AuthorName"],
                            row["GenreName"],
                            row["PublisherName"],
                            row["YearPublished"],
                            row["AuthorID"],
                            row["GenreID"],
                            row["PublisherID"]);
                    }
                }
            }
        }
    }
}
