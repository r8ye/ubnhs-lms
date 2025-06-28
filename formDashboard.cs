using copyyyy_lms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ubnhs_lms
{
    public partial class formDashboard : Form
    {
        public formDashboard()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateTotalBooks();
            UpdateTotalMembers();
            UpdateTotalUsers();
            UpdateFinesCollected();
            UpdateBookStatusChart();
            UpdateMemberStatusChart();
            UpdateOverdueBooks();
            UpdateAvailableBooks();
            UpdateBorrowedBooks();
            UpdateDueToday();
        }

        private void UpdateDueToday()
        {
            int dueToday = 0;

            string query = "SELECT COUNT(*) FROM circulations WHERE DueDate = CURDATE() AND Status = 'Issued'";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    dueToday = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading due today books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblDueToday.Text = dueToday.ToString();
        }


        private void UpdateBorrowedBooks()
        {
            int borrowedBooks = 0;
            string query = "SELECT COUNT(*) FROM accessions WHERE Status = 'Issued'";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    borrowedBooks = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching borrowed books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblBorrowedBooks.Text = borrowedBooks.ToString();
        }


        private void UpdateAvailableBooks()
        {
            int availableBooks = 0;

            string query = "SELECT COUNT(*) FROM accessions WHERE Status = 'Available'";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    availableBooks = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading available books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblAvailableBooks.Text = availableBooks.ToString();
        }


        private void UpdateOverdueBooks()
        {
            int overdueCount = 0;

            string query = @"
                SELECT COUNT(*) 
                FROM circulations 
                WHERE DATEDIFF(NOW(), DueDate) > 0 
                AND Status != 'Returned'";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    overdueCount = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking overdue books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblOverDueBooks.Text = overdueCount.ToString();
        }


        // bar chart

        private void UpdateBookStatusChart()
        {
            var bookStatusCounts = GetBookStatusCountsFromDatabase();

            chartBookStatus.Series.Clear();

            Series series = new Series("BookStatus");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = Color.Black;

            int pointIndex = 0;

            series.Points.AddXY("Available", bookStatusCounts["Available"]);
            series.Points[pointIndex++].Color = Color.FromArgb(135, 206, 235);

            series.Points.AddXY("Damaged", bookStatusCounts["Damaged"]);
            series.Points[pointIndex++].Color = Color.FromArgb(236, 154, 133);

            series.Points.AddXY("Issued", bookStatusCounts["Issued"]);
            series.Points[pointIndex++].Color = Color.FromArgb(154, 205, 50);

            series.Points.AddXY("Lost", bookStatusCounts["Lost"]);
            series.Points[pointIndex++].Color = Color.FromArgb(240, 128, 128);

            chartBookStatus.Series.Add(series);

            chartBookStatus.ChartAreas[0].AxisX.Title = "Current Book Status";
            chartBookStatus.ChartAreas[0].AxisY.Title = "Count";

            chartBookStatus.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8);
            chartBookStatus.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Segoe UI", 8);

            chartBookStatus.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chartBookStatus.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chartBookStatus.Update();
        }




        // donut
        private void UpdateMemberStatusChart()
        {
            var memberStatusCounts = GetMemberStatusCountsFromDatabase();
            chartMemberStatus.Series.Clear();
            chartMemberStatus.Titles.Clear();

            chartMemberStatus.Series.Add("MemberStatus");
            chartMemberStatus.Series["MemberStatus"].ChartType = SeriesChartType.Doughnut;
            chartMemberStatus.Series["MemberStatus"].BorderWidth = 5;
            chartMemberStatus.Series["MemberStatus"]["PieLabelStyle"] = "Disabled";
            chartMemberStatus.Series["MemberStatus"].IsValueShownAsLabel = false;
            chartMemberStatus.Series["MemberStatus"].Points.AddXY("Not Present", memberStatusCounts["Not Present"]);
            chartMemberStatus.Series["MemberStatus"].Points.AddXY("Present", memberStatusCounts["Present"]);

            foreach (var point in chartMemberStatus.Series["MemberStatus"].Points)
            {
                point.LegendText = "#VALX: #VAL";
            }

            chartMemberStatus.ChartAreas[0].Area3DStyle.Enable3D = false;
            chartMemberStatus.ChartAreas[0].AxisX.IsLabelAutoFit = false;
            chartMemberStatus.ChartAreas[0].AxisY.IsLabelAutoFit = false;

            chartMemberStatus.Series["MemberStatus"]["DoughnutRadius"] = "70";
            chartMemberStatus.ChartAreas[0].Position = new ElementPosition(5, 5, 120, 150);
            chartMemberStatus.ChartAreas[0].InnerPlotPosition = new ElementPosition(10, 10, 50, 50);

            Title chartTitle = new Title();
            chartTitle.Text = "Current Member Status";
            chartTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            chartTitle.ForeColor = Color.Black;
            chartTitle.Alignment = ContentAlignment.TopCenter;
            chartMemberStatus.Titles.Add(chartTitle);

            chartMemberStatus.Update();
        }




        private Dictionary<string, int> GetMemberStatusCountsFromDatabase()
        {
            var statusCounts = new Dictionary<string, int>
            {
                { "Present", 0 },
                { "Not Present", 0 }
            };

            string query = "SELECT Status, COUNT(*) FROM members GROUP BY Status";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string status = row["Status"].ToString();
                        int count = Convert.ToInt32(row["COUNT(*)"]);

                        if (status == "Present")
                            statusCounts["Present"] = count;
                        else if (status == "Not Present")
                            statusCounts["Not Present"] = count;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching member status counts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return statusCounts;
        }

        private Dictionary<string, int> GetBookStatusCountsFromDatabase()
        {
            var counts = new Dictionary<string, int>
            {
                { "Available", 0 },
                { "Damaged", 0 },
                { "Issued", 0 },
                { "Lost", 0 }
            };

            string query = "SELECT Status, COUNT(*) FROM accessions GROUP BY Status";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string status = row["Status"].ToString();
                        int count = Convert.ToInt32(row["COUNT(*)"]);
                        if (counts.ContainsKey(status))
                        {
                            counts[status] = count;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching book status counts: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return counts;
        }

        private void UpdateTotalBooks()
        {
            int totalBooks = GetTotalBooksFromDatabase();
            lblTotalBooks.Text = totalBooks.ToString();
        }

        private void UpdateTotalMembers()
        {
            int totalMembers = GetTotalMembersFromDatabase();
            lblTotalMembers.Text = totalMembers.ToString();
        }

        private void UpdateTotalUsers()
        {
            int totalUsers = GetTotalUsersFromDatabase();
            lblTotalUsers.Text = totalUsers.ToString();
        }

        private void UpdateFinesCollected()
        {
            decimal finesCollected = GetTotalFinesCollectedFromDatabase();
            lblFinesCollected.Text = finesCollected.ToString("N2");
        }

        private int GetTotalBooksFromDatabase()
        {
            int totalBooks = 0;
            string query = "SELECT COUNT(*) FROM accessions WHERE Status != 'Lost'";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalBooks = Convert.ToInt32(dataTable.Rows[0][0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching total books: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return totalBooks;
        }


        private int GetTotalMembersFromDatabase()
        {
            int totalMembers = 0;
            string query = "SELECT COUNT(*) FROM members";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalMembers = Convert.ToInt32(dataTable.Rows[0][0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching total members: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return totalMembers;
        }

        private int GetTotalUsersFromDatabase()
        {
            int totalUsers = 0;
            string query = "SELECT COUNT(*) FROM users";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalUsers = Convert.ToInt32(dataTable.Rows[0][0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching total users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return totalUsers;
        }

        private decimal GetTotalFinesCollectedFromDatabase()
        {
            decimal totalFines = 0;
            string query = "SELECT SUM(PenaltyFee) FROM circulations WHERE Remarks = 'Paid'";

            using (MySqlConnection connection = new MySqlConnection(classDatabase.ConnectionString))
            {
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
                    {
                        totalFines = Convert.ToDecimal(dataTable.Rows[0][0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching fines collected: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return totalFines;
        }

        
    }
}
