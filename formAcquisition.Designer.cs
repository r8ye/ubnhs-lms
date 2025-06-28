namespace ubnhs_lms
{
    partial class formAcquisition
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formAcquisition));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTotalCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbFilterMethod = new System.Windows.Forms.ComboBox();
            this.dgvAcquisition = new Guna.UI2.WinForms.Guna2DataGridView();
            this.AcquisitionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MethodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ISBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuthorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublisherName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateAcquired = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Donor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BookID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Update = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblLogo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcquisition)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalCount.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.Color.Black;
            this.lblTotalCount.Location = new System.Drawing.Point(10, 519);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(109, 21);
            this.lblTotalCount.TabIndex = 23;
            this.lblTotalCount.Text = "Total Entries: 00";
            this.lblTotalCount.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbFilterMethod
            // 
            this.cmbFilterMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterMethod.BackColor = System.Drawing.Color.White;
            this.cmbFilterMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbFilterMethod.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterMethod.ForeColor = System.Drawing.Color.Black;
            this.cmbFilterMethod.FormattingEnabled = true;
            this.cmbFilterMethod.Location = new System.Drawing.Point(637, 10);
            this.cmbFilterMethod.Name = "cmbFilterMethod";
            this.cmbFilterMethod.Size = new System.Drawing.Size(131, 27);
            this.cmbFilterMethod.TabIndex = 20;
            this.cmbFilterMethod.SelectedIndexChanged += new System.EventHandler(this.cmbFilterMethod_SelectedIndexChanged);
            this.cmbFilterMethod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFilterMethod_KeyDown);
            this.cmbFilterMethod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilterMethod_KeyPress);
            // 
            // dgvAcquisition
            // 
            this.dgvAcquisition.AllowUserToAddRows = false;
            this.dgvAcquisition.AllowUserToDeleteRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            this.dgvAcquisition.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvAcquisition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAcquisition.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAcquisition.BackgroundColor = System.Drawing.Color.White;
            this.dgvAcquisition.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAcquisition.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAcquisition.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcquisition.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvAcquisition.ColumnHeadersHeight = 51;
            this.dgvAcquisition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AcquisitionID,
            this.TransactionNumber,
            this.MethodName,
            this.Title,
            this.ISBN,
            this.AuthorName,
            this.PublisherName,
            this.DateAcquired,
            this.Donor,
            this.SupplierName,
            this.Cost,
            this.Quantity,
            this.BookID,
            this.SupplierID,
            this.Update,
            this.Delete});
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle35.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAcquisition.DefaultCellStyle = dataGridViewCellStyle35;
            this.dgvAcquisition.EnableHeadersVisualStyles = false;
            this.dgvAcquisition.GridColor = System.Drawing.Color.White;
            this.dgvAcquisition.Location = new System.Drawing.Point(10, 43);
            this.dgvAcquisition.Name = "dgvAcquisition";
            this.dgvAcquisition.ReadOnly = true;
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.Color.MistyRose;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcquisition.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
            this.dgvAcquisition.RowHeadersVisible = false;
            this.dgvAcquisition.RowHeadersWidth = 51;
            this.dgvAcquisition.RowTemplate.DividerHeight = 1;
            this.dgvAcquisition.RowTemplate.Height = 50;
            this.dgvAcquisition.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAcquisition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcquisition.ShowCellToolTips = false;
            this.dgvAcquisition.Size = new System.Drawing.Size(760, 449);
            this.dgvAcquisition.TabIndex = 18;
            this.dgvAcquisition.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.WhiteGrid;
            this.dgvAcquisition.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAcquisition.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvAcquisition.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvAcquisition.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvAcquisition.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvAcquisition.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvAcquisition.ThemeStyle.GridColor = System.Drawing.Color.White;
            this.dgvAcquisition.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.dgvAcquisition.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAcquisition.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAcquisition.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvAcquisition.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAcquisition.ThemeStyle.HeaderStyle.Height = 51;
            this.dgvAcquisition.ThemeStyle.ReadOnly = true;
            this.dgvAcquisition.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAcquisition.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAcquisition.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvAcquisition.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvAcquisition.ThemeStyle.RowsStyle.Height = 50;
            this.dgvAcquisition.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.dgvAcquisition.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.IndianRed;
            this.dgvAcquisition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAcquisition_CellClick);
            this.dgvAcquisition.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvAcquisition_CellPainting);
            // 
            // AcquisitionID
            // 
            this.AcquisitionID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AcquisitionID.DefaultCellStyle = dataGridViewCellStyle21;
            this.AcquisitionID.FillWeight = 150F;
            this.AcquisitionID.HeaderText = "No.";
            this.AcquisitionID.MinimumWidth = 20;
            this.AcquisitionID.Name = "AcquisitionID";
            this.AcquisitionID.ReadOnly = true;
            this.AcquisitionID.Visible = false;
            this.AcquisitionID.Width = 150;
            // 
            // TransactionNumber
            // 
            this.TransactionNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TransactionNumber.DefaultCellStyle = dataGridViewCellStyle22;
            this.TransactionNumber.HeaderText = "ID";
            this.TransactionNumber.MinimumWidth = 10;
            this.TransactionNumber.Name = "TransactionNumber";
            this.TransactionNumber.ReadOnly = true;
            this.TransactionNumber.Width = 200;
            // 
            // MethodName
            // 
            this.MethodName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MethodName.DefaultCellStyle = dataGridViewCellStyle23;
            this.MethodName.HeaderText = "Method";
            this.MethodName.MinimumWidth = 20;
            this.MethodName.Name = "MethodName";
            this.MethodName.ReadOnly = true;
            this.MethodName.Width = 200;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Title.DefaultCellStyle = dataGridViewCellStyle24;
            this.Title.FillWeight = 200F;
            this.Title.HeaderText = "Title";
            this.Title.MinimumWidth = 50;
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 400;
            // 
            // ISBN
            // 
            this.ISBN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ISBN.DefaultCellStyle = dataGridViewCellStyle25;
            this.ISBN.FillWeight = 200F;
            this.ISBN.HeaderText = "ISBN";
            this.ISBN.MinimumWidth = 20;
            this.ISBN.Name = "ISBN";
            this.ISBN.ReadOnly = true;
            this.ISBN.Width = 200;
            // 
            // AuthorName
            // 
            this.AuthorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.AuthorName.DefaultCellStyle = dataGridViewCellStyle26;
            this.AuthorName.FillWeight = 200F;
            this.AuthorName.HeaderText = "Author Name";
            this.AuthorName.MinimumWidth = 50;
            this.AuthorName.Name = "AuthorName";
            this.AuthorName.ReadOnly = true;
            this.AuthorName.Width = 200;
            // 
            // PublisherName
            // 
            this.PublisherName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PublisherName.DefaultCellStyle = dataGridViewCellStyle27;
            this.PublisherName.HeaderText = "Publisher";
            this.PublisherName.MinimumWidth = 30;
            this.PublisherName.Name = "PublisherName";
            this.PublisherName.ReadOnly = true;
            this.PublisherName.Visible = false;
            this.PublisherName.Width = 200;
            // 
            // DateAcquired
            // 
            this.DateAcquired.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateAcquired.DefaultCellStyle = dataGridViewCellStyle28;
            this.DateAcquired.FillWeight = 200F;
            this.DateAcquired.HeaderText = "Date Acquired";
            this.DateAcquired.MinimumWidth = 20;
            this.DateAcquired.Name = "DateAcquired";
            this.DateAcquired.ReadOnly = true;
            this.DateAcquired.Width = 200;
            // 
            // Donor
            // 
            this.Donor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Donor.DefaultCellStyle = dataGridViewCellStyle29;
            this.Donor.FillWeight = 200F;
            this.Donor.HeaderText = "Donor";
            this.Donor.MinimumWidth = 30;
            this.Donor.Name = "Donor";
            this.Donor.ReadOnly = true;
            this.Donor.Width = 200;
            // 
            // SupplierName
            // 
            this.SupplierName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SupplierName.DefaultCellStyle = dataGridViewCellStyle30;
            this.SupplierName.FillWeight = 170F;
            this.SupplierName.HeaderText = "Supplier";
            this.SupplierName.MinimumWidth = 30;
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.ReadOnly = true;
            this.SupplierName.Width = 200;
            // 
            // Cost
            // 
            this.Cost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Cost.DefaultCellStyle = dataGridViewCellStyle31;
            this.Cost.HeaderText = "Total Cost";
            this.Cost.MinimumWidth = 20;
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            this.Cost.Width = 200;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle32;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 20;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 200;
            // 
            // BookID
            // 
            this.BookID.FillWeight = 10F;
            this.BookID.HeaderText = "BookID";
            this.BookID.MinimumWidth = 10;
            this.BookID.Name = "BookID";
            this.BookID.ReadOnly = true;
            this.BookID.Visible = false;
            // 
            // SupplierID
            // 
            this.SupplierID.HeaderText = "SupplierID";
            this.SupplierID.MinimumWidth = 10;
            this.SupplierID.Name = "SupplierID";
            this.SupplierID.ReadOnly = true;
            this.SupplierID.Visible = false;
            // 
            // Update
            // 
            this.Update.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle33.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle33.NullValue")));
            dataGridViewCellStyle33.Padding = new System.Windows.Forms.Padding(10);
            this.Update.DefaultCellStyle = dataGridViewCellStyle33;
            this.Update.FillWeight = 80F;
            this.Update.HeaderText = "Update";
            this.Update.Image = global::ubnhs_lms.Properties.Resources.pen;
            this.Update.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Update.MinimumWidth = 80;
            this.Update.Name = "Update";
            this.Update.ReadOnly = true;
            this.Update.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Update.Width = 80;
            // 
            // Delete
            // 
            this.Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle34.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle34.NullValue")));
            dataGridViewCellStyle34.Padding = new System.Windows.Forms.Padding(10);
            this.Delete.DefaultCellStyle = dataGridViewCellStyle34;
            this.Delete.FillWeight = 80F;
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = global::ubnhs_lms.Properties.Resources.bin;
            this.Delete.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Delete.MinimumWidth = 80;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Delete.Width = 81;
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
            this.btnAdd.Location = new System.Drawing.Point(680, 505);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ShadowDecoration.BorderRadius = 1;
            this.btnAdd.ShadowDecoration.Color = System.Drawing.Color.RosyBrown;
            this.btnAdd.ShadowDecoration.Parent = this.btnAdd;
            this.btnAdd.Size = new System.Drawing.Size(90, 35);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this.dgvAcquisition;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 12;
            // 
            // lblLogo
            // 
            this.lblLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.DimGray;
            this.lblLogo.Location = new System.Drawing.Point(598, 13);
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
            this.txtSearch.Location = new System.Drawing.Point(10, 7);
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
            // formAcquisition
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(780, 548);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.cmbFilterMethod);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.dgvAcquisition);
            this.Controls.Add(this.btnAdd);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "formAcquisition";
            this.Text = "Acquisition Management";
            this.Load += new System.EventHandler(this.formAcquisition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcquisition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2HtmlLabel lblTotalCount;
        private System.Windows.Forms.ComboBox cmbFilterMethod;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAcquisition;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogo;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn AcquisitionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn MethodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn ISBN;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuthorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublisherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateAcquired;
        private System.Windows.Forms.DataGridViewTextBoxColumn Donor;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn BookID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridViewImageColumn btnUpdate;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.DataGridViewImageColumn Update;
    }
}