using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;

namespace ubnhs_lms
{
    public partial class formTransaction : Form
    {
        public formTransaction()
        {
            InitializeComponent();
        }

        private void formTransaction_Load(object sender, EventArgs e)
        {
            LoadCirculations();

            new classTotalCount(dgvCirculations, lblTotalCount);
            new classFilteringCirculations(dgvCirculations, cmbFilterStatus, cmbFilterRemarks);

            cmbFilterStatus.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterStatus.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbFilterStatus.Items.AddRange(new object[] { "All Status", "Issued", "Returned", "Damaged", "Lost" });
            cmbFilterStatus.SelectedIndex = 0;

            cmbFilterRemarks.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterRemarks.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterRemarks.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbFilterRemarks.Items.AddRange(new object[] { "All Remarks", "None", "Paid", "Unpaid" });
            cmbFilterRemarks.SelectedIndex = 0;

            

        }

        public void LoadCirculations()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                    c.CirculationID,
                    c.AccessionID,
                    c.MemberID,
                    c.Status,
                    acc.AccessionNumber,
                    b.ISBN,
                    b.Title,
                    m.BorrowerID,
                    CONCAT(m.LastName, ', ', m.FirstName, ' ', m.MiddleName) AS FullName,
                    c.IssueDate,
                    c.DueDate,
                    c.ReturnDate,
                    c.DaysLate,
                    c.PenaltyFee,
                    c.Remarks
                FROM circulations c
                INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                INNER JOIN members m ON c.MemberID = m.MemberID
                ORDER BY c.CirculationID DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvCirculations.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                    DateTime currentDate = DateTime.Now;

                    string status = row["Status"].ToString();
                    if (status == "Issued" && dueDate < currentDate)
                    {
                        status += " (Overdue)";
                    }

                    dgvCirculations.Rows.Add(
                        row["CirculationID"],
                        row["AccessionID"],
                        row["MemberID"],
                        row["AccessionNumber"],
                        status,
                        row["ISBN"],
                        row["Title"],
                        row["BorrowerID"],
                        row["FullName"],
                        Convert.ToDateTime(row["IssueDate"]).ToString("MM/dd/yyyy"),
                        dueDate.ToString("MM/dd/yyyy"),
                        row["ReturnDate"] == DBNull.Value ? "" : Convert.ToDateTime(row["ReturnDate"]).ToString("MM/dd/yyyy"),
                        row["DaysLate"] == DBNull.Value ? "" : row["DaysLate"].ToString(),
                        row["PenaltyFee"] == DBNull.Value ? "" : Convert.ToDecimal(row["PenaltyFee"]).ToString("0.00"),
                        row["Remarks"]
                    );
                }

                dgvCirculations.Columns["IssueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgvCirculations.Columns["DueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgvCirculations.Columns["ReturnDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                txtSearch.Clear();
            }
        }





