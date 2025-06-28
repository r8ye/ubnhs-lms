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

namespace ubnhs_lms.updateForms
{
    public partial class updCirculations : Form
    {
        private int circulationID;
        public updCirculations(int circulationID)
        {
            InitializeComponent();
            this.circulationID = circulationID;

            txtPenaltyFee.Enabled = false;
            txtDaysLate.Enabled = false;
        }

        private void updCirculations_Load(object sender, EventArgs e)
        {
            LoadAccessionNumbers();

            cmbStatus.Items.AddRange(new object[] { "Select", "Returned", "Damaged", "Lost", "Issued" });
            cmbRemarks.Items.AddRange(new object[] { "None", "Unpaid", "Paid" });

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }
            if (cmbRemarks.Items.Count > 0)
            {
                cmbRemarks.SelectedIndex = 0;
            }

            dtpIssueDate.Value = DateTime.Now;
            dtpIssueDate.MaxDate = DateTime.Now;

            //dtpReturnDate.Value = DateTime.Now;
            dtpReturnDate.MaxDate = DateTime.Now;

            LoadCirculationDetails();

            cmbAccessionNumber.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAccessionNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAccessionNumber.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void LoadCirculationDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
            SELECT 
                c.CirculationID, c.AccessionID, c.MemberID, c.IssueDate, c.DueDate,
                b.ISBN, b.Title,
                m.BorrowerID, CONCAT(m.LastName, ', ', m.FirstName, ' ', m.MiddleName) AS FullName,
                c.Status, c.ReturnDate, c.DaysLate, c.PenaltyFee, c.Remarks,
                acc.AccessionNumber
            FROM circulations c
            INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
            INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
            INNER JOIN books b ON acq.BookID = b.BookID
            INNER JOIN members m ON c.MemberID = m.MemberID
            WHERE c.CirculationID = @CirculationID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CirculationID", circulationID);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cmbAccessionNumber.Text = reader["AccessionNumber"].ToString();
                        int index = cmbAccessionNumber.Items.IndexOf(reader["AccessionNumber"].ToString());
                        if (index >= 0)
                        {
                            cmbAccessionNumber.SelectedIndex = index;
                        }

                        cmbISBN.Text = reader["ISBN"].ToString();
                        cmbTitle.Text = reader["Title"].ToString();
                        cmbBorrowerID.Text = reader["BorrowerID"].ToString();
                        cmbFullName.Text = reader["FullName"].ToString();
                        dtpIssueDate.Value = Convert.ToDateTime(reader["IssueDate"]);
                        dtpDueDate.Value = Convert.ToDateTime(reader["DueDate"]);
                        cmbStatus.SelectedItem = reader["Status"].ToString();

                        if (reader["ReturnDate"] != DBNull.Value)
                        {
                            DateTime returnDate = Convert.ToDateTime(reader["ReturnDate"]);
                            if (returnDate >= dtpReturnDate.MinDate && returnDate <= dtpReturnDate.MaxDate)
                            {
                                dtpReturnDate.Value = returnDate;
                            }
                            else
                            {
                                dtpReturnDate.Value = dtpReturnDate.MinDate;
                            }
                        }

                        else
                        {
                            if (DateTime.Now >= dtpReturnDate.MinDate && DateTime.Now <= dtpReturnDate.MaxDate)
                            {
                                dtpReturnDate.Value = DateTime.Now;
                            }
                            else
                            {
                                dtpReturnDate.Value = dtpReturnDate.MinDate;
                            }
                        }



