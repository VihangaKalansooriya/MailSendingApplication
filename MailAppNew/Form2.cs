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
using System.Data.SqlClient;
using Serilog.Core;


namespace MailAppNew
{
    public partial class Form2 : Form
    {
        private string connectionString = Globalconfig.ConnectionString;
        private Label internetStatus;
        private mailapp mailAppInstance;
        private System.Windows.Forms.Timer timer;

        public Form2()
        {
            InitializeComponent();
            InitializeButtonDesign();
            LoadDataIntoDataGridView();
            mailAppInstance = new mailapp();
            dateTimePicker1.MaxDate = DateTime.Now;
            InitializeInternetConnectionTimer();
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

        private void LoadDataIntoDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT TB_ID, TB_RECEIVERMAIL, TB_LOCATION, TB_DESC, TB_RUNNO FROM M_TBLMAILDETAILS WHERE TB_STATUS=0";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.AllowUserToAddRows = false;
                            dataGridView1.ReadOnly = true;
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
                if (dataGridView1.Columns["Select"] == null)
                {
                    ConfigureColumnHeadersAndWidths();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in :", ex);
            }
        }

        private void ConfigureColumnHeadersAndWidths()
        {
            // Add checkbox column
            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                HeaderText = "Select",
                Name = "Select",
            };

            dataGridView1.Columns.Insert(5, checkBoxColumn);

            // Set header style
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle(dataGridView1.ColumnHeadersDefaultCellStyle);
            headerStyle.Font = new Font("Arial", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;

            // Set column headers alignment
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Prevent adding rows directly
            dataGridView1.AllowUserToAddRows = false;

            // Set column headers and widths
            dataGridView1.Columns["TB_ID"].HeaderText = "ID";
            dataGridView1.Columns["TB_RECEIVERMAIL"].HeaderText = "Receiver Email";
            dataGridView1.Columns["TB_LOCATION"].HeaderText = "Location";
            dataGridView1.Columns["TB_DESC"].HeaderText = "Type";
            dataGridView1.Columns["TB_RUNNO"].HeaderText = "Report Number";

            dataGridView1.Columns["TB_ID"].Width = 50;
            dataGridView1.Columns["TB_RECEIVERMAIL"].Width = 200;
            dataGridView1.Columns["TB_LOCATION"].Width = 100;
            dataGridView1.Columns["TB_DESC"].Width = 200;
            dataGridView1.Columns["TB_RUNNO"].Width = 120;
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = true;
                }
            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;

            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT TB_ID, TB_RECEIVERMAIL, TB_LOCATION, TB_DESC, TB_RUNNO " +
                                   "FROM M_TBLMAILDETAILS " +
                                   "WHERE CONVERT(DATE, TB_DATE) = @SelectedDate";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dataGridView1.DataSource = dataTable;

                            if (dataGridView1.Columns["Select"] == null)
                            {
                                ConfigureColumnHeadersAndWidths();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception in the application", ex);
            }
        }

        private void btn_DeselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null)
                {
                    checkBoxCell.Value = false;
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.Date;
            RB_01.Checked = false;
            RB_02.Checked = false;
            txt_Search.Text = string.Empty;
            LoadDataIntoDataGridView();
            //if (dataGridView1.Columns["Select"] == null)
            //{
            //    LoadDataIntoDataGridView();
            //}
        }

        private async void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Send.Enabled = false;
                btn_DeselectAll.Enabled = false;
                btn_SelectAll.Enabled = false;
                btn_Refresh.Enabled = false;


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["Select"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && (bool)checkBoxCell.EditedFormattedValue)
                    {
                        int id = Convert.ToInt32(row.Cells["TB_ID"].Value);
                        string recipientEmail = row.Cells["TB_RECEIVERMAIL"].Value.ToString();

                        await mailAppInstance.ProcessEmailAsync(recipientEmail, id);
                    }
                }

                btn_Send.Enabled = true;
                btn_DeselectAll.Enabled = true;
                btn_SelectAll.Enabled = true;
                btn_Refresh.Enabled = true;
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception in the application", ex);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string searchText = txt_Search.Text.Trim();

            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT TB_ID, TB_RECEIVERMAIL, TB_LOCATION, TB_DESC, TB_RUNNO " +
                                   "FROM M_TBLMAILDETAILS " +
                                   "WHERE TB_STATUS = @Status AND TB_RECEIVERMAIL LIKE @SearchText " +
                                   "AND CONVERT(DATE, TB_DATE) = @SelectedDate";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                        if (RB_01.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Status", 1);
                        }
                        else if (RB_02.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Status", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Status", 0);
                        }

                        cmd.Parameters.AddWithValue("@SelectedDate", dateTimePicker1.Value.Date);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception in the application", ex);
            }
        }

        public bool IsDatabaseConnected()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
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
    }
}
