using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace copyyyy_lms
{
    public class classFilteringSections
    {
        public static void LoadFilters(ComboBox cmbDepartments, ComboBox cmbGradeLevels)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT departmentName FROM departments ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbDepartments.Items.Clear();
                cmbDepartments.Items.Add("All Departments");
                foreach (DataRow row in dt.Rows)
                {
                    cmbDepartments.Items.Add(row["departmentName"].ToString());
                }
                cmbDepartments.SelectedIndex = 0;
            }
        }

        public static void LoadDepartments(ComboBox cmbDepartments)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT departmentName FROM departments ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbDepartments.Items.Clear();
                cmbDepartments.Items.Add("All Departments");
                foreach (DataRow row in dt.Rows)
                {
                    cmbDepartments.Items.Add(row["departmentName"].ToString());
                }
                cmbDepartments.SelectedIndex = 0;
            }
        }

        public static void LoadGradeLevels(ComboBox cmbGradeLevels, string departmentName, string strandName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT g.gradelevelName FROM gradelevels g " +
                               "INNER JOIN departments d ON g.departmentID = d.departmentID " +
                               "INNER JOIN strands s ON g.strandID = s.strandID " +
                               "WHERE d.departmentName = @departmentName AND s.strandName = @strandName " +
                               "ORDER BY g.gradelevelName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentName", departmentName);
                cmd.Parameters.AddWithValue("@strandName", strandName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbGradeLevels.Items.Clear();
                cmbGradeLevels.Items.Add("All Grade Levels");
                foreach (DataRow row in dt.Rows)
                {
                    cmbGradeLevels.Items.Add(row["gradelevelName"].ToString());
                }
                cmbGradeLevels.SelectedIndex = 0;
                cmbGradeLevels.Enabled = true;
            }
        }

        public static void LoadStrands(ComboBox cmbFilteringStrands, string departmentName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT s.strandName FROM strands s " +
                               "INNER JOIN departments d ON s.departmentID = d.departmentID " +
                               "WHERE d.departmentName = @departmentName ORDER BY s.strandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentName", departmentName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbFilteringStrands.Items.Clear();
                cmbFilteringStrands.Items.Add("All Strands");
                foreach (DataRow row in dt.Rows)
                {
                    cmbFilteringStrands.Items.Add(row["strandName"].ToString());
                }
                cmbFilteringStrands.SelectedIndex = 0;
                cmbFilteringStrands.Enabled = true;
            }
        }

        public static void FilterSections(DataGridView dgvSections, string departmentName, string strandName, string gradelevelName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT s.sectionID, d.departmentName, st.strandName, g.gradelevelName, s.sectionName " +
                               "FROM sections s " +
                               "INNER JOIN departments d ON s.departmentID = d.departmentID " +
                               "INNER JOIN strands st ON s.strandID = st.strandID " +
                               "INNER JOIN gradelevels g ON s.gradelevelID = g.gradelevelID " +
                               "WHERE (@departmentName IS NULL OR d.departmentName = @departmentName) " +
                               "AND (@strandName IS NULL OR st.strandName = @strandName) " +
                               "AND (@gradelevelName IS NULL OR g.gradelevelName = @gradelevelName) " +
                               "ORDER BY d.departmentName, st.strandName, g.gradelevelName, s.sectionName";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentName", string.IsNullOrEmpty(departmentName) || departmentName == "All Departments" ? (object)DBNull.Value : departmentName);
                cmd.Parameters.AddWithValue("@strandName", string.IsNullOrEmpty(strandName) || strandName == "All Strands" ? (object)DBNull.Value : strandName);
                cmd.Parameters.AddWithValue("@gradelevelName", string.IsNullOrEmpty(gradelevelName) || gradelevelName == "All Grade Levels" ? (object)DBNull.Value : gradelevelName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvSections.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgvSections.Rows.Add(row["sectionID"], row["departmentName"], row["strandName"], row["gradelevelName"], row["sectionName"]);
                }
            }
        }
    }
}
