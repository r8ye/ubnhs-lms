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

namespace ubnhs_lms.updateForms
{
    public partial class updMembers : Form
    {
        private int memberID;
        public updMembers(int id, string borrowerType, string borrowerID, string lastName, string firstName, string middleName, string email, string departmentName, string strandName, string gradelevelName, string sectionName)
        {
            InitializeComponent();
            memberID = id;
            LoadDepartments();
            chkNoMiddleName.CheckedChanged += NoMiddleNameChecked;

            txtBorrowerID.MaxLength = 12;

            txtBorrowerID.Text = borrowerID;
            txtLastName.Text = lastName;
            txtFirstName.Text = firstName;
            txtMiddleName.Text = string.IsNullOrWhiteSpace(middleName) ? " " : middleName;
            txtEmail.Text = email;
            cmbDepartments.Text = departmentName;
            cmbStrand.Text = strandName;
            cmbGradeLevels.Text = gradelevelName;
            cmbSections.Text = sectionName;

            if (borrowerType == "Faculty")
            {
                lblMemberID.Text = "EID *";
                rbFaculty.Checked = true;
                cmbStrand.Enabled = false;
                cmbGradeLevels.Enabled = false;
                cmbSections.Enabled = false;

                
            }
            else
            {
                lblMemberID.Text = "LRN *";
                rbStudent.Checked = true;
                cmbStrand.Enabled = true;
                cmbGradeLevels.Enabled = true;
                cmbSections.Enabled = true;

                lblGradelevel.Text = "Grade Level *";
                lblStrand.Text = "Strand *";
                lblSection.Text = "Section *";

                
            }
        }

        private void NoMiddleNameChecked(object sender, EventArgs e)
        {
            txtMiddleName.Text = chkNoMiddleName.Checked ? " " : "";
            txtMiddleName.Enabled = !chkNoMiddleName.Checked;
        }

        private void LoadDepartments()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DepartmentID, DepartmentName FROM departments ORDER BY DepartmentName";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow newRow = dt.NewRow();
                newRow["DepartmentID"] = DBNull.Value;
                newRow["DepartmentName"] = "Select";
                dt.Rows.InsertAt(newRow, 0);

