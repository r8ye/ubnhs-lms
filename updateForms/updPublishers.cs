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
using System.Xml.Linq;

namespace ubnhs_lms.updateForms
{
    public partial class updPublishers: Form
    {
        private int publisherID;

        public updPublishers(int id, string name, string contactNo, string email)
        {
            InitializeComponent();
            publisherID = id;
            txtPublisher.Text = name;
            txtContactNo.Text = contactNo;
            txtEmail.Text = email;

            txtContactNo.MaxLength = 17;
        }

        //private bool IsValidContactNumber(string contactNo)
        //{
        //    return Regex.IsMatch(contactNo, @"^\d{9}$");
        //}

        // 02
        private bool IsValidContactNumber(string contactNo)
        {
            return Regex.IsMatch(contactNo, @"^02\d{7}$");
        }


        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string newPublisherName = txtPublisher.Text.Trim();
            string newContactNo = txtContactNo.Text.Trim();
            string newEmail = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPublisherName) ||
                string.IsNullOrWhiteSpace(newContactNo) || string.IsNullOrWhiteSpace(newEmail))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidContactNumber(newContactNo))
            {
                MessageBox.Show("Invalid contact number format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM publishers WHERE PublisherName = @PublisherName AND PublisherID != @PublisherID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@PublisherName", newPublisherName);
                checkCmd.Parameters.AddWithValue("@PublisherID", publisherID);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Publisher name already exists. Please enter a different name.", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "UPDATE publishers SET PublisherName = @PublisherName, ContactNo = @ContactNo, Email = @Email WHERE PublisherID = @PublisherID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PublisherName", newPublisherName);
                cmd.Parameters.AddWithValue("@ContactNo", newContactNo);
                cmd.Parameters.AddWithValue("@Email", newEmail);
                cmd.Parameters.AddWithValue("@PublisherID", publisherID);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Publisher updated successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.SelectionStart = txtEmail.Text.Length;

            txtEmail.MaxLength = 40;
        }

        private void txtPublisher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtPublisher_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)
            {
                return;
            }
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '&' && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '@' || e.KeyChar == (char)8)
            {
                if (char.IsLetter(e.KeyChar))
                {
                    e.KeyChar = char.ToLower(e.KeyChar);
                }

                if (e.KeyChar == '@' && txtEmail.Text.Contains("@"))
                {
                    e.Handled = true;
                    return;
                }

                if (e.KeyChar == '.')
                {
                    int atIndex = txtEmail.Text.IndexOf('@');
                    int dotCount = txtEmail.Text.Count(c => c == '.');

                    if ((atIndex == -1 && dotCount >= 1) ||
                        (atIndex != -1 && txtEmail.Text.Substring(atIndex).Contains(".")))
                    {
                        e.Handled = true;
                        return;
                    }
                }

                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPublisher_TextChanged(object sender, EventArgs e)
        {
            txtPublisher.MaxLength = 30;
        }

       

        private void txtContactNo_TextChanged(object sender, EventArgs e)
        {
            txtContactNo.MaxLength = 9;
        }
    }
}
