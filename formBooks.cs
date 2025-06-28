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
    public partial class formBooks : Form
    {
        
        public formBooks()
        {
            InitializeComponent();
            classFilteringBooks.LoadFilters(cmbFilterGenre);
        }

        private void formBooks_Load(object sender, EventArgs e)
        {
            LoadBooks();
            new classTotalCount(dgvBooks, lblTotalCount);

            cmbFilterAuthor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterAuthor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterAuthor.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterGenre.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterGenre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterGenre.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterPublisher.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterPublisher.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterPublisher.AutoCompleteSource = AutoCompleteSource.ListItems;

            
        }

        public void LoadBooks()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                    b.BookID, 
                    b.ISBN, 
                    b.Description,
                    b.Title, 
                    b.AuthorID, a.AuthorName, 
                    b.GenreID, g.GenreName, 
                    b.PublisherID, p.PublisherName, 
                    b.YearPublished
                FROM books b
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvBooks.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvBooks.Rows.Add(row["BookID"], row["Title"], row["ISBN"], row["Description"], row["AuthorName"], row["GenreName"], row["PublisherName"], row["YearPublished"], row["AuthorID"], row["GenreID"], row["PublisherID"]);
                }
                txtSearch.Clear();
            }
        }

        private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int bookID = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["BookID"].Value);
                string isbn = dgvBooks.Rows[e.RowIndex].Cells["ISBN"].Value.ToString();
                string title = dgvBooks.Rows[e.RowIndex].Cells["Title"].Value.ToString();
                string description = dgvBooks.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                int authorID = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["AuthorID"].Value);
                int genreID = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["GenreID"].Value);
                int publisherID = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["PublisherID"].Value);
                int yearPublished = Convert.ToInt32(dgvBooks.Rows[e.RowIndex].Cells["YearPublished"].Value);

                if (dgvBooks.Columns[e.ColumnIndex].Name == "Update")
                {
                    updBooks updatebooks = new updBooks(bookID);
                    updatebooks.FormClosed += (s, args) => LoadBooks();
                    updatebooks.ShowDialog();
                }
                else if (dgvBooks.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteBook(bookID);
                    }
                }
            }
        }

        private void DeleteBook(int bookID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM acquisitions WHERE BookID = @BookID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@BookID", bookID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete this book because it is linked to acquisitions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM books WHERE BookID = @BookID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.ExecuteNonQuery();
            }
            LoadBooks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addBooks addbooks = new addBooks();
            addbooks.FormClosed += (s, args) => LoadBooks();
            addbooks.ShowDialog();
        }

        private void cmbFilterAuthor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbFilterGenre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void cmbFilterPublisher_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void ApplyFilters()
        {
            string author = cmbFilterAuthor.Text == "All Authors" ? null : cmbFilterAuthor.Text.Trim();
            string genre = cmbFilterGenre.Text == "All Genre" ? null : cmbFilterGenre.Text.Trim();
            string publisher = cmbFilterPublisher.Text == "All Publishers" ? null : cmbFilterPublisher.Text.Trim();

            classFilteringBooks.FilterBooks(dgvBooks, genre);
        }

        private void cmbFilterGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();

            ApplyFilters();
        }

        private void cmbFilterPublisher_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbFilterAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbFilterAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbFilterGenre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbFilterPublisher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchQuery = txtSearch.Text.Trim().ToLower();

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = @"
            SELECT 
                b.BookID, 
                b.ISBN, 
                b.Description,
                b.Title, 
                b.AuthorID, a.AuthorName, 
                b.GenreID, g.GenreName, 
                b.PublisherID, p.PublisherName, 
                b.YearPublished
            FROM books b
            LEFT JOIN authors a ON b.AuthorID = a.AuthorID
            LEFT JOIN genres g ON b.GenreID = g.GenreID
            LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
            WHERE LOWER(a.AuthorName) LIKE @SearchText
               OR LOWER(p.PublisherName) LIKE @SearchText
               OR LOWER(b.ISBN) LIKE @SearchText
               OR LOWER(b.Title) LIKE @SearchText
               OR LOWER(CAST(b.YearPublished AS CHAR)) LIKE @SearchText";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchQuery + "%");  

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvBooks.Rows.Clear(); 

                foreach (DataRow row in dataTable.Rows)
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
                        row["PublisherID"]
                    );
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != '-')
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
