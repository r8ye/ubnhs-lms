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
    public partial class updGradeLevels : Form
    {
        private int gradeLevelID;
        public updGradeLevels(int gradeLevelID, string departmentName, string gradeLevelName, string strandName)
        {
            InitializeComponent();
            this.gradeLevelID = gradeLevelID;

            LoadDepartments();
            cmbDepartments.SelectedIndexChanged += cmbDepartments_SelectedIndexChanged;

            cmbDepartments.SelectedItem = cmbDepartments.Items.Cast<DataRowView>()
                .FirstOrDefault(item => item["departmentName"].ToString() == departmentName);

            txtGradeLevels.Text = gradeLevelName;

            LoadStrands();

            cmbStrand.SelectedItem = cmbStrand.Items.Cast<DataRowView>()
                .FirstOrDefault(item => item["strandName"].ToString() == strandName);
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
            }
        }

        private void LoadStrands()
        {
            if (cmbDepartments.SelectedValue == null || cmbDepartments.SelectedValue.ToString() == "0")
            {
                cmbStrand.DataSource = null;
                cmbStrand.Enabled = true;
                return;
            }

            int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT strandID, strandName FROM strands WHERE departmentID = @departmentID ORDER BY strandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentID", departmentID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbStrand.DisplayMember = "strandName";
                cmbStrand.ValueMember = "strandID";
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
            int departmentID = cmbDepartments.SelectedValue != null ? Convert.ToInt32(cmbDepartments.SelectedValue) : 0;
            int strandID = cmbStrand.SelectedValue != null ? Convert.ToInt32(cmbStrand.SelectedValue) : 0;

            if (departmentID == 0)
            {
                MessageBox.Show("Please select a valid department.");
                return;
            }

            if (strandID == 0)
            {
                MessageBox.Show("Please select a valid strand.");
                return;
            }

            if (string.IsNullOrEmpty(gradeLevel))
            {
                MessageBox.Show("Please enter the grade level name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM gradelevels WHERE departmentID = @departmentID AND strandID = @strandID AND gradelevelName = @gradeLevelName AND gradeLevelID != @gradeLevelID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@departmentID", departmentID);
                checkCmd.Parameters.AddWithValue("@strandID", strandID);
                checkCmd.Parameters.AddWithValue("@gradeLevelName", gradeLevel);
                checkCmd.Parameters.AddWithValue("@gradeLevelID", gradeLevelID);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This grade level already exists in the selected department and strand.", "Duplicate Grade Level", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string query = "UPDATE gradelevels SET departmentID = @departmentID, strandID = @strandID, gradeLevelName = @gradeLevelName WHERE gradeLevelID = @gradeLevelID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentID", departmentID);
                cmd.Parameters.AddWithValue("@strandID", strandID);
                cmd.Parameters.AddWithValue("@gradeLevelName", gradeLevel);
                cmd.Parameters.AddWithValue("@gradeLevelID", gradeLevelID);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Grade level updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

            formGrades formGrade = Application.OpenForms["formGrades"] as formGrades;
            formGrade?.LoadGrades();
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

        private void txtGradeLevels_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
