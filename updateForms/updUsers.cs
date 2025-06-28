using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ubnhs_lms.updateForms
{
    public partial class updUsers : Form
    {
        private int userID;
        private string userType;
        private string unhashedPassword;

        public updUsers(int userID, string fullName, string email, string username, string userType)
        {
            InitializeComponent();
            this.userID = userID;
            this.userType = userType;
            LoadUserData();

            if (userType == "Librarian")
            {
                rbLibrarian.Checked = true;
                rbLibrarian.Enabled = false;
                rbAssistant.Enabled = false;
            }
            else
            {
                rbAssistant.Checked = true;
                rbLibrarian.Enabled = false;
                rbAssistant.Enabled = false;
            }

            txtFullName.Text = fullName;
            txtEmail.Text = email;
            txtUsername.Text = username;

            txtPassword.MaxLength = 50;
            txtUsername.MaxLength = 30;
        }


        private void LoadUserData()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT FullName, Email, Username, UserType, Password FROM users WHERE UserID = @UserID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtFullName.Text = reader["FullName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtUsername.Text = reader["Username"].ToString();
                        unhashedPassword = reader["Password"].ToString();
                        txtPassword.Text = unhashedPassword;
                        txtPassword.UseSystemPasswordChar = true;
                    }
                }
                
            }
        }

        

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string selectedUserType = rbLibrarian.Checked ? "Librarian" : "Assistant Librarian";

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                if (IsDuplicate(conn, "Username", username))
                {
                    MessageBox.Show("Username already exists. Please choose a different one.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (IsDuplicate(conn, "Email", email))
                {
                    MessageBox.Show("Email already exists. Please use a different email address.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (selectedUserType == "Librarian" && IsLibrarianExists(conn))
                {
                    MessageBox.Show("A Librarian already exists. Only one Librarian is allowed.", "Role Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Invalid email format. Please enter a valid email.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!IsValidPassword(password))
                {
                    MessageBox.Show("Password must be at least 8 characters long and contain at least one special character.", "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string updateQuery = "UPDATE users SET FullName = @FullName, Email = @Email, Username = @Username, Password = @Password, UserType = @UserType WHERE UserID = @UserID";
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@UserType", selectedUserType);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        

        
        private bool IsDuplicate(MySqlConnection conn, string column, string value)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE {column} = @Value AND UserID != @UserID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Value", value);
            cmd.Parameters.AddWithValue("@UserID", userID);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private bool IsLibrarianExists(MySqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM users WHERE UserType = 'Librarian' AND UserID != @UserID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserID", userID);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private bool IsValidPassword(string password) => password.Length >= 8 && password.Any(ch => !Char.IsLetterOrDigit(ch));

        private bool IsEmailExists(string email)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM users WHERE Email = @Email AND UserID != @UserID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@UserID", userID);

                int emailCount = Convert.ToInt32(cmd.ExecuteScalar());
                return emailCount > 0;
            }
        }

        
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, emailPattern);
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

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
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

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.SelectionStart = txtEmail.Text.Length;

            txtEmail.MaxLength = 30;
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            txtFullName.MaxLength = 30;
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

