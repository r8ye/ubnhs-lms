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
using System.Windows.Controls;
using System.Windows.Forms;
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;


namespace ubnhs_lms
{
    public partial class formMembers: Form
    {
        public formMembers()
        {
            InitializeComponent();
            LoadMembers();
        }

        public void LoadMembers(string borrowerType = null, string department = null, string strand = null, string gradeLevel = null, string section = null)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                    m.MemberID, 
                    m.BorrowerType, 
                    m.BorrowerID, 
                    m.LastName, 
                    m.FirstName, 
                    m.MiddleName, 
                    m.Email, 
                    d.DepartmentName, 
                    st.StrandName, 
                    g.GradeLevelName, 
                    s.SectionName,
                    m.Status  
                FROM members m 
                INNER JOIN departments d ON m.DepartmentID = d.DepartmentID 
                LEFT JOIN strands st ON m.StrandID = st.StrandID
                LEFT JOIN gradelevels g ON m.GradeLevelID = g.GradeLevelID 
                LEFT JOIN sections s ON m.SectionID = s.SectionID";

                List<string> conditions = new List<string>();
                if (!string.IsNullOrEmpty(borrowerType)) conditions.Add("m.BorrowerType = @BorrowerType");
                if (!string.IsNullOrEmpty(department)) conditions.Add("d.DepartmentName = @DepartmentName");
                if (!string.IsNullOrEmpty(strand)) conditions.Add("st.StrandName = @StrandName");
                if (!string.IsNullOrEmpty(gradeLevel)) conditions.Add("g.GradeLevelName = @GradeLevelName");
                if (!string.IsNullOrEmpty(section)) conditions.Add("s.SectionName = @SectionName");

                if (conditions.Count > 0)
                    query += " WHERE " + string.Join(" AND ", conditions);

                query += " ORDER BY m.LastName, m.FirstName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (!string.IsNullOrEmpty(borrowerType)) cmd.Parameters.AddWithValue("@BorrowerType", borrowerType);
                if (!string.IsNullOrEmpty(department)) cmd.Parameters.AddWithValue("@DepartmentName", department);
                if (!string.IsNullOrEmpty(strand)) cmd.Parameters.AddWithValue("@StrandName", strand);
                if (!string.IsNullOrEmpty(gradeLevel)) cmd.Parameters.AddWithValue("@GradeLevelName", gradeLevel);
                if (!string.IsNullOrEmpty(section)) cmd.Parameters.AddWithValue("@SectionName", section);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvMembers.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    string status = row["Status"]?.ToString() ?? "Not Present"; 
                    string action = status == "Present" ? "Deactivate" : "Activate";

