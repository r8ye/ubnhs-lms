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

namespace ubnhs_lms.updateForms
{
    public partial class updGenres: Form
    {
        private int genreID;
        public updGenres(int id, string name)
        {
            InitializeComponent();
            genreID = id;
            txtGenre.Text = name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newGenreName = txtGenre.Text.Trim();

            if (string.IsNullOrWhiteSpace(newGenreName))
            {
                MessageBox.Show("Genre name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM genres WHERE GenreName = @GenreName AND GenreID != @GenreID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@GenreName", newGenreName);
                checkCmd.Parameters.AddWithValue("@GenreID", genreID);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Genre name already exists. Please enter a different name.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "UPDATE genres SET GenreName = @GenreName WHERE GenreID = @GenreID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GenreName", newGenreName);
                cmd.Parameters.AddWithValue("@GenreID", genreID);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Genre updated successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
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
