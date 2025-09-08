using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using MailAppNew;
using MailSendingApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailAppNew
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.Text = "Promotional SMS";
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            FilterData("");
            InitializeButtonDesign();
            this.Size = new Size(900, 800); //W:H
            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                                    SELECT CM_CODE, CM_GROUP, CM_FULLNAME, CM_LINKREF, CM_MOBILE1,CM_NIC
                                    FROM M_TBLCUSTOMER
                                    WHERE CM_CONSENT_STATUS=1 AND CM_MOBILE1 IS NOT NULL AND CM_MOBILE1 <> ''";

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

                    // Loyalty filter
                    List<string> genderFilters = new List<string>();
                    if (checkBox5.Checked) genderFilters.Add("CM_GENDER = 'M'"); 
                    if (checkBox6.Checked) genderFilters.Add("CM_GENDER = 'F'"); 
                    if (checkBox7.Checked) genderFilters.Add("(CM_GENDER IS NULL OR CM_GENDER = '')");

                    if (genderFilters.Count > 0)
                    {
                        string genderCondition = string.Join(" OR ", genderFilters);
                        query += $" AND ({genderCondition})";
                    }

                    // Add filtering only if searchValue is not empty
                    if (!string.IsNullOrWhiteSpace(searchValue) && !string.IsNullOrWhiteSpace(selectedColumn))
                    {
                        query += $" AND {selectedColumn} LIKE @search";
                    }

                    // Dates: DOB
                    if (!checkBox8.Checked && dateTimePicker1.Checked && dateTimePicker2.Checked)
                    {
                        query += $" AND CM_DOB BETWEEN @DobFrom AND @DobTo";
                    }
                       
                    // Dates: Anniversary
                    if (!checkBox9.Checked && dateTimePicker3.Checked && dateTimePicker4.Checked)
                    {
                        query += $" AND CM_ANNIVERSARY BETWEEN @AnnFrom AND @AnnTo";
                    }
                     
                    // Multi-select textbox filters
                    if (!string.IsNullOrWhiteSpace(textBox5.Text)) // GROUP
                    {
                        string[] groups = textBox5.Text.Split(',');
                        query += $" AND CM_GROUP IN ({string.Join(",", groups.Select(g => $"'{g.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox6.Text)) // LOCATION
                    {
                        string[] locs = textBox6.Text.Split(',');
                        query += $" AND CM_LOCCODE IN ({string.Join(",", locs.Select(l => $"'{l.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox7.Text)) // AREA
                    {
                        string[] areas = textBox7.Text.Split(',');
                        query += $" AND CM_AREA IN ({string.Join(",", areas.Select(a => $"'{a.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox8.Text)) // DESIGNATION
                    {
                        string[] designations = textBox8.Text.Split(',');
                        query += $" AND CM_DESIGNATION IN ({string.Join(",", designations.Select(d => $"'{d.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox9.Text)) // PROFESSION
                    {
                        string[] professions = textBox9.Text.Split(',');
                        query += $" AND CM_PROFESSION IN ({string.Join(",", professions.Select(p => $"'{p.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox10.Text)) // COMPANY
                    {
                        string[] companies = textBox10.Text.Split(',');
                        query += $" AND CM_COMPANY IN ({string.Join(",", companies.Select(c => $"'{c.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox11.Text)) // RELIGION
                    {
                        string[] religions = textBox11.Text.Split(',');
                        query += $" AND CM_RELIGION IN ({string.Join(",", religions.Select(r => $"'{r.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox12.Text)) // ETHNICITY
                    {
                        string[] ethnicities = textBox12.Text.Split(',');
                        query += $" AND CM_ETHNICITY IN ({string.Join(",", ethnicities.Select(e => $"'{e.Trim()}'"))})";
                    }

                    if (!string.IsNullOrWhiteSpace(textBox13.Text)) // LOYALTY
                    {
                        string[] loyalties = textBox13.Text.Split(',');
                        query += $" AND CM_LOYGROUP IN ({string.Join(",", loyalties.Select(l => $"'{l.Trim()}'"))})";
                    }

                    query += " ORDER BY CM_CODE;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(searchValue))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + searchValue + "%");
                        }

                        if (dateTimePicker1.Checked && dateTimePicker2.Checked)
                        {
                            cmd.Parameters.AddWithValue("@DobFrom", dateTimePicker1.Value.Date);
                            cmd.Parameters.AddWithValue("@DobTo", dateTimePicker2.Value.Date);
                        }

                        if (dateTimePicker3.Checked && dateTimePicker4.Checked)
                        {
                            cmd.Parameters.AddWithValue("@AnnFrom", dateTimePicker3.Value.Date);
                            cmd.Parameters.AddWithValue("@AnnTo", dateTimePicker4.Value.Date);
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
                                Width = 55
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
                dataGridView1.Columns["CM_NIC"].HeaderText = "NIC Number";

                dataGridView1.Columns["CM_CODE"].Width = 75;
                dataGridView1.Columns["CM_GROUP"].Width = 50;
                dataGridView1.Columns["CM_FULLNAME"].Width = 235;
                dataGridView1.Columns["CM_LINKREF"].Width = 85;
                dataGridView1.Columns["CM_MOBILE1"].Width = 80;
                dataGridView1.Columns["CM_NIC"].Width = 85;
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
                case "NIC Number": return "CM_NIC";
                case "Area": return "CM_AREA";
                case "Profession": return "CM_PROFESSION";
                case "Designation": return "CM_DESIGNATION";
                case "Religion": return "CM_RELIGION";
                case "Customer Code":
                default: return "CM_CODE";
            }
        }

        private void LoadDescription(string codeValue, string tableName, string codeColumn, string descColumn)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT {descColumn} FROM {tableName} WHERE {codeColumn} = @Code";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Code", codeValue);

                        object result = cmd.ExecuteScalar();
                        //textBox4.Text = result != null ? result.ToString() : "Not found";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading description: " + ex.Message);
                Logger.LogError("Error loading description: ", ex);
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
                //List<string> customers = new List<string>();
                List<(string CusCode, string MobileNo, string NIC)> selectedCustomers = new List<(string, string, string)>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells["Select"] is DataGridViewCheckBoxCell checkBoxCell)
                    {
                        bool isSelected = checkBoxCell.Value != null && (bool)checkBoxCell.Value;
                        if (!isSelected) continue;

                        string mobileNo = row.Cells["CM_MOBILE1"].Value?.ToString().Trim();
                        string cusCode = row.Cells["CM_CODE"].Value?.ToString().Trim();
                        string nic = row.Cells["CM_NIC"].Value?.ToString().Trim();

                        if (string.IsNullOrEmpty(mobileNo)) continue;

                        string digitsOnly = new string(mobileNo.Where(char.IsDigit).ToArray());
                        if (digitsOnly.Length < 9) continue;

                        string last9 = digitsOnly.Substring(digitsOnly.Length - 9);
                        string formattedNo = "94" + last9;
                        selectedCustomers.Add((cusCode, formattedNo, nic));
                    }
                }

                if (selectedCustomers.Count == 0)
                {
                    MessageBox.Show("No customers selected!");
                    Logger.LogInformation("No customers selected!");
                    return;
                }
                string message = textBox3.Text.Trim();
                var MultipleSMSSender = new MultipleSMSSender();
                int successCount = 0;

                foreach (var customer in selectedCustomers)
                {
                    bool sent = await MultipleSMSSender.SendSmsAsync(customer.MobileNo, message);

                    // Insert into U_TBLPROMOTIONSMS
                    await MultipleSMSSender.InsertPromotionSmsAsync(
                        customer.CusCode,
                        "00001",                      // PS_LOCCODE fixed
                        DateTime.Now,
                        customer.MobileNo,
                        sent ? 1 : 0,                 // PS_STATUS
                        "P",                          // PS_TYPE
                        message,
                        customer.NIC
                    );

                    if (sent) successCount++;
                }

                MessageBox.Show($"SMS sent successfully to {successCount} customer(s)!");
                Logger.LogInformation($"SMS sent successfully to {successCount} customer(s)!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Logger.LogError("Unhandled exception in the application", ex);
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

        private void textBox2_Leave_1(object sender, EventArgs e)
        {
            string codeValue = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(codeValue))
            {
                //textBox4.Text = "";
                return;
            }

            switch (cmbSearchField.SelectedItem?.ToString())
            {
                case "Customer Group":
                    LoadDescription(codeValue, "M_TBLCUSGROUPS", "CG_CODE", "CG_DESC");
                    break;

                case "Area":
                    LoadDescription(codeValue, "M_TBLAREA", "AM_CODE", "AM_DESC");
                    break;

                case "Profession":
                    LoadDescription(codeValue, "M_TBLPROFESSION", "CF_CODE", "CF_DESC");
                    break;

                case "Designation":
                    LoadDescription(codeValue, "M_TBLDESIGNATION", "CD_CODE", "CD_DESC");
                    break;

                case "Religion":
                    LoadDescription(codeValue, "M_TBLRELIGION", "CR_CODE", "CR_DESC");
                    break;

                default:
                    //textBox4.Text = "";
                    break;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("GROUP");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("LOCATION");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("AREA");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("DESIGNATION");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("PROFESSION");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox9.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("COMPANY");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox10.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("RELIGION");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox11.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("ETHNICITY");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox12.Text = string.Join(",", frm.SelectedCodes);
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Form9 frm = new Form9("LOYALTY");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                textBox13.Text = string.Join(",", frm.SelectedCodes);
            }
        }
    }

}
