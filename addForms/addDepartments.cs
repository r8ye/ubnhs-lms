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
    public partial class addDepartments : Form
    {
        public addDepartments()
        {
            InitializeComponent();
            txtDepartment.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string department = txtDepartment.Text.Trim();

            if (string.IsNullOrEmpty(department))
            {
                MessageBox.Show("Please enter the department name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM departments WHERE departmentName = @departmentName";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@departmentName", department);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This department already exists.");
                }
                else
                {
                    string query = "INSERT INTO departments (departmentName) VALUES (@departmentName)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@departmentName", department);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Department saved successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDepartment.Clear();
                    this.Close();

                    formDepartments formDepartment = Application.OpenForms["formDepartments"] as formDepartments;
                    if (formDepartment != null)
                    {
                        formDepartment.LoadDepartments();
                    }
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {
            txtDepartment.MaxLength = 20;
        }
    }
}
