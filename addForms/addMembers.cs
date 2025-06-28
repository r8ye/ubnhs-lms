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

namespace ubnhs_lms.addForms
{
    public partial class addMembers : Form
    {
        public addMembers()
        {
            InitializeComponent();
            LoadDepartments();

            rbFaculty.CheckedChanged += BorrowerType;
            rbStudent.CheckedChanged += BorrowerType;
            cmbDepartments.SelectedIndexChanged += cmbDepartments_SelectedIndexChanged;
            cmbStrand.SelectedIndexChanged += cmbStrand_SelectedIndexChanged;
            cmbGradeLevels.SelectedIndexChanged += cmbGradeLevels_SelectedIndexChanged;

            
        }

        private void BorrowerType(object sender, EventArgs e)
        {
            txtBorrowerID.Text = "";
            

            cmbDepartments.Enabled = true;

            if (rbFaculty.Checked)
            {
                lblMemberID.Text = "EID *";
                lblStrand.Text = "Strand";
                lblGradelevel.Text = "Grade Level";
                lblSection.Text = "Section";

                txtBorrowerID.Enabled = true;
                cmbStrand.Enabled = false;
                cmbGradeLevels.Enabled = false;
                cmbSections.Enabled = false;

                cmbStrand.DataSource = null;
                cmbGradeLevels.DataSource = null;
                cmbSections.DataSource = null;

            }
            else if (rbStudent.Checked)
            {
                lblMemberID.Text = "LRN *";
                lblStrand.Text = "Strand *";
                lblGradelevel.Text = "Grade Level *";
                lblSection.Text = "Section *";

                txtBorrowerID.Enabled = true;
                cmbStrand.Enabled = true;
                cmbGradeLevels.Enabled = true;
                cmbSections.Enabled = true;

                if (cmbDepartments.SelectedValue != null && int.TryParse(cmbDepartments.SelectedValue.ToString(), out int departmentID))
                {
                    LoadStrands(departmentID);
                }
            }
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

                DataRow row = dt.NewRow();
                row["DepartmentID"] = 0;
                row["DepartmentName"] = "Select";
                dt.Rows.InsertAt(row, 0);

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

                if (dt.Rows.Count > 1)
                {
                    cmbStrand.SelectedIndex = 1;
                }
                else
                {
                    cmbStrand.SelectedIndex = 0;
                }


                if (rbFaculty.Checked)
                {
                    cmbStrand.Enabled = false;
                    lblStrand.Text = "Strand";

                    

                }
                else if (cmbStrand.Text == "N/A")
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

                DataRow row = dt.NewRow();
                row["GradeLevelID"] = 0;
                row["GradeLevelName"] = "Select";
                dt.Rows.InsertAt(row, 0);

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

                DataRow row = dt.NewRow();
                row["SectionID"] = 0;
                row["SectionName"] = "Select";
                dt.Rows.InsertAt(row, 0);

                cmbSections.DataSource = dt;
                cmbSections.DisplayMember = "SectionName";
                cmbSections.ValueMember = "SectionID";
                cmbSections.SelectedIndex = 0;
            }
        }         

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!chkNoMiddleName.Checked && string.IsNullOrWhiteSpace(txtMiddleName.Text))
                //{
                //    MessageBox.Show("Middle Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtMiddleName.Focus();
                //    return;
                //}

                if (!rbFaculty.Checked && !rbStudent.Checked)
                {
                    MessageBox.Show("Please select a borrower type (Faculty or Student).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                if (!ValidateFields()) return;

                string borrowerType = rbFaculty.Checked ? "Faculty" : "Student";
                string borrowerID = txtBorrowerID.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string firstName = txtFirstName.Text.Trim();
                string middleName = txtMiddleName.Text.Trim();
                string email = txtEmail.Text.Trim();
                int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);

                //if (cmbDepartments.SelectedValue == null || cmbDepartments.SelectedItem.ToString() == "Select")
                //{
                //    MessageBox.Show("Please add a valid department first.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                if (cmbDepartments.SelectedIndex == 0 || cmbDepartments.SelectedItem.ToString() == "Select")
                {
                    MessageBox.Show("Please add a valid department first.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                int? gradeLevelID = null;
                int? sectionID = null;
                int? strandID = null;

                if (rbStudent.Checked)
                {
                    if (cmbGradeLevels.SelectedValue == null || cmbGradeLevels.SelectedItem.ToString() == "Unavailable")
                    {
                        MessageBox.Show("Please add a valid grade level first.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    gradeLevelID = Convert.ToInt32(cmbGradeLevels.SelectedValue);

                    if (cmbSections.SelectedValue == null || cmbSections.SelectedItem.ToString() == "Unavailable")
                    {
                        MessageBox.Show("Please add a valid section first.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    sectionID = Convert.ToInt32(cmbSections.SelectedValue);

                    if (cmbStrand.SelectedValue == null || cmbStrand.SelectedItem.ToString() == "Unavailable")
                    {
                        MessageBox.Show("Please select a valid strand.", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    strandID = Convert.ToInt32(cmbStrand.SelectedValue);
                }

                if (CheckDuplicate(borrowerID, email)) return;

                using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();
                    string query = @"INSERT INTO members 
                            (BorrowerType, BorrowerID, LastName, FirstName, MiddleName, Email, DepartmentID, GradeLevelID, SectionID, StrandID) 
                            VALUES 
                            (@BorrowerType, @BorrowerID, @LastName, @FirstName, @MiddleName, @Email, @DepartmentID, @GradeLevelID, @SectionID, @StrandID)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
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

                        cmd.ExecuteNonQuery();
                    }
                }

                if (rbStudent.Checked)
                {
                    if (cmbDepartments.SelectedIndex == 0 ||
                        cmbStrand.SelectedIndex == 0 ||
                        cmbGradeLevels.SelectedIndex == 0 ||
                        cmbSections.SelectedIndex == 0)
                    {
                        MessageBox.Show("All fields are required for students.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Member added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

                if (Application.OpenForms["formMembers"] is formMembers formMem)
                {
                    formMem.LoadMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            return true;
        }


        private bool CheckDuplicate(string borrowerID, string email)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM members WHERE BorrowerID = @BorrowerID OR Email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BorrowerID", borrowerID);
                cmd.Parameters.AddWithValue("@Email", email);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    MessageBox.Show("Borrower ID or Email already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            return false;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedIndex > 0 && int.TryParse(cmbDepartments.SelectedValue.ToString(), out int departmentID))
            {
                LoadStrands(departmentID);
            }
            else
            {
                cmbStrand.DataSource = null;
                cmbGradeLevels.DataSource = null;
                cmbSections.DataSource = null;
            }
        }

        private void cmbGradeLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGradeLevels.SelectedIndex > 0 && int.TryParse(cmbGradeLevels.SelectedValue.ToString(), out int gradeLevelID))
            {
                LoadSections(gradeLevelID);
            }
            else
            {
                cmbSections.DataSource = null;
            }
        }

        private void txtBorrowerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void cmbStrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStrand.SelectedIndex > 0 && int.TryParse(cmbStrand.SelectedValue.ToString(), out int strandID))
            {
                LoadGradeLevels(strandID);
            }
            else
            {
                cmbGradeLevels.DataSource = null;
                cmbSections.DataSource = null;
            }
        }

        private void chkNoMiddleName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoMiddleName.Checked)
            {
                txtMiddleName.Text = " ";
                txtMiddleName.Enabled = false;
            }
            else
            {
                txtMiddleName.Text = "";
                txtMiddleName.Enabled = true;
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

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            txtEmail.Text = txtEmail.Text.ToLower();
            txtEmail.SelectionStart = txtEmail.Text.Length;
        }

        private void addMembers_Load(object sender, EventArgs e)
        {
            txtBorrowerID.MaxLength = 10;
        }

        private void txtBorrowerID_TextChanged(object sender, EventArgs e)
        {
            txtBorrowerID.MaxLength = 12;
        }

        private void rbFaculty_CheckedChanged(object sender, EventArgs e)
        {
            lblStrand.Visible = false;
            lblGradelevel.Visible = false;
            lblSection.Visible = false;
            cmbStrand.Visible = false;
            cmbGradeLevels.Visible = false;
            cmbSections.Visible = false;
        }

        private void rbStudent_CheckedChanged(object sender, EventArgs e)
        {

            lblStrand.Visible = true;
            lblGradelevel.Visible = true;
            lblSection.Visible = true;
            cmbGradeLevels.Visible = true;
            cmbSections.Visible = true;
            cmbStrand.Visible = true;

        }
    }
}
