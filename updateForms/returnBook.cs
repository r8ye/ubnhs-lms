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

namespace ubnhs_lms.updateForms
{
    public partial class returnBook : Form
    {
        
        public returnBook()
        {
            InitializeComponent();
            
        }

        private void returnBook_Load(object sender, EventArgs e)
        {
            LoadAccessionNumbers();
            cmbStatus.Items.AddRange(new object[] { "Select", "Returned", "Damaged", "Lost" });
            cmbStatus.SelectedIndex = 0;
            dtpReturnDate.Value = DateTime.Now;
            dtpReturnDate.MaxDate = DateTime.Now;

            cmbAccessionNumber.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAccessionNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAccessionNumber.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void LoadAccessionNumbers()
        {
            cmbAccessionNumber.Items.Clear();
            cmbAccessionNumber.Items.Add("Select");

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT acc.AccessionNumber
                                FROM circulations c
                                INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                                WHERE c.Status = 'Issued'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
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

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                                c.CirculationID, c.AccessionID, c.MemberID, c.IssueDate, c.DueDate,
                                b.ISBN, b.Title,
                                m.BorrowerID, CONCAT(m.LastName, ', ', m.FirstName, ' ', m.MiddleName) AS FullName,
                                m.BorrowerType
                                FROM circulations c
                                INNER JOIN accessions acc ON c.AccessionID = acc.AccessionID
                                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                                INNER JOIN books b ON acq.BookID = b.BookID
                                INNER JOIN members m ON c.MemberID = m.MemberID
                                WHERE acc.AccessionNumber = @AccessionNumber AND c.Status = 'Issued'";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccessionNumber", cmbAccessionNumber.Text);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cmbISBN.Text = reader["ISBN"].ToString();
                        cmbTitle.Text = reader["Title"].ToString();
                        cmbBorrowerID.Text = reader["BorrowerID"].ToString();
                        cmbFullName.Text = reader["FullName"].ToString();
                        dtpIssueDate.Value = Convert.ToDateTime(reader["IssueDate"]);
                        dtpDueDate.Value = Convert.ToDateTime(reader["DueDate"]);

                        cmbISBN.Enabled = false;
                        cmbTitle.Enabled = false;
                        cmbBorrowerID.Enabled = false;
                        cmbFullName.Enabled = false;
                        dtpIssueDate.Enabled = false;
                        dtpDueDate.Enabled = false;
                    }
                }
            }
        }

        


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbAccessionNumber.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select an Accession Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStatus.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a Status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            txtRemarks.Text = SetRemarks(cmbStatus.Text, daysLate);

            UpdateCirculationAndAccession(cmbAccessionNumber.Text, cmbStatus.Text, dtpReturnDate.Value, daysLate, penaltyFee);

            MessageBox.Show(
                "Return book successfully saved.\n\n" +
                $"Status: {cmbStatus.Text}\n" +
                $"Days Late: {daysLate}\n" +
                $"Penalty Fee: {breakdown}\n",
                //$"Remarks: {txtRemarks.Text}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.Close();

        }

        private string SetRemarks(string status, int daysLate)
        {
            if (status == "Returned" && daysLate == 0)
            {
                return "None";
            }
            else if (status == "Lost" || status == "Damaged" || (status == "Returned" && daysLate > 0))
            {
                return "Unpaid";
            }
            return "Paid";


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
                         WHERE acc.AccessionNumber = @AccessionNumber";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AccessionNumber", cmbAccessionNumber.Text);

                    var result = cmd.ExecuteScalar();
                    return result?.ToString().Trim() ?? "";
                }
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




        private void UpdateCirculationAndAccession(string accessionNumber, string newStatus, DateTime returnDate, int daysLate, decimal penaltyFee)
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
                             WHERE acc.AccessionNumber = @AccessionNumber AND c.Status = 'Issued'";

                MySqlCommand cmd = new MySqlCommand(updateCirculation, conn);
                cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                cmd.Parameters.AddWithValue("@ReturnDate", returnDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@DaysLate", daysLate);
                cmd.Parameters.AddWithValue("@PenaltyFee", penaltyFee);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                cmd.Parameters.AddWithValue("@AccessionNumber", accessionNumber);
                cmd.ExecuteNonQuery();

                string accessionStatus = (newStatus == "Returned") ? "Available" : newStatus;

                string updateAccession = @"UPDATE accessions 
                           SET Status = @AccessionStatus 
                           WHERE AccessionNumber = @AccessionNumber";
                MySqlCommand cmd2 = new MySqlCommand(updateAccession, conn);
                cmd2.Parameters.AddWithValue("@AccessionStatus", accessionStatus);
                cmd2.Parameters.AddWithValue("@AccessionNumber", accessionNumber);
                cmd2.ExecuteNonQuery();
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
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
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
    }
}