        private void dgvCirculations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvCirculations.Columns.Contains("CirculationID"))
                {
                    int circulationID = Convert.ToInt32(dgvCirculations.Rows[e.RowIndex].Cells["CirculationID"].Value);
                    string status = dgvCirculations.Rows[e.RowIndex].Cells["Status"].Value?.ToString();
                    string remarks = dgvCirculations.Rows[e.RowIndex].Cells["Remarks"].Value?.ToString();

                    if (status == "Issued" && dgvCirculations.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        MessageBox.Show("Book is currently issued to a borrower.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (status == "Issued (Overdue)" && dgvCirculations.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        MessageBox.Show("Book is currently issued to a borrower.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //if (status == "Damaged" && dgvCirculations.Columns[e.ColumnIndex].Name == "Delete")
                    //{
                    //    MessageBox.Show("Damaged book cannot be deleted.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //if (status == "Lost" && dgvCirculations.Columns[e.ColumnIndex].Name == "Delete")
                    //{
                    //    MessageBox.Show("Lost book cannot be deleted.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    //if ((status == "Returned" || status == "Lost") && remarks == "Unpaid" && dgvCirculations.Columns[e.ColumnIndex].Name == "Delete")
                    //{
                    //    MessageBox.Show("Book is unpaid yet.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}

                    if (dgvCirculations.Columns[e.ColumnIndex].Name == "Update")
                    {
                        updCirculations updateTransaction = new updCirculations(circulationID);
                        updateTransaction.FormClosed += (s, args) => LoadCirculations();
                        updateTransaction.ShowDialog();
                    }
                    else if (dgvCirculations.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to archive this record?", "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            DeleteTransaction(circulationID);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The column 'CirculationID' does not exist in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

        // archive
        private void DeleteTransaction(int circulationID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string insertQuery = @"
            INSERT INTO archived_circulations (
                CirculationID, AccessionID, MemberID, Status,
                AccessionNumber, ISBN, Title, BorrowerID, FullName,
                IssueDate, DueDate, ReturnDate, DaysLate,
                PenaltyFee, Remarks
            )
            SELECT 
                c.CirculationID, c.AccessionID, c.MemberID, c.Status,
                acc.AccessionNumber, b.ISBN, b.Title, m.BorrowerID,
                CONCAT(m.LastName, ', ', m.FirstName, ' ', m.MiddleName) AS FullName,
                c.IssueDate, c.DueDate, c.ReturnDate, c.DaysLate,
                c.PenaltyFee, c.Remarks
            FROM circulations c
            JOIN accessions acc ON c.AccessionID = acc.AccessionID
            JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
            JOIN books b ON acq.BookID = b.BookID
            JOIN members m ON c.MemberID = m.MemberID
            WHERE c.CirculationID = @CirculationID";

                using (var insertCmd = new MySqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@CirculationID", circulationID);
                    insertCmd.ExecuteNonQuery();
                }

                string deleteQuery = "DELETE FROM circulations WHERE CirculationID = @CirculationID";
                using (var deleteCmd = new MySqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@CirculationID", circulationID);
                    deleteCmd.ExecuteNonQuery();
                }
                LoadCirculations();
            }
        }



        // borrow
        private void btnAdd_Click(object sender, EventArgs e)
        {
            addCirculations addFormTransaction = new addCirculations();
            addFormTransaction.FormClosed += (s, args) => LoadCirculations();
            addFormTransaction.ShowDialog();
        }

        // return
        private void btnReturn_Click(object sender, EventArgs e)
        {
            returnBook formReturnBook = new returnBook();
            formReturnBook.FormClosed += (s, args) => LoadCirculations();
            formReturnBook.ShowDialog();
        }

        private void dgvCirculations_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string status = dgvCirculations.Rows[e.RowIndex].Cells["Status"].Value?.ToString();
                string remarks = dgvCirculations.Rows[e.RowIndex].Cells["Remarks"].Value?.ToString();

                if (e.ColumnIndex == dgvCirculations.Columns["Status"].Index)
                {
                    if (status == "Returned")
                    {
                        e.CellStyle.ForeColor = Color.CornflowerBlue;
                    }
                    else if (status == "Issued")
                    {
                        e.CellStyle.ForeColor = Color.OliveDrab;
                    }
                    else if (status == "Damaged")
                    {
                        e.CellStyle.ForeColor = Color.Peru;
                    }
                    else if (status == "Lost")
                    {
                        e.CellStyle.ForeColor = Color.OrangeRed;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Gray;
                    }
                }
                else if (e.ColumnIndex == dgvCirculations.Columns["Remarks"].Index)
                {
                    if (remarks == "Paid")
                    {
                        e.CellStyle.ForeColor = Color.CornflowerBlue;
                    }
                    else if (remarks == "Unpaid")
                    {
                        e.CellStyle.ForeColor = Color.OrangeRed;
                    }
                    else if (remarks == "None")
                    {
                        e.CellStyle.ForeColor = Color.DimGray;
                    }
                }
            }
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
        }





        private void cmbFilterStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbFilterRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        public void SearchCirculations(string searchTerm)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                            c.CirculationID,
                            c.AccessionID,
                            c.MemberID,
                            c.Status,
                            acc.AccessionNumber,
                            b.ISBN,
                            b.Title,
                            m.BorrowerID,
                            CONCAT(m.LastName, ', ', m.FirstName, ' ', m.MiddleName) AS FullName,
                            c.IssueDate,
                            c.DueDate,
                            c.ReturnDate,
                            c.DaysLate,
                            c.PenaltyFee,
                            c.Remarks
                        FROM circulations c
                        INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                        INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                        INNER JOIN books b ON acq.BookID = b.BookID
                        INNER JOIN members m ON c.MemberID = m.MemberID
                        WHERE 
                            acc.AccessionNumber LIKE @SearchTerm OR
                            b.ISBN LIKE @SearchTerm OR
                            b.Title LIKE @SearchTerm OR
                            m.BorrowerID LIKE @SearchTerm OR
                            CONCAT(m.LastName, ', ', m.FirstName, ' ', m.MiddleName) LIKE @SearchTerm
                        ORDER BY c.CirculationID ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchTerm", "%" + searchTerm + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvCirculations.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvCirculations.Rows.Add(
                        row["CirculationID"],
                        row["AccessionID"],
                        row["MemberID"],
                        row["AccessionNumber"],
                        row["Status"],
                        row["ISBN"],
                        row["Title"],
                        row["BorrowerID"],
                        row["FullName"],
                        Convert.ToDateTime(row["IssueDate"]).ToString("MM/dd/yyyy"),
                        Convert.ToDateTime(row["DueDate"]).ToString("MM/dd/yyyy"),
                        row["ReturnDate"] == DBNull.Value ? "" : Convert.ToDateTime(row["ReturnDate"]).ToString("MM/dd/yyyy"),
                        row["DaysLate"] == DBNull.Value ? "" : row["DaysLate"].ToString(),
                        row["PenaltyFee"] == DBNull.Value ? "" : Convert.ToDecimal(row["PenaltyFee"]).ToString("0.00"),
                        row["Remarks"]
                    );
                }

                dgvCirculations.Columns["IssueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgvCirculations.Columns["DueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgvCirculations.Columns["ReturnDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchCirculations(txtSearch.Text);
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void cmbFilterRemarks_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
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

        

        private void btnRefreshDGV_Click(object sender, EventArgs e)
        {
            LoadCirculations();
        }

        private void btnViewArchive_Click(object sender, EventArgs e)
        {
            formArchiveAccessions archiveForm = new formArchiveAccessions();
            archiveForm.ShowDialog();
        }
    }
}
