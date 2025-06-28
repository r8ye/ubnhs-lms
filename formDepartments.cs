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
    public partial class formDepartments: Form
    {
        public formDepartments()
        {
            InitializeComponent();
            LoadDepartments();   
        }

        public void LoadDepartments()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT departmentID, departmentName FROM departments ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvDepartments.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvDepartments.Rows.Add(row["departmentID"], row["departmentName"]);
                }
            }
        }

        private void formDepartments_Load(object sender, EventArgs e)
        {
            new classTotalCount(dgvDepartments, lblTotalCount);
        }

        private void DeleteDepartment(int departmentID)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM gradelevels WHERE departmentID = @departmentID";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@departmentID", departmentID);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Cannot delete department with associated grade levels.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE FROM departments WHERE departmentID = @departmentID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentID", departmentID);
                cmd.ExecuteNonQuery();
            }
            LoadDepartments();
        }

        private void dgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int departmentID = Convert.ToInt32(dgvDepartments.Rows[e.RowIndex].Cells["departmentID"].Value);
                string departmentName = dgvDepartments.Rows[e.RowIndex].Cells["departmentName"].Value.ToString();

                if (dgvDepartments.Columns[e.ColumnIndex].Name == "Update")
                {
                    updDepartments updateDepartments = new updDepartments(departmentID, departmentName);
                    updateDepartments.FormClosed += (s, args) => LoadDepartments();
                    updateDepartments.ShowDialog();
                }
                else if (dgvDepartments.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this department?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        DeleteDepartment(departmentID);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addDepartments addDepartments = new addDepartments();
            addDepartments.FormClosed += (s, args) => LoadDepartments();
            addDepartments.ShowDialog();
        }
    }
}
