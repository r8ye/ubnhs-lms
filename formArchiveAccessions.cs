using copyyyy_lms;
using Microsoft.VisualBasic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ubnhs_lms
{
    public partial class formArchiveAccessions : Form
    {
        public formArchiveAccessions()
        {
            InitializeComponent();
            LoadArchivedCirculations();
        }

        private void LoadArchivedCirculations()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
            CirculationID,
            AccessionID,
            MemberID,
            AccessionNumber,
            Status,
            ISBN,
            Title,
            BorrowerID,
            FullName,
            IssueDate,
            DueDate,
            ReturnDate,
            DaysLate,
            PenaltyFee,
            Remarks
        FROM archived_circulations
        ORDER BY CirculationID DESC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvArchive.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                    DateTime currentDate = DateTime.Now;

                    string status = row["Status"].ToString();
                    if (status == "Issued" && dueDate < currentDate)
                    {
                        status += " (Overdue)";
                    }

                    dgvArchive.Rows.Add(
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

                dgvArchive.Columns["IssueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgvArchive.Columns["DueDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dgvArchive.Columns["ReturnDate"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
            List<int> selectedCirculationIDs = new List<int>();

            foreach (DataGridViewRow row in dgvArchive.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    int circulationID = Convert.ToInt32(row.Cells["CirculationID"].Value);
                    selectedCirculationIDs.Add(circulationID);
                }
            }

            if (selectedCirculationIDs.Count == 0)
            {
                MessageBox.Show("No records selected for restoration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                foreach (int circulationID in selectedCirculationIDs)
                {
                    string insertQuery = @"
            INSERT INTO circulations (
                CirculationID, AccessionID, MemberID, Status,
                IssueDate, DueDate, ReturnDate, DaysLate,
                PenaltyFee, Remarks
            )
            SELECT 
                c.CirculationID, c.AccessionID, c.MemberID, c.Status,
                c.IssueDate, c.DueDate, c.ReturnDate, c.DaysLate,
                c.PenaltyFee, c.Remarks
            FROM archived_circulations c
            WHERE c.CirculationID = @CirculationID";

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@CirculationID", circulationID);
                        insertCmd.ExecuteNonQuery();
                    }

                    string deleteQuery = "DELETE FROM archived_circulations WHERE CirculationID = @CirculationID";

                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@CirculationID", circulationID);
                        deleteCmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show($"{selectedCirculationIDs.Count} record(s) restored successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

            var form = Application.OpenForms.OfType<formTransaction>().FirstOrDefault();
            if (form != null)
            {
                form.LoadCirculations();
            }
            else
            {
                formTransaction newForm = new formTransaction();
                newForm.Show();
            }

        }

        private void cbX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
            SELECT 
                CirculationID,
                AccessionID,
                MemberID,
                AccessionNumber,
                Status,
                ISBN,
                Title,
                BorrowerID,
                FullName,
                IssueDate,
                DueDate,
                ReturnDate,
                DaysLate,
                PenaltyFee,
                Remarks
            FROM archived_circulations
            WHERE 
                AccessionNumber LIKE @search OR
                ISBN LIKE @search OR
                Title LIKE @search OR
                BorrowerID LIKE @search OR
                FullName LIKE @search OR
                Status LIKE @search
            ORDER BY CirculationID DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvArchive.Rows.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                        DateTime currentDate = DateTime.Now;

                        string status = row["Status"].ToString();
                        if (status == "Issued" && dueDate < currentDate)
                        {
                            status += " (Overdue)";
                        }

                        dgvArchive.Rows.Add(
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
                }
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
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
    }
}
