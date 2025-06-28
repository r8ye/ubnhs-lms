using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ubnhs_lms
{
    public static class classFilteringAccessions
    {
        public static void LoadFilters(ComboBox cmbFilterStatus)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StatusValue");
            dt.Columns.Add("StatusName");

            dt.Rows.Add("All Status", "All Status");
            dt.Rows.Add("Available", "Available");
            dt.Rows.Add("Damaged", "Damaged");
            dt.Rows.Add("Issued", "Issued");
            dt.Rows.Add("Lost", "Lost");

            cmbFilterStatus.DataSource = dt;
            cmbFilterStatus.DisplayMember = "StatusName";
            cmbFilterStatus.ValueMember = "StatusValue";
        }

        public static void FilterAccessions(DataGridView dgvAccessions, string selectedStatus)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query;

                if (selectedStatus == "All Status")
                {
                    query = @"
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
                    FROM accessions acc
                    INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                    INNER JOIN books b ON acq.BookID = b.BookID
                    LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                    LEFT JOIN genres g ON b.GenreID = g.GenreID
                    LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                    LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                    ORDER BY acc.AccessionNumber ASC";
                }
                else
                {
                    query = @"
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
                    FROM accessions acc
                    INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                    INNER JOIN books b ON acq.BookID = b.BookID
                    LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                    LEFT JOIN genres g ON b.GenreID = g.GenreID
                    LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                    LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                    WHERE acc.Status = @Status
                    ORDER BY acc.AccessionNumber ASC";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (selectedStatus != "All Status")
                {
                    cmd.Parameters.AddWithValue("@Status", selectedStatus);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvAccessions.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    if (IsAccessionDuplicate(row["AccessionNumber"].ToString(), dgvAccessions))
                    {
                        MessageBox.Show("Duplicate Accession Number found: " + row["AccessionNumber"], "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    dgvAccessions.Rows.Add(
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

        private static bool IsAccessionDuplicate(string accessionNumber, DataGridView dgvAccessions)
        {
            foreach (DataGridViewRow row in dgvAccessions.Rows)
            {
                if (row.Cells["AccessionNumber"].Value?.ToString() == accessionNumber)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
