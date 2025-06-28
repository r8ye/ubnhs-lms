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
    public partial class addStrands : Form
    {
        public addStrands()
        {
            InitializeComponent();
            LoadDepartments();
            txtStrand.Focus();
        }

        private void LoadDepartments()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT departmentID, departmentName FROM departments ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["departmentID"] = 0;
                row["departmentName"] = "Select";
                dt.Rows.InsertAt(row, 0);

                cmbDepartments.DisplayMember = "departmentName";
                cmbDepartments.ValueMember = "departmentID";
                cmbDepartments.DataSource = dt;
                cmbDepartments.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strand = txtStrand.Text.Trim();
            int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);

            if (departmentID == 0)
            {
                MessageBox.Show("Please select a valid department.");
                return;
            }

            if (string.IsNullOrEmpty(strand))
            {
                MessageBox.Show("Please enter the strand name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM strands WHERE departmentID = @departmentID AND strandName = @strandName";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@departmentID", departmentID);
                checkCmd.Parameters.AddWithValue("@strandName", strand);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This strand already exists in the selected department.");
                }
                else
                {
                    string query = "INSERT INTO strands (departmentID, strandName) VALUES (@departmentID, @strandName)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@departmentID", departmentID);
                    cmd.Parameters.AddWithValue("@strandName", strand);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Strand saved successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStrand.Clear();
                    this.Close();

                    formStrands formStrand = Application.OpenForms["formStrands"] as formStrands;
                    if (formStrand != null)
                    {
                        formStrand.LoadStrands();
                    }
                }
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkNoStrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoStrand.Checked)
            {
                txtStrand.Text = "N/A";
                txtStrand.Enabled = false;
            }
            else
            {
                txtStrand.Enabled = true;
                txtStrand.Clear();
            }
        }

        private void txtStrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void cmbDepartments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtStrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtStrand_TextChanged(object sender, EventArgs e)
        {
            txtStrand.MaxLength = 30;
        }
    }
}
