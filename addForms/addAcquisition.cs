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
    public partial class addAcquisition : Form
    {
        private string currentTransactionNumber;

        public addAcquisition()
        {
            InitializeComponent();
        }    

        private void addAcquisition_Load(object sender, EventArgs e)
        {
            dtpDateAcquired.Value = DateTime.Today;

            currentTransactionNumber = GenerateTransactionNumber();
            txtTransactionNumber.Text = currentTransactionNumber;

            LoadISBN();
            LoadSuppliers();

            dtpDateAcquired.MaxDate = DateTime.Now.Date;
            dtpDateAcquired.MinDate = DateTime.Now.AddYears(-50).Date;

            cmbSupplier.Enabled = false;
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

            cmbPublisher.DropDownStyle = ComboBoxStyle.DropDown;
            cmbPublisher.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPublisher.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbSupplier.AutoCompleteSource = AutoCompleteSource.ListItems;

            cmbISBN.SelectedIndexChanged += cmbISBN_SelectedIndexChanged;

        }


        private string GenerateTransactionNumber()
        {
            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = "SELECT TransactionNumber FROM acquisitions ORDER BY AcquisitionID DESC LIMIT 1";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    var lastNumber = cmd.ExecuteScalar()?.ToString();
                    if (!string.IsNullOrEmpty(lastNumber) && lastNumber.StartsWith("T-"))
                    {
                        int num = int.Parse(lastNumber.Substring(2));
                        return $"T-{(num + 1).ToString("D4")}";
                    }
                    else
                    {
                        return "T-0001";
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


        private void Methods_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDonated.Checked)
            {
                txtDonor.Enabled = true;

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

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return; 

            string transactionNumber = currentTransactionNumber;

            using (var conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                var query = @"INSERT INTO acquisitions 
                     (TransactionNumber, MethodName, BookID, DateAcquired, Donor, SupplierID, Cost, Quantity)
                      VALUES 
                     (@TransactionNumber, @MethodName, 
                      (SELECT BookID FROM books WHERE ISBN = @ISBN), 
                      @DateAcquired, @Donor, @SupplierID, @Cost, @Quantity)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TransactionNumber", transactionNumber);
                    cmd.Parameters.AddWithValue("@MethodName", rbDonated.Checked ? "Donated" : "Purchased");
                    cmd.Parameters.AddWithValue("@ISBN", ((ISBNItem)cmbISBN.SelectedItem).ISBN);
                    cmd.Parameters.AddWithValue("@DateAcquired", dtpDateAcquired.Value);
                    cmd.Parameters.AddWithValue("@Donor", rbDonated.Checked ? txtDonor.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SupplierID", rbPurchased.Checked && cmbSupplier.SelectedIndex > 0 ? GetSupplierID(cmbSupplier.SelectedItem.ToString()) : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cost", txtCost.Text);
                    cmd.Parameters.AddWithValue("@Quantity", nudQuantity.Value);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Acquisition added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // clear
                    cmbISBN.SelectedIndex = -1;
                    cmbTitle.Items.Clear();
                    cmbAuthor.Items.Clear();
                    cmbPublisher.Items.Clear();
                    nudQuantity.Value = 1;
                    txtCost.Clear();

                    cmbISBN.Text = "";
                    cmbTitle.Text = "";
                    cmbAuthor.Text = "";
                    cmbPublisher.Text = "";
                    txtCost.Text = "";

                    // the same
                    txtTransactionNumber.Text = currentTransactionNumber;
                    dtpDateAcquired.Value = DateTime.Today;

                    // disabled
                    rbDonated.Enabled = false;
                    rbPurchased.Enabled = false;
                    dtpDateAcquired.Enabled = false;
                    txtDonor.Enabled = false;
                    cmbSupplier.Enabled = false;
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

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void cmbAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void cmbPublisher_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
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

        private void nudQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the add form?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                currentTransactionNumber = GenerateTransactionNumber();
                this.Close();
            }
        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            txtCost.MaxLength = 7;
        }
    }
}
