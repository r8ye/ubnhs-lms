using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace copyyyy_lms
{
    public class classFilteringMembers
    {
        public string BorrowerType { get; set; }
        public string Department { get; set; }
        public string Strand { get; set; }
        public string GradeLevel { get; set; }
        public string Section { get; set; }

        public classFilteringMembers(string borrowerType, string department, string strand, string gradeLevel, string section)
        {
            BorrowerType = borrowerType;
            Department = department;
            Strand = strand;
            GradeLevel = gradeLevel;
            Section = section;
        }

        public static void LoadBorrowerTypes(ComboBox cmbBorrowerTypes)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT borrowerType FROM borrowers ORDER BY borrowerType";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbBorrowerTypes.Items.Clear();
                cmbBorrowerTypes.Items.Add("All Borrower Types");
                foreach (DataRow row in dt.Rows)
                {
                    cmbBorrowerTypes.Items.Add(row["borrowerType"].ToString());
                }
                cmbBorrowerTypes.SelectedIndex = 0;
            }
        }

        public static void LoadDepartments(ComboBox cmbFilterDepartments, string borrowerType)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT departmentName FROM departments WHERE borrowerType = @borrowerType ORDER BY departmentName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@borrowerType", borrowerType);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbFilterDepartments.Items.Clear();
                cmbFilterDepartments.Items.Add("All Departments");
                foreach (DataRow row in dt.Rows)
                {
                    cmbFilterDepartments.Items.Add(row["departmentName"].ToString());
                }
                cmbFilterDepartments.SelectedIndex = 0;
                cmbFilterDepartments.Enabled = true;
            }
        }

        public static void LoadStrands(ComboBox cmbFilterStrands, string departmentName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT strandName FROM strands WHERE departmentName = @departmentName ORDER BY strandName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@departmentName", departmentName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbFilterStrands.Items.Clear();
                cmbFilterStrands.Items.Add("All Strands");
                foreach (DataRow row in dt.Rows)
                {
                    cmbFilterStrands.Items.Add(row["strandName"].ToString());
                }
                cmbFilterStrands.SelectedIndex = 0;
                cmbFilterStrands.Enabled = true;
            }
        }

        public static void LoadGradeLevels(ComboBox cmbFilterGradeLevels, string strandName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT gradeLevelName FROM gradelevels WHERE strandName = @strandName ORDER BY gradeLevelName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@strandName", strandName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbFilterGradeLevels.Items.Clear();
                cmbFilterGradeLevels.Items.Add("All Grade Levels");
                foreach (DataRow row in dt.Rows)
                {
                    cmbFilterGradeLevels.Items.Add(row["gradeLevelName"].ToString());
                }
                cmbFilterGradeLevels.SelectedIndex = 0;
                cmbFilterGradeLevels.Enabled = true;
            }
        }

        public static void LoadSections(ComboBox cmbFilterSections, string gradeLevelName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT sectionName FROM sections WHERE gradeLevelName = @gradeLevelName ORDER BY sectionName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@gradeLevelName", gradeLevelName);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmbFilterSections.Items.Clear();
                cmbFilterSections.Items.Add("All Sections");
                foreach (DataRow row in dt.Rows)
                {
                    cmbFilterSections.Items.Add(row["sectionName"].ToString());
                }
                cmbFilterSections.SelectedIndex = 0;
                cmbFilterSections.Enabled = true;
            }
        }
    }
}
