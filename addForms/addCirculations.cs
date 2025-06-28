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
    public partial class addCirculations : Form
    {
        public addCirculations()
        {
            InitializeComponent();
        }

        private void addCirculations_Load(object sender, EventArgs e)
        {
            LoadAvailableAccessions();
            LoadPresentMembers();
            cmbStatus.Text = "Issued";
            dtpIssueDate.Value = DateTime.Today;
            dtpIssueDate.MaxDate = DateTime.Today;
            dtpIssueDate.MinDate = DateTime.Today.AddDays(-7);

            cmbAccessionNumber.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAccessionNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAccessionNumber.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbBorrowerID.DropDownStyle = ComboBoxStyle.DropDown;
            cmbBorrowerID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbBorrowerID.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void LoadAvailableAccessions()
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                    acc.AccessionID, 
                    acc.AccessionNumber, 
                    b.ISBN, 
                    b.Title 
                 FROM accessions acc
                 INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                 INNER JOIN books b ON acq.BookID = b.BookID
                 WHERE acc.Status = 'Available'";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        DataRow newRow = dt.NewRow();
                        newRow["AccessionID"] = 0;
                        newRow["AccessionNumber"] = "Select";
                        newRow["ISBN"] = "";
                        newRow["Title"] = "";
                        dt.Rows.InsertAt(newRow, 0);

                        cmbAccessionNumber.DataSource = dt;
                        cmbAccessionNumber.DisplayMember = "AccessionNumber";
                        cmbAccessionNumber.ValueMember = "AccessionID";
                        cmbAccessionNumber.SelectedIndex = 0;
                    }
                }
            }
        }


        private void LoadPresentMembers()
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT MemberID, BorrowerID FROM members WHERE Status = 'Present'";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        DataRow newRow = dt.NewRow();
                        newRow["MemberID"] = 0;
                        newRow["BorrowerID"] = "Select";
                        dt.Rows.InsertAt(newRow, 0);

                        cmbBorrowerID.DataSource = dt;
                        cmbBorrowerID.DisplayMember = "BorrowerID";
                        cmbBorrowerID.ValueMember = "MemberID";
                        cmbBorrowerID.SelectedIndex = 0;
                    }
                }
            }
        }

        private void cmbAccessionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccessionNumber.SelectedIndex != -1)
            {
                int accessionID = Convert.ToInt32(((DataRowView)cmbAccessionNumber.SelectedItem)["AccessionID"]);
                LoadAccessionDetails(accessionID);
            }
        }

        private void LoadAccessionDetails(int accessionID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                                    b.ISBN, 
                                    b.Title 
                                 FROM accessions acc
                                 INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                                 INNER JOIN books b ON acq.BookID = b.BookID
                                 WHERE acc.AccessionID = @AccessionID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccessionID", accessionID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbISBN.Text = reader["ISBN"].ToString();
                            cmbTitle.Text = reader["Title"].ToString();
                        }
                    }
                }
            }
        }

        private void cmbBorrowerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBorrowerID.SelectedIndex != -1)
            {
                int memberID = Convert.ToInt32(((DataRowView)cmbBorrowerID.SelectedItem)["MemberID"]);
                LoadMemberFullName(memberID);
                SetDueDate(memberID);
            }
        }

        private void LoadMemberFullName(int memberID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT LastName, FirstName, MiddleName FROM members WHERE MemberID = @MemberID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fullName = $"{reader["LastName"]}, {reader["FirstName"]} {reader["MiddleName"]}";
                            cmbFullName.Text = fullName;
                        }
                    }
                }
            }
        }


        private void SetDueDate(int memberID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT ps.borrowDuration, m.BorrowerType
                         FROM penaltysettings ps
                         CROSS JOIN members m
                         WHERE m.MemberID = @MemberID
                         AND ps.borrowerType = m.BorrowerType";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MemberID", memberID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int borrowDuration = Convert.ToInt32(reader["borrowDuration"]);
                            dtpDueDate.Value = dtpIssueDate.Value.AddDays(borrowDuration);
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (cmbAccessionNumber.SelectedIndex == 0 || cmbBorrowerID.SelectedIndex == 0)
            {
                MessageBox.Show("Please fill in all required fields correctly.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberID = Convert.ToInt32(cmbBorrowerID.SelectedValue);

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string borrowerType = "";
                string getTypeQuery = "SELECT BorrowerType FROM members WHERE MemberID = @MemberID";
                using (var cmdType = new MySqlCommand(getTypeQuery, conn))
                {
                    cmdType.Parameters.AddWithValue("@MemberID", memberID);
                    borrowerType = cmdType.ExecuteScalar()?.ToString();
                }

                if (string.IsNullOrEmpty(borrowerType))
                {
                    MessageBox.Show("Cannot find Borrower Type for this member.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int borrowLimit = 0;
                string getLimitQuery = "SELECT borrowLimit FROM penaltysettings WHERE borrowerType = @BorrowerType";
                using (var cmdLimit = new MySqlCommand(getLimitQuery, conn))
                {
                    cmdLimit.Parameters.AddWithValue("@BorrowerType", borrowerType);
                    borrowLimit = Convert.ToInt32(cmdLimit.ExecuteScalar());
                }

                int currentBorrowed = 0;
                string countQuery = @"SELECT COUNT(*) FROM circulations 
                      WHERE MemberID = @MemberID AND Status = 'Issued'";
                using (var cmdCount = new MySqlCommand(countQuery, conn))
                {
                    cmdCount.Parameters.AddWithValue("@MemberID", memberID);
                    currentBorrowed = Convert.ToInt32(cmdCount.ExecuteScalar());
                }

                if (currentBorrowed >= borrowLimit)
                {
                    MessageBox.Show("This member has already reached the borrow limit.", "Borrow Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int accessionID = Convert.ToInt32(cmbAccessionNumber.SelectedValue);

                string isbnToCheck = "";
                string isbnQuery = @"SELECT b.ISBN
                            FROM accessions acc
                            INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                            INNER JOIN books b ON acq.BookID = b.BookID
                            WHERE acc.AccessionID = @AccessionID";

                using (var cmdISBN = new MySqlCommand(isbnQuery, conn))
                {
                    cmdISBN.Parameters.AddWithValue("@AccessionID", accessionID);
                    isbnToCheck = cmdISBN.ExecuteScalar()?.ToString();
                }

                string checkExisting = @"SELECT COUNT(*) FROM circulations c
                                 INNER JOIN accessions a ON c.AccessionID = a.AccessionID
                                 INNER JOIN acquisitions aq ON a.AcquisitionID = aq.AcquisitionID
                                 INNER JOIN books b ON aq.BookID = b.BookID
                                 WHERE c.MemberID = @MemberID AND b.ISBN = @ISBN AND c.Status = 'Issued'";

                using (var cmdCheck = new MySqlCommand(checkExisting, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@MemberID", memberID);
                    cmdCheck.Parameters.AddWithValue("@ISBN", isbnToCheck);

                    int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("This member has already borrowed a book with the same ISBN.", "Duplicate ISBN Borrow", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string insertQuery = @"INSERT INTO circulations 
                 (AccessionID, MemberID, IssueDate, DueDate, Status)
                 VALUES (@AccessionID, @MemberID, @IssueDate, @DueDate, @Status)";
                using (var cmdInsert = new MySqlCommand(insertQuery, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@AccessionID", cmbAccessionNumber.SelectedValue);
                    cmdInsert.Parameters.AddWithValue("@MemberID", cmbBorrowerID.SelectedValue);
                    cmdInsert.Parameters.AddWithValue("@IssueDate", dtpIssueDate.Value.ToString("yyyy-MM-dd"));
                    cmdInsert.Parameters.AddWithValue("@DueDate", dtpDueDate.Value.ToString("yyyy-MM-dd"));
                    cmdInsert.Parameters.AddWithValue("@Status", cmbStatus.Text);
                    cmdInsert.ExecuteNonQuery();
                }

                string updateAccession = "UPDATE accessions SET Status = 'Issued' WHERE AccessionID = @AccessionID";
                using (var updateCmd = new MySqlCommand(updateAccession, conn))
                {
                    updateCmd.Parameters.AddWithValue("@AccessionID", cmbAccessionNumber.SelectedValue);
                    updateCmd.ExecuteNonQuery();
                }

                int updatedBorrowedCount = 0;
                string updatedCountQuery = @"SELECT COUNT(*) FROM circulations 
                             WHERE MemberID = @MemberID AND Status = 'Issued'";
                using (var cmdUpdatedCount = new MySqlCommand(updatedCountQuery, conn))
                {
                    cmdUpdatedCount.Parameters.AddWithValue("@MemberID", memberID);
                    updatedBorrowedCount = Convert.ToInt32(cmdUpdatedCount.ExecuteScalar());
                }

                MessageBox.Show($"Book successfully borrowed!\n\nBorrow Count: {updatedBorrowedCount}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void UpdateDueDate()
        {
            if (cmbBorrowerID.SelectedIndex > 0)
            {
                int memberID = Convert.ToInt32(cmbBorrowerID.SelectedValue);

                using (var conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT ps.borrowDuration, m.BorrowerType
                             FROM penaltysettings ps
                             CROSS JOIN members m
                             WHERE m.MemberID = @MemberID
                             AND ps.borrowerType = m.BorrowerType";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberID", memberID);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int borrowDuration = Convert.ToInt32(reader["borrowDuration"]);
                                dtpDueDate.Value = dtpIssueDate.Value.AddDays(borrowDuration);
                            }
                        }
                    }
                }
            }
        }

        private void dtpIssueDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateDueDate();
        }

        private void cmbAccessionNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (e.KeyChar != 'a' && !char.IsDigit(e.KeyChar) && e.KeyChar != 'A' && e.KeyChar != '-' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void cmbAccessionNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbBorrowerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void cmbBorrowerID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
