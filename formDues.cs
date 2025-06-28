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

namespace ubnhs_lms
{
    public partial class formDues: Form
    {
        public formDues()
        {
            InitializeComponent();
            LoadPenaltySettings();
        }

        private void LoadPenaltySettings()
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM penaltysettings";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string type = reader["borrowerType"].ToString();
                        if (type == "Faculty")
                        {
                            nudFacultyBorrowDuration.Value = Convert.ToInt32(reader["borrowDuration"]);
                            nudFacultyBorrowLimitBooks.Value = Convert.ToInt32(reader["borrowLimit"]);
                            txtFacultyDamageCharge.Text = reader["damageCharge"].ToString();
                            txtFacultyOverdueFee.Text = reader["overdueFee"].ToString();
                            txtFacultyLostItemFee.Text = reader["lostItemFee"].ToString();
                        }
                        else if (type == "Student")
                        {
                            nudStudentBorrowDuration.Value = Convert.ToInt32(reader["borrowDuration"]);
                            nudStudentBorrowLimitBooks.Value = Convert.ToInt32(reader["borrowLimit"]);
                            txtStudentDamageCharge.Text = reader["damageCharge"].ToString();
                            txtStudentOverdueFee.Text = reader["overdueFee"].ToString();
                            txtStudentLostItemFee.Text = reader["lostItemFee"].ToString();
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            SavePenalty("Faculty", (int)nudFacultyBorrowDuration.Value, (int)nudFacultyBorrowLimitBooks.Value,
                txtFacultyDamageCharge.Text, txtFacultyOverdueFee.Text, txtFacultyLostItemFee.Text);

            SavePenalty("Student", (int)nudStudentBorrowDuration.Value, (int)nudStudentBorrowLimitBooks.Value,
                txtStudentDamageCharge.Text, txtStudentOverdueFee.Text, txtStudentLostItemFee.Text);

            MessageBox.Show("Penalty settings saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtFacultyDamageCharge.Text) ||
                string.IsNullOrWhiteSpace(txtFacultyOverdueFee.Text) ||
                string.IsNullOrWhiteSpace(txtFacultyLostItemFee.Text) ||
                string.IsNullOrWhiteSpace(txtStudentDamageCharge.Text) ||
                string.IsNullOrWhiteSpace(txtStudentOverdueFee.Text) ||
                string.IsNullOrWhiteSpace(txtStudentLostItemFee.Text))
            {
                MessageBox.Show("Please fill in all penalty charge fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudFacultyBorrowDuration.Value <= 0 || nudFacultyBorrowLimitBooks.Value <= 0)
            {
                MessageBox.Show("Please enter valid borrow duration and borrow limit for Faculty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudStudentBorrowDuration.Value <= 0 || nudStudentBorrowLimitBooks.Value <= 0)
            {
                MessageBox.Show("Please enter valid borrow duration and borrow limit for Student.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void SavePenalty(string borrowerType, int duration, int limit, string damage, string overdue, string lost)
        {
            using (MySqlConnection conn = new MySqlConnection(classDatabase.ConnectionString))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM penaltysettings WHERE borrowerType = @borrowerType";
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@borrowerType", borrowerType);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    string sql;
                    if (exists > 0)
                    {
                        sql = @"UPDATE penaltysettings 
                                SET borrowDuration = @duration, 
                                    borrowLimit = @limit, 
                                    damageCharge = @damage, 
                                    overdueFee = @overdue, 
                                    lostItemFee = @lost 
                                WHERE borrowerType = @borrowerType";
                    }
                    else
                    {
                        sql = @"INSERT INTO penaltysettings 
                                (borrowerType, borrowDuration, borrowLimit, damageCharge, overdueFee, lostItemFee)
                                VALUES (@borrowerType, @duration, @limit, @damage, @overdue, @lost)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@borrowerType", borrowerType);
                        cmd.Parameters.AddWithValue("@duration", duration);
                        cmd.Parameters.AddWithValue("@limit", limit);
                        cmd.Parameters.AddWithValue("@damage", Convert.ToDecimal(damage));
                        cmd.Parameters.AddWithValue("@overdue", Convert.ToDecimal(overdue));
                        cmd.Parameters.AddWithValue("@lost", Convert.ToDecimal(lost));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void nudFacultyBorrowDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void nudFacultyBorrowLimitBooks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtFacultyDamageCharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtFacultyOverdueFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtFacultyLostItemFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void nudStudentBorrowDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void nudStudentBorrowLimitBooks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtStudentDamageCharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtStudentOverdueFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtStudentLostItemFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            if ((e.KeyChar == (char)3) || (e.KeyChar == (char)22) || (e.KeyChar == (char)24))
            {
                e.Handled = true;
            }
        }

        private void txtFacultyDamageCharge_TextChanged(object sender, EventArgs e)
        {
            txtFacultyDamageCharge.MaxLength = 4;
        }

        private void txtFacultyOverdueFee_TextChanged(object sender, EventArgs e)
        {
            txtFacultyOverdueFee.MaxLength = 4;
        }

        private void txtFacultyLostItemFee_TextChanged(object sender, EventArgs e)
        {
            txtFacultyLostItemFee.MaxLength = 4;
        }

        private void txtStudentDamageCharge_TextChanged(object sender, EventArgs e)
        {
            txtStudentDamageCharge.MaxLength = 4;
        }

        private void txtStudentOverdueFee_TextChanged(object sender, EventArgs e)
        {
            txtStudentOverdueFee.MaxLength = 4;
        }

        private void txtStudentLostItemFee_TextChanged(object sender, EventArgs e)
        {
            txtStudentLostItemFee.MaxLength = 4;
        }

        private void nudFacultyBorrowDuration_ValueChanged(object sender, EventArgs e)
        {
            nudFacultyBorrowDuration.Maximum = 30;
        }

        private void nudFacultyBorrowLimitBooks_ValueChanged(object sender, EventArgs e)
        {
            nudFacultyBorrowLimitBooks.Maximum = 10;
        }

        private void nudStudentBorrowDuration_ValueChanged(object sender, EventArgs e)
        {
            nudStudentBorrowDuration.Maximum = 30;
        }

        private void nudStudentBorrowLimitBooks_ValueChanged(object sender, EventArgs e)
        {
            nudStudentBorrowLimitBooks.Maximum = 10;
        }
    }
}
