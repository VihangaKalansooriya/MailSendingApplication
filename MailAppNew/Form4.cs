using DocumentFormat.OpenXml.Spreadsheet;
using MailSendingApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailAppNew;

namespace MailAppNew
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.Text = "Promotional SMS";
            FilterData("");
            InitializeButtonDesign();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void FilterData(string searchValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();

                    string selectedColumn = GetSelectedColumn();
                    string query = @"
                                    SELECT CM_CODE, CM_GROUP, CM_FULLNAME, CM_LINKREF, CM_MOBILE1
                                    FROM M_TBLCUSTOMER
                                    WHERE 1=1";

                    // Status filter based on checkboxes
                    List<string> statusFilters = new List<string>();
                    if (checkBox1.Checked) statusFilters.Add("'1'");  // Active
                    if (checkBox2.Checked) statusFilters.Add("'0'");  // Inactive

                    if (statusFilters.Count > 0)
                    {
                        string statusCondition = string.Join(",", statusFilters);
                        query += $" AND CM_STATUS IN ({statusCondition})";
                    }

                    // Loyalty filter
                    List<string> loyaltyFilters = new List<string>();
                    if (checkBox3.Checked) loyaltyFilters.Add("'1'"); // Loyalty = Yes
                    if (checkBox4.Checked) loyaltyFilters.Add("'0'"); // Loyalty = No

                    if (loyaltyFilters.Count > 0)
                    {
                        string loyaltyCondition = string.Join(",", loyaltyFilters);
                        query += $" AND CM_LOYALTY IN ({loyaltyCondition})";
                    }

                    // Add filtering only if searchValue is not empty
                    if (!string.IsNullOrWhiteSpace(searchValue) && !string.IsNullOrWhiteSpace(selectedColumn))
                    {
                        query += $" AND {selectedColumn} LIKE @search";
                    }

                    query += " ORDER BY CM_CODE;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(searchValue))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                        }

                        DataTable dt = new DataTable();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dataGridView1.DataSource = dt;
                        //dataGridView1.ReadOnly = true;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            col.ReadOnly = col.Name != "Select"; // Only "Select" column editable
                        }

                        if (dataGridView1.Columns["Select"] == null)
                        {
                            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                            {
                                HeaderText = "Select",
                                Name = "Select",
                                Width = 65
                            };
                            dataGridView1.Columns.Add(checkBoxColumn);
                        }
                    }
                }

                // Styling
                DataGridViewCellStyle headerStyle = new DataGridViewCellStyle(dataGridView1.ColumnHeadersDefaultCellStyle)
                {
                    Font = new System.Drawing.Font("Arial", 8, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                };
                dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;

                dataGridView1.AllowUserToAddRows = false;

                // Column headers & widths
                dataGridView1.Columns["CM_CODE"].HeaderText = "Customer Code";
                dataGridView1.Columns["CM_GROUP"].HeaderText = "Group";
                dataGridView1.Columns["CM_FULLNAME"].HeaderText = "Customer Name";
                dataGridView1.Columns["CM_LINKREF"].HeaderText = "Ref Code";
                dataGridView1.Columns["CM_MOBILE1"].HeaderText = "Mobile Number";

                dataGridView1.Columns["CM_CODE"].Width = 75;
                dataGridView1.Columns["CM_GROUP"].Width = 50;
                dataGridView1.Columns["CM_FULLNAME"].Width = 240;
                dataGridView1.Columns["CM_LINKREF"].Width = 90;
                dataGridView1.Columns["CM_MOBILE1"].Width = 90;
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception in the application", ex);
            }
        }


        private string GetSelectedColumn()
        {
            switch (cmbSearchField.SelectedItem?.ToString())
            {
                case "Customer Name": return "CM_FULLNAME";
                case "Customer Group": return "CM_GROUP";
                case "Ref Code": return "CM_LINKREF";
                case "Mobile Number": return "CM_MOBILE1";
                case "Customer Code":
                default: return "CM_CODE";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchText = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
                FilterData("");
            else
                FilterData(searchText);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Select"] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    checkBoxCell.Value = true; // Select the checkbox
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Select"] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    checkBoxCell.Value = false; // Unselect the checkbox
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> customers = new List<string>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells["Select"] is DataGridViewCheckBoxCell checkBoxCell)
                    {
                        bool isSelected = checkBoxCell.Value != null && (bool)checkBoxCell.Value;
                        if (!isSelected) continue;

                        string mobileNo = row.Cells["CM_MOBILE1"].Value?.ToString().Trim();
                        if (string.IsNullOrEmpty(mobileNo)) continue;

                        string digitsOnly = new string(mobileNo.Where(char.IsDigit).ToArray());
                        if (digitsOnly.Length < 9) continue;

                        string last9 = digitsOnly.Substring(digitsOnly.Length - 9);
                        string formattedNo = "94" + last9;
                        customers.Add(formattedNo);
                    }
                }

                if (customers.Count == 0)
                {
                    MessageBox.Show("No customers selected!");
                    return;
                }

                string message = textBox3.Text.Trim();
                SmsSender smsSender = new SmsSender();

                // Send SMS for all selected customers asynchronously
                await smsSender.InitializeAsync();

                // Send bulk SMS
                await smsSender.SendBulkSmsAsync(customers, message);

                MessageBox.Show("SMS sent successfully to " + customers.Count + " customer(s)!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }





        private void button6_Click(object sender, EventArgs e)
        {
            string singlish = textBox1.Text;   // get input
            string sinhala = SinhalaTranslator.ConvertToSinhala(singlish); // convert
            textBox3.Text = sinhala;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox3.Text.ToUpper();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty; 
            textBox3.Text = string.Empty;
        }
    }
}
