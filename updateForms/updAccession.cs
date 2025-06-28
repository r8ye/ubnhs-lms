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
    public partial class updAccession : Form
    {
        private int accessionID;
        public updAccession(int accessionID)
        {
            InitializeComponent();
            this.accessionID = accessionID;
            LoadStatusAndCondition();            
        }

        private void updAccession_Load(object sender, EventArgs e)
        {
            LoadAccessionDetails();
        }

        private void LoadTransactionNumbers()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT TransactionNumber, AcquisitionID FROM acquisitions";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbTransactionNumber.Items.Add(new { Text = reader["TransactionNumber"].ToString(), Value = reader["AcquisitionID"] });
                }
            }
        }


        private void LoadStatusAndCondition()
        {
            cmbStatus.Items.AddRange(new string[] { "Available", "Issued", "Damaged", "Lost" });
            cmbCondition.Items.AddRange(new string[] { "As New", "Used", "Poor" });
        }

        private void LoadAccessionDetails()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    acc.AccessionNumber, acc.Status, acc.Condition, acc.Remarks,
                    acq.TransactionNumber, acq.AcquisitionID, acq.Donor, acq.Quantity,
                    b.Title, b.ISBN, b.Description, b.YearPublished,
                    a.AuthorName, g.GenreName, p.PublisherName,
                    s.SupplierName
                FROM accessions acc
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                WHERE acc.AccessionID = @AccessionID";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccessionID", accessionID);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtAccessionNumber.Text = reader["AccessionNumber"].ToString();
                    cmbTransactionNumber.Text = reader["TransactionNumber"].ToString();
                    cmbISBN.Text = reader["ISBN"].ToString();
                    cmbTitle.Text = reader["Title"].ToString();
                    cmbAuthor.Text = reader["AuthorName"].ToString();
                    cmbPublisher.Text = reader["PublisherName"].ToString();
                    cmbGenre.Text = reader["GenreName"].ToString();
                    cmbSupplier.Text = reader["SupplierName"].ToString();
                    txtDonor.Text = reader["Donor"].ToString();
                    nudQuantity.Value = Convert.ToDecimal(reader["Quantity"]);
                    txtDescription.Text = reader["Description"].ToString();

                    if (reader["YearPublished"] != DBNull.Value)
                    {
                        int year = Convert.ToInt32(reader["YearPublished"]);
                        dtpYearPublished.Value = new DateTime(year, 1, 1);
                        dtpYearPublished.Format = DateTimePickerFormat.Custom;
                        dtpYearPublished.CustomFormat = "yyyy";
                    }

                    cmbStatus.Text = reader["Status"].ToString();
                    cmbCondition.Text = reader["Condition"].ToString();
                    txtRemarks.Text = reader["Remarks"].ToString();
                }
            }
        }



        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbStatus.Text) || string.IsNullOrWhiteSpace(cmbCondition.Text))
            {
                MessageBox.Show("Please select status and condition.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRemarks.Text))
            {
                MessageBox.Show("Condition field cannot be empty.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string updateQuery = "UPDATE accessions SET Status = @Status, `Condition` = @Condition, Remarks = @Remarks WHERE AccessionID = @AccessionID";

                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                cmd.Parameters.AddWithValue("@Condition", cmbCondition.Text);
                cmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text.Trim());
                cmd.Parameters.AddWithValue("@AccessionID", accessionID);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Accession updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void cmbTransactionNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = cmbTransactionNumber.SelectedItem as dynamic;
            int acquisitionID = selectedItem.Value;

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    acc.AccessionNumber, acc.Status, acc.Condition,
                    acq.TransactionNumber, acq.AcquisitionID, acq.Donor, acq.Quantity,
                    b.Title, b.ISBN, b.Description, b.YearPublished,
                    a.AuthorName, g.GenreName, p.PublisherName,
                    s.SupplierName
                FROM accessions acc
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                WHERE acc.AccessionID = @AccessionID";

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
                }
            }
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

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)8)
            {
                e.Handled = true;  
            }

        }

        private void txtDonor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
