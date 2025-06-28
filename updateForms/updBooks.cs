using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ubnhs_lms.updateForms
{
    public partial class updBooks : Form
    {
        private int bookID;
        public updBooks(int id)
        {
            InitializeComponent();
            bookID = id; 
            LoadDropdowns();
            LoadBookDetails();

            dtpYearPublished.Format = DateTimePickerFormat.Custom;
            dtpYearPublished.CustomFormat = "yyyy";
            dtpYearPublished.MaxDate = DateTime.Today.AddDays(-1);

            txtISBN.MaxLength = 17;
        }

        private void LoadDropdowns()
        {
            LoadComboBox("SELECT * FROM authors", cmbAuthor, "AuthorID", "AuthorName");
            LoadComboBox("SELECT * FROM genres", cmbGenre, "GenreID", "GenreName");
            LoadComboBox("SELECT * FROM publishers", cmbPublisher, "PublisherID", "PublisherName");
        }

        private void LoadComboBox(string query, ComboBox comboBox, string valueMember, string displayMember)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                comboBox.DataSource = dataTable;
                comboBox.ValueMember = valueMember;
                comboBox.DisplayMember = displayMember;
            }
        }

        private void LoadBookDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM books WHERE BookID = @BookID", conn);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtISBN.Text = reader["ISBN"].ToString();
                    txtTitle.Text = reader["Title"].ToString();
                    txtDescription.Text = reader["Description"].ToString(); 
                    cmbAuthor.SelectedValue = reader["AuthorID"];
                    cmbGenre.SelectedValue = reader["GenreID"];
                    cmbPublisher.SelectedValue = reader["PublisherID"];

                    if (reader["YearPublished"] != DBNull.Value && int.TryParse(reader["YearPublished"].ToString(), out int yearPublished))
                    {
                        dtpYearPublished.Value = new DateTime(yearPublished, 1, 1);
                    }
                    else
                    {
                        dtpYearPublished.Value = DateTime.Today;
                    }
                }

                reader.Close();
            }
        }

        private string GenerateUniqueISBN()
        {
            string isbn;
            do
            {
                isbn = GenerateISBN13();
            } while (IsISBNExists(isbn));

            return isbn;
        }

        private string GenerateISBN13()
        {
            Random rand = new Random();
            string isbnBase = "979";
            isbnBase += "-" + rand.Next(100, 999);
            isbnBase += "-" + rand.Next(100, 999);
            isbnBase += "-" + rand.Next(10, 99);

            string rawISBN = isbnBase.Replace("-", "");
            int checkDigit = CalculateISBN13CheckDigit(rawISBN);

            return isbnBase + "-" + checkDigit;
        }

        private int CalculateISBN13CheckDigit(string isbnBase)
        {
            int sum = 0;
            for (int i = 0; i < isbnBase.Length; i++)
            {
                if (char.IsDigit(isbnBase[i]))
                {
                    int digit = int.Parse(isbnBase[i].ToString());
                    sum += (i % 2 == 0) ? digit : digit * 3;
                }
            }

            int remainder = sum % 10;
            return (remainder == 0) ? 0 : 10 - remainder;
        }


        private bool IsISBNExists(string isbn)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM books WHERE ISBN = @ISBN";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", isbn);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private bool IsValidISBN(string isbn)
        {
            return Regex.IsMatch(isbn, @"^[0-9\-]{12,17}$");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValidISBN(txtISBN.Text))
            {
                MessageBox.Show("Invalid ISBN format.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Description cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbAuthor.SelectedValue == null || Convert.ToInt32(cmbAuthor.SelectedValue) == 0 ||
                cmbGenre.SelectedValue == null || Convert.ToInt32(cmbGenre.SelectedValue) == 0 ||
                cmbPublisher.SelectedValue == null || Convert.ToInt32(cmbPublisher.SelectedValue) == 0)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"UPDATE books SET ISBN = @ISBN, Title = @Title, Description = @Description, 
                                                      AuthorID = @Author, GenreID = @Genre, PublisherID = @Publisher, 
                                                      YearPublished = @YearPublished WHERE BookID = @BookID", conn);
                cmd.Parameters.AddWithValue("@ISBN", txtISBN.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text); 
                cmd.Parameters.AddWithValue("@Author", cmbAuthor.SelectedValue);
                cmd.Parameters.AddWithValue("@Genre", cmbGenre.SelectedValue);
                cmd.Parameters.AddWithValue("@Publisher", cmbPublisher.SelectedValue);
                cmd.Parameters.AddWithValue("@YearPublished", dtpYearPublished.Value.Year);
                cmd.Parameters.AddWithValue("@BookID", bookID);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Book updated successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void updBooks_Load(object sender, EventArgs e)
        {
            cmbAuthor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAuthor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAuthor.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbGenre.DropDownStyle = ComboBoxStyle.DropDown;
            cmbGenre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbGenre.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbPublisher.DropDownStyle = ComboBoxStyle.DropDown;
            cmbPublisher.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPublisher.AutoCompleteSource = AutoCompleteSource.ListItems;

            txtISBN.MaxLength = 17;
        }

        private void cmbAuthor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cmbGenre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void cmbPublisher_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (e.KeyChar == (char)8)
            {
                return;
            }
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '&' && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void txtISBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void txtISBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbGenre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dtpYearPublished_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbPublisher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void chkNoDescription_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoDescription.Checked)
            {
                txtDescription.Text = "No available description.";
            }
            else
            {
                txtDescription.Clear();
            }
        }

        private void txtISBN_TextChanged(object sender, EventArgs e)
        {
            txtISBN.MaxLength = 17;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            txtTitle.MaxLength = 50;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            txtDescription.MaxLength = 50;
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != 39)
            {
                e.Handled = true; 
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void chkNoISBN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoISBN.Checked)
            {
                txtISBN.Text = GenerateUniqueISBN();
                txtISBN.Enabled = false;
            }
            else
            {
                txtISBN.Text = "";
                txtISBN.Enabled = true;
            }
        }
    }
}
