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
    public partial class formSections: Form
    {
        public formSections()
        {
            InitializeComponent();
            LoadSections();
        }

        public void LoadSections()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT s.sectionID, d.departmentName, str.strandName, g.gradelevelName, s.sectionName " +
                               "FROM sections s " +
                               "INNER JOIN departments d ON s.departmentID = d.departmentID " +
                               "INNER JOIN strands str ON s.strandID = str.strandID " +
                               "INNER JOIN gradelevels g ON s.gradelevelID = g.gradelevelID " +
                               "ORDER BY d.departmentName, str.strandName, g.gradelevelName, s.sectionName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvSections.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvSections.Rows.Add(row["sectionID"], row["departmentName"], row["strandName"], row["gradelevelName"], row["sectionName"]);
                }

                txtSearch.Clear();
            }
        }


        private void formSections_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvSections, lblTotalCount);

            cmbFilterDepartments.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterDepartments.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterDepartments.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterStrands.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterStrands.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterStrands.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterGradeLevels.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterGradeLevels.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterGradeLevels.AutoCompleteSource = AutoCompleteSource.ListItems;

            classFilteringSections.LoadDepartments(cmbFilterDepartments);
        }

        private void cmbFilterDepartments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            e.Handled = false;
        }

        private void cmbFilterGradeLevels_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            e.Handled = false;
        }

        private void DeleteSection(int sectionID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM members WHERE SectionID = @SectionID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@SectionID", sectionID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete strand with associated members.",
                        "Delete Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string deleteQuery = "DELETE FROM sections WHERE SectionID = @SectionID";
                MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@SectionID", sectionID);
                deleteCmd.ExecuteNonQuery();
            }
            LoadSections();
        }



        private void dgvSections_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int sectionID = Convert.ToInt32(dgvSections.Rows[e.RowIndex].Cells["sectionID"].Value);
                string departmentName = dgvSections.Rows[e.RowIndex].Cells["departmentName"].Value.ToString();
                string strandName = dgvSections.Rows[e.RowIndex].Cells["strandName"].Value.ToString();
                string gradelevelName = dgvSections.Rows[e.RowIndex].Cells["gradelevelName"].Value.ToString();
                string sectionName = dgvSections.Rows[e.RowIndex].Cells["sectionName"].Value.ToString();

                if (dgvSections.Columns[e.ColumnIndex].Name == "Update")
                {
                    updSections updateSections = new updSections(sectionID, departmentName, strandName, gradelevelName, sectionName);
                    updateSections.FormClosed += (s, args) => LoadSections();
                    updateSections.ShowDialog();
                }
                else if (dgvSections.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this section?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteSection(sectionID);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addSections addSectionsForm = new addSections();
            addSectionsForm.FormClosed += (s, args) => LoadSections();
            addSectionsForm.ShowDialog();
        }

        private void cmbFilterDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDepartment = cmbFilterDepartments.Text;

            if (selectedDepartment == "All Departments")
            {
                cmbFilterStrands.Enabled = false;
                cmbFilterStrands.Items.Clear();
                cmbFilterStrands.Items.Add("All Strands");
                cmbFilterStrands.SelectedIndex = 0;

                cmbFilterGradeLevels.Enabled = false;
                cmbFilterGradeLevels.Items.Clear();
                cmbFilterGradeLevels.Items.Add("All Grade Levels");
                cmbFilterGradeLevels.SelectedIndex = 0;
            }
            else
            {
                cmbFilterStrands.Enabled = true;
                cmbFilterStrands.Items.Clear(); 
                classFilteringSections.LoadStrands(cmbFilterStrands, selectedDepartment);
            }

            txtSearch.Clear();
        }

        private void cmbFilterGradeLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department = cmbFilterDepartments.Text == "All Departments" ? null : cmbFilterDepartments.Text.Trim();
            string strand = cmbFilterStrands.Text == "All Strands" ? null : cmbFilterStrands.Text.Trim();
            string gradelevel = cmbFilterGradeLevels.Text == "All Grade Levels" ? null : cmbFilterGradeLevels.Text.Trim();

            classFilteringSections.FilterSections(dgvSections, department, strand, gradelevel);

            txtSearch.Clear();
        }

        private void cmbFilterStrands_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            e.Handled = false;
        }

        private void cmbFilterStrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDepartment = cmbFilterDepartments.Text == "All Departments" ? null : cmbFilterDepartments.Text.Trim();
            string selectedStrand = cmbFilterStrands.Text;

            if (selectedStrand == "All Strands")
            {
                cmbFilterGradeLevels.Enabled = false;
                cmbFilterGradeLevels.Items.Clear();
                cmbFilterGradeLevels.Items.Add("All Grade Levels");
                cmbFilterGradeLevels.SelectedIndex = 0;
            }
            else
            {
                cmbFilterGradeLevels.Enabled = true;
                cmbFilterGradeLevels.Items.Clear(); 
                classFilteringSections.LoadGradeLevels(cmbFilterGradeLevels, selectedDepartment, selectedStrand);
            }

            txtSearch.Clear();
        }

        private void cmbFilterDepartments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbFilterStrands_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbFilterGradeLevels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

       

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim().ToLower();

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = @"
            SELECT 
                s.sectionID, 
                d.departmentName, 
                str.strandName, 
                g.gradelevelName, 
                s.sectionName 
            FROM sections s
            INNER JOIN departments d ON s.departmentID = d.departmentID
            INNER JOIN strands str ON s.strandID = str.strandID
            INNER JOIN gradelevels g ON s.gradelevelID = g.gradelevelID
            WHERE LOWER(s.sectionName) LIKE @SearchText
                OR LOWER(d.departmentName) LIKE @SearchText
                OR LOWER(str.strandName) LIKE @SearchText
                OR LOWER(g.gradelevelName) LIKE @SearchText
            ORDER BY d.departmentName, str.strandName, g.gradelevelName, s.sectionName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchQuery + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvSections.Rows.Clear();

                foreach (DataRow row in dataTable.Rows)
                {
                    dgvSections.Rows.Add(
                        row["sectionID"],
                        row["departmentName"],
                        row["strandName"],
                        row["gradelevelName"],
                        row["sectionName"]
                    );
                }
            }
        }
    }
}
