using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ubnhs_lms.addForms
{
    public partial class addUsers : Form
    {
        public addUsers()
        {
            InitializeComponent();

            txtPassword.MaxLength = 50;
            txtUsername.MaxLength = 30;
        }

        private void addUsers_Load(object sender, EventArgs e)
        {
            rbAssistantLibrarian.Checked = true;
            rbLibrarian.Enabled = false;
            txtPassword.UseSystemPasswordChar = true;
        }

        

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string fullName = txtFullName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string userType = rbLibrarian.Checked ? "Librarian" : "Assistant Librarian";

            // validations
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please enter the full name.");
                return;
            }

            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            if (string.IsNullOrEmpty(password) || !IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 8 characters long, contain an uppercase letter, a digit, and a special character.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                // duplicates
                if (IsFieldExists(conn, "Username", username))
                {
                    MessageBox.Show("This username is already in use. Please choose another username.");
                    return;
                }

                if (IsFieldExists(conn, "Email", email))
                {
                    MessageBox.Show("This email is already in use. Please choose another email.");
                    return;
                }

                if (IsFieldExists(conn, "FullName", fullName))
                {
                    MessageBox.Show("This full name is already in use. Please choose another name.");
                    return;
                }

                if (userType == "Librarian")
                {
                    string checkLibrarianQuery = "SELECT COUNT(*) FROM users WHERE UserType = 'Librarian'";
                    MySqlCommand checkCmd = new MySqlCommand(checkLibrarianQuery, conn);
                    int librarianCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (librarianCount > 0)
                    {
                        MessageBox.Show("Only one Librarian is allowed.");
                        return;
                    }
                }

                string query = "INSERT INTO users (Username, UserType, FullName, Email, Password) VALUES (@Username, @UserType, @FullName, @Email, @Password)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@UserType", userType);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("User added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private bool IsEmailExists(string email)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                int emailCount = Convert.ToInt32(cmd.ExecuteScalar());
                return emailCount > 0;
            }
        }

        private bool IsFieldExists(MySqlConnection conn, string fieldName, string fieldValue)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE {fieldName} = @FieldValue";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FieldValue", fieldValue);

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }

        

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            else
            {
                e.KeyChar = char.ToLower(e.KeyChar);
            }
        }

        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtFullName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void rbLibrarian_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblBorrowerType_Click(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            txtFullName.MaxLength = 30;
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.SelectionStart = txtEmail.Text.Length;

            txtEmail.MaxLength = 30;
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
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {

                e.Handled = true;
            }

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            txtUsername.MaxLength = 20;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.MaxLength = 30;
        }
    }

}