                        txtDaysLate.Text = reader["DaysLate"].ToString();
                        txtPenaltyFee.Text = reader["PenaltyFee"].ToString();
                        cmbRemarks.SelectedItem = reader["Remarks"].ToString();
                    }
                }
            }
        }
 

        private void LoadAccessionNumbers()
        {
            cmbAccessionNumber.Items.Clear();
            cmbAccessionNumber.Items.Add("Select");

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = @"
            SELECT DISTINCT acc.AccessionNumber
            FROM accessions acc
            INNER JOIN circulations c ON c.AccessionID = acc.AccessionID
            WHERE c.Status = 'Issued'
               OR c.CirculationID = @CirculationID";  

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CirculationID", circulationID);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbAccessionNumber.Items.Add(reader["AccessionNumber"].ToString());
                    }
                }
            }

            cmbAccessionNumber.SelectedIndex = 0;
        }


        private void cmbAccessionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccessionNumber.SelectedIndex <= 0)
            {
                ClearFields();
                return;
            }
            LoadCirculationDetails();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (cmbAccessionNumber.SelectedIndex <= 0)
            //{
            //    MessageBox.Show("Please select an Accession Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (cmbStatus.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a Status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus.Text == "Issued")
            {
                AdjustDueDateOnly();

                using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();
                    string updateIssued = @"UPDATE circulations c
                                    INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                                    SET c.IssueDate = @IssueDate,
                                        c.DueDate = @DueDate
                                    WHERE c.CirculationID = @CirculationID";

                    MySqlCommand cmd = new MySqlCommand(updateIssued, conn);
                    cmd.Parameters.AddWithValue("@IssueDate", dtpIssueDate.Value);
                    cmd.Parameters.AddWithValue("@DueDate", dtpDueDate.Value);
                    cmd.Parameters.AddWithValue("@CirculationID", circulationID);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(
                    "Circulation updated successfully.\n\n" +
                    $"Status: {cmbStatus.Text}\n" +
                    $"New Issue Date: {dtpIssueDate.Value:yyyy-MM-dd}\n" +
                    $"New Due Date: {dtpDueDate.Value:yyyy-MM-dd}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
            else
            {
                string borrowerType = GetBorrowerType();
                int daysLate;
                string breakdown;
                decimal penaltyFee = ComputePenaltyFee(borrowerType, cmbStatus.Text, dtpIssueDate.Value, dtpReturnDate.Value, out daysLate, out breakdown);

                if (cmbStatus.Text == "Returned" && daysLate == 0)
                {
                    penaltyFee = 0;
                }

                txtDaysLate.Text = daysLate.ToString();
                txtPenaltyFee.Text = penaltyFee.ToString("0.00");

                string remarks = cmbRemarks.SelectedItem?.ToString() ?? "None";
                cmbRemarks.Text = remarks;

                UpdateCirculationAndAccession(cmbAccessionNumber.Text, cmbStatus.Text, dtpReturnDate.Value, daysLate, penaltyFee, remarks);

                MessageBox.Show(
                    "Circulation updated successfully.\n\n" +
                    $"Status: {cmbStatus.Text}\n" +
                    $"Days Late: {daysLate}\n" +
                    $"Penalty Fee: {breakdown}\n" +
                    $"Remarks: {remarks}",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.Close();
            }
        }

        private void AdjustDueDateOnly()
        {
            int borrowDuration = 0;

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT borrowDuration FROM penaltysettings WHERE borrowerType = @BorrowerType";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BorrowerType", GetBorrowerType());

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        borrowDuration = Convert.ToInt32(reader["borrowDuration"]);
                    }
                }
            }

            dtpDueDate.Value = dtpIssueDate.Value.AddDays(borrowDuration);
        }

        private void AdjustControlsBasedOnStatus()
        {
            bool isIssued = cmbStatus.Text == "Issued";

            dtpReturnDate.Enabled = !isIssued;
            txtDaysLate.Enabled = !isIssued;
            txtPenaltyFee.Enabled = !isIssued;
            cmbRemarks.Enabled = !isIssued;
            cmbStatus.Enabled  = !isIssued;
            lblReturnDate.Visible = !isIssued;
            dtpReturnDate.Visible = !isIssued;
        }



        private string GetBorrowerType()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT m.BorrowerType
                                FROM circulations c
                                INNER JOIN members m ON c.MemberID = m.MemberID
                                INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                                WHERE c.CirculationID = @CirculationID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CirculationID", circulationID);

                return cmd.ExecuteScalar()?.ToString() ?? "";
            }
        }

        private decimal ComputePenaltyFee(string borrowerType, string status, DateTime issueDate, DateTime returnDate, out int daysLate, out string breakdown)
        {
            decimal overdueFee = 0;
            decimal lostItemFee = 0;
            decimal damageCharge = 0;
            int borrowDuration = 0;
            breakdown = "";

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT borrowDuration, overdueFee, lostItemFee, damageCharge
                         FROM penaltysettings
                         WHERE borrowerType = @BorrowerType";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BorrowerType", borrowerType);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        borrowDuration = Convert.ToInt32(reader["borrowDuration"]);
                        overdueFee = Convert.ToDecimal(reader["overdueFee"]);
                        lostItemFee = Convert.ToDecimal(reader["lostItemFee"]);
                        damageCharge = Convert.ToDecimal(reader["damageCharge"]);
                    }
                }
            }

            DateTime computedDueDate = issueDate.AddDays(borrowDuration);
            daysLate = (returnDate.Date - computedDueDate.Date).Days;
            if (daysLate < 0) daysLate = 0;

            decimal penalty = 0;

            if (status == "Lost")
            {
                if (daysLate > 0)
                {
                    penalty = lostItemFee + (daysLate * overdueFee);
                    breakdown = $"₱{lostItemFee:0.00} (Lost Fee) + ₱{daysLate * overdueFee:0.00} (Overdue)";
                }
                else
                {
                    penalty = lostItemFee;
                    breakdown = $"₱{lostItemFee:0.00} (Lost Fee)";
                }
            }
            else if (status == "Damaged")
            {
                if (daysLate > 0)
                {
                    penalty = damageCharge + (daysLate * overdueFee);
                    breakdown = $"₱{damageCharge:0.00} (Damage Fee) + ₱{daysLate * overdueFee:0.00} (Overdue)";
                }
                else
                {
                    penalty = damageCharge;
                    breakdown = $"₱{damageCharge:0.00} (Damage Fee)";
                }
            }
            else if (status == "Returned")
            {
                if (daysLate > 0)
                {
                    penalty = daysLate * overdueFee;
                    breakdown = $"₱{penalty:0.00} (Overdue)";
                }
                else
                {
                    penalty = 0;
                    breakdown = "₱0.00 (Returned on time)";
                }
            }

            return penalty;
        }

        private void UpdateCirculationAndAccession(string accessionNumber, string newStatus, DateTime returnDate, int daysLate, decimal penaltyFee, string remarks)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string updateCirculation = @"UPDATE circulations c
                                      INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                                      SET c.Status = @NewStatus,
                                          c.ReturnDate = @ReturnDate,
                                          c.DaysLate = @DaysLate,
                                          c.PenaltyFee = @PenaltyFee,
                                          c.Remarks = @Remarks
                                      WHERE c.CirculationID = @CirculationID";

                MySqlCommand cmdCirculation = new MySqlCommand(updateCirculation, conn);
                cmdCirculation.Parameters.AddWithValue("@NewStatus", newStatus);
                cmdCirculation.Parameters.AddWithValue("@ReturnDate", returnDate);
                cmdCirculation.Parameters.AddWithValue("@DaysLate", daysLate);
                cmdCirculation.Parameters.AddWithValue("@PenaltyFee", penaltyFee);
                cmdCirculation.Parameters.AddWithValue("@Remarks", remarks);
                cmdCirculation.Parameters.AddWithValue("@CirculationID", circulationID);

                cmdCirculation.ExecuteNonQuery();
                string updatedAccessionStatus = "";

                if (newStatus == "Returned")
                    updatedAccessionStatus = "Available";
                else if (newStatus == "Lost")
                    updatedAccessionStatus = "Lost";
                else if (newStatus == "Damaged")
                    updatedAccessionStatus = "Damaged";
                else if (newStatus == "Issued")
                    updatedAccessionStatus = "Issued";

                if (!string.IsNullOrEmpty(updatedAccessionStatus))
                {
                    string updateAccessions = @"UPDATE accessions 
                                        SET Status = @AccessionsStatus 
                                        WHERE AccessionNumber = @AccessionNumber";

                    MySqlCommand cmdAccessions = new MySqlCommand(updateAccessions, conn);
                    cmdAccessions.Parameters.AddWithValue("@AccessionsStatus", updatedAccessionStatus);
                    cmdAccessions.Parameters.AddWithValue("@AccessionNumber", accessionNumber);

                    cmdAccessions.ExecuteNonQuery();
                }
            }
        }



        private void ClearFields()
        {
            cmbISBN.Text = "";
            cmbTitle.Text = "";
            cmbBorrowerID.Text = "";
            cmbFullName.Text = "";
            dtpIssueDate.Value = DateTime.Now;
            dtpDueDate.Value = DateTime.Now;

            if (cmbStatus.Items.Count > 0)
            {
                cmbStatus.SelectedIndex = 0;
            }

            txtDaysLate.Text = "";
            txtPenaltyFee.Text = "";

            if (cmbRemarks.Items.Count > 0)
            {
                cmbRemarks.SelectedIndex = 0;
            }
        }

        private void dtpIssueDate_ValueChanged(object sender, EventArgs e)
        {
            AdjustDueDateAndPenalties();
        }

        private void dtpReturnDate_ValueChanged(object sender, EventArgs e)
        {
            AdjustDueDateAndPenalties();

            dtpReturnDate.MinDate = dtpIssueDate.Value;
        }

        private void AdjustDueDateAndPenalties()
        {
            DateTime issueDate = dtpIssueDate.Value;
            int borrowDuration = 0;

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT borrowDuration FROM penaltysettings WHERE borrowerType = @BorrowerType";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BorrowerType", GetBorrowerType());

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        borrowDuration = Convert.ToInt32(reader["borrowDuration"]);
                    }
                }
            }

            dtpDueDate.Value = issueDate.AddDays(borrowDuration);

            RecalculatePenalties();
        }

        private void RecalculatePenalties()
        {
            string borrowerType = GetBorrowerType();
            int daysLate;
            string breakdown;
            decimal penaltyFee = ComputePenaltyFee(borrowerType, cmbStatus.Text, dtpIssueDate.Value, dtpReturnDate.Value, out daysLate, out breakdown);

            if (cmbStatus.Text == "Returned" && daysLate == 0)
            {
                penaltyFee = 0;
               
            }

            txtDaysLate.Text = daysLate.ToString();
            txtPenaltyFee.Text = penaltyFee.ToString("0.00");
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem != null)
            {
                string selectedStatus = cmbStatus.SelectedItem.ToString();

                if (selectedStatus == "Damaged" || selectedStatus == "Lost" || selectedStatus == "Returned")
                {
                    for (int i = cmbStatus.Items.Count - 1; i >= 0; i--)
                    {
                        if (cmbStatus.Items[i].ToString() == "Issued")
                        {
                            cmbStatus.Items.RemoveAt(i);
                            break; 
                        }
                    }
                    cmbRemarks.SelectedItem = "Unpaid";
                }
                else
                {
                    if (!cmbStatus.Items.Contains("Issued"))
                    {
                        cmbStatus.Items.Add("Issued");
                    }
                }
            }

            AdjustControlsBasedOnStatus();
        }

        private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void cmbRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void cmbAccessionNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }
    }
}
