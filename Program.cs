using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ubnhs_lms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    //Application.Run(new Form1());           
        //    Application.Run(new fLogin());
        //}

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (fLogin loginForm = new fLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    string userType = loginForm.UserType;   

                    Application.Run(new Form1(userType));
                }
            }
        }



    }
}
