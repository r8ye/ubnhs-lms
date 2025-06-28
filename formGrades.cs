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
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;

namespace ubnhs_lms
{
    public partial class formGrades: Form
    {
        public formGrades()
        {
            InitializeComponent();
            LoadGrades();
        }

        public void LoadGrades(string department = null, string strand = null)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT g.GradeLevelID, s.strandName, d.departmentName, g.GradeLevelName, d.DepartmentID, s.StrandID " +
                               "FROM gradelevels g " +
                               "INNER JOIN strands s ON g.StrandID = s.StrandID " +
                               "INNER JOIN departments d ON g.DepartmentID = d.DepartmentID ";

                if (!string.IsNullOrEmpty(department) || !string.IsNullOrEmpty(strand))
                {
                    query += " WHERE ";
                    if (!string.IsNullOrEmpty(department))
                    {
                        query += " d.departmentName = @Department ";
                    }
                    if (!string.IsNullOrEmpty(strand))
                    {
                        query += (!string.IsNullOrEmpty(department) ? " AND " : "") + " s.strandName = @Strand ";
                    }
                }

                query += " ORDER BY d.departmentName, s.strandName, g.GradeLevelName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (!string.IsNullOrEmpty(department)) cmd.Parameters.AddWithValue("@Department", department);
                if (!string.IsNullOrEmpty(strand)) cmd.Parameters.AddWithValue("@Strand", strand);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvGradeLevels.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvGradeLevels.Rows.Add(
                        row["GradeLevelID"],
                        row["departmentName"],
                        row["strandName"],
                        row["GradeLevelName"],
                        row["DepartmentID"],
                        row["StrandID"]
                    );
                }
            }
            dgvGradeLevels.Columns["DepartmentID"].Visible = false;
            dgvGradeLevels.Columns["StrandID"].Visible = false;
        }




        private void formGrades_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvGradeLevels, lblTotalCount);

            cmbFilterDepartments.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterDepartments.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterDepartments.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterStrand.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterStrand.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterStrand.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterStrand.Enabled = false;

            classFilteringGrades.LoadFilters(cmbFilterDepartments, cmbFilterStrand);

            LoadGrades();
        }

        private void DeleteGrade(int gradeLevelID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM sections WHERE GradeLevelID = @GradeLevelID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete grade level with associated sections.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM gradelevels WHERE GradeLevelID = @GradeLevelID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID);
                cmd.ExecuteNonQuery();
            }
            LoadGrades();
        }

        private void dgvGradeLevels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int gradeLevelID = Convert.ToInt32(dgvGradeLevels.Rows[e.RowIndex].Cells["GradeLevelID"].Value);
                string strandName = dgvGradeLevels.Rows[e.RowIndex].Cells["StrandName"].Value.ToString();  
                string departmentName = dgvGradeLevels.Rows[e.RowIndex].Cells["DepartmentName"].Value.ToString();  
                string gradeLevelName = dgvGradeLevels.Rows[e.RowIndex].Cells["GradeLevelName"].Value.ToString(); 

                if (dgvGradeLevels.Columns[e.ColumnIndex].Name == "Update")
                {
                    updGradeLevels updateGrades = new updGradeLevels(gradeLevelID, departmentName, gradeLevelName, strandName);
                    updateGrades.FormClosed += (s, args) => LoadGrades();  
                    updateGrades.ShowDialog();
                }
                else if (dgvGradeLevels.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this grade level?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteGrade(gradeLevelID);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addGradeLevels addGradesForm = new addGradeLevels();
            addGradesForm.FormClosed += (s, args) => LoadGrades();
            addGradesForm.ShowDialog();
        }

        private void cmbFilterDepartments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            e.Handled = false;
        }

        private void cmbFilterDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department = cmbFilterDepartments.Text == "All Departments" ? null : cmbFilterDepartments.Text.Trim();

            cmbFilterStrand.Enabled = !string.IsNullOrEmpty(department);
            cmbFilterStrand.Items.Clear();

            if (!string.IsNullOrEmpty(department))
            {
                classFilteringGrades.LoadStrands(cmbFilterStrand, department);
            }

            string strand = cmbFilterStrand.Text == "All Strands" ? null : cmbFilterStrand.Text.Trim();
            LoadGrades(department, strand);
        }

        private void cmbFilterStrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            e.Handled = false;
        }

        private void cmbFilterStrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department = cmbFilterDepartments.Text == "All Departments" ? null : cmbFilterDepartments.Text.Trim();
            string strand = cmbFilterStrand.Text == "All Strands" ? null : cmbFilterStrand.Text.Trim();
            LoadGrades(department, strand);
        }

        private void cmbFilterDepartments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbFilterStrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
