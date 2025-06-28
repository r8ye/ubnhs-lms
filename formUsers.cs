using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;

namespace ubnhs_lms
{
    public partial class formUsers: Form
    {
        private string currentUserType;

        public formUsers(string userType)
        {
            InitializeComponent();
            this.currentUserType = userType; 
            LoadUsers();
        }

        public void LoadUsers()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT UserID, UserType, FullName, Email, Username, Password FROM users";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvUser.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    // hide librarian
                    if (row["UserType"].ToString() == "Librarian")
                        continue;
                    // end hide

                    dgvUser.Rows.Add(row["UserID"], row["UserType"], row["FullName"], row["Email"], row["Username"], row["Password"]);
                }
                txtSearch.Clear();
            }
        }




        private void formUsers_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvUser, lblTotalCount);
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int userID = Convert.ToInt32(dgvUser.Rows[e.RowIndex].Cells["UserID"].Value);
                string fullName = dgvUser.Rows[e.RowIndex].Cells["FullName"].Value.ToString();
                string email = dgvUser.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                string username = dgvUser.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                string userType = dgvUser.Rows[e.RowIndex].Cells["UserType"].Value.ToString();

                if (dgvUser.Columns[e.ColumnIndex].Name == "Update")
                {
                    updUsers updateUsersForm = new updUsers(userID, fullName, email, username, userType);
                    updateUsersForm.FormClosed += (s, args) => LoadUsers();
                    updateUsersForm.ShowDialog();
                }

                if (dgvUser.Columns[e.ColumnIndex].Name == "Delete")
                {
                    if (currentUserType == "Librarian" && userType == "Librarian")
                    {
                        MessageBox.Show("Librarians cannot delete themselves.", "Delete Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteUser(userID);
                        }
                    }
                }
            }
        }

        private void DeleteUser(int userID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM users WHERE UserID = @UserID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
            LoadUsers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addUsers addUserForm = new addUsers();
            addUserForm.FormClosed += (s, args) => LoadUsers();
            addUserForm.ShowDialog();
        }

        private void dgvUser_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string usertype = dgvUser.Rows[e.RowIndex].Cells["UserType"].Value?.ToString();

                if (e.ColumnIndex == dgvUser.Columns["UserType"].Index)
                {
                    if (usertype == "Librarian")
                    {
                        e.CellStyle.ForeColor = Color.CornflowerBlue;
                    }
                    else if (usertype == "Assistant Librarian")
                    {
                        e.CellStyle.ForeColor = Color.OliveDrab;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Gray;
                    }
                }
            }
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txtSearch.MaxLength = 30;

            string searchTerm = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadUsers();
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT UserID, UserType, FullName, Email, Username, Password " +
                                   "FROM users WHERE Username LIKE @searchTerm OR FullName LIKE @searchTerm";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvUser.Rows.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        dgvUser.Rows.Add(row["UserID"], row["UserType"], row["FullName"], row["Email"], row["Username"], row["Password"]);
                    }
                }
            }
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
    }
}
