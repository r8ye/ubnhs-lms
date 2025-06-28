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
    public partial class addAccessions : Form
    {
        private Guna.UI2.WinForms.Guna2DataGridView dgvAccessions;
        

        public addAccessions(Guna.UI2.WinForms.Guna2DataGridView dgvAccessions)
        {
            InitializeComponent();
            this.dgvAccessions = dgvAccessions;  
            LoadTransactionNumbers();
            LoadStatusAndCondition();

            cmbTransactionNumber.SelectedIndexChanged += cmbTransactionNumber_SelectedIndexChanged;
        }

        public class Transaction
        {
            public string TransactionNumber { get; set; }
            public int AcquisitionID { get; set; }
            public string Title { get; set; }

            public override string ToString()
            {
                return $"{TransactionNumber} - {Title}";
            }
        }

        private void LoadTransactionNumbers()
        {
            HashSet<string> savedTransactionTitlePairs = new HashSet<string>();

            foreach (DataGridViewRow row in dgvAccessions.Rows)
            {
                if (row.Cells["TransactionNumber"].Value != null && row.Cells["Title"].Value != null)
                {
                    string transactionNumber = row.Cells["TransactionNumber"].Value.ToString();
                    string title = row.Cells["Title"].Value.ToString();
                    savedTransactionTitlePairs.Add($"{transactionNumber}|{title}");
                }
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
            SELECT acq.TransactionNumber, acq.AcquisitionID, b.Title 
            FROM acquisitions acq
            INNER JOIN books b ON acq.BookID = b.BookID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string transactionNumber = reader["TransactionNumber"].ToString();
                    string title = reader["Title"].ToString();

                    string key = $"{transactionNumber}|{title}";

                    if (!savedTransactionTitlePairs.Contains(key))
                    {
                        cmbTransactionNumber.Items.Add(new Transaction
                        {
                            TransactionNumber = transactionNumber,
                            AcquisitionID = Convert.ToInt32(reader["AcquisitionID"]),
                            Title = title
                        });
                    }
                }
            }
        }








        private void LoadStatusAndCondition()
        {
            cmbStatus.Items.Add("Available");
            cmbStatus.Items.Add("Issued");
            cmbStatus.Items.Add("Damaged");
            cmbStatus.Items.Add("Lost");
            cmbStatus.SelectedIndex = cmbStatus.Items.IndexOf("Available");

            cmbCondition.Items.Add("As New");
            cmbCondition.Items.Add("Used");
            cmbCondition.Items.Add("Poor");
            cmbCondition.Items.Insert(0, "Select");
            cmbCondition.SelectedIndex = 1; 
        }


        private void cmbTransactionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTransactionNumber.SelectedItem != null)
            {
                var selectedItem = (Transaction)cmbTransactionNumber.SelectedItem;
                int acquisitionID = selectedItem.AcquisitionID;

                using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            acq.TransactionNumber, 
                            b.ISBN, b.Title, a.AuthorName, p.PublisherName, g.GenreName, 
                            s.SupplierName, acq.Donor, acq.Quantity, b.YearPublished, b.Description 
                        FROM acquisitions acq
                        INNER JOIN books b ON acq.BookID = b.BookID
                        LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                        LEFT JOIN genres g ON b.GenreID = g.GenreID
                        LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                        LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                        WHERE acq.AcquisitionID = @AcquisitionID";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionID);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cmbISBN.Text = reader["ISBN"].ToString();
                        cmbTitle.Text = reader["Title"].ToString();
                        cmbAuthor.Text = reader["AuthorName"].ToString();
                        cmbPublisher.Text = reader["PublisherName"].ToString();
                        cmbGenre.Text = reader["GenreName"].ToString();
                        cmbSupplier.Text = reader["SupplierName"].ToString();
                        txtDonor.Text = reader["Donor"].ToString();
                        nudQuantity.Value = Convert.ToDecimal(reader["Quantity"]);

                        if (reader["YearPublished"] != DBNull.Value)
                        {
                            int yearPublished = Convert.ToInt32(reader["YearPublished"]);
                            dtpYearPublished.Value = new DateTime(yearPublished, 1, 1);
                            dtpYearPublished.Format = DateTimePickerFormat.Custom;
                            dtpYearPublished.CustomFormat = "yyyy";
                        }

                        txtDescription.Text = reader["Description"].ToString();


                        if (!string.IsNullOrWhiteSpace(cmbSupplier.Text) && string.IsNullOrWhiteSpace(txtDonor.Text))
                        {
                            txtRemarks.Text = "As New";
                        }
                        else
                        {
                            txtRemarks.Text = string.Empty;
                        }
                    }
                }
            }
        }

        private void EnableConditionAndStatus()
        {
            cmbCondition.Enabled = true;
            cmbStatus.Enabled = true;

            if (cmbCondition.Items.Count > 0)
                cmbCondition.SelectedIndex = 0; 

            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0; 
        }

        private int GetBookID(string isbn, string title)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT BookID FROM books WHERE ISBN = @ISBN AND Title = @Title";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                cmd.Parameters.AddWithValue("@Title", title);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int GetSupplierID(string supplierName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT SupplierID FROM suppliers WHERE SupplierName = @SupplierName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbTransactionNumber.Text) ||
                string.IsNullOrWhiteSpace(cmbStatus.Text) ||
                string.IsNullOrWhiteSpace(cmbCondition.Text) ||
                cmbCondition.Text == "Select" ||
                nudQuantity.Value == 0 ||
                string.IsNullOrWhiteSpace(txtRemarks.Text)) 
                 {
                       MessageBox.Show("Please fill in all required fields correctly.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                       return;
                 }


            var selectedItem = (Transaction)cmbTransactionNumber.SelectedItem;
            int acquisitionID = selectedItem.AcquisitionID;
            string status = cmbStatus.Text;
            string condition = cmbCondition.Text;
            int quantity = (int)nudQuantity.Value;



            //if (IsTransactionNumberDuplicate(selectedItem.TransactionNumber))
            //{
            //    MessageBox.Show("Duplicate Transaction Number found: " + selectedItem.TransactionNumber, "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            int bookID = GetBookID(cmbISBN.Text, cmbTitle.Text);

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                for (int i = 1; i <= quantity; i++)
                {
                    string accessionNumber = $"A-{i.ToString("D4")}";

                    string query = @"
                    INSERT INTO accessions (
                        AcquisitionID, Status, `Condition`, AccessionNumber, Remarks
                    ) 
                    VALUES (
                        @AcquisitionID, @Status, @Condition, @AccessionNumber, @Remarks
                    )";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionID);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Condition", condition);
                    cmd.Parameters.AddWithValue("@AccessionNumber", accessionNumber);
                    cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());

                    cmd.ExecuteNonQuery();

                    if (dgvAccessions != null)
                    {
                        dgvAccessions.Rows.Add(accessionNumber, status, condition);  
                    }
                    else
                    {
                        MessageBox.Show("DataGridView is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }

            MessageBox.Show("Accessions successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private bool IsTransactionNumberDuplicate(string transactionNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = @"
                    SELECT COUNT(*) 
                    FROM accessions acc
                    INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                    WHERE acq.TransactionNumber = @TransactionNumber";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TransactionNumber", transactionNumber);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        

        private void cmbTransactionNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (e.KeyChar != 't' && !char.IsDigit(e.KeyChar) && e.KeyChar != 'T' && e.KeyChar != '-' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

        }

        private void addAccessions_Load(object sender, EventArgs e)
        {
            cmbTransactionNumber.DropDownStyle = ComboBoxStyle.DropDown;
            cmbTransactionNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbTransactionNumber.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void txtDonor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)8)
            {
                e.Handled = true;  
            }
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void cmbTransactionNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
