using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;
using Org.BouncyCastle.Asn1.Crmf;

namespace ubnhs_lms
{
    public partial class Form1 : Form
    {      
        // fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        private string UserType; // login

        // constructor
        public Form1(string UserType) // login
        {

            InitializeComponent();

            this.UserType = UserType; // login

            if (UserType == "Assistant Librarian") //login
            {
                btnSettings.Visible = false;
                btnPenalty.Visible = false;
                btnUsers.Visible = false;
                conSettings.Visible = false;
            }

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(6, 35);
            pMenu.Controls.Add (leftBorderBtn);

           
            //OpenchildForm(new formDashboard());

            // form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        


        // structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(243, 105, 116);
        }

        // methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                // buttons
                currentBtn = (IconButton) senderBtn;
                // currentBtn.BackColor = Color.White;
                currentBtn.BackColor = Color.FromArgb(255, 242, 242);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                // left border button
                // leftBorderBtn.BackColor = color;
                // leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                // leftBorderBtn.Visible = true;
                // leftBorderBtn.BringToFront();

               

            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.White;
                currentBtn.ForeColor = Color.Silver;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor =  Color.Silver;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenchildForm(Form childForm)
        {
            if(currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pDesktop.Controls.Add(childForm);
            pDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }



        // drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void pNavBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
 

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formDashboard());
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formAuthors());
        }

        private void btnGenres_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formGenres());
        }

        private void btnPublishers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formPublishers());
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formSuppliers());
        }

        private void btnAcquisition_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formAcquisition());
        }

        private void btnCatalog_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formCatalog());
        }

        private void btnDepartments_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formDepartments());
        }

        private void btnGrades_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formGrades());
        }

        private void btnSections_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formSections());
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formMembers());
        }

        

        private void btnDues_Click(object sender, EventArgs e)
        {
            if (UserType == "Librarian") // login
            {
                ActivateButton(sender, RGBColors.color1);
                OpenchildForm(new formDues());
            }
            else
            {
                MessageBox.Show("You do not have permission to access the Users form.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            if (UserType == "Librarian") // login
            {
                ActivateButton(sender, RGBColors.color1);
                OpenchildForm(new formUsers(this.UserType));
            }
            else
            {
                MessageBox.Show("You do not have permission to access the Users form.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formTransaction());
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formBooks());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ActivateButton(btnDashboard, RGBColors.color1);

            txtUsertype.Text = $"{UserType}";
        }

        private void btnStrands_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            OpenchildForm(new formStrands());
        }

        bool bibliographyExpand = false;
        private void transBibliography_Tick(object sender, EventArgs e)
        {
            if (bibliographyExpand == false)
            {
                conBibliography.Height += 10;

                if (conBibliography.Height >= 210)
                {
                    transBibliography.Stop();
                    bibliographyExpand = true;
                }
            }
            else
            {
                conBibliography.Height -= 10;
                
                if (conBibliography.Height <= 35)
                {
                    transBibliography.Stop();
                    bibliographyExpand = false;
                }
            }

        }

        private void btnBibliography_Click(object sender, EventArgs e)
        {
            
            transBibliography.Start();
        }

        bool organizationExpand = false;
        private void transOrganization_Tick(object sender, EventArgs e)
        {
            if (organizationExpand == false)
            {
                conOrganization.Height += 10;

                if (conOrganization.Height >= 175)
                {
                    transOrganization.Stop();
                    organizationExpand = true;
                }
            }
            else
            {
                conOrganization.Height -= 10;

                if (conOrganization.Height <= 35)
                {
                    transOrganization.Stop();
                    organizationExpand = false;
                }
            }
        }

        private void btnOrganization_Click(object sender, EventArgs e)
        {
            
            transOrganization.Start();
        }

        bool settingsExpand = false;
        private void transSettings_Tick(object sender, EventArgs e)
        {
            if (settingsExpand == false)
            {
               conSettings.Height += 10;

                if (conSettings.Height >= 105)
                {
                    transSettings.Stop();
                    settingsExpand = true;
                }
            }
            else
            {
                conSettings.Height -= 10;

                if (conSettings.Height <= 35)
                {
                    transSettings.Stop();
                    settingsExpand = false;
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            transSettings.Start();
        }

        private void pMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1); // log out

            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else if (result == DialogResult.No)
            {
                return;
            }


        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
