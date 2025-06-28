namespace ubnhs_lms
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pMenu = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new FontAwesome.Sharp.IconButton();
            this.conSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSettings = new FontAwesome.Sharp.IconButton();
            this.btnPenalty = new FontAwesome.Sharp.IconButton();
            this.btnUsers = new FontAwesome.Sharp.IconButton();
            this.conOrganization = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOrganization = new FontAwesome.Sharp.IconButton();
            this.btnDepartments = new FontAwesome.Sharp.IconButton();
            this.btnStrands = new FontAwesome.Sharp.IconButton();
            this.btnGrades = new FontAwesome.Sharp.IconButton();
            this.btnSections = new FontAwesome.Sharp.IconButton();
            this.conBibliography = new System.Windows.Forms.FlowLayoutPanel();
            this.btnBibliography = new FontAwesome.Sharp.IconButton();
            this.btnBook = new FontAwesome.Sharp.IconButton();
            this.btnAuthors = new FontAwesome.Sharp.IconButton();
            this.btnGenres = new FontAwesome.Sharp.IconButton();
            this.btnPublishers = new FontAwesome.Sharp.IconButton();
            this.btnSuppliers = new FontAwesome.Sharp.IconButton();
            this.btnMembers = new FontAwesome.Sharp.IconButton();
            this.btnTransactions = new FontAwesome.Sharp.IconButton();
            this.btnCatalog = new FontAwesome.Sharp.IconButton();
            this.btnAcquisition = new FontAwesome.Sharp.IconButton();
            this.btnDashboard = new FontAwesome.Sharp.IconButton();
            this.pSeparator = new Guna.UI2.WinForms.Guna2Panel();
            this.sLogo = new Guna.UI2.WinForms.Guna2Separator();
            this.pLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLogo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pbLogo = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pNavBar = new Guna.UI2.WinForms.Guna2Panel();
            this.txtUsertype = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnMin = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnMax = new Guna.UI2.WinForms.Guna2ControlBox();
            this.pDesktop = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.eDesktop = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.transBibliography = new System.Windows.Forms.Timer(this.components);
            this.transOrganization = new System.Windows.Forms.Timer(this.components);
            this.transSettings = new System.Windows.Forms.Timer(this.components);
            this.pMenu.SuspendLayout();
            this.conSettings.SuspendLayout();
            this.conOrganization.SuspendLayout();
            this.conBibliography.SuspendLayout();
            this.pSeparator.SuspendLayout();
            this.pLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.pNavBar.SuspendLayout();
            this.pDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pMenu
            // 
            this.pMenu.AutoScroll = true;
            this.pMenu.BackColor = System.Drawing.Color.White;
            this.pMenu.Controls.Add(this.btnLogout);
            this.pMenu.Controls.Add(this.conSettings);
            this.pMenu.Controls.Add(this.conOrganization);
            this.pMenu.Controls.Add(this.conBibliography);
            this.pMenu.Controls.Add(this.btnMembers);
            this.pMenu.Controls.Add(this.btnTransactions);
            this.pMenu.Controls.Add(this.btnCatalog);
            this.pMenu.Controls.Add(this.btnAcquisition);
            this.pMenu.Controls.Add(this.btnDashboard);
            this.pMenu.Controls.Add(this.pSeparator);
            this.pMenu.Controls.Add(this.pLogo);
            this.pMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pMenu.Location = new System.Drawing.Point(0, 0);
            this.pMenu.Name = "pMenu";
            this.pMenu.ShadowDecoration.Parent = this.pMenu;
            this.pMenu.Size = new System.Drawing.Size(263, 671);
            this.pMenu.TabIndex = 0;
            this.pMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.pMenu_Paint);
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.Silver;
            this.btnLogout.IconChar = FontAwesome.Sharp.IconChar.CircleArrowLeft;
            this.btnLogout.IconColor = System.Drawing.Color.Silver;
            this.btnLogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLogout.IconSize = 28;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(0, 333);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnLogout.Size = new System.Drawing.Size(263, 35);
            this.btnLogout.TabIndex = 21;
            this.btnLogout.Text = "   Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // conSettings
            // 
            this.conSettings.BackColor = System.Drawing.Color.White;
            this.conSettings.Controls.Add(this.btnSettings);
            this.conSettings.Controls.Add(this.btnPenalty);
            this.conSettings.Controls.Add(this.btnUsers);
            this.conSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.conSettings.Location = new System.Drawing.Point(0, 298);
            this.conSettings.Name = "conSettings";
            this.conSettings.Size = new System.Drawing.Size(263, 35);
            this.conSettings.TabIndex = 17;
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.Silver;
            this.btnSettings.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.btnSettings.IconColor = System.Drawing.Color.Silver;
            this.btnSettings.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSettings.IconSize = 25;
            this.btnSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.Location = new System.Drawing.Point(0, 0);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSettings.Size = new System.Drawing.Size(284, 35);
            this.btnSettings.TabIndex = 18;
            this.btnSettings.Text = "    Settings";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnPenalty
            // 
            this.btnPenalty.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPenalty.FlatAppearance.BorderSize = 0;
            this.btnPenalty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPenalty.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPenalty.ForeColor = System.Drawing.Color.Silver;
            this.btnPenalty.IconChar = FontAwesome.Sharp.IconChar.MoneyCheckDollar;
            this.btnPenalty.IconColor = System.Drawing.Color.Silver;
            this.btnPenalty.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPenalty.IconSize = 30;
            this.btnPenalty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPenalty.Location = new System.Drawing.Point(0, 35);
            this.btnPenalty.Margin = new System.Windows.Forms.Padding(0);
            this.btnPenalty.Name = "btnPenalty";
            this.btnPenalty.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnPenalty.Size = new System.Drawing.Size(284, 35);
            this.btnPenalty.TabIndex = 15;
            this.btnPenalty.Text = "   Penalty";
            this.btnPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPenalty.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPenalty.UseVisualStyleBackColor = true;
            this.btnPenalty.Click += new System.EventHandler(this.btnDues_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.ForeColor = System.Drawing.Color.Silver;
            this.btnUsers.IconChar = FontAwesome.Sharp.IconChar.UserEdit;
            this.btnUsers.IconColor = System.Drawing.Color.Silver;
            this.btnUsers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUsers.IconSize = 30;
            this.btnUsers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.Location = new System.Drawing.Point(0, 70);
            this.btnUsers.Margin = new System.Windows.Forms.Padding(0);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnUsers.Size = new System.Drawing.Size(284, 35);
            this.btnUsers.TabIndex = 16;
            this.btnUsers.Text = "   User";
            this.btnUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // conOrganization
            // 
            this.conOrganization.BackColor = System.Drawing.Color.White;
            this.conOrganization.Controls.Add(this.btnOrganization);
            this.conOrganization.Controls.Add(this.btnDepartments);
            this.conOrganization.Controls.Add(this.btnStrands);
            this.conOrganization.Controls.Add(this.btnGrades);
            this.conOrganization.Controls.Add(this.btnSections);
            this.conOrganization.Dock = System.Windows.Forms.DockStyle.Top;
            this.conOrganization.Location = new System.Drawing.Point(0, 263);
            this.conOrganization.Name = "conOrganization";
            this.conOrganization.Size = new System.Drawing.Size(263, 35);
            this.conOrganization.TabIndex = 19;
            // 
            // btnOrganization
            // 
            this.btnOrganization.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOrganization.FlatAppearance.BorderSize = 0;
            this.btnOrganization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrganization.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrganization.ForeColor = System.Drawing.Color.Silver;
            this.btnOrganization.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.btnOrganization.IconColor = System.Drawing.Color.Silver;
            this.btnOrganization.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOrganization.IconSize = 25;
            this.btnOrganization.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrganization.Location = new System.Drawing.Point(0, 0);
            this.btnOrganization.Margin = new System.Windows.Forms.Padding(0);
            this.btnOrganization.Name = "btnOrganization";
            this.btnOrganization.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnOrganization.Size = new System.Drawing.Size(284, 35);
            this.btnOrganization.TabIndex = 20;
            this.btnOrganization.Text = "    Organization";
            this.btnOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrganization.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOrganization.UseVisualStyleBackColor = true;
            this.btnOrganization.Click += new System.EventHandler(this.btnOrganization_Click);
            // 
            // btnDepartments
            // 
            this.btnDepartments.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDepartments.FlatAppearance.BorderSize = 0;
            this.btnDepartments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDepartments.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepartments.ForeColor = System.Drawing.Color.Silver;
            this.btnDepartments.IconChar = FontAwesome.Sharp.IconChar.School;
            this.btnDepartments.IconColor = System.Drawing.Color.Silver;
            this.btnDepartments.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDepartments.IconSize = 30;
            this.btnDepartments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDepartments.Location = new System.Drawing.Point(0, 35);
            this.btnDepartments.Margin = new System.Windows.Forms.Padding(0);
            this.btnDepartments.Name = "btnDepartments";
            this.btnDepartments.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnDepartments.Size = new System.Drawing.Size(284, 35);
            this.btnDepartments.TabIndex = 10;
            this.btnDepartments.Text = "   Department";
            this.btnDepartments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDepartments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDepartments.UseVisualStyleBackColor = true;
            this.btnDepartments.Click += new System.EventHandler(this.btnDepartments_Click);
            // 
            // btnStrands
            // 
            this.btnStrands.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStrands.FlatAppearance.BorderSize = 0;
            this.btnStrands.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStrands.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStrands.ForeColor = System.Drawing.Color.Silver;
            this.btnStrands.IconChar = FontAwesome.Sharp.IconChar.UsersGear;
            this.btnStrands.IconColor = System.Drawing.Color.Silver;
            this.btnStrands.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStrands.IconSize = 30;
            this.btnStrands.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStrands.Location = new System.Drawing.Point(0, 70);
            this.btnStrands.Margin = new System.Windows.Forms.Padding(0);
            this.btnStrands.Name = "btnStrands";
            this.btnStrands.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnStrands.Size = new System.Drawing.Size(284, 35);
            this.btnStrands.TabIndex = 18;
            this.btnStrands.Text = "   Strand";
            this.btnStrands.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStrands.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStrands.UseVisualStyleBackColor = true;
            this.btnStrands.Click += new System.EventHandler(this.btnStrands_Click);
            // 
            // btnGrades
            // 
            this.btnGrades.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGrades.FlatAppearance.BorderSize = 0;
            this.btnGrades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrades.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrades.ForeColor = System.Drawing.Color.Silver;
            this.btnGrades.IconChar = FontAwesome.Sharp.IconChar.Stairs;
            this.btnGrades.IconColor = System.Drawing.Color.Silver;
            this.btnGrades.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGrades.IconSize = 30;
            this.btnGrades.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrades.Location = new System.Drawing.Point(0, 105);
            this.btnGrades.Margin = new System.Windows.Forms.Padding(0);
            this.btnGrades.Name = "btnGrades";
            this.btnGrades.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnGrades.Size = new System.Drawing.Size(284, 35);
            this.btnGrades.TabIndex = 11;
            this.btnGrades.Text = "   Grade Level";
            this.btnGrades.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrades.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGrades.UseVisualStyleBackColor = true;
            this.btnGrades.Click += new System.EventHandler(this.btnGrades_Click);
            // 
            // btnSections
            // 
            this.btnSections.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSections.FlatAppearance.BorderSize = 0;
            this.btnSections.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSections.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSections.ForeColor = System.Drawing.Color.Silver;
            this.btnSections.IconChar = FontAwesome.Sharp.IconChar.FolderTree;
            this.btnSections.IconColor = System.Drawing.Color.Silver;
            this.btnSections.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSections.IconSize = 30;
            this.btnSections.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSections.Location = new System.Drawing.Point(0, 140);
            this.btnSections.Margin = new System.Windows.Forms.Padding(0);
            this.btnSections.Name = "btnSections";
            this.btnSections.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSections.Size = new System.Drawing.Size(284, 35);
            this.btnSections.TabIndex = 12;
            this.btnSections.Text = "   Section";
            this.btnSections.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSections.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSections.UseVisualStyleBackColor = true;
            this.btnSections.Click += new System.EventHandler(this.btnSections_Click);
            // 
            // conBibliography
            // 
            this.conBibliography.BackColor = System.Drawing.Color.White;
            this.conBibliography.Controls.Add(this.btnBibliography);
            this.conBibliography.Controls.Add(this.btnBook);
            this.conBibliography.Controls.Add(this.btnAuthors);
            this.conBibliography.Controls.Add(this.btnGenres);
            this.conBibliography.Controls.Add(this.btnPublishers);
            this.conBibliography.Controls.Add(this.btnSuppliers);
            this.conBibliography.Dock = System.Windows.Forms.DockStyle.Top;
            this.conBibliography.Location = new System.Drawing.Point(0, 228);
            this.conBibliography.Name = "conBibliography";
            this.conBibliography.Size = new System.Drawing.Size(263, 35);
            this.conBibliography.TabIndex = 19;
            // 
            // btnBibliography
            // 
            this.btnBibliography.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBibliography.FlatAppearance.BorderSize = 0;
            this.btnBibliography.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBibliography.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBibliography.ForeColor = System.Drawing.Color.Silver;
            this.btnBibliography.IconChar = FontAwesome.Sharp.IconChar.AngleDown;
            this.btnBibliography.IconColor = System.Drawing.Color.Silver;
            this.btnBibliography.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBibliography.IconSize = 25;
            this.btnBibliography.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBibliography.Location = new System.Drawing.Point(0, 0);
            this.btnBibliography.Margin = new System.Windows.Forms.Padding(0);
            this.btnBibliography.Name = "btnBibliography";
            this.btnBibliography.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnBibliography.Size = new System.Drawing.Size(284, 35);
            this.btnBibliography.TabIndex = 20;
            this.btnBibliography.Text = "    Book Info";
            this.btnBibliography.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBibliography.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBibliography.UseVisualStyleBackColor = true;
            this.btnBibliography.Click += new System.EventHandler(this.btnBibliography_Click);
            // 
            // btnBook
            // 
            this.btnBook.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBook.FlatAppearance.BorderSize = 0;
            this.btnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBook.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBook.ForeColor = System.Drawing.Color.Silver;
            this.btnBook.IconChar = FontAwesome.Sharp.IconChar.BookOpen;
            this.btnBook.IconColor = System.Drawing.Color.Silver;
            this.btnBook.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBook.IconSize = 30;
            this.btnBook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBook.Location = new System.Drawing.Point(0, 35);
            this.btnBook.Margin = new System.Windows.Forms.Padding(0);
            this.btnBook.Name = "btnBook";
            this.btnBook.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnBook.Size = new System.Drawing.Size(284, 35);
            this.btnBook.TabIndex = 17;
            this.btnBook.Text = "   Book";
            this.btnBook.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // btnAuthors
            // 
            this.btnAuthors.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAuthors.FlatAppearance.BorderSize = 0;
            this.btnAuthors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuthors.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAuthors.ForeColor = System.Drawing.Color.Silver;
            this.btnAuthors.IconChar = FontAwesome.Sharp.IconChar.BookOpenReader;
            this.btnAuthors.IconColor = System.Drawing.Color.Silver;
            this.btnAuthors.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAuthors.IconSize = 30;
            this.btnAuthors.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAuthors.Location = new System.Drawing.Point(0, 70);
            this.btnAuthors.Margin = new System.Windows.Forms.Padding(0);
            this.btnAuthors.Name = "btnAuthors";
            this.btnAuthors.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnAuthors.Size = new System.Drawing.Size(284, 35);
            this.btnAuthors.TabIndex = 3;
            this.btnAuthors.Text = "   Author";
            this.btnAuthors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAuthors.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAuthors.UseVisualStyleBackColor = true;
            this.btnAuthors.Click += new System.EventHandler(this.btnAuthors_Click);
            // 
            // btnGenres
            // 
            this.btnGenres.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenres.FlatAppearance.BorderSize = 0;
            this.btnGenres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenres.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenres.ForeColor = System.Drawing.Color.Silver;
            this.btnGenres.IconChar = FontAwesome.Sharp.IconChar.BookQuran;
            this.btnGenres.IconColor = System.Drawing.Color.Silver;
            this.btnGenres.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGenres.IconSize = 30;
            this.btnGenres.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenres.Location = new System.Drawing.Point(0, 105);
            this.btnGenres.Margin = new System.Windows.Forms.Padding(0);
            this.btnGenres.Name = "btnGenres";
            this.btnGenres.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnGenres.Size = new System.Drawing.Size(284, 35);
            this.btnGenres.TabIndex = 4;
            this.btnGenres.Text = "   Genre";
            this.btnGenres.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenres.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenres.UseVisualStyleBackColor = true;
            this.btnGenres.Click += new System.EventHandler(this.btnGenres_Click);
            // 
            // btnPublishers
            // 
            this.btnPublishers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPublishers.FlatAppearance.BorderSize = 0;
            this.btnPublishers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublishers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublishers.ForeColor = System.Drawing.Color.Silver;
            this.btnPublishers.IconChar = FontAwesome.Sharp.IconChar.BuildingCircleArrowRight;
            this.btnPublishers.IconColor = System.Drawing.Color.Silver;
            this.btnPublishers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPublishers.IconSize = 30;
            this.btnPublishers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPublishers.Location = new System.Drawing.Point(0, 140);
            this.btnPublishers.Margin = new System.Windows.Forms.Padding(0);
            this.btnPublishers.Name = "btnPublishers";
            this.btnPublishers.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnPublishers.Size = new System.Drawing.Size(284, 35);
            this.btnPublishers.TabIndex = 5;
            this.btnPublishers.Text = "   Publisher";
            this.btnPublishers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPublishers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPublishers.UseVisualStyleBackColor = true;
            this.btnPublishers.Click += new System.EventHandler(this.btnPublishers_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSuppliers.FlatAppearance.BorderSize = 0;
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppliers.ForeColor = System.Drawing.Color.Silver;
            this.btnSuppliers.IconChar = FontAwesome.Sharp.IconChar.Truck;
            this.btnSuppliers.IconColor = System.Drawing.Color.Silver;
            this.btnSuppliers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSuppliers.IconSize = 30;
            this.btnSuppliers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSuppliers.Location = new System.Drawing.Point(0, 175);
            this.btnSuppliers.Margin = new System.Windows.Forms.Padding(0);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSuppliers.Size = new System.Drawing.Size(284, 35);
            this.btnSuppliers.TabIndex = 6;
            this.btnSuppliers.Text = "   Supplier";
            this.btnSuppliers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSuppliers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnMembers
            // 
            this.btnMembers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMembers.FlatAppearance.BorderSize = 0;
            this.btnMembers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMembers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMembers.ForeColor = System.Drawing.Color.Silver;
            this.btnMembers.IconChar = FontAwesome.Sharp.IconChar.UsersRectangle;
            this.btnMembers.IconColor = System.Drawing.Color.Silver;
            this.btnMembers.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMembers.IconSize = 30;
            this.btnMembers.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembers.Location = new System.Drawing.Point(0, 193);
            this.btnMembers.Name = "btnMembers";
            this.btnMembers.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnMembers.Size = new System.Drawing.Size(263, 35);
            this.btnMembers.TabIndex = 13;
            this.btnMembers.Text = "   Member";
            this.btnMembers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMembers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMembers.UseVisualStyleBackColor = true;
            this.btnMembers.Click += new System.EventHandler(this.btnMembers_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransactions.FlatAppearance.BorderSize = 0;
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransactions.ForeColor = System.Drawing.Color.Silver;
            this.btnTransactions.IconChar = FontAwesome.Sharp.IconChar.ArrowsSpin;
            this.btnTransactions.IconColor = System.Drawing.Color.Silver;
            this.btnTransactions.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnTransactions.IconSize = 30;
            this.btnTransactions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransactions.Location = new System.Drawing.Point(0, 158);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnTransactions.Size = new System.Drawing.Size(263, 35);
            this.btnTransactions.TabIndex = 14;
            this.btnTransactions.Text = "   Circulation";
            this.btnTransactions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransactions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTransactions.UseVisualStyleBackColor = true;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            // 
            // btnCatalog
            // 
            this.btnCatalog.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCatalog.FlatAppearance.BorderSize = 0;
            this.btnCatalog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCatalog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCatalog.ForeColor = System.Drawing.Color.Silver;
            this.btnCatalog.IconChar = FontAwesome.Sharp.IconChar.Swatchbook;
            this.btnCatalog.IconColor = System.Drawing.Color.Silver;
            this.btnCatalog.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCatalog.IconSize = 30;
            this.btnCatalog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCatalog.Location = new System.Drawing.Point(0, 123);
            this.btnCatalog.Name = "btnCatalog";
            this.btnCatalog.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnCatalog.Size = new System.Drawing.Size(263, 35);
            this.btnCatalog.TabIndex = 8;
            this.btnCatalog.Text = "   Accession";
            this.btnCatalog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCatalog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCatalog.UseVisualStyleBackColor = true;
            this.btnCatalog.Click += new System.EventHandler(this.btnCatalog_Click);
            // 
            // btnAcquisition
            // 
            this.btnAcquisition.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAcquisition.FlatAppearance.BorderSize = 0;
            this.btnAcquisition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAcquisition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcquisition.ForeColor = System.Drawing.Color.Silver;
            this.btnAcquisition.IconChar = FontAwesome.Sharp.IconChar.HandshakeSimple;
            this.btnAcquisition.IconColor = System.Drawing.Color.Silver;
            this.btnAcquisition.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAcquisition.IconSize = 30;
            this.btnAcquisition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcquisition.Location = new System.Drawing.Point(0, 88);
            this.btnAcquisition.Name = "btnAcquisition";
            this.btnAcquisition.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnAcquisition.Size = new System.Drawing.Size(263, 35);
            this.btnAcquisition.TabIndex = 7;
            this.btnAcquisition.Text = "   Acquisition";
            this.btnAcquisition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAcquisition.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAcquisition.UseVisualStyleBackColor = true;
            this.btnAcquisition.Click += new System.EventHandler(this.btnAcquisition_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.Silver;
            this.btnDashboard.IconChar = FontAwesome.Sharp.IconChar.ChartSimple;
            this.btnDashboard.IconColor = System.Drawing.Color.Silver;
            this.btnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDashboard.IconSize = 30;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(0, 53);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnDashboard.Size = new System.Drawing.Size(263, 35);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Text = "   Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // pSeparator
            // 
            this.pSeparator.Controls.Add(this.sLogo);
            this.pSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSeparator.Location = new System.Drawing.Point(0, 43);
            this.pSeparator.Name = "pSeparator";
            this.pSeparator.ShadowDecoration.Parent = this.pSeparator;
            this.pSeparator.Size = new System.Drawing.Size(263, 10);
            this.pSeparator.TabIndex = 1;
            // 
            // sLogo
            // 
            this.sLogo.FillColor = System.Drawing.Color.Silver;
            this.sLogo.Location = new System.Drawing.Point(6, -4);
            this.sLogo.Name = "sLogo";
            this.sLogo.Size = new System.Drawing.Size(275, 10);
            this.sLogo.TabIndex = 0;
            // 
            // pLogo
            // 
            this.pLogo.Controls.Add(this.lblLogo);
            this.pLogo.Controls.Add(this.pbLogo);
            this.pLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pLogo.Location = new System.Drawing.Point(0, 0);
            this.pLogo.Name = "pLogo";
            this.pLogo.ShadowDecoration.Parent = this.pLogo;
            this.pLogo.Size = new System.Drawing.Size(263, 43);
            this.pLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.lblLogo.Location = new System.Drawing.Point(63, 11);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(120, 30);
            this.lblLogo.TabIndex = 1;
            this.lblLogo.Text = "UBNHS-LMS";
            this.lblLogo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbLogo
            // 
            this.pbLogo.BackColor = System.Drawing.Color.Transparent;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(17, 1);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.ShadowDecoration.Parent = this.pbLogo;
            this.pbLogo.Size = new System.Drawing.Size(44, 43);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pNavBar
            // 
            this.pNavBar.BackColor = System.Drawing.Color.White;
            this.pNavBar.Controls.Add(this.txtUsertype);
            this.pNavBar.Controls.Add(this.lblTitle);
            this.pNavBar.Controls.Add(this.btnMin);
            this.pNavBar.Controls.Add(this.btnMax);
            this.pNavBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pNavBar.ForeColor = System.Drawing.Color.Black;
            this.pNavBar.Location = new System.Drawing.Point(263, 0);
            this.pNavBar.Name = "pNavBar";
            this.pNavBar.ShadowDecoration.Parent = this.pNavBar;
            this.pNavBar.Size = new System.Drawing.Size(893, 43);
            this.pNavBar.TabIndex = 1;
            this.pNavBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pNavBar_MouseDown);
            // 
            // txtUsertype
            // 
            this.txtUsertype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsertype.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsertype.DefaultText = "";
            this.txtUsertype.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtUsertype.DisabledState.FillColor = System.Drawing.Color.White;
            this.txtUsertype.DisabledState.ForeColor = System.Drawing.Color.Black;
            this.txtUsertype.DisabledState.Parent = this.txtUsertype;
            this.txtUsertype.DisabledState.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtUsertype.Enabled = false;
            this.txtUsertype.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.txtUsertype.FocusedState.Parent = this.txtUsertype;
            this.txtUsertype.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsertype.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtUsertype.HoverState.Parent = this.txtUsertype;
            this.txtUsertype.Location = new System.Drawing.Point(702, 8);
            this.txtUsertype.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsertype.Name = "txtUsertype";
            this.txtUsertype.PasswordChar = '\0';
            this.txtUsertype.PlaceholderText = "";
            this.txtUsertype.SelectedText = "";
            this.txtUsertype.ShadowDecoration.Parent = this.txtUsertype;
            this.txtUsertype.Size = new System.Drawing.Size(110, 28);
            this.txtUsertype.TabIndex = 7;
            this.txtUsertype.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(19, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(57, 27);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Home";
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.Animated = true;
            this.btnMin.BorderColor = System.Drawing.Color.Silver;
            this.btnMin.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.btnMin.FillColor = System.Drawing.Color.White;
            this.btnMin.HoverState.IconColor = System.Drawing.Color.Brown;
            this.btnMin.HoverState.Parent = this.btnMin;
            this.btnMin.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.btnMin.Location = new System.Drawing.Point(819, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.PressedColor = System.Drawing.Color.Brown;
            this.btnMin.ShadowDecoration.Parent = this.btnMin;
            this.btnMin.Size = new System.Drawing.Size(33, 40);
            this.btnMin.TabIndex = 3;
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.Animated = true;
            this.btnMax.BorderColor = System.Drawing.Color.Silver;
            this.btnMax.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.btnMax.FillColor = System.Drawing.Color.White;
            this.btnMax.HoverState.IconColor = System.Drawing.Color.Brown;
            this.btnMax.HoverState.Parent = this.btnMax;
            this.btnMax.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.btnMax.Location = new System.Drawing.Point(848, 0);
            this.btnMax.Name = "btnMax";
            this.btnMax.PressedColor = System.Drawing.Color.Brown;
            this.btnMax.ShadowDecoration.Parent = this.btnMax;
            this.btnMax.Size = new System.Drawing.Size(33, 40);
            this.btnMax.TabIndex = 2;
            // 
            // pDesktop
            // 
            this.pDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pDesktop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pDesktop.Controls.Add(this.guna2HtmlLabel2);
            this.pDesktop.Controls.Add(this.guna2HtmlLabel1);
            this.pDesktop.Controls.Add(this.guna2PictureBox1);
            this.pDesktop.Location = new System.Drawing.Point(274, 43);
            this.pDesktop.Name = "pDesktop";
            this.pDesktop.ShadowDecoration.Parent = this.pDesktop;
            this.pDesktop.Size = new System.Drawing.Size(870, 616);
            this.pDesktop.TabIndex = 2;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(205, 461);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(546, 56);
            this.guna2HtmlLabel2.TabIndex = 6;
            this.guna2HtmlLabel2.Text = "Library Management System";
            this.guna2HtmlLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(105)))), ((int)(((byte)(116)))));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(116, 407);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(787, 64);
            this.guna2HtmlLabel1.TabIndex = 5;
            this.guna2HtmlLabel1.Text = "Upper Bicutan National High School";
            this.guna2HtmlLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.Location = new System.Drawing.Point(3, 0);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(867, 411);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 4;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // eDesktop
            // 
            this.eDesktop.TargetControl = this.pDesktop;
            // 
            // transBibliography
            // 
            this.transBibliography.Interval = 1;
            this.transBibliography.Tick += new System.EventHandler(this.transBibliography_Tick);
            // 
            // transOrganization
            // 
            this.transOrganization.Interval = 1;
            this.transOrganization.Tick += new System.EventHandler(this.transOrganization_Tick);
            // 
            // transSettings
            // 
            this.transSettings.Interval = 1;
            this.transSettings.Tick += new System.EventHandler(this.transSettings_Tick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1156, 671);
            this.Controls.Add(this.pDesktop);
            this.Controls.Add(this.pNavBar);
            this.Controls.Add(this.pMenu);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pMenu.ResumeLayout(false);
            this.conSettings.ResumeLayout(false);
            this.conOrganization.ResumeLayout(false);
            this.conBibliography.ResumeLayout(false);
            this.pSeparator.ResumeLayout(false);
            this.pLogo.ResumeLayout(false);
            this.pLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.pNavBar.ResumeLayout(false);
            this.pNavBar.PerformLayout();
            this.pDesktop.ResumeLayout(false);
            this.pDesktop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pMenu;
        private Guna.UI2.WinForms.Guna2Panel pLogo;
        private Guna.UI2.WinForms.Guna2Panel pNavBar;
        private Guna.UI2.WinForms.Guna2Panel pSeparator;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLogo;
        private Guna.UI2.WinForms.Guna2Panel pDesktop;
        private Guna.UI2.WinForms.Guna2Elipse eDesktop;
        private Guna.UI2.WinForms.Guna2Separator sLogo;
        private Guna.UI2.WinForms.Guna2ControlBox btnMin;
        private Guna.UI2.WinForms.Guna2ControlBox btnMax;
        private Guna.UI2.WinForms.Guna2PictureBox pbLogo;
        private FontAwesome.Sharp.IconButton btnUsers;
        private FontAwesome.Sharp.IconButton btnSections;
        private FontAwesome.Sharp.IconButton btnGrades;
        private FontAwesome.Sharp.IconButton btnDepartments;
        private FontAwesome.Sharp.IconButton btnMembers;
        private FontAwesome.Sharp.IconButton btnPenalty;
        private FontAwesome.Sharp.IconButton btnBook;
        private FontAwesome.Sharp.IconButton btnSuppliers;
        private FontAwesome.Sharp.IconButton btnPublishers;
        private FontAwesome.Sharp.IconButton btnGenres;
        private FontAwesome.Sharp.IconButton btnAuthors;
        private FontAwesome.Sharp.IconButton btnTransactions;
        private FontAwesome.Sharp.IconButton btnCatalog;
        private FontAwesome.Sharp.IconButton btnAcquisition;
        private FontAwesome.Sharp.IconButton btnDashboard;
        private FontAwesome.Sharp.IconButton btnStrands;
        private FontAwesome.Sharp.IconButton btnBibliography;
        private System.Windows.Forms.FlowLayoutPanel conBibliography;
        private System.Windows.Forms.Timer transBibliography;
        private System.Windows.Forms.FlowLayoutPanel conOrganization;
        private FontAwesome.Sharp.IconButton btnOrganization;
        private System.Windows.Forms.Timer transOrganization;
        private System.Windows.Forms.FlowLayoutPanel conSettings;
        private FontAwesome.Sharp.IconButton btnSettings;
        private System.Windows.Forms.Timer transSettings;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private FontAwesome.Sharp.IconButton btnLogout;
        private Guna.UI2.WinForms.Guna2TextBox txtUsertype;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
    }
}

