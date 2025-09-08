using MailSendingApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MailAppNew
{
    public partial class Form3 : Form
    {
        private System.Windows.Forms.Timer timer;
        public Form3()
        {
            InitializeComponent();
            InitializeInternetConnectionTimer();
            this.Text = "Home";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Check internet connection status and update the status indicator
            bool isConnected = IsInternetConnected();
            status.Text = isConnected ? "Connected" : "Disconnected";
            status.ForeColor = isConnected ? Color.Green : Color.Red;
        }

        private void InitializeInternetConnectionTimer()
        {
            // Initialize and start the timer for checking internet connection status periodically
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000; // Check every 5 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        public bool IsInternetConnected()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private void DisplayServerID()
        {
            try
            {
                // Get the server information from the connection string
                string serverInfo = GetServerInfoFromConnectionString(Globalconfig.ConnectionString);

                // Display the server information in the label
                LBL_database.Text = serverInfo;
            }
            catch (Exception ex)
            {
                // Handle any exceptions if necessary
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private string GetServerInfoFromConnectionString(string connectionString)
        {
            // Split the connection string by semicolons
            string[] parts = connectionString.Split(';');

            // Search for the part containing server information
            foreach (string part in parts)
            {
                // Look for "Data Source=" or "Server=" part
                if (part.StartsWith("Data Source=") || part.StartsWith("Server="))
                {
                    // Extract the server information
                    string[] serverInfoParts = part.Split('=');
                    if (serverInfoParts.Length == 2)
                    {
                        return serverInfoParts[1]; // Return the server information
                    }
                }
            }

            return "Server information not found in connection string";
        }


        private void LBL_database_Click(object sender, EventArgs e)
        {
            DisplayServerID();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            this.Hide();
        }
    }
}
