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
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;

namespace ubnhs_lms
{
    public partial class formGenres : Form
    {
        public formGenres()
        {
            InitializeComponent();
            LoadGenres();
        }

        public void LoadGenres()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT GenreID, GenreName FROM genres";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvGenres.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvGenres.Rows.Add(row["GenreID"], row["GenreName"]);
                }
                txtSearch.Clear();
            }
        }

        public void SearchGenres(string searchQuery)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT GenreID, GenreName FROM genres WHERE GenreName LIKE @SearchQuery";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvGenres.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvGenres.Rows.Add(row["GenreID"], row["GenreName"]);
                }
            }
        }


        private void formGenres_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvGenres, lblTotalCount);
        }

        private void DeleteGenre(int genreID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM books WHERE GenreID = @GenreID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@GenreID", genreID);
                int bookCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (bookCount > 0)
                {
                    MessageBox.Show("Cannot delete genre with associated books. Remove the books first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM genres WHERE GenreID = @GenreID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GenreID", genreID);
                cmd.ExecuteNonQuery();
            }
            LoadGenres();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addGenres addGenre = new addGenres();
            addGenre.FormClosed += (s, args) => LoadGenres();
            addGenre.ShowDialog();
        }

        private void dgvGenres_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int genreID = Convert.ToInt32(dgvGenres.Rows[e.RowIndex].Cells["GenreID"].Value);
                string genreName = dgvGenres.Rows[e.RowIndex].Cells["GenreName"].Value.ToString();

                if (dgvGenres.Columns[e.ColumnIndex].Name == "Update")
                {
                    updGenres updateGenres = new updGenres(genreID, genreName);
                    updateGenres.FormClosed += (s, args) => LoadGenres();
                    updateGenres.ShowDialog();
                }
                else if (dgvGenres.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this genre?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteGenre(genreID);
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                SearchGenres(searchQuery);
            }
            else
            {
                LoadGenres();
            }

            txtSearch.MaxLength = 20;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
