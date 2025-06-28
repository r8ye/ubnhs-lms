using copyyyy_lms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;

namespace ubnhs_lms
{
    public partial class formCatalog: Form
    {
        public formCatalog()
        {
            InitializeComponent();
            classFilteringAccessions.LoadFilters(cmbFilterStatus);
        }
     
        private void formCatalog_Load(object sender, EventArgs e)
        {
            LoadAccessions();
            new classTotalCount(dgvAccessions, lblTotalCount);
          
            cmbFilterStatus.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterStatus.AutoCompleteSource = AutoCompleteSource.ListItems;           
        }

        

        public void LoadAccessions()
        {
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
                FROM accessions acc
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                ORDER BY acc.AccessionNumber ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvAccessions.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    if (IsAccessionDuplicate(row["AccessionNumber"].ToString()))
                    {
                        MessageBox.Show("Duplicate Accession Number found: " + row["AccessionNumber"], "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    int acquired = Convert.ToInt32(row["Quantity"]);
                    int remaining = GetRemainingAccessions(Convert.ToInt32(row["AcquisitionID"]));

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
                        $"{acquired} acquired / {remaining} available", 
                        row["AcquisitionID"],
                        row["BookID"],
                        row["SupplierID"]
                    );

                }


                txtSearch.Clear();
            }
        }

