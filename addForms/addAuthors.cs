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
    public partial class addAuthors: Form
    {
        public addAuthors()
        {
            InitializeComponent();
            txtAuthor.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string author = txtAuthor.Text.Trim();

            if (string.IsNullOrEmpty(author))
            {
                MessageBox.Show("Please enter the author's name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();


                string checkQuery = "SELECT COUNT(*) FROM authors WHERE AuthorName = @AuthorName";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@AuthorName", author);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This author already exists.");
                }
                else
                {
                    string query = "INSERT INTO authors (AuthorName) VALUES (@AuthorName)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AuthorName", author);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Author saved successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAuthor.Clear();
                    this.Close();


                    formAuthors formAuthor = Application.OpenForms["formAuthors"] as formAuthors;
                    if (formAuthor != null)
                    {
                        formAuthor.LoadAuthors();
                    }
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtAuthor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {
            txtAuthor.MaxLength = 30;
        }
    }
}

