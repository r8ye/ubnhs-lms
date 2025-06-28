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
    public partial class formStrands : Form
    {
        public formStrands()
        {
            InitializeComponent();
        }

        public void LoadStrands()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT s.StrandID, d.departmentName, s.StrandName " +
                               "FROM strands s " +
                               "INNER JOIN departments d ON s.DepartmentID = d.DepartmentID " +
                               "ORDER BY d.departmentName, s.StrandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvStrands.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvStrands.Rows.Add(row["StrandID"], row["departmentName"], row["StrandName"]);
                }
            }
        }

        private void formStrands_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvStrands, lblTotalCount);

            cmbFilterDepartments.DropDownStyle = ComboBoxStyle.DropDown;
            cmbFilterDepartments.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbFilterDepartments.AutoCompleteSource = AutoCompleteSource.ListItems;

            classFilteringStrands.LoadFilters(cmbFilterDepartments);
        }

        private void DeleteStrand(int strandID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM gradelevels WHERE StrandID = @StrandID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@StrandID", strandID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete strand with associated grade levels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM strands WHERE StrandID = @StrandID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StrandID", strandID);
                cmd.ExecuteNonQuery();
            }
            LoadStrands();
        }

        private void dgvStrands_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int strandID = Convert.ToInt32(dgvStrands.Rows[e.RowIndex].Cells["StrandID"].Value);
                string departmentName = dgvStrands.Rows[e.RowIndex].Cells["departmentName"].Value.ToString();
                string strandName = dgvStrands.Rows[e.RowIndex].Cells["StrandName"].Value.ToString();

                if (dgvStrands.Columns[e.ColumnIndex].Name == "Update")
                {
                    updStrands updateStrands = new updStrands(strandID, departmentName, strandName);
                    updateStrands.FormClosed += (s, args) => LoadStrands();
                    updateStrands.ShowDialog();
                }
                else if (dgvStrands.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this strand?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteStrand(strandID);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addStrands addStrandsForm = new addStrands();
            addStrandsForm.FormClosed += (s, args) => LoadStrands();
            addStrandsForm.ShowDialog();
        }

        private void cmbFilterDepartments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }

            e.Handled = false;
        }

        private void cmbFilterDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            string department = cmbFilterDepartments.Text == "All Departments" ? null : cmbFilterDepartments.Text.Trim();
            classFilteringStrands.FilterStrandsByDepartment(dgvStrands, department);
        }

        private void cmbFilterDepartments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.X || e.KeyCode == Keys.V))
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
