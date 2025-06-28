using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Guna.UI2.WinForms;

namespace copyyyy_lms
{
    public class classFilteringGrades
    {
        public static void LoadFilters(ComboBox cmbFilterDepartments, ComboBox cmbFilterStrand)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string deptQuery = "SELECT DISTINCT departmentName FROM departments ORDER BY departmentName";
                MySqlCommand deptCmd = new MySqlCommand(deptQuery, conn);
                MySqlDataReader deptReader = deptCmd.ExecuteReader();

                cmbFilterDepartments.Items.Clear();
                cmbFilterDepartments.Items.Add("All Departments");

                while (deptReader.Read())
                {
                    cmbFilterDepartments.Items.Add(deptReader["departmentName"].ToString());
                }
                deptReader.Close();

                cmbFilterDepartments.SelectedIndexChanged += (sender, e) =>
                {
                    string selectedDepartment = cmbFilterDepartments.SelectedItem.ToString();
                    LoadStrands(cmbFilterStrand, selectedDepartment);
                    cmbFilterStrand.Enabled = cmbFilterStrand.Items.Count > 1;
                };

                cmbFilterDepartments.SelectedIndex = 0;
                LoadStrands(cmbFilterStrand, null);
            }
        }

        public static void LoadStrands(ComboBox cmbStrand, string department)
        {
            cmbStrand.Items.Clear();
            cmbStrand.Items.Add("All Strands");

            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT s.strandName FROM strands s " +
                               "INNER JOIN gradelevels g ON s.StrandID = g.StrandID " +
                               "INNER JOIN departments d ON g.DepartmentID = d.DepartmentID " +
                               "WHERE d.departmentName = @Department ORDER BY s.strandName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Department", department);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string strandName = reader["strandName"].ToString();
                        if (!cmbStrand.Items.Contains(strandName)) 
                        {
                            cmbStrand.Items.Add(strandName);
                        }
                    }
                }
            }

            cmbStrand.SelectedIndex = 0; 
        }



        public static void FilterGradeLevels(Guna2DataGridView dgvGradeLevels, string departmentName, string strandName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = "SELECT d.departmentName, s.strandName, g.gradelevelName " +
                               "FROM gradelevels g " +
                               "INNER JOIN departments d ON g.departmentID = d.departmentID " +
                               "INNER JOIN strands s ON g.strandID = s.strandID ";

                if (!string.IsNullOrEmpty(departmentName) && departmentName != "All Departments")
                {
                    query += "WHERE d.departmentName = @departmentName ";
                }

                if (!string.IsNullOrEmpty(strandName) && strandName != "All Strands")
                {
                    if (!string.IsNullOrEmpty(departmentName) && departmentName != "All Departments")
                    {
                        query += "AND s.strandName = @strandName ";
                    }
                    else
                    {
                        query += "WHERE s.strandName = @strandName ";
                    }
                }

                query += "ORDER BY d.departmentName, s.strandName, g.gradelevelName";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (!string.IsNullOrEmpty(departmentName) && departmentName != "All Departments")
                {
                    cmd.Parameters.AddWithValue("@departmentName", departmentName);
                }

                if (!string.IsNullOrEmpty(strandName) && strandName != "All Strands")
                {
                    cmd.Parameters.AddWithValue("@strandName", strandName);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvGradeLevels.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvGradeLevels.Rows.Add(row["departmentName"], row["strandName"], row["gradelevelName"]);
                }
            }
        }
    }
}
