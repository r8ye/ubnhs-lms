namespace ubnhs_lms
{
    partial class formStrands
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formStrands));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.dgvStrands = new Guna.UI2.WinForms.Guna2DataGridView();
            this.StrandID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbFilterDepartments = new System.Windows.Forms.ComboBox();
            this.lblLogo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTotalCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStrands)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this.dgvStrands;
            // 
            // dgvStrands
            // 
            this.dgvStrands.AllowUserToAddRows = false;
            this.dgvStrands.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.dgvStrands.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvStrands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStrands.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStrands.BackgroundColor = System.Drawing.Color.White;
            this.dgvStrands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStrands.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStrands.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStrands.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvStrands.ColumnHeadersHeight = 51;
            this.dgvStrands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StrandID,
            this.DepartmentName,
            this.StrandName,
            this.DepartmentID,
            this.btnUpdate,
            this.Delete});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStrands.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvStrands.EnableHeadersVisualStyles = false;
            this.dgvStrands.GridColor = System.Drawing.Color.White;
            this.dgvStrands.Location = new System.Drawing.Point(12, 41);
            this.dgvStrands.Name = "dgvStrands";
            this.dgvStrands.ReadOnly = true;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStrands.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvStrands.RowHeadersVisible = false;
            this.dgvStrands.RowHeadersWidth = 51;
            this.dgvStrands.RowTemplate.DividerHeight = 1;
            this.dgvStrands.RowTemplate.Height = 50;
            this.dgvStrands.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStrands.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStrands.Size = new System.Drawing.Size(756, 449);
            this.dgvStrands.TabIndex = 22;
            this.dgvStrands.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.WhiteGrid;
            this.dgvStrands.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStrands.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvStrands.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvStrands.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvStrands.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvStrands.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvStrands.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dgvStrands.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.dgvStrands.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStrands.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStrands.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvStrands.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvStrands.ThemeStyle.HeaderStyle.Height = 51;
            this.dgvStrands.ThemeStyle.ReadOnly = true;
            this.dgvStrands.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStrands.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStrands.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvStrands.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStrands.ThemeStyle.RowsStyle.Height = 50;
            this.dgvStrands.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvStrands.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.IndianRed;
            this.dgvStrands.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStrands_CellClick);
            // 
            // StrandID
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StrandID.DefaultCellStyle = dataGridViewCellStyle12;
            this.StrandID.FillWeight = 50F;
            this.StrandID.HeaderText = "No.";
            this.StrandID.MinimumWidth = 6;
            this.StrandID.Name = "StrandID";
            this.StrandID.ReadOnly = true;
            this.StrandID.Visible = false;
            // 
            // DepartmentName
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DepartmentName.DefaultCellStyle = dataGridViewCellStyle13;
            this.DepartmentName.HeaderText = "Department";
            this.DepartmentName.MinimumWidth = 20;
            this.DepartmentName.Name = "DepartmentName";
            this.DepartmentName.ReadOnly = true;
            // 
            // StrandName
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StrandName.DefaultCellStyle = dataGridViewCellStyle14;
            this.StrandName.FillWeight = 152.7284F;
            this.StrandName.HeaderText = "Strand";
            this.StrandName.MinimumWidth = 20;
            this.StrandName.Name = "StrandName";
            this.StrandName.ReadOnly = true;
            // 
            // DepartmentID
            // 
            this.DepartmentID.HeaderText = "DepartmentID";
            this.DepartmentID.MinimumWidth = 6;
            this.DepartmentID.Name = "DepartmentID";
            this.DepartmentID.ReadOnly = true;
            this.DepartmentID.Visible = false;
            // 
            // Update
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle15.NullValue")));
            dataGridViewCellStyle15.Padding = new System.Windows.Forms.Padding(10);
            this.btnUpdate.DefaultCellStyle = dataGridViewCellStyle15;
            this.btnUpdate.FillWeight = 45.81853F;
            this.btnUpdate.HeaderText = "Update";
            this.btnUpdate.Image = global::ubnhs_lms.Properties.Resources.pen;
            this.btnUpdate.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.btnUpdate.MinimumWidth = 80;
            this.btnUpdate.Name = "Update";
            this.btnUpdate.ReadOnly = true;
            this.btnUpdate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Delete
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle16.NullValue")));
            dataGridViewCellStyle16.Padding = new System.Windows.Forms.Padding(10);
            this.Delete.DefaultCellStyle = dataGridViewCellStyle16;
            this.Delete.FillWeight = 45.81853F;
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = global::ubnhs_lms.Properties.Resources.bin;
            this.Delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Delete.MinimumWidth = 80;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cmbFilterDepartments
            // 
            this.cmbFilterDepartments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterDepartments.BackColor = System.Drawing.Color.White;
            this.cmbFilterDepartments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterDepartments.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterDepartments.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterDepartments.FormattingEnabled = true;
            this.cmbFilterDepartments.Location = new System.Drawing.Point(637, 8);
            this.cmbFilterDepartments.Name = "cmbFilterDepartments";
            this.cmbFilterDepartments.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterDepartments.TabIndex = 27;
            this.cmbFilterDepartments.SelectedIndexChanged += new System.EventHandler(this.cmbFilterDepartments_SelectedIndexChanged);
            this.cmbFilterDepartments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterDepartments_KeyDown);
            this.cmbFilterDepartments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterDepartments_KeyPress);
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.DimGray;
            this.lblLogo.Location = new System.Drawing.Point(597, 10);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(37, 21);
            this.lblLogo.TabIndex = 25;
            this.lblLogo.Text = "Filter";
            this.lblLogo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalCount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCount.Location = new System.Drawing.Point(12, 518);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(190, 19);
            this.lblTotalCount.TabIndex = 29;
            this.lblTotalCount.Text = "Total Entries: 1 million dollars";
            this.lblTotalCount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Animated = true;
            this.btnAdd.AutoRoundedCorners = true;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BorderColor = System.Drawing.Color.RosyBrown;
            this.btnAdd.BorderRadius = 16;
            this.btnAdd.CheckedState.Parent = this.btnAdd;
            this.btnAdd.CustomImages.Parent = this.btnAdd;
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.HoverState.Parent = this.btnAdd;
            this.btnAdd.Location = new System.Drawing.Point(679, 502);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ShadowDecoration.BorderRadius = 1;
            this.btnAdd.ShadowDecoration.Color = System.Drawing.Color.RosyBrown;
            this.btnAdd.ShadowDecoration.Parent = this.btnAdd;
            this.btnAdd.Size = new System.Drawing.Size(90, 35);
            this.btnAdd.TabIndex = 28;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // formStrands
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(780, 548);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbFilterDepartments);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.dgvStrands);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formStrands";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strand Management";
            this.Load += new System.EventHandler(this.formStrands_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStrands)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvStrands;
        private System.Windows.Forms.ComboBox cmbFilterDepartments;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalCount;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrandID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.DataGridViewImageColumn btnUpdate;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
    }
}