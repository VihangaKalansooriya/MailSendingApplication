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

namespace MailAppNew
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            FilterData("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
                FilterData(searchText);
            else
                FilterData(searchText);
        }

        private void FilterData(string searchValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
                {
                    connection.Open();
                    string query = @"SELECT PS_CUSCODE,PS_LOCCODE,PS_DATE,PS_MOBILENO,PS_STATUS 
                                    FROM U_TBLPROMOTIONSMS
                                    WHERE PS_CUSCODE LIKE @searchValue
                                    OR PS_MOBILENO LIKE @searchValue
                                    OR PS_NIC LIKE @searchValue;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {

                        DataTable dt = new DataTable();
                        cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }

                        dataGridView1.AutoGenerateColumns = true;
                        dataGridView1.DataSource = dt;
                        dataGridView1.ReadOnly = true;
                        //foreach (DataGridViewColumn col in dataGridView1.Columns)
                        //{
                        //    col.ReadOnly = col.Name != "Select"; // Only "Select" column editable
                        //}

                        //if (dataGridView1.Columns["Select"] == null)
                        //{
                        //    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                        //    {
                        //        HeaderText = "Select",
                        //        Name = "Select",
                        //        Width = 65
                        //    };
                        //    dataGridView1.Columns.Add(checkBoxColumn);
                        //}
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
                dataGridView1.Columns["PS_CUSCODE"].HeaderText = "Customer Code";
                dataGridView1.Columns["PS_LOCCODE"].HeaderText = "Location Code";
                dataGridView1.Columns["PS_DATE"].HeaderText = "Date";
                dataGridView1.Columns["PS_MOBILENO"].HeaderText = "Mobile Number";
                dataGridView1.Columns["PS_STATUS"].HeaderText = "Status";

                dataGridView1.Columns["PS_CUSCODE"].Width = 75;
                dataGridView1.Columns["PS_LOCCODE"].Width = 75;
                dataGridView1.Columns["PS_DATE"].Width = 240;
                dataGridView1.Columns["PS_MOBILENO"].Width = 90;
                dataGridView1.Columns["PS_STATUS"].Width = 90;
            }
            catch (Exception ex)
            {
                Logger.LogError("Unhandled exception in the application", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }
    }
}
