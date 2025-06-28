using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace copyyyy_lms
{
    internal class classDatabase
    {
        private static string server = "localhost";
        private static string database = "ubnhs";
        private static string username = "root";
        private static string password = "";

        public static string ConnectionString
        {
            get
            {
                return $"Server={server};Database={database};User ID={username};Password={password};";
            }
        }

        public static bool Connection()
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    return true; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
