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
    public partial class formPublishers: Form
    {
        public formPublishers()
        {
            InitializeComponent();
            LoadPublishers();
        }

        public void LoadPublishers()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT PublisherID, PublisherName, ContactNo, Email FROM publishers";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvPublisher.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvPublisher.Rows.Add(row["PublisherID"], row["PublisherName"], row["ContactNo"], row["Email"]);
                }
                txtSearch.Clear();
            }
        }

        private void formPublishers_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvPublisher, lblTotalCount);
        }

        private void DeletePublisher(int publisherID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM books WHERE PublisherID = @PublisherID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@PublisherID", publisherID);
                int bookCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (bookCount > 0)
                {
                    MessageBox.Show("Cannot delete publisher with associated books. Remove the books first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM publishers WHERE PublisherID = @PublisherID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PublisherID", publisherID);
                cmd.ExecuteNonQuery();
            }
            LoadPublishers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addPublishers addPublisher = new addPublishers();
            addPublisher.FormClosed += (s, args) => LoadPublishers();
            addPublisher.ShowDialog();

        }

        private void dgvPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int publisherID = Convert.ToInt32(dgvPublisher.Rows[e.RowIndex].Cells["PublisherID"].Value);
                string publisherName = dgvPublisher.Rows[e.RowIndex].Cells["PublisherName"].Value.ToString();
                string contactNo = dgvPublisher.Rows[e.RowIndex].Cells["ContactNo"].Value.ToString();
                string email = dgvPublisher.Rows[e.RowIndex].Cells["Email"].Value.ToString();

                if (dgvPublisher.Columns[e.ColumnIndex].Name == "Update")
                {
                    updPublishers updatePublishers = new updPublishers(publisherID, publisherName, contactNo, email);
                    updatePublishers.FormClosed += (s, args) => LoadPublishers();
                    updatePublishers.ShowDialog();
                }
                else if (dgvPublisher.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this publisher?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeletePublisher(publisherID);
                    }
                }
            }
        }

        public void SearchPublishers(string searchQuery)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT PublisherID, PublisherName, ContactNo, Email FROM publishers WHERE PublisherName LIKE @SearchQuery";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dgvPublisher.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dgvPublisher.Rows.Add(row["PublisherID"], row["PublisherName"], row["ContactNo"], row["Email"]);
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                SearchPublishers(searchQuery);
            }
            else
            {
                LoadPublishers();  
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
