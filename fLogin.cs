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

namespace ubnhs_lms
{
    public partial class fLogin : Form
    {
        public string UserType { get; private set; }

        public fLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            iconPictureBox1.Visible = false;
            iconPictureBox2.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            iconPictureBox1.Visible = false;
            iconPictureBox2.Visible = false;

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Password, UserType FROM users WHERE BINARY Username = @Username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", Username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        iconPictureBox1.Visible = true;
                        iconPictureBox2.Visible = true; 
                        MessageBox.Show("Invalid Username and Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string correctPassword = reader.GetString("Password");
                    string userType = reader.GetString("UserType");

                    if (password != correctPassword)
                    {
                        iconPictureBox2.Visible = true;
                        MessageBox.Show("Incorrect Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UserType = userType;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private bool ValidateCredentials(string Username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                if (conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Failed to connect to the database.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string query = "SELECT UserType FROM users WHERE BINARY Username = @Username AND BINARY Password = @Password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", password);

                object result = cmd.ExecuteScalar();
                conn.Close();

                if (result != null)
                {
                    UserType = result.ToString();
                    return true;
                }

                return false;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            iconPictureBox1.Visible = false;

            txtUsername.MaxLength = 20;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            iconPictureBox2.Visible = false;
            txtPassword.MaxLength = 30;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
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

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetterOrDigit(e.KeyChar) || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }
    }
}
