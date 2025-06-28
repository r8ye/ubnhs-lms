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

    public partial class updAcquisition : Form
    {
        private int acquisitionId;

        public updAcquisition(int acquisitionId)
        {
            InitializeComponent();
            this.acquisitionId = acquisitionId;
            
        }

        private void updAcquisition_Load(object sender, EventArgs e)
        {
            

            LoadTitles();
            LoadAuthors(); 
            LoadPublishers(); 
            LoadISBN();
            LoadSuppliers();
            LoadAcquisitionDetails();

            dtpDateAcquired.MaxDate = DateTime.Now.Date;
            dtpDateAcquired.MinDate = DateTime.Now.AddYears(-50).Date;

            rbDonated.CheckedChanged += Methods_CheckedChanged;
            rbPurchased.CheckedChanged += Methods_CheckedChanged;

            cmbAuthor.DropDownStyle = ComboBoxStyle.DropDown;
            cmbAuthor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAuthor.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbISBN.DropDownStyle = ComboBoxStyle.DropDown;
            cmbISBN.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbISBN.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbTitle.DropDownStyle = ComboBoxStyle.DropDown;
            cmbTitle.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbTitle.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbPublisher.DropDownStyle = ComboBoxStyle.DropDown;
            cmbPublisher.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPublisher.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbISBN.SelectedIndexChanged += cmbISBN_SelectedIndexChanged;
        }

        private void LoadTitles()
        {
            cmbTitle.Items.Clear();
            cmbTitle.Items.Add("Select");

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT DISTINCT Title FROM books";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbTitle.Items.Add(reader["Title"].ToString());
                    }
                }
            }
        }


        private void LoadAuthors()
        {
            cmbAuthor.Items.Clear();
            cmbAuthor.Items.Add("Select");

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT AuthorName FROM authors";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbAuthor.Items.Add(reader["AuthorName"].ToString());
                    }
                }
            }
        }

        private void LoadPublishers()
        {
            cmbPublisher.Items.Clear();
            cmbPublisher.Items.Add("Select");

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT PublisherName FROM publishers";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbPublisher.Items.Add(reader["PublisherName"].ToString());
                    }
                }
            }
        }

        public class ISBNItem
        {
            public string ISBN { get; set; }
            public string Title { get; set; }

            public ISBNItem(string isbn, string title)
            {
                ISBN = isbn;
                Title = title;
            }

            public override string ToString()
            {
                return $"{ISBN} - {Title}";
            }
        }


        private void LoadISBN()
        {
            cmbISBN.Items.Clear();
            cmbISBN.Items.Add("Select");

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT ISBN, Title FROM books";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string isbn = reader["ISBN"].ToString();
                        string title = reader["Title"].ToString();
                        cmbISBN.Items.Add(new ISBNItem(isbn, title));
                    }
                }
            }

            cmbISBN.SelectedIndex = 0;
        }


        private void LoadSuppliers()
        {
            cmbSupplier.Items.Clear();
            cmbSupplier.Items.Add("Select");

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT SupplierName FROM suppliers";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbSupplier.Items.Add(reader["SupplierName"].ToString());
                    }
                }
            }
        }


        private void LoadAcquisitionDetails()
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = @"SELECT 
                    a.TransactionNumber,
                    a.MethodName, 
                    b.Title, 
                    b.ISBN, 
                    au.AuthorName, 
                    p.PublisherName, 
                    a.Donor, 
                    a.SupplierID, 
                    a.Cost, 
                    a.Quantity, 
                    a.DateAcquired 
            FROM acquisitions a
            LEFT JOIN books b ON a.BookID = b.BookID
            LEFT JOIN authors au ON b.AuthorID = au.AuthorID
            LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
            WHERE a.AcquisitionID = @AcquisitionID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTransactionNumber.Text = reader["TransactionNumber"].ToString();

                            string methodName = reader["MethodName"].ToString();
                            string isbn = reader["ISBN"].ToString();
                            string title = reader["Title"].ToString();
                            string author = reader["AuthorName"].ToString();
                            string publisher = reader["PublisherName"].ToString();
                            string donor = reader["Donor"].ToString();
                            string supplierID = reader["SupplierID"].ToString();
                            decimal cost = Convert.ToDecimal(reader["Cost"]);
                            int quantity = Convert.ToInt32(reader["Quantity"]);
                            DateTime dateAcquired = Convert.ToDateTime(reader["DateAcquired"]);

                            foreach (var item in cmbISBN.Items)
                            {
                                if (item is ISBNItem isbnItem && isbnItem.ISBN == isbn)
                                {
                                    cmbISBN.SelectedItem = isbnItem;
                                    break;
                                }
                            }

                            cmbTitle.Items.Clear();
                            cmbTitle.Items.Add(title);
                            cmbTitle.SelectedIndex = 0;

                            cmbAuthor.Items.Clear();
                            cmbAuthor.Items.Add(author);
                            cmbAuthor.SelectedIndex = 0;

                            cmbPublisher.Items.Clear();
                            cmbPublisher.Items.Add(publisher);
                            cmbPublisher.SelectedIndex = 0;

                            dtpDateAcquired.Value = dateAcquired;
                            nudQuantity.Value = quantity;
                            txtCost.Text = cost.ToString("F2");

                            if (methodName == "Donated")
                            {
                                rbDonated.Checked = true;
                                txtDonor.Text = donor;
                                cmbSupplier.Enabled = false;
                            }
                            else if (methodName == "Purchased")
                            {
                                rbPurchased.Checked = true;
                                txtDonor.Enabled = false;

                                if (int.TryParse(supplierID, out int parsedSupplierID))
                                {
                                    cmbSupplier.SelectedItem = GetSupplierName(parsedSupplierID);
                                    cmbSupplier.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Invalid supplier ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }




        private void LoadTitleAndAuthorByISBN(string isbn)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = @"SELECT Title, AuthorID FROM books WHERE ISBN = @ISBN";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ISBN", isbn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cmbTitle.SelectedItem = reader["Title"].ToString();
                            cmbAuthor.SelectedItem = reader["AuthorID"].ToString();
                        }
                    }
                }
            }
        }
        private string GetSupplierName(int supplierID)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT SupplierName FROM suppliers WHERE SupplierID = @SupplierID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    var result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : string.Empty;
                }
            }
        }





        private void Methods_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDonated.Checked)
            {
                cmbSupplier.Enabled = false;
                cmbSupplier.SelectedIndex = -1;

                txtDonor.Enabled = true;
                txtDonor.Clear();

                txtCost.Enabled = false;
                txtCost.Clear();

                lblDonor.Text = "Donor *";
                lblSupplier.Text = "Supplier";
                lblTotalcost.Text = "Total Cost";
            }
            else if (rbPurchased.Checked)
            {
                txtDonor.Enabled = false;
                txtDonor.Clear();

                cmbSupplier.Enabled = true;

                txtCost.Enabled = true;

                lblDonor.Text = "Donor";
                lblSupplier.Text = "Supplier *";
                lblTotalcost.Text = "Total Cost *";
            }
        }

        private bool ValidateFields()
        {
            if (cmbTitle.Items.Count == 0 || cmbISBN.Items.Count == 0 || cmbAuthor.Items.Count == 0 || cmbPublisher.Items.Count == 0)
            {
                MessageBox.Show("Book title and ISBN options are not loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbTitle.SelectedItem == null || cmbISBN.SelectedItem == null)
            {
                MessageBox.Show("Please select a title and ISBN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dtpDateAcquired.Value.Date > DateTime.Today)
            {
                MessageBox.Show("Date acquired cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (rbDonated.Checked && string.IsNullOrWhiteSpace(txtDonor.Text))
            {
                MessageBox.Show("Donor field is required for donations.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (rbPurchased.Checked)
            {
                if (cmbSupplier.SelectedItem == null)
                {
                    MessageBox.Show("Please select a supplier.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!SupplierExists(cmbSupplier.SelectedItem.ToString()))
                {
                    MessageBox.Show("Selected supplier does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!rbPurchased.Checked && !rbDonated.Checked)
            {
                MessageBox.Show("Please select either 'Purchased' or 'Donated'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (nudQuantity.Value <= 0)
            {
                MessageBox.Show("Quantity must be greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (rbPurchased.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtCost.Text))
                {
                    MessageBox.Show("Cost is required for purchased items.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!decimal.TryParse(txtCost.Text, out decimal cost) || cost <= 0)
                {
                    MessageBox.Show("Please enter a valid cost greater than zero.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }


            return true;
        }

        private bool SupplierExists(string supplierName)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM suppliers WHERE SupplierName = @SupplierName";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = @"UPDATE acquisitions
                      SET MethodName = @MethodName, 
                          BookID = (SELECT BookID FROM books WHERE ISBN = @ISBN),
                          DateAcquired = @DateAcquired, 
                          Donor = @Donor, 
                          SupplierID = @SupplierID, 
                          Cost = @Cost, 
                          Quantity = @Quantity
                      WHERE AcquisitionID = @AcquisitionID";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MethodName", rbDonated.Checked ? "Donated" : "Purchased");
                    cmd.Parameters.AddWithValue("@ISBN", ((ISBNItem)cmbISBN.SelectedItem).ISBN);
                    cmd.Parameters.AddWithValue("@DateAcquired", dtpDateAcquired.Value);
                    cmd.Parameters.AddWithValue("@Donor", rbDonated.Checked ? txtDonor.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SupplierID", rbPurchased.Checked && cmbSupplier.SelectedIndex > 0 ? GetSupplierID(cmbSupplier.SelectedItem.ToString()) : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    cmd.Parameters.AddWithValue("@Quantity", nudQuantity.Value);
                    cmd.Parameters.AddWithValue("@AcquisitionID", acquisitionId);
                    cmd.Parameters.AddWithValue("@AuthorID", GetAuthorID(cmbAuthor.SelectedItem.ToString()));
                    cmd.Parameters.AddWithValue("@PublisherID", GetPublisherID(cmbPublisher.SelectedItem.ToString()));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Acquisition updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private int GetAuthorID(string authorName)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT AuthorID FROM authors WHERE AuthorName = @AuthorName";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AuthorName", authorName);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int GetPublisherID(string publisherName)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT PublisherID FROM publishers WHERE PublisherName = @PublisherName";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PublisherName", publisherName);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private int GetSupplierID(string supplierName)
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT SupplierID FROM suppliers WHERE SupplierName = @SupplierName";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        



        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbAuthor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbISBN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;

            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '-' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cmbISBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTitle.Items.Clear();
            cmbAuthor.Items.Clear();
            cmbPublisher.Items.Clear();

            if (cmbISBN.SelectedItem is ISBNItem selectedItem)
            {
                using (var conn = new MySqlConnection(classDatabase.ConnectionString))
                {
                    conn.Open();
                    var query = @"SELECT b.Title, a.AuthorName, p.PublisherName 
                          FROM books b 
                          JOIN authors a ON b.AuthorID = a.AuthorID 
                          JOIN publishers p ON b.PublisherID = p.PublisherID 
                          WHERE b.ISBN = @ISBN";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ISBN", selectedItem.ISBN);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cmbTitle.Items.Add(reader["Title"].ToString());
                                cmbTitle.SelectedIndex = 0;

                                cmbAuthor.Items.Add(reader["AuthorName"].ToString());
                                cmbAuthor.SelectedIndex = 0;

                                cmbPublisher.Items.Add(reader["PublisherName"].ToString());
                                cmbPublisher.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        private void nudQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }

            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDonor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsControl(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbISBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtDonor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void nudQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void rbDonated_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDonated.Checked)
            {
                txtCost.Enabled = false;
                txtCost.Text = "0.00"; 
            }
        }

        private void rbPurchased_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPurchased.Checked)
            {
                txtCost.Enabled = true;
            }
        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            txtCost.MaxLength = 7;
        }
    }
}
