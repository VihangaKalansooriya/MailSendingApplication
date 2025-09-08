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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            LoadLatestMessage("B");
            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        private void LoadLatestMessage(string type)
        {
            string query = @"
                SELECT TOP 1 CS_MASSAGE 
                FROM U_TBLCUSTOMSMS 
                WHERE CS_TYPE = @Type
                ORDER BY CR_DATE DESC";

            using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Type", type);

                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        textBox2.Text = result.ToString();
                    }
                    else
                    {
                        textBox2.Text = $"No message found for Type {type}.";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading message: " + ex.Message);
                }
            }
        }
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)  // Type B
            {
                LoadLatestMessage("B");
            }
            else if (radioButton2.Checked)  // Type A
            {
                LoadLatestMessage("A");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string singlish = textBox1.Text;   // get input
            string sinhala = SinhalaTranslator.ConvertToSinhala(singlish); // convert
            textBox2.Text = sinhala;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.ToUpper();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedType = radioButton1.Checked ? "B" : "A";

            string query = @"
                    INSERT INTO U_TBLCUSTOMSMS (CS_MASSAGE, CS_TYPE, CR_DATE, CR_BY)
                    VALUES (@Message, @Type, GETDATE(), @CreatedBy)";

            using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Message", textBox2.Text);
                cmd.Parameters.AddWithValue("@Type", selectedType);
                cmd.Parameters.AddWithValue("@CreatedBy", "ADMIN");

                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Message saved successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to save message.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving message: " + ex.Message);
                }
            }
        }
    }
}
