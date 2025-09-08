using MailSendingApp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace MailAppNew
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            InitializeButtonDesign();
            radioButton1.Checked = true;
            dateTimePicker1.Value = DateTime.Today;
            FilterData(textBox1.Text.Trim(), DateTime.Today,1);
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Search button click
            string searchText = textBox1.Text.Trim();
            FilterData(searchText, dateTimePicker1.Value.Date);
        }

        private void FilterData(string searchValue, DateTime? filterDate = null, int? statusFilter = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();

                    string query = @"SELECT 
                                     ps.PS_CUSCODE, c.CM_GROUP, ps.PS_MOBILENO,ps.PS_NIC,ps.PS_BODY, ps.PS_STATUS
                                     FROM U_TBLPROMOTIONSMS ps
                                     LEFT JOIN M_TBLCUSTOMER c ON c.CM_CODE = ps.PS_CUSCODE
                                     WHERE ps.PS_TYPE='B' AND (ps.PS_CUSCODE LIKE @searchValue
                                     OR ps.PS_MOBILENO LIKE @searchValue
                                     OR ps.PS_NIC LIKE @searchValue)";

                    if (filterDate.HasValue)
                        query += " AND CAST(PS_DATE AS DATE) = @filterDate";
                    if (statusFilter.HasValue)
                        query += " AND ps.PS_STATUS = @statusFilter";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");
                        if (filterDate.HasValue)
                            cmd.Parameters.AddWithValue("@filterDate", filterDate.Value.Date);
                        if (statusFilter.HasValue)
                            cmd.Parameters.AddWithValue("@statusFilter", statusFilter.Value);

                        DataTable dt = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = dt;
                        dataGridView1.ReadOnly = true;
                        dataGridView1.AllowUserToAddRows = false;

                        // Styling
                        DataGridViewCellStyle headerStyle = new DataGridViewCellStyle(dataGridView1.ColumnHeadersDefaultCellStyle)
                        {
                            Font = new Font("Arial", 8, FontStyle.Bold),
                            Alignment = DataGridViewContentAlignment.MiddleCenter
                        };
                        dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;

                        // Column headers & widths
                        if (dataGridView1.Columns.Contains("PS_CUSCODE"))
                        {
                            dataGridView1.Columns["PS_CUSCODE"].HeaderText = "Customer Code";
                            dataGridView1.Columns["PS_CUSCODE"].Width = 70;
                        }
                        if (dataGridView1.Columns.Contains("CM_GROUP"))
                        {
                            dataGridView1.Columns["CM_GROUP"].HeaderText = "Customer Group";
                            dataGridView1.Columns["CM_GROUP"].Width = 70;
                        }
                        if (dataGridView1.Columns.Contains("PS_MOBILENO"))
                        {
                            dataGridView1.Columns["PS_MOBILENO"].HeaderText = "Mobile Number";
                            dataGridView1.Columns["PS_MOBILENO"].Width = 75;
                        }
                        if (dataGridView1.Columns.Contains("PS_NIC"))
                        {
                            dataGridView1.Columns["PS_NIC"].HeaderText = "NIC";
                            dataGridView1.Columns["PS_NIC"].Width = 70;
                        }
                        if (dataGridView1.Columns.Contains("PS_BODY"))
                        {
                            dataGridView1.Columns["PS_BODY"].HeaderText = "Massage";
                            dataGridView1.Columns["PS_BODY"].Width = 170;
                        }
                        if (dataGridView1.Columns.Contains("PS_STATUS"))
                        {
                            dataGridView1.Columns["PS_STATUS"].HeaderText = "Status";
                            dataGridView1.Columns["PS_STATUS"].Width = 45;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception in the application", ex);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Filter in real-time when date changes
            string searchText = textBox1.Text.Trim();
            FilterData(searchText, dateTimePicker1.Value.Date);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                FilterData(textBox1.Text.Trim(), dateTimePicker1.Value.Date, 1); // PS_STATUS = 1
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                FilterData(textBox1.Text.Trim(), dateTimePicker1.Value.Date, 0); // PS_STATUS = 0
            }
        }
    }
}
