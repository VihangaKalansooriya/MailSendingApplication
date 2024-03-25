using System;
using System.Windows.Forms;

namespace MailAppNew
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        public Form1()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 3000;
            timer.Tick += Timer_Tick;
            timer.Start();

            LBL_Copyright.Parent = mainIMG;
            LBL_Copyright.BackColor = Color.Transparent;
            LBL_Version.Parent = mainIMG;
            LBL_Version.BackColor = Color.Transparent;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