                    dgvMembers.Rows.Add(
                        row["MemberID"],
                        row["BorrowerType"],
                        row["BorrowerID"],
                        row["LastName"],
                        row["FirstName"],
                        row["MiddleName"],
                        row["Email"],
                        row["DepartmentName"],
                        row["StrandName"],
                        row["GradeLevelName"],
                        row["SectionName"],
                        status,     
                        action     
                    );
                }
                txtSearch.Clear();
            }
        }


        private void formMembers_Load(object sender, EventArgs e)
        {
            dgvMembers.CellPainting += dgvMembers_CellPainting;

            cmbBorrowerTypes.Items.Clear();
            cmbBorrowerTypes.Items.Add("Borrower Types");
            cmbBorrowerTypes.Items.Add("Student");
            cmbBorrowerTypes.Items.Add("Faculty");

            cmbBorrowerTypes.SelectedIndex = 0;

            new classTotalCount(dgvMembers, lblTotalCount);

            cmbBorrowerTypes.DropDownStyle = ComboBoxStyle.DropDown;
            cmbBorrowerTypes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbBorrowerTypes.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterDepartments.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterDepartments.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterDepartments.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterStrands.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterStrands.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterStrands.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterGradeLevels.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterGradeLevels.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterGradeLevels.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbFilterSections.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterSections.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterSections.AutoCompleteSource = AutoCompleteSource.ListItems;

            LoadMembers();
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int memberID = Convert.ToInt32(dgvMembers.Rows[e.RowIndex].Cells["MemberID"].Value);
                string borrowerType = dgvMembers.Rows[e.RowIndex].Cells["BorrowerType"].Value?.ToString();
                string borrowerID = dgvMembers.Rows[e.RowIndex].Cells["BorrowerID"].Value?.ToString();
                string lastName = dgvMembers.Rows[e.RowIndex].Cells["LastName"].Value?.ToString();
                string firstName = dgvMembers.Rows[e.RowIndex].Cells["FirstName"].Value?.ToString();
                string middleName = dgvMembers.Rows[e.RowIndex].Cells["MiddleName"].Value?.ToString();
                string email = dgvMembers.Rows[e.RowIndex].Cells["Email"].Value?.ToString();
                string departmentName = dgvMembers.Rows[e.RowIndex].Cells["DepartmentName"].Value?.ToString();
                string strand = dgvMembers.Rows[e.RowIndex].Cells["StrandName"].Value?.ToString();
                string gradeLevelName = dgvMembers.Rows[e.RowIndex].Cells["GradeLevelName"].Value?.ToString();
                string sectionName = dgvMembers.Rows[e.RowIndex].Cells["SectionName"].Value?.ToString();

                if (dgvMembers.Columns[e.ColumnIndex].Name == "Update")
                {
                    updMembers updateMember = new updMembers(memberID, borrowerType, borrowerID, lastName, firstName,
                                                             middleName, email, departmentName, strand, gradeLevelName, sectionName);
                    updateMember.FormClosed += (s, args) => LoadMembers();
                    updateMember.ShowDialog();
                }

                else if (dgvMembers.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete",
                                                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteMember(memberID);
                    }
                }

                else if (dgvMembers.Columns[e.ColumnIndex].Name == "Action")
                {
                    string currentStatus = dgvMembers.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                    string newStatus = currentStatus == "Not Present" ? "Present" : "Not Present";
                    string newAction = newStatus == "Present" ? "Deactivate" : "Activate";

                    using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
                    {
                        conn.Open();

                        string query = "UPDATE members SET Status = @status WHERE memberID = @memberID";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@status", newStatus);
                            cmd.Parameters.AddWithValue("@memberID", memberID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    dgvMembers.Rows[e.RowIndex].Cells["Status"].Value = newStatus;
                    dgvMembers.Rows[e.RowIndex].Cells["Action"].Value = newAction;
                }

            }
        }

        private void DeleteMember(int memberID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string statusQuery = "SELECT Status FROM members WHERE MemberID = @MemberID";
                MySqlCommand statusCmd = new MySqlCommand(statusQuery, conn);
                statusCmd.Parameters.AddWithValue("@MemberID", memberID);
                string status = statusCmd.ExecuteScalar()?.ToString();

                if (status == "Present")
                {
                    MessageBox.Show("Cannot delete a member who is currently present in the library.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string checkQuery = "SELECT COUNT(*) FROM circulations WHERE MemberID = @MemberID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@MemberID", memberID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete member with existing transactions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MySqlCommand cmd = new MySqlCommand("DELETE FROM members WHERE MemberID = @MemberID", conn);
                cmd.Parameters.AddWithValue("@MemberID", memberID);
                cmd.ExecuteNonQuery();
            }

            LoadMembers();
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            addMembers addMembersForm = new addMembers();
            addMembersForm.FormClosed += (s, args) => LoadMembers();
            addMembersForm.ShowDialog();
        }

        private void cmbFilterDepartments_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbFilterGradeLevels_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbFilterSections_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbBorrowerTypes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void ApplyFilters()
        {
            string borrowerType = cmbBorrowerTypes.SelectedItem?.ToString();
            string department = cmbFilterDepartments.SelectedItem?.ToString();
            string strand = cmbFilterStrands.SelectedItem?.ToString();
            string gradeLevel = cmbFilterGradeLevels.SelectedItem?.ToString();
            string section = cmbFilterSections.SelectedItem?.ToString();

            classFilteringMembers filters = new classFilteringMembers(borrowerType, department, strand, gradeLevel, section);
            LoadMembers(filters.BorrowerType, filters.Department, filters.Strand, filters.GradeLevel, filters.Section);
        }


        private void cmbFilterDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();

        }

        private void cmbFilterGradeLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();

        }

        private void cmbFilterSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbBorrowerTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();

            if (cmbBorrowerTypes.SelectedIndex == 0)
            {
                LoadMembers();
            }
            else
            {
                ApplyFilters();
            }
        }

        private void cmbFilterStrands_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbFilterStrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
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

        private void cmbFilterSections_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbBorrowerTypes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dgvMembers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // button
            if (dgvMembers.Columns[e.ColumnIndex].Name == "Action" && e.RowIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                string actionText = dgvMembers.Rows[e.RowIndex].Cells["Action"].Value?.ToString();

                Color backColor = Color.White;
                Color foreColor = Color.Black;

                if (actionText == "Activate")
                {
                    backColor = Color.FromArgb(189, 253, 192);
                    foreColor = Color.FromArgb(3, 38, 0);
                }
                else if (actionText == "Deactivate")
                {
                    backColor = Color.FromArgb(255, 190, 195);
                    foreColor = Color.FromArgb(96, 0, 39);
                }

                Rectangle paddedRect = new Rectangle(
                    e.CellBounds.X + 10,
                    e.CellBounds.Y + 10,
                    e.CellBounds.Width - 20,
                    e.CellBounds.Height - 20
                );

                using (SolidBrush backBrush = new SolidBrush(backColor))
                using (SolidBrush textBrush = new SolidBrush(foreColor))
                using (StringFormat sf = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    e.Graphics.FillRectangle(backBrush, paddedRect);
                    e.Graphics.DrawString(actionText, dgvMembers.Font, textBrush, paddedRect, sf);
                }

                e.Handled = true;
                
            }

            
        }

        private void dgvMembers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMembers.Columns[e.ColumnIndex].Name == "BorrowerType" && e.Value != null)
            {
                string borrowerType = e.Value.ToString();

                if (borrowerType == "Faculty")
                {
                    e.CellStyle.ForeColor = Color.RoyalBlue;
                }
                else if (borrowerType == "Student")
                {
                    e.CellStyle.ForeColor = Color.SandyBrown;
                }
            }

            if (dgvMembers.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status == "Present")
                {
                    e.CellStyle.ForeColor = Color.YellowGreen;
                }
                else if (status == "Not Present")
                {
                    e.CellStyle.ForeColor = Color.OrangeRed;
                }
            }
        }

        private void SearchMembers(string searchTerm)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    m.MemberID, 
                    m.BorrowerType, 
                    m.BorrowerID, 
                    m.LastName, 
                    m.FirstName, 
                    m.MiddleName, 
                    m.Email, 
                    d.DepartmentName, 
                    st.StrandName, 
                    g.GradeLevelName, 
                    s.SectionName,
                    m.Status  
                FROM members m 
                INNER JOIN departments d ON m.DepartmentID = d.DepartmentID 
                LEFT JOIN strands st ON m.StrandID = st.StrandID
                LEFT JOIN gradelevels g ON m.GradeLevelID = g.GradeLevelID 
                LEFT JOIN sections s ON m.SectionID = s.SectionID
                WHERE m.BorrowerID LIKE @SearchTerm 
                    OR m.LastName LIKE @SearchTerm 
                    OR m.FirstName LIKE @SearchTerm
                    OR m.Status LIKE @SearchTerm"; 

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvMembers.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    string status = row["Status"]?.ToString() ?? "Not Present";
                    string action = status == "Present" ? "Deactivate" : "Activate";

                    dgvMembers.Rows.Add(
                        row["MemberID"],
                        row["BorrowerType"],
                        row["BorrowerID"],
                        row["LastName"],
                        row["FirstName"],
                        row["MiddleName"],
                        row["Email"],
                        row["DepartmentName"],
                        row["StrandName"],
                        row["GradeLevelName"],
                        row["SectionName"],
                        status,
                        action
                    );
                }
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            SearchMembers(searchTerm);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '-')
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
