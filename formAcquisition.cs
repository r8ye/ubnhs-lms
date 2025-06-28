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
using ubnhs_lms.addForms;
using ubnhs_lms.updateForms;

namespace ubnhs_lms
{
    public partial class formAcquisition: Form
    {
        public formAcquisition()
        {
            InitializeComponent();
            
            classFilteringAcquisition.LoadFilters(cmbFilterMethod);
        }

        private void formAcquisition_Load(object sender, EventArgs e)
        {
            LoadAcquisitions();
            new classTotalCount(dgvAcquisition, lblTotalCount);

            cmbFilterMethod.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterMethod.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterMethod.AutoCompleteSource = AutoCompleteSource.ListItems;
        }



        public void LoadAcquisitions()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                            a.AcquisitionID, 
                            a.TransactionNumber,
                            a.MethodName, 
                            b.Title, 
                            b.ISBN, 
                            au.AuthorName, 
                            p.PublisherName, 
                            a.DateAcquired, 
                            a.Donor, 
                            s.SupplierName, 
                            a.Cost, 
                            a.Quantity,
                            a.BookID,
                            a.SupplierID 
                        FROM acquisitions a
                        LEFT JOIN books b ON a.BookID = b.BookID
                        LEFT JOIN authors au ON b.AuthorID = au.AuthorID
                        LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                        LEFT JOIN suppliers s ON a.SupplierID = s.SupplierID
                        ORDER BY a.TransactionNumber ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvAcquisition.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvAcquisition.Rows.Add(row["AcquisitionID"], row["TransactionNumber"], row["MethodName"], row["Title"], row["ISBN"],
                        row["AuthorName"], row["PublisherName"], row["DateAcquired"], row["Donor"], row["SupplierName"],
                        row["Cost"], row["Quantity"], row["BookID"], row["SupplierID"]);
                }

                dgvAcquisition.Columns["DateAcquired"].DefaultCellStyle.Format = "MM/dd/yyyy";
                txtSearch.Clear();
            }
        }


        private void dgvAcquisition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvAcquisition.Columns.Contains("AcquisitionID"))
                {
                    int acquisitionID = Convert.ToInt32(dgvAcquisition.Rows[e.RowIndex].Cells["AcquisitionID"].Value);
                    int bookID = Convert.ToInt32(dgvAcquisition.Rows[e.RowIndex].Cells["BookID"].Value);

                    if (dgvAcquisition.Columns[e.ColumnIndex].Name == "Update")
                    {
                        updAcquisition updateAcquisition = new updAcquisition(acquisitionID);
                        updateAcquisition.FormClosed += (s, args) => LoadAcquisitions();
                        updateAcquisition.ShowDialog();
                    }
                    else if (dgvAcquisition.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this acquisition?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            PreventDeleteIfAcquisitionHasRecordsInAccessions(acquisitionID);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The column 'AcquisitionID' does not exist in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PreventDeleteIfAcquisitionHasRecordsInAccessions(int acquisitionID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM accessions WHERE AcquisitionID = @AcquisitionID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionID);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Cannot delete this acquisition because it has related records in the accessions table.", "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        DeleteAcquisition(acquisitionID);
                    }
                }
            }
        }

        private void DeleteAcquisition(int acquisitionID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM acquisitions WHERE AcquisitionID = @AcquisitionID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionID);
                    cmd.ExecuteNonQuery();
                    LoadAcquisitions();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addAcquisition addFormAcquisition = new addAcquisition();
            addFormAcquisition.FormClosed += (s, args) => LoadAcquisitions();
            addFormAcquisition.ShowDialog();
        }

        private void cmbFilterMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();

            string selectedMethod = cmbFilterMethod.SelectedItem.ToString();
            classFilteringAcquisition.FilterAcquisitionsByMethod(dgvAcquisition, selectedMethod);
        }

        private void cmbFilterMethod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dgvAcquisition_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                string method = dgvAcquisition.Rows[e.RowIndex].Cells["MethodName"].Value?.ToString();

                if (e.ColumnIndex == dgvAcquisition.Columns["MethodName"].Index)
                {
                    if (method == "Purchased")
                    {
                        e.CellStyle.ForeColor = Color.SlateBlue; 
                    }
                    else if (method == "Donated")
                    {
                        e.CellStyle.ForeColor = Color.DarkGoldenrod; 
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black; 
                    }
                }
            }
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
        }

        private void SearchAcquisitions(string searchText)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = @"SELECT 
                            a.AcquisitionID, 
                            a.TransactionNumber,
                            a.MethodName, 
                            b.Title, 
                            b.ISBN, 
                            au.AuthorName, 
                            p.PublisherName, 
                            a.DateAcquired, 
                            a.Donor, 
                            s.SupplierName, 
                            a.Cost, 
                            a.Quantity,
                            a.BookID,
                            a.SupplierID 
                        FROM acquisitions a
                        LEFT JOIN books b ON a.BookID = b.BookID
                        LEFT JOIN authors au ON b.AuthorID = au.AuthorID
                        LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                        LEFT JOIN suppliers s ON a.SupplierID = s.SupplierID
                        WHERE 
                            a.TransactionNumber LIKE @SearchText OR
                            b.Title LIKE @SearchText OR
                            b.ISBN LIKE @SearchText OR
                            au.AuthorName LIKE @SearchText OR
                            a.Donor LIKE @SearchText OR
                            s.SupplierName LIKE @SearchText
                        ORDER BY a.TransactionNumber ASC";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvAcquisition.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvAcquisition.Rows.Add(row["AcquisitionID"], row["TransactionNumber"], row["MethodName"], row["Title"], row["ISBN"],
                        row["AuthorName"], row["PublisherName"], row["DateAcquired"], row["Donor"], row["SupplierName"],
                        row["Cost"], row["Quantity"], row["BookID"], row["SupplierID"]);
                }

                dgvAcquisition.Columns["DateAcquired"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchAcquisitions(txtSearch.Text);
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

        private void cmbFilterMethod_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }
    }
}
