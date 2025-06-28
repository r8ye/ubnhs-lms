namespace ubnhs_lms
{
    partial class formGrades
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formGrades));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.lblTotalCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.dgvGradeLevels = new Guna.UI2.WinForms.Guna2DataGridView();
            this.GradeLevelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GradeLevelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrandID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnUpdate = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbFilterDepartments = new System.Windows.Forms.ComboBox();
            this.cmbFilterStrand = new System.Windows.Forms.ComboBox();
            this.lblLogo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGradeLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Animated = true;
            this.btnAdd.AutoRoundedCorners = true;
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BorderColor = System.Drawing.Color.Salmon;
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
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.lblTotalCount.TabIndex = 20;
            this.lblTotalCount.Text = "Total Entries: 1 million dollars";
            this.lblTotalCount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this.dgvGradeLevels;
            // 
            // dgvGradeLevels
            // 
            this.dgvGradeLevels.AllowUserToAddRows = false;
            this.dgvGradeLevels.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvGradeLevels.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGradeLevels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGradeLevels.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGradeLevels.BackgroundColor = System.Drawing.Color.White;
            this.dgvGradeLevels.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGradeLevels.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvGradeLevels.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGradeLevels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvGradeLevels.ColumnHeadersHeight = 51;
            this.dgvGradeLevels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GradeLevelID,
            this.DepartmentName,
            this.StrandName,
            this.GradeLevelName,
            this.DepartmentID,
            this.StrandID,
            this.btnUpdate,
            this.Delete});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGradeLevels.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvGradeLevels.EnableHeadersVisualStyles = false;
            this.dgvGradeLevels.GridColor = System.Drawing.Color.White;
            this.dgvGradeLevels.Location = new System.Drawing.Point(12, 41);
            this.dgvGradeLevels.Name = "dgvGradeLevels";
            this.dgvGradeLevels.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGradeLevels.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvGradeLevels.RowHeadersVisible = false;
            this.dgvGradeLevels.RowHeadersWidth = 51;
            this.dgvGradeLevels.RowTemplate.DividerHeight = 1;
            this.dgvGradeLevels.RowTemplate.Height = 50;
            this.dgvGradeLevels.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGradeLevels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGradeLevels.Size = new System.Drawing.Size(756, 449);
            this.dgvGradeLevels.TabIndex = 21;
            this.dgvGradeLevels.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.WhiteGrid;
            this.dgvGradeLevels.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvGradeLevels.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvGradeLevels.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvGradeLevels.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvGradeLevels.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvGradeLevels.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvGradeLevels.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dgvGradeLevels.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.dgvGradeLevels.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvGradeLevels.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGradeLevels.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvGradeLevels.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvGradeLevels.ThemeStyle.HeaderStyle.Height = 51;
            this.dgvGradeLevels.ThemeStyle.ReadOnly = true;
            this.dgvGradeLevels.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvGradeLevels.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvGradeLevels.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvGradeLevels.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvGradeLevels.ThemeStyle.RowsStyle.Height = 50;
            this.dgvGradeLevels.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvGradeLevels.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.IndianRed;
            this.dgvGradeLevels.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGradeLevels_CellClick);
            // 
            // GradeLevelID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GradeLevelID.DefaultCellStyle = dataGridViewCellStyle3;
            this.GradeLevelID.FillWeight = 50F;
            this.GradeLevelID.HeaderText = "No.";
            this.GradeLevelID.MinimumWidth = 6;
            this.GradeLevelID.Name = "GradeLevelID";
            this.GradeLevelID.ReadOnly = true;
            this.GradeLevelID.Visible = false;
            // 
            // DepartmentName
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DepartmentName.DefaultCellStyle = dataGridViewCellStyle4;
            this.DepartmentName.HeaderText = "Department";
            this.DepartmentName.MinimumWidth = 200;
            this.DepartmentName.Name = "DepartmentName";
            this.DepartmentName.ReadOnly = true;
            // 
            // StrandName
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StrandName.DefaultCellStyle = dataGridViewCellStyle5;
            this.StrandName.HeaderText = "Strand";
            this.StrandName.MinimumWidth = 15;
            this.StrandName.Name = "StrandName";
            this.StrandName.ReadOnly = true;
            // 
            // GradeLevelName
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GradeLevelName.DefaultCellStyle = dataGridViewCellStyle6;
            this.GradeLevelName.FillWeight = 152.7284F;
            this.GradeLevelName.HeaderText = "Grade Level";
            this.GradeLevelName.MinimumWidth = 20;
            this.GradeLevelName.Name = "GradeLevelName";
            this.GradeLevelName.ReadOnly = true;
            // 
            // DepartmentID
            // 
            this.DepartmentID.HeaderText = "DepartmentID";
            this.DepartmentID.MinimumWidth = 6;
            this.DepartmentID.Name = "DepartmentID";
            this.DepartmentID.ReadOnly = true;
            this.DepartmentID.Visible = false;
            // 
            // StrandID
            // 
            this.StrandID.HeaderText = "StrandID";
            this.StrandID.MinimumWidth = 6;
            this.StrandID.Name = "StrandID";
            this.StrandID.ReadOnly = true;
            this.StrandID.Visible = false;
            // 
            // Update
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(10);
            this.btnUpdate.DefaultCellStyle = dataGridViewCellStyle7;
            this.btnUpdate .FillWeight = 45.81853F;
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
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle8.NullValue")));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(10);
            this.Delete.DefaultCellStyle = dataGridViewCellStyle8;
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
            this.cmbFilterDepartments.Location = new System.Drawing.Point(500, 8);
            this.cmbFilterDepartments.Name = "cmbFilterDepartments";
            this.cmbFilterDepartments.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterDepartments.TabIndex = 24;
            this.cmbFilterDepartments.SelectedIndexChanged += new System.EventHandler(this.cmbFilterDepartments_SelectedIndexChanged);
            this.cmbFilterDepartments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterDepartments_KeyDown);
            this.cmbFilterDepartments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterDepartments_KeyPress);
            // 
            // cmbFilterStrand
            // 
            this.cmbFilterStrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterStrand.BackColor = System.Drawing.Color.White;
            this.cmbFilterStrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterStrand.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStrand.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterStrand.FormattingEnabled = true;
            this.cmbFilterStrand.Location = new System.Drawing.Point(637, 7);
            this.cmbFilterStrand.Name = "cmbFilterStrand";
            this.cmbFilterStrand.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterStrand.TabIndex = 25;
            this.cmbFilterStrand.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStrand_SelectedIndexChanged);
            this.cmbFilterStrand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterStrand_KeyDown);
            this.cmbFilterStrand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterStrand_KeyPress);
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.DimGray;
            this.lblLogo.Location = new System.Drawing.Point(461, 13);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(35, 19);
            this.lblLogo.TabIndex = 34;
            this.lblLogo.Text = "Filter";
            this.lblLogo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formGrades
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(780, 548);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.cmbFilterStrand);
            this.Controls.Add(this.cmbFilterDepartments);
            this.Controls.Add(this.dgvGradeLevels);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "formGrades";
            this.Text = "Grade Level Management";
            this.Load += new System.EventHandler(this.formGrades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGradeLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalCount;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvGradeLevels;
        private System.Windows.Forms.ComboBox cmbFilterDepartments;
        private System.Windows.Forms.ComboBox cmbFilterStrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeLevelID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeLevelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrandID;
        private System.Windows.Forms.DataGridViewImageColumn btnUpdate;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogo;
    }
}