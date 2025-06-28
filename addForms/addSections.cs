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
    public partial class addSections : Form
    {
        public addSections()
        {
            InitializeComponent();
            LoadDepartments();
            txtSections.Focus();
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

        private void LoadStrands(int departmentID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT strandID, strandName FROM strands WHERE departmentID = @departmentID ORDER BY strandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentID", departmentID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["strandID"] = 0;
                row["strandName"] = "Select";
                dt.Rows.InsertAt(row, 0);

                cmbStrand.DisplayMember = "strandName";
                cmbStrand.ValueMember = "strandID";
                cmbStrand.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    cmbStrand.SelectedIndex = 1;  // This ensures the "Select" option is skipped.
                }

                if (cmbStrand.Text == "N/A")
                {
                    cmbStrand.Enabled = false;
                    lblstrand.Text = "Strand";
                }
                else
                {
                    cmbStrand.Enabled = true;
                    lblstrand.Text = "Strand *";
                }
            }
        }




        private void LoadGradeLevels(int strandID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT gradelevelID, gradelevelName FROM gradelevels WHERE strandID = @strandID ORDER BY gradelevelName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@strandID", strandID);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                DataRow row = dt.NewRow();
                row["gradelevelID"] = 0;
                row["gradelevelName"] = "Select";
                dt.Rows.InsertAt(row, 0);

                cmbGradeLevels.DisplayMember = "gradelevelName";
                cmbGradeLevels.ValueMember = "gradelevelID";
                cmbGradeLevels.DataSource = dt;
                cmbGradeLevels.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sectionName = txtSections.Text.Trim();
            int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);
            int strandID = Convert.ToInt32(cmbStrand.SelectedValue);
            int gradelevelID = Convert.ToInt32(cmbGradeLevels.SelectedValue);

            if (departmentID == 0 || strandID == 0 || gradelevelID == 0)
            {
                MessageBox.Show("Please select a valid department, strand, and grade level.");
                return;
            }

            if (string.IsNullOrEmpty(sectionName))
            {
                MessageBox.Show("Please enter the section name.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM sections WHERE departmentID = @departmentID AND strandID = @strandID AND gradelevelID = @gradelevelID AND sectionName = @sectionName";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@departmentID", departmentID);
                checkCmd.Parameters.AddWithValue("@strandID", strandID);
                checkCmd.Parameters.AddWithValue("@gradelevelID", gradelevelID);
                checkCmd.Parameters.AddWithValue("@sectionName", sectionName);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("This section already exists in the selected department, strand, and grade level.");
                }
                else
                {
                    string query = "INSERT INTO sections (departmentID, strandID, gradelevelID, sectionName) VALUES (@departmentID, @strandID, @gradelevelID, @sectionName)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@departmentID", departmentID);
                    cmd.Parameters.AddWithValue("@strandID", strandID);
                    cmd.Parameters.AddWithValue("@gradelevelID", gradelevelID);
                    cmd.Parameters.AddWithValue("@sectionName", sectionName);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Section saved successfully.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSections.Clear();
                    this.Close();

                    formSections formSec = Application.OpenForms["formSections"] as formSections;
                    if (formSec != null)
                    {
                        formSec.LoadSections();
                    }
                }
            }
        }

        private void cmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedIndex > 0)
            {
                int departmentID = Convert.ToInt32(cmbDepartments.SelectedValue);
                LoadStrands(departmentID);
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbStrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStrand.SelectedIndex > 0)
            {
                int strandID = Convert.ToInt32(cmbStrand.SelectedValue);
                LoadGradeLevels(strandID);
            }
            else
            {
                cmbGradeLevels.DataSource = null;
                cmbGradeLevels.Items.Clear();
            }
        }

        private void txtSections_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
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

        private void cmbStrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void cmbGradeLevels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtSections_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtSections_TextChanged(object sender, EventArgs e)
        {
            txtSections.MaxLength = 20;
        }
    }
}
