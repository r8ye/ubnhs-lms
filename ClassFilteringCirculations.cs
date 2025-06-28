using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ubnhs_lms
{
    public class classFilteringCirculations
    {
        private DataGridView dgvCirculations;
        private ComboBox cmbFilterStatus;
        private ComboBox cmbFilterRemarks;

        public classFilteringCirculations(DataGridView dgv, ComboBox cmbStatus, ComboBox cmbRemarks)
        {
            dgvCirculations = dgv;
            cmbFilterStatus = cmbStatus;
            cmbFilterRemarks = cmbRemarks;

            cmbFilterStatus.SelectedIndexChanged += new EventHandler(FilterChanged);
            cmbFilterRemarks.SelectedIndexChanged += new EventHandler(FilterChanged);
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            if (cmbFilterStatus.Text == "Issued")
            {
                cmbFilterRemarks.Enabled = false;
                cmbFilterRemarks.SelectedIndex = 0; 
            }
            else
            {
                cmbFilterRemarks.Enabled = true;
            }

            FilterCirculations();
        }

        private void FilterCirculations()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string baseQuery = @"SELECT 
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
                INNER JOIN members m ON c.MemberID = m.MemberID";

                string filter = " WHERE 1=1";

                if (!string.IsNullOrEmpty(cmbFilterStatus.Text) && cmbFilterStatus.Text != "All Status")
                {
                    filter += " AND c.Status = @Status";
                }

                if (cmbFilterRemarks.Enabled && !string.IsNullOrEmpty(cmbFilterRemarks.Text) && cmbFilterRemarks.Text != "All Remarks")
                {
                    filter += " AND c.Remarks = @Remarks";
                }

                string finalQuery = baseQuery + filter + " ORDER BY c.CirculationID DESC";

                using (MySqlCommand cmd = new MySqlCommand(finalQuery, conn))
                {
                    if (!string.IsNullOrEmpty(cmbFilterStatus.Text) && cmbFilterStatus.Text != "All Status")
                    {
                        cmd.Parameters.AddWithValue("@Status", cmbFilterStatus.Text);
                    }
                    if (cmbFilterRemarks.Enabled && !string.IsNullOrEmpty(cmbFilterRemarks.Text) && cmbFilterRemarks.Text != "All Remarks")
                    {
                        cmd.Parameters.AddWithValue("@Remarks", cmbFilterRemarks.Text);
                    }

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
                }
            }
        }
    }
}
