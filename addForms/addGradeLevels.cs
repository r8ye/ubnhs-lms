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
    public partial class addGradeLevels : Form
    {
        public addGradeLevels()
        {
            InitializeComponent();
            LoadDepartments();
            cmbDepartments.SelectedIndexChanged += cmbDepartments_SelectedIndexChanged;
            txtGradeLevels.Focus();
        }

        private void LoadDepartments()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT departmentID, departmentName FROM departments ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["departmentID"] = 0;
                row["departmentName"] = "Select";
                dt.Rows.InsertAt(row, 0);

                cmbDepartments.DisplayMember = "departmentName";
                cmbDepartments.ValueMember = "departmentID";
                cmbDepartments.DataSource = dt;
                cmbDepartments.SelectedIndex = 0;
            }
        }

        private void LoadStrands()
        {
            int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);

            if (departmentID == 0)
            {
                cmbStrand.DataSource = null;
                cmbStrand.Items.Clear();
                cmbStrand.Enabled = true;
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT StrandID, strandName FROM strands WHERE DepartmentID = @departmentID ORDER BY strandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentID", departmentID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbStrand.DisplayMember = "strandName";
                cmbStrand.ValueMember = "StrandID";
                cmbStrand.DataSource = dt;

                if (dt.Rows.Count > 0)
                    cmbStrand.SelectedIndex = 0;

                if (cmbStrand.Text == "N/A")
                {
                    cmbStrand.Enabled = false;
                    lblStrand.Text = "Strand";
                }
                else
                {
                    cmbStrand.Enabled = true;
                    lblStrand.Text = "Strand *";
                }
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            string gradeLevel = txtGradeLevels.Text.Trim();
            int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);
            int strandID = cmbStrand.SelectedValue != null ? Convert.ToInt32(cmbStrand.SelectedValue) : 0;

            if (departmentID == 0)
            {
                MessageBox.Show("Please select a valid department.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStrand.Enabled && strandID == 0)
            {
                MessageBox.Show("Please select a valid strand.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(gradeLevel))
            {
                MessageBox.Show("Please enter the grade level name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM gradelevels WHERE gradelevelName = @gradelevelName AND strandID = @strandID AND departmentID = @departmentID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@departmentID", departmentID);
                checkCmd.Parameters.AddWithValue("@strandID", strandID);
                checkCmd.Parameters.AddWithValue("@gradelevelName", gradeLevel);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This grade level already exists in the selected strand and department.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string query = "INSERT INTO gradelevels (departmentID, strandID, gradelevelName) VALUES (@departmentID, @strandID, @gradelevelName)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@departmentID", departmentID);
                    cmd.Parameters.AddWithValue("@strandID", strandID);
                    cmd.Parameters.AddWithValue("@gradelevelName", gradeLevel);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Grade level saved successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGradeLevels.Clear();
                    this.Close();

                    formGrades formGrade = Application.OpenForms["formGrades"] as formGrades;
                    if (formGrade != null)
                    {
                        formGrade.LoadGrades();
                    }
                }
            }
        }



        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStrands();
        }

        private void cmbDepartments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbStrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtGradeLevels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtGradeLevels_TextChanged(object sender, EventArgs e)
        {
            txtGradeLevels.MaxLength = 8;
        }

        private void cmbStrand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtGradeLevels_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
