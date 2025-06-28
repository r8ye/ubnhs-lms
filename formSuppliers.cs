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
    public partial class formSuppliers: Form
    {
        public formSuppliers()
        {
            InitializeComponent();
            LoadSuppliers();

        
        }

        public void LoadSuppliers()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT SupplierID, SupplierName, Address, ContactNo, Email FROM suppliers";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvSuppliers.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvSuppliers.Rows.Add(row["SupplierID"], row["SupplierName"], row["Address"], row["ContactNo"], row["Email"]);
                }
                txtSearch.Clear();
            }
        }

        private void formSuppliers_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvSuppliers, lblTotalCount);
        }

        private void DeleteSupplier(int supplierID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM acquisitions WHERE SupplierID = @SupplierID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@SupplierID", supplierID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete this supplier because it is linked to acquisitions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM suppliers WHERE SupplierID = @SupplierID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.ExecuteNonQuery();
            }
            LoadSuppliers();
        }




        private void btnAdd_Click(object sender, EventArgs e)
        {
            addSuppliers addSupplier = new addSuppliers();
            addSupplier.FormClosed += (s, args) => LoadSuppliers();
            addSupplier.ShowDialog();

        }

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int supplierID = Convert.ToInt32(dgvSuppliers.Rows[e.RowIndex].Cells["SupplierID"].Value);
                string supplierName = dgvSuppliers.Rows[e.RowIndex].Cells["SupplierName"].Value.ToString();
                string address = dgvSuppliers.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                string contactNo = dgvSuppliers.Rows[e.RowIndex].Cells["ContactNo"].Value.ToString();
                string email = dgvSuppliers.Rows[e.RowIndex].Cells["Email"].Value.ToString();

                if (dgvSuppliers.Columns[e.ColumnIndex].Name == "Update")
                {
                    updSuppliers updateSuppliers = new updSuppliers(supplierID, supplierName, address, contactNo, email);
                    updateSuppliers.FormClosed += (s, args) => LoadSuppliers();
                    updateSuppliers.ShowDialog();
                }
                else if (dgvSuppliers.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteSupplier(supplierID);
                    }
                }
            }
        }
        public void SearchSuppliers(string searchQuery)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT SupplierID, SupplierName, Address, ContactNo, Email FROM suppliers WHERE SupplierName LIKE @SearchQuery";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvSuppliers.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvSuppliers.Rows.Add(row["SupplierID"], row["SupplierName"], row["Address"], row["ContactNo"], row["Email"]);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                SearchSuppliers(searchQuery); 
            }
            else
            {
                LoadSuppliers(); 
            }

            txtSearch.MaxLength = 30;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)
            {
                return;
            }
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '&' && e.KeyChar != '.' && e.KeyChar != ',')
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
    }
}