        private int GetRemainingAccessions(int acquisitionID)
        {
            int totalIssued = 0;

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM accessions WHERE AcquisitionID = @AcquisitionID AND Status = 'Available'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionID);
                totalIssued = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return totalIssued;
        }



        private bool IsAccessionDuplicate(string accessionNumber)
        {
            foreach (DataGridViewRow row in dgvAccessions.Rows)
            {
                if (row.Cells["AccessionNumber"].Value.ToString() == accessionNumber)
                {
                    return true;
                }
            }
            return false;
        }

        private void dgvAccessions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int accessionID = Convert.ToInt32(dgvAccessions.Rows[e.RowIndex].Cells["AccessionID"].Value);
                string accessionNumber = dgvAccessions.Rows[e.RowIndex].Cells["AccessionNumber"].Value.ToString();

                if (dgvAccessions.Columns[e.ColumnIndex].Name == "Update")
                {
                    updAccession updateaccession = new updAccession(accessionID);
                    updateaccession.FormClosed += (s, args) => LoadAccessions();
                    updateaccession.ShowDialog();
                }
                else if (dgvAccessions.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to archive this accession?", "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteAccession(accessionID);
                    }
                }
            }
        }

        //private void DeleteAccession(int accessionID)
        //{
        //    using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
        //    {
        //        conn.Open();
        //        string statusQuery = "SELECT Status FROM accessions WHERE AccessionID = @AccessionID";
        //        MySqlCommand statusCmd = new MySqlCommand(statusQuery, conn);
        //        statusCmd.Parameters.AddWithValue("@AccessionID", accessionID);
        //        string status = statusCmd.ExecuteScalar()?.ToString();

        //        if (status == "Issued")
        //        {
        //            MessageBox.Show("Cannot archive accession: Book is currently issued to a borrower.",
        //                            "Archive Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        string checkQuery = "SELECT COUNT(*) FROM circulations WHERE AccessionID = @AccessionID";
        //        MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
        //        checkCmd.Parameters.AddWithValue("@AccessionID", accessionID);
        //        int relatedRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

        //        if (relatedRecords > 0)
        //        {
        //            MessageBox.Show("Cannot archive this accession because there are related records in circulations.",
        //                            "Archive Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        string insertQuery = @"
        //        INSERT INTO archived_accessions (
        //            AccessionID, AccessionNumber, `Status`, `Condition`, `Remarks`, AcquisitionID,
        //            TransactionNumber, Quantity, Donor, SupplierID, SupplierName, BookID,
        //            Title, ISBN, `Description`, AuthorName, GenreName, PublisherName, YearPublished)
        //        SELECT 
        //            acc.AccessionID,
        //            acc.AccessionNumber,
        //            acc.Status,
        //            acc.`Condition`,
        //            acc.`Remarks`,
        //            acq.AcquisitionID,
        //            acq.TransactionNumber,
        //            acq.Quantity,
        //            acq.Donor,
        //            acq.SupplierID,
        //            s.SupplierName,
        //            b.BookID,
        //            b.Title,
        //            b.ISBN,
        //            b.`Description`,
        //            a.AuthorName,
        //            g.GenreName,
        //            p.PublisherName,
        //            b.YearPublished
        //        FROM accessions acc
        //        INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
        //        INNER JOIN books b ON acq.BookID = b.BookID
        //        LEFT JOIN authors a ON b.AuthorID = a.AuthorID
        //        LEFT JOIN genres g ON b.GenreID = g.GenreID
        //        LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
        //        LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
        //        WHERE acc.AccessionID = @AccessionID";


        //        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
        //        {
        //            insertCmd.Parameters.AddWithValue("@AccessionID", accessionID);
        //            insertCmd.ExecuteNonQuery();
        //        }

        //        string deleteQuery = "DELETE FROM accessions WHERE AccessionID = @AccessionID";
        //        using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
        //        {
        //            deleteCmd.Parameters.AddWithValue("@AccessionID", accessionID);
        //            deleteCmd.ExecuteNonQuery();
        //        }

        //        LoadAccessions(); 
        //    }
        //}

        private void DeleteAccession(int accessionID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string statusQuery = "SELECT Status FROM accessions WHERE AccessionID = @AccessionID";
                MySqlCommand statusCmd = new MySqlCommand(statusQuery, conn);
                statusCmd.Parameters.AddWithValue("@AccessionID", accessionID);
                string status = statusCmd.ExecuteScalar()?.ToString();

                if (status == "Issued")
                {
                    MessageBox.Show("Cannot archive accession: Book is currently issued to a borrower.",
                                    "Archive Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string checkQuery = "SELECT COUNT(*) FROM circulations WHERE AccessionID = @AccessionID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@AccessionID", accessionID);
                int relatedRecords = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (relatedRecords > 0)
                {
                    MessageBox.Show("Cannot archive this accession because there are related records in circulations.",
                                    "Archive Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string insertQuery = @"
                INSERT INTO archived_accessions (
                    AccessionID, AccessionNumber, `Status`, `Condition`, `Remarks`, AcquisitionID
                )
                SELECT 
                    AccessionID,
                    AccessionNumber,
                    `Status`,
                    `Condition`,
                    `Remarks`,
                    AcquisitionID
                FROM accessions
                WHERE AccessionID = @AccessionID";

                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@AccessionID", accessionID);
                    insertCmd.ExecuteNonQuery();
                }

                string deleteQuery = "DELETE FROM accessions WHERE AccessionID = @AccessionID";
                using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                {
                    deleteCmd.Parameters.AddWithValue("@AccessionID", accessionID);
                    deleteCmd.ExecuteNonQuery();
                }

                LoadAccessions();
            }
        }





        private void btnAdd_Click(object sender, EventArgs e)
        {
            addAccessions addaccession = new addAccessions(dgvAccessions); 
            addaccession.FormClosed += (s, args) => LoadAccessions();
            addaccession.ShowDialog();
        }
        

        private void dgvAccessions_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string status = dgvAccessions.Rows[e.RowIndex].Cells["Status"].Value?.ToString();

                if (e.ColumnIndex == dgvAccessions.Columns["Status"].Index)
                {
                    if (status == "Available")
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
            }
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();

            if (cmbFilterStatus.SelectedValue != null)
            {
                string selectedStatus = cmbFilterStatus.SelectedValue.ToString();
                classFilteringAccessions.FilterAccessions(dgvAccessions, selectedStatus);
            }
        }

        private void cmbFilterStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void SearchAccessions(string searchQuery)
        {
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
                FROM accessions acc
                INNER JOIN acquisitions acq ON acc.AcquisitionID = acq.AcquisitionID
                INNER JOIN books b ON acq.BookID = b.BookID
                LEFT JOIN authors a ON b.AuthorID = a.AuthorID
                LEFT JOIN genres g ON b.GenreID = g.GenreID
                LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                LEFT JOIN suppliers s ON acq.SupplierID = s.SupplierID
                WHERE acc.AccessionNumber LIKE @SearchQuery
                OR b.Title LIKE @SearchQuery
                OR b.ISBN LIKE @SearchQuery
                OR a.AuthorName LIKE @SearchQuery
                OR g.GenreName LIKE @SearchQuery
                OR b.YearPublished LIKE @SearchQuery
                ORDER BY acc.AccessionNumber ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvAccessions.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAccessions(txtSearch.Text);
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

        private void btnViewArchive_Click(object sender, EventArgs e)
        {
            formArchiveCatalog archiveForm = new formArchiveCatalog();
            archiveForm.ShowDialog();
        }
    }
}
