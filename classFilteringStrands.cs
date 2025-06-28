using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Guna.UI2.WinForms;

namespace copyyyy_lms
{
    public class classFilteringStrands
    {
        public static void LoadFilters(ComboBox cmbDepartments)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT departmentName FROM departments ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cmbDepartments.Items.Clear();
                cmbDepartments.Items.Add("All Departments");
                while (reader.Read())
                {
                    cmbDepartments.Items.Add(reader["departmentName"].ToString());
                }
                reader.Close();

                cmbDepartments.SelectedIndex = 0;
            }
        }

        public static void FilterStrandsByDepartment(Guna2DataGridView dgvStrands, string departmentName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT s.StrandID, d.departmentName, s.StrandName, d.DepartmentID " +
                               "FROM strands s " +
                               "INNER JOIN departments d ON s.DepartmentID = d.DepartmentID ";

                if (!string.IsNullOrEmpty(departmentName) && departmentName != "All Departments")
                {
                    query += "WHERE d.departmentName = @departmentName ";
                }

                query += "ORDER BY d.departmentName, s.StrandName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (!string.IsNullOrEmpty(departmentName) && departmentName != "All Departments")
                {
                    cmd.Parameters.AddWithValue("@departmentName", departmentName);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvStrands.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvStrands.Rows.Add(row["StrandID"], row["departmentName"], row["StrandName"], row["DepartmentID"]);
                }
            }
        }
    }
}
