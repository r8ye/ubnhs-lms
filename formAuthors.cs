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
    public partial class formAuthors : Form
    {
        public formAuthors()
        {
            InitializeComponent();
            LoadAuthors();

            foreach (DataGridViewColumn column in dgvAuthors.Columns)
            {
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void LoadAuthors()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT AuthorID, AuthorName FROM authors";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvAuthors.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvAuthors.Rows.Add(row["AuthorID"], row["AuthorName"]);
                }
                txtSearch.Clear();
            }
        }

        public void SearchAuthors(string searchText)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT AuthorID, AuthorName FROM authors WHERE AuthorName LIKE @SearchText";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvAuthors.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvAuthors.Rows.Add(row["AuthorID"], row["AuthorName"]);
                }
            }
        }


        private void formAuthors_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvAuthors, lblTotalCount);
        }

        private void dgvAuthors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int authorID = Convert.ToInt32(dgvAuthors.Rows[e.RowIndex].Cells["AuthorID"].Value);
                string authorName = dgvAuthors.Rows[e.RowIndex].Cells["AuthorName"].Value.ToString();

                if (dgvAuthors.Columns[e.ColumnIndex].Name == "Update")
                {
                    updAuthors updateAuthors = new updAuthors(authorID, authorName);
                    updateAuthors.FormClosed += (s, args) => LoadAuthors();
                    updateAuthors.ShowDialog();
                }
                else if (dgvAuthors.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this author?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteAuthor(authorID);
                    }
                }
            }

        }

        private void DeleteAuthor(int authorID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM books WHERE AuthorID = @AuthorID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@AuthorID", authorID);
                int bookCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (bookCount > 0)
                {
                    MessageBox.Show("Cannot delete author with associated books. Remove the books first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM authors WHERE AuthorID = @AuthorID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AuthorID", authorID);
                cmd.ExecuteNonQuery();
            }
            LoadAuthors();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addAuthors addAuthor = new addAuthors();
            addAuthor.FormClosed += (s, args) => LoadAuthors();
            addAuthor.ShowDialog();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.')
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
            string searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadAuthors();
            }
            else
            {
                SearchAuthors(searchText);
            }

            txtSearch.MaxLength = 30;
        }
    }
}
