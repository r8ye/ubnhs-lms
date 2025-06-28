namespace ubnhs_lms
{
    partial class formSections
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSections));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.lblTotalCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.dgvSections = new Guna.UI2.WinForms.Guna2DataGridView();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrandName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GradeLevelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrandID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GradeLevelID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Update = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbFilterGradeLevels = new System.Windows.Forms.ComboBox();
            this.cmbFilterDepartments = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbFilterStrands = new System.Windows.Forms.ComboBox();
            this.lblLogo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).BeginInit();
            this.SuspendLayout();
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
            this.btnAdd.TabIndex = 23;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalCount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCount.Location = new System.Drawing.Point(12, 516);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(201, 21);
            this.lblTotalCount.TabIndex = 24;
            this.lblTotalCount.Text = "Total Entries: 1 million dollars";
            this.lblTotalCount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this.dgvSections;
            // 
            // dgvSections
            // 
            this.dgvSections.AllowUserToAddRows = false;
            this.dgvSections.AllowUserToDeleteRows = false;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            this.dgvSections.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSections.BackgroundColor = System.Drawing.Color.White;
            this.dgvSections.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSections.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSections.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSections.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvSections.ColumnHeadersHeight = 51;
            this.dgvSections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SectionID,
            this.DepartmentName,
            this.StrandName,
            this.GradeLevelName,
            this.SectionName,
            this.DepartmentID,
            this.StrandID,
            this.GradeLevelID,
            this.Update,
            this.Delete});
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSections.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvSections.EnableHeadersVisualStyles = false;
            this.dgvSections.GridColor = System.Drawing.Color.White;
            this.dgvSections.Location = new System.Drawing.Point(12, 41);
            this.dgvSections.Name = "dgvSections";
            this.dgvSections.ReadOnly = true;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSections.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvSections.RowHeadersVisible = false;
            this.dgvSections.RowHeadersWidth = 51;
            this.dgvSections.RowTemplate.DividerHeight = 1;
            this.dgvSections.RowTemplate.Height = 50;
            this.dgvSections.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSections.Size = new System.Drawing.Size(756, 449);
            this.dgvSections.TabIndex = 25;
            this.dgvSections.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.WhiteGrid;
            this.dgvSections.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSections.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvSections.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvSections.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvSections.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvSections.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvSections.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dgvSections.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.dgvSections.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSections.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSections.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSections.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvSections.ThemeStyle.HeaderStyle.Height = 51;
            this.dgvSections.ThemeStyle.ReadOnly = true;
            this.dgvSections.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSections.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSections.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvSections.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvSections.ThemeStyle.RowsStyle.Height = 50;
            this.dgvSections.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvSections.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.IndianRed;
            this.dgvSections.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSections_CellClick);
            // 
            // SectionID
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle16;
            this.SectionID.FillWeight = 50F;
            this.SectionID.HeaderText = "No.";
            this.SectionID.MinimumWidth = 6;
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Visible = false;
            // 
            // DepartmentName
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DepartmentName.DefaultCellStyle = dataGridViewCellStyle17;
            this.DepartmentName.FillWeight = 110F;
            this.DepartmentName.HeaderText = "Department";
            this.DepartmentName.MinimumWidth = 10;
            this.DepartmentName.Name = "DepartmentName";
            this.DepartmentName.ReadOnly = true;
            // 
            // StrandName
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StrandName.DefaultCellStyle = dataGridViewCellStyle18;
            this.StrandName.HeaderText = "Strand";
            this.StrandName.MinimumWidth = 10;
            this.StrandName.Name = "StrandName";
            this.StrandName.ReadOnly = true;
            // 
            // GradeLevelName
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GradeLevelName.DefaultCellStyle = dataGridViewCellStyle19;
            this.GradeLevelName.FillWeight = 115F;
            this.GradeLevelName.HeaderText = "Grade Level";
            this.GradeLevelName.MinimumWidth = 20;
            this.GradeLevelName.Name = "GradeLevelName";
            this.GradeLevelName.ReadOnly = true;
            // 
            // SectionName
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SectionName.DefaultCellStyle = dataGridViewCellStyle20;
            this.SectionName.FillWeight = 150F;
            this.SectionName.HeaderText = "Section";
            this.SectionName.MinimumWidth = 20;
            this.SectionName.Name = "SectionName";
            this.SectionName.ReadOnly = true;
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
            // GradeLevelID
            // 
            this.GradeLevelID.HeaderText = "GradeLevelID";
            this.GradeLevelID.MinimumWidth = 6;
            this.GradeLevelID.Name = "GradeLevelID";
            this.GradeLevelID.ReadOnly = true;
            this.GradeLevelID.Visible = false;
            // 
            // Update
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle21.NullValue")));
            dataGridViewCellStyle21.Padding = new System.Windows.Forms.Padding(10);
            this.Update.DefaultCellStyle = dataGridViewCellStyle21;
            this.Update.FillWeight = 50F;
            this.Update.HeaderText = "Update";
            this.Update.Image = global::ubnhs_lms.Properties.Resources.pen;
            this.Update.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Update.MinimumWidth = 80;
            this.Update.Name = "Update";
            this.Update.ReadOnly = true;
            this.Update.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Delete
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle22.NullValue")));
            dataGridViewCellStyle22.Padding = new System.Windows.Forms.Padding(10);
            this.Delete.DefaultCellStyle = dataGridViewCellStyle22;
            this.Delete.FillWeight = 50F;
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = global::ubnhs_lms.Properties.Resources.bin;
            this.Delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Delete.MinimumWidth = 80;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cmbFilterGradeLevels
            // 
            this.cmbFilterGradeLevels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterGradeLevels.BackColor = System.Drawing.Color.White;
            this.cmbFilterGradeLevels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterGradeLevels.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterGradeLevels.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterGradeLevels.FormattingEnabled = true;
            this.cmbFilterGradeLevels.Location = new System.Drawing.Point(638, 8);
            this.cmbFilterGradeLevels.Name = "cmbFilterGradeLevels";
            this.cmbFilterGradeLevels.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterGradeLevels.TabIndex = 27;
            this.cmbFilterGradeLevels.SelectedIndexChanged += new System.EventHandler(this.cmbFilterGradeLevels_SelectedIndexChanged);
            this.cmbFilterGradeLevels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterGradeLevels_KeyDown);
            this.cmbFilterGradeLevels.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterGradeLevels_KeyPress);
            // 
            // cmbFilterDepartments
            // 
            this.cmbFilterDepartments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterDepartments.BackColor = System.Drawing.Color.White;
            this.cmbFilterDepartments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterDepartments.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterDepartments.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterDepartments.FormattingEnabled = true;
            this.cmbFilterDepartments.Location = new System.Drawing.Point(364, 8);
            this.cmbFilterDepartments.Name = "cmbFilterDepartments";
            this.cmbFilterDepartments.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterDepartments.TabIndex = 28;
            this.cmbFilterDepartments.SelectedIndexChanged += new System.EventHandler(this.cmbFilterDepartments_SelectedIndexChanged);
            this.cmbFilterDepartments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterDepartments_KeyDown);
            this.cmbFilterDepartments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterDepartments_KeyPress);
            // 
            // dataGridViewImageColumn1
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle25.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle25.NullValue")));
            dataGridViewCellStyle25.Padding = new System.Windows.Forms.Padding(8);
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewImageColumn1.FillWeight = 40F;
            this.dataGridViewImageColumn1.HeaderText = "Update";
            this.dataGridViewImageColumn1.Image = global::ubnhs_lms.Properties.Resources.edit_text;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.Width = 66;
            // 
            // dataGridViewImageColumn2
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle26.NullValue")));
            dataGridViewCellStyle26.Padding = new System.Windows.Forms.Padding(10);
            this.dataGridViewImageColumn2.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridViewImageColumn2.FillWeight = 40F;
            this.dataGridViewImageColumn2.HeaderText = "Delete";
            this.dataGridViewImageColumn2.Image = global::ubnhs_lms.Properties.Resources.delete;
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.MinimumWidth = 6;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewImageColumn2.Width = 65;
            // 
            // cmbFilterStrands
            // 
            this.cmbFilterStrands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterStrands.BackColor = System.Drawing.Color.White;
            this.cmbFilterStrands.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterStrands.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterStrands.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterStrands.FormattingEnabled = true;
            this.cmbFilterStrands.Location = new System.Drawing.Point(501, 8);
            this.cmbFilterStrands.Name = "cmbFilterStrands";
            this.cmbFilterStrands.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterStrands.TabIndex = 30;
            this.cmbFilterStrands.SelectedIndexChanged += new System.EventHandler(this.cmbFilterStrands_SelectedIndexChanged);
            this.cmbFilterStrands.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterStrands_KeyDown);
            this.cmbFilterStrands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterStrands_KeyPress);
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.DimGray;
            this.lblLogo.Location = new System.Drawing.Point(325, 12);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(35, 19);
            this.lblLogo.TabIndex = 34;
            this.lblLogo.Text = "Filter";
            this.lblLogo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearch
            // 
            this.txtSearch.AutoRoundedCorners = true;
            this.txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.txtSearch.BorderRadius = 14;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.DisabledState.Parent = this.txtSearch;
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.txtSearch.FocusedState.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txtSearch.FocusedState.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.FocusedState.Parent = this.txtSearch;
            this.txtSearch.FocusedState.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.DimGray;
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.RosyBrown;
            this.txtSearch.HoverState.Parent = this.txtSearch;
            this.txtSearch.Location = new System.Drawing.Point(12, 5);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtSearch.PlaceholderText = " Search...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.ShadowDecoration.Parent = this.txtSearch;
            this.txtSearch.Size = new System.Drawing.Size(567, 30);
            this.txtSearch.TabIndex = 35;
            this.txtSearch.TextOffset = new System.Drawing.Point(5, 0);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // formSections
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(780, 548);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.cmbFilterStrands);
            this.Controls.Add(this.cmbFilterDepartments);
            this.Controls.Add(this.cmbFilterGradeLevels);
            this.Controls.Add(this.dgvSections);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "formSections";
            this.Text = "Section Management";
            this.Load += new System.EventHandler(this.formSections_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSections)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalCount;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSections;
        private System.Windows.Forms.ComboBox cmbFilterGradeLevels;
        private System.Windows.Forms.ComboBox cmbFilterDepartments;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.ComboBox cmbFilterStrands;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrandName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeLevelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrandID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeLevelID;
        private System.Windows.Forms.DataGridViewImageColumn btnUpdate;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogo;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.DataGridViewImageColumn Update;
    }
}