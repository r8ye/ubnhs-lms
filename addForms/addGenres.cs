using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ubnhs_lms.addForms
{
    public partial class addGenres: Form
    {
        public addGenres()
        {
            InitializeComponent();
            txtGenre.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string genre = txtGenre.Text.Trim();

            if (string.IsNullOrEmpty(genre))
            {
                MessageBox.Show("Please enter the book's genre");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();


                string checkQuery = "SELECT COUNT(*) FROM genres WHERE GenreName = @GenreName";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@GenreName", genre);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This genre already exists.");
                }
                else
                {
                    string query = "INSERT INTO genres (GenreName) VALUES (@GenreName)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@GenreName", genre);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Genre saved successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGenre.Clear();
                    this.Close();

                    formGenres formGenre = Application.OpenForms["formGenre"] as formGenres;
                    if (formGenre != null)
                    {
                        formGenre.LoadGenres();
                    }
                }
            }

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtGenre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtGenre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtGenre_TextChanged(object sender, EventArgs e)
        {
            txtGenre.MaxLength = 20;
        }
    }
}