                cmbDepartments.DataSource = dt;
                cmbDepartments.DisplayMember = "DepartmentName";
                cmbDepartments.ValueMember = "DepartmentID";
                cmbDepartments.SelectedIndex = 0; 
            }
        }

        private void LoadStrands(int departmentID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT strandID, strandName FROM strands WHERE departmentID = @departmentID ORDER BY strandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentID", departmentID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["strandID"] = 0;
                row["strandName"] = "Select";
                dt.Rows.InsertAt(row, 0);

                cmbStrand.DisplayMember = "strandName";
                cmbStrand.ValueMember = "strandID";
                cmbStrand.DataSource = dt;

                cmbStrand.SelectedIndex = dt.Rows.Count > 1 ? 1 : 0;

                if (rbFaculty.Checked)
                {
                    cmbStrand.Enabled = false;
                    lblStrand.Text = "Strand";

                    

                }
                else if (cmbStrand.Text == "N/A" || cmbStrand.SelectedValue?.ToString() == "0")
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

        private void LoadGradeLevels(int strandID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT GradeLevelID, GradeLevelName FROM gradelevels WHERE StrandID = @StrandID ORDER BY GradeLevelName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StrandID", strandID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow newRow = dt.NewRow();
                newRow["GradeLevelID"] = DBNull.Value;
                newRow["GradeLevelName"] = "Select";
                dt.Rows.InsertAt(newRow, 0);

                cmbGradeLevels.DataSource = dt;
                cmbGradeLevels.DisplayMember = "GradeLevelName";
                cmbGradeLevels.ValueMember = "GradeLevelID";
                cmbGradeLevels.SelectedIndex = 0; 
            }
        }

        private void LoadSections(int gradeLevelID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT SectionID, SectionName FROM sections WHERE GradeLevelID = @GradeLevelID ORDER BY SectionName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow newRow = dt.NewRow();
                newRow["SectionID"] = DBNull.Value;
                newRow["SectionName"] = "Select";
                dt.Rows.InsertAt(newRow, 0);

                cmbSections.DataSource = dt;
                cmbSections.DisplayMember = "SectionName";
                cmbSections.ValueMember = "SectionID";
                cmbSections.SelectedIndex = 0; 
            }
        }




        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            string borrowerType = rbFaculty.Checked ? "Faculty" : "Student";
            string borrowerID = txtBorrowerID.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string middleName = txtMiddleName.Text.Trim();
            string email = txtEmail.Text.Trim();
            //int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);

            //if (cmbDepartments.SelectedIndex == 0 || cmbDepartments.SelectedItem.ToString() == "Select")
            //{
            //    MessageBox.Show("Please add a valid department first.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            object selectedDepartment = cmbDepartments.SelectedValue;

            if (selectedDepartment == null || selectedDepartment == DBNull.Value || selectedDepartment.ToString() == "")
            {
                MessageBox.Show("Please select a valid department.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int departmentID = Convert.ToInt32(selectedDepartment);


            //if (string.IsNullOrWhiteSpace(middleName))
            //{
            //    MessageBox.Show("Middle name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            int? gradeLevelID = rbStudent.Checked && cmbGradeLevels.SelectedValue != null ? Convert.ToInt32(cmbGradeLevels.SelectedValue) : (int?)null;
            int? sectionID = rbStudent.Checked && cmbSections.SelectedValue != null ? Convert.ToInt32(cmbSections.SelectedValue) : (int?)null;
            int? strandID = rbStudent.Checked && cmbStrand.SelectedValue != null ? Convert.ToInt32(cmbStrand.SelectedValue) : (int?)null;

            if (CheckDuplicate(borrowerID, email)) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();

                    if (rbStudent.Checked && strandID.HasValue)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM strands WHERE StrandID = @StrandID";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@StrandID", strandID);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (count == 0)
                            {
                                MessageBox.Show("Invalid StrandID. Please select a valid strand.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE members SET BorrowerType = @BorrowerType, BorrowerID = @BorrowerID, LastName = @LastName, " +
                                          "FirstName = @FirstName, MiddleName = @MiddleName, Email = @Email, DepartmentID = @DepartmentID, " +
                                          "GradeLevelID = @GradeLevelID, SectionID = @SectionID, StrandID = @StrandID WHERE MemberID = @MemberID";

                        cmd.Parameters.AddWithValue("@BorrowerType", borrowerType);
                        cmd.Parameters.AddWithValue("@BorrowerID", borrowerID);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@MiddleName", middleName);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                        cmd.Parameters.AddWithValue("@GradeLevelID", gradeLevelID.HasValue ? gradeLevelID : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SectionID", sectionID.HasValue ? sectionID : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@StrandID", strandID.HasValue ? strandID : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MemberID", memberID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Member updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();

                            formMembers formMem = Application.OpenForms["formMembers"] as formMembers;
                            formMem?.LoadMembers();
                        }
                        else
                        {
                            MessageBox.Show("No record was updated. Please check the Member ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtBorrowerID.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("All fields are required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbFaculty.Checked && !Regex.IsMatch(txtBorrowerID.Text, "^\\d{4}-\\d{5}-\\d{1}$"))
            {
                MessageBox.Show("Faculty Borrower ID format should be XXXX-XXXXX-X", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbStudent.Checked && !Regex.IsMatch(txtBorrowerID.Text, "^\\d{12}$"))
            {
                MessageBox.Show("Student Borrower ID should be 12 digits.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbStudent.Checked && !txtEmail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("Invalid email address.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbFaculty.Checked && !Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@(deped\.gov\.ph|gmail\.com|yahoo\.com)$"))
            {
                MessageBox.Show("Invalid email address.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            if (rbStudent.Checked) 
            {
                if (cmbDepartments.SelectedIndex == 0 ||
                    cmbStrand.SelectedIndex == 0 ||
                    cmbGradeLevels.SelectedIndex == 0 ||
                    cmbSections.SelectedIndex == 0)
                {
                    MessageBox.Show("All fields are required for students.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (rbFaculty.Checked && cmbStrand.Text == "Select")
            {
                MessageBox.Show("Please select a valid strand (cannot be 'Select').", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return true;
        }

        private bool CheckDuplicate(string borrowerID, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM members WHERE (BorrowerID = @BorrowerID OR Email = @Email) AND MemberID != @MemberID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BorrowerID", borrowerID);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@MemberID", memberID);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedValue != null && int.TryParse(cmbDepartments.SelectedValue.ToString(), out int departmentID))
            {
                LoadStrands(departmentID);
            }
        }

        private void cmbGradeLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGradeLevels.SelectedValue != null && int.TryParse(cmbGradeLevels.SelectedValue.ToString(), out int gradeLevelID))
            {
                LoadSections(gradeLevelID);
            }
        }

        private void cmbStrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStrand.SelectedValue != null && int.TryParse(cmbStrand.SelectedValue.ToString(), out int strandID))
            {
                LoadGradeLevels(strandID);
            }
        }

        private void txtBorrowerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

            if (rbFaculty.Checked)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else if (rbStudent.Checked)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
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

        private void txtBorrowerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtMiddleName_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbGradeLevels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbSections_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void rbFaculty_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFaculty.Checked)
            {
                lblMemberID.Text = "EID";
                cmbStrand.Enabled = false;
                cmbGradeLevels.Enabled = false;
                cmbSections.Enabled = false;

                lblStrand.Visible = false;
                lblGradelevel.Visible = false;
                lblSection.Visible = false;
                cmbStrand.Visible = false;
                cmbGradeLevels.Visible = false;
                cmbSections.Visible = false;
            }
        }

        private void rbStudent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStudent.Checked)
            {
                lblMemberID.Text = "LRN";
                cmbStrand.Enabled = true;
                cmbGradeLevels.Enabled = true;
                cmbSections.Enabled = true;

                lblGradelevel.Visible = true;
                lblSection.Visible = true;
                cmbGradeLevels.Visible = true;
                cmbSections.Visible = true;
                lblStrand.Visible = true;
                cmbStrand.Visible = true;
            }

            
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.SelectionStart = txtEmail.Text.Length;

        }

        private void txtBorrowerID_TextChanged(object sender, EventArgs e)
        {
            txtBorrowerID.MaxLength = 12;
        }
    }
}
