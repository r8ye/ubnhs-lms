using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace copyyyy_lms
{
    public static class classFilteringAcquisition
    {
        public static void LoadFilters(ComboBox cmb)
        {
            cmb.Items.Clear();
            cmb.Items.Add("Method");
            cmb.Items.Add("Purchased");
            cmb.Items.Add("Donated");
            cmb.SelectedIndex = 0;
        }

        public static void FilterAcquisitionsByMethod(DataGridView dgv, string methodName)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string query = @"SELECT 
                            a.AcquisitionID, 
                            a.TransactionNumber,
                            a.MethodName, 
                            b.Title, 
                            b.ISBN, 
                            au.AuthorName, 
                            p.PublisherName, 
                            a.DateAcquired, 
                            a.Donor, 
                            s.SupplierName, 
                            a.Cost, 
                            a.Quantity,
                            a.BookID,
                            a.SupplierID 
                        FROM acquisitions a
                        LEFT JOIN books b ON a.BookID = b.BookID
                        LEFT JOIN authors au ON b.AuthorID = au.AuthorID
                        LEFT JOIN publishers p ON b.PublisherID = p.PublisherID
                        LEFT JOIN suppliers s ON a.SupplierID = s.SupplierID";

                if (methodName != "Method")
                {
                    query += " WHERE a.MethodName = @MethodName";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                if (methodName != "Method")
                {
                    cmd.Parameters.AddWithValue("@MethodName", methodName);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgv.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    dgv.Rows.Add(row["AcquisitionID"], row["TransactionNumber"], row["MethodName"], row["Title"], row["ISBN"],
                        row["AuthorName"], row["PublisherName"], row["DateAcquired"], row["Donor"], row["SupplierName"],
                        row["Cost"], row["Quantity"], row["BookID"], row["SupplierID"]);
                }

                dgv.Columns["DateAcquired"].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
        }
    }
}
