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

namespace ubnhs_lms
{
    public partial class formArchiveCatalog: Form
    {
        public formArchiveCatalog()
        {
            InitializeComponent();
            LoadArchivedAccessions();
        }

        private void LoadArchivedAccessions()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                    acc.AccessionID,
                    acc.AccessionNumber,
                    acc.Status,
                    acc.Condition,
                    acc.Remarks,
                    acq.AcquisitionID,
                    acq.TransactionNumber,
                    acq.Quantity,
                    acq.Donor,
                    acq.SupplierID,
                    s.SupplierName,
                    b.BookID,
                    b.Title,
                    b.ISBN,
                    b.Description,
                    a.AuthorName,
                    g.GenreName,
                    p.PublisherName,
                    b.YearPublished
                FROM archived_accessions acc
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                ORDER BY acc.AccessionNumber ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvArchive.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvArchive.Rows.Add(
                        row["AccessionID"],
                        row["TransactionNumber"],
                        row["AccessionNumber"],
                        row["Status"],
                        row["Title"],
                        row["ISBN"],
                        row["Description"],
                        row["AuthorName"],
                        row["GenreName"],
                        row["PublisherName"],
                        row["YearPublished"],
                        row["Donor"],
                        row["SupplierName"],
                        row["Condition"],
                        row["Remarks"],
                        row["Quantity"],
                        row["AcquisitionID"],
                        row["BookID"],
                        row["SupplierID"]
                    );
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            List<int> selectedAccessionIDs = new List<int>();

            foreach (DataGridViewRow row in dgvArchive.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    int accessionID = Convert.ToInt32(row.Cells["AccessionID"].Value);
                    selectedAccessionIDs.Add(accessionID);
                }
            }

            if (selectedAccessionIDs.Count == 0)
            {
                MessageBox.Show("No records selected for restoration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                foreach (int accessionID in selectedAccessionIDs)
                {
                    string insertQuery = @"
                INSERT INTO accessions (
                    AccessionID, AccessionNumber, `Status`, `Condition`, `Remarks`, AcquisitionID
                )
                SELECT 
                    AccessionID,
                    AccessionNumber,
                    `Status`,
                    `Condition`,
                    `Remarks`,
                    AcquisitionID
                FROM archived_accessions
                WHERE AccessionID = @AccessionID";

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@AccessionID", accessionID);
                        insertCmd.ExecuteNonQuery();
                    }

                    string deleteQuery = "DELETE FROM archived_accessions WHERE AccessionID = @AccessionID";
                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@AccessionID", accessionID);
                        deleteCmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show($"{selectedAccessionIDs.Count} record(s) restored successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

            var form = Application.OpenForms.OfType<formCatalog>().FirstOrDefault();
            if (form != null)
            {
                form.LoadAccessions();
            }
            else
            {
                formCatalog newForm = new formCatalog();
                newForm.Show();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"
                SELECT 
                    acc.AccessionID,
                    acc.AccessionNumber,
                    acc.Status,
                    acc.Condition,
                    acc.Remarks,
                    acq.AcquisitionID,
                    acq.TransactionNumber,
                    acq.Quantity,
                    acq.Donor,
                    acq.SupplierID,
                    s.SupplierName,
                    b.BookID,
                    b.Title,
                    b.ISBN,
                    b.Description,
                    a.AuthorName,
                    g.GenreName,
                    p.PublisherName,
                    b.YearPublished
                FROM archived_accessions acc 
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                WHERE LOWER(acc.AccessionNumber) LIKE LOWER(@SearchQuery)
                OR LOWER(b.Title) LIKE LOWER(@SearchQuery)
                OR LOWER(b.ISBN) LIKE LOWER(@SearchQuery)
                OR LOWER(a.AuthorName) LIKE LOWER(@SearchQuery)
                OR LOWER(g.GenreName) LIKE LOWER(@SearchQuery)
                OR LOWER(b.YearPublished) LIKE LOWER(@SearchQuery)
                OR LOWER(acc.Status) LIKE LOWER(@SearchQuery)
                ORDER BY acc.AccessionNumber ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchText + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dgvArchive.Rows.Clear();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        dgvArchive.Rows.Add(
                            row["AccessionID"],
                            row["TransactionNumber"],
                            row["AccessionNumber"],
                            row["Status"],
                            row["Title"],
                            row["ISBN"],
                            row["Description"],
                            row["AuthorName"],
                            row["GenreName"],
                            row["PublisherName"],
                            row["YearPublished"],
                            row["Donor"],
                            row["SupplierName"],
                            row["Condition"],
                            row["Remarks"],
                            row["Quantity"],
                            row["AcquisitionID"],
                            row["BookID"],
                            row["SupplierID"]
                        );
                    }

                    dgvArchive.Refresh();
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
