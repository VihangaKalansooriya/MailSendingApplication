using MailSendingApp;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MailAppNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeButtonDesign();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();   // username
            string password = textBox2.Text.Trim();   // password (user input)

            string storedPassword = GetPasswordFromDb(username);

            if (storedPassword == null)
            {
                MessageBox.Show("❌ User not found.");
                return;
            }

            bool isValid = PasswordHasher.VerifyPassword(password, storedPassword);
            //bool isValid = VerifyPassword(password, storedPassword);

            if (isValid)
            {
                //MessageBox.Show("✅ Login successful!");
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("❌ Invalid password.");
            }

        }

        private string GetPasswordFromDb(string username)
        {
            string query = "SELECT UH_USER_PASSWORD FROM U_TBLUSERHEAD WHERE UH_ID=@User";

            using (SqlConnection connection = new SqlConnection(Globalconfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@User", username);

                connection.Open();
                object result = cmd.ExecuteScalar();

                return result != null ? result.ToString() : null;
            }
        }

    }
}
