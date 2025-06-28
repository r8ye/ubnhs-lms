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
    public partial class updAuthors: Form
    {
        private int authorID;
        public updAuthors(int id, string name)
        {
            InitializeComponent();
            authorID = id;
            txtAuthor.Text = name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newAuthorName = txtAuthor.Text.Trim();

            if (string.IsNullOrWhiteSpace(newAuthorName))
            {
                MessageBox.Show("Author name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM authors WHERE AuthorName = @AuthorName AND AuthorID != @AuthorID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@AuthorName", newAuthorName);
                checkCmd.Parameters.AddWithValue("@AuthorID", authorID);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Author name already e" +
                        "xists. Please enter a different name.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "UPDATE authors SET AuthorName = @AuthorName WHERE AuthorID = @AuthorID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AuthorName", newAuthorName);
                cmd.Parameters.AddWithValue("@AuthorID", authorID);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Author updated successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
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
