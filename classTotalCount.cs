using System;
using Guna.UI2.WinForms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ubnhs_lms
{
    internal class classTotalCount
    {
        private Guna2DataGridView dgv;
        private Guna2HtmlLabel lbl;

        public classTotalCount(Guna2DataGridView dataGridView, Guna2HtmlLabel label)
        {
            dgv = dataGridView;
            lbl = label;
            dgv.RowsAdded += (s, e) => UpdateCount();
            dgv.RowsRemoved += (s, e) => UpdateCount();
            UpdateCount();
        }

        private void UpdateCount()
        {
            lbl.Text = "Total Entries: " + (dgv.Rows.Count - (dgv.AllowUserToAddRows ? 1 : 0));
        }
    }
}
