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
    public partial class Form9 : Form
    {
        private string _tableName;
        public List<string> SelectedCodes { get; private set; } = new List<string>();

        public Form9(string tableName)
        {
            InitializeComponent();
            _tableName = tableName;
            LoadTableData(_tableName);
        }

        private void LoadTableData(string mode)
        {
            string query = "";

            switch (mode)
            {
                case "AREA":
                    query = "SELECT AM_CODE AS CODE, AM_DESC AS DESCRIPTION FROM M_TBLAREA";
                    break;
                case "LOCATION":
                    query = "SELECT LOC_CODE AS CODE, LOC_DESC AS DESCRIPTION FROM M_TBLLOCATIONS";
                    break;
                case "GROUP":
                    query = "SELECT CG_CODE AS CODE, CG_DESC AS DESCRIPTION FROM M_TBLCUSGROUPS";
                    break;
                case "DESIGNATION":
                    query = "SELECT CD_CODE AS CODE, CD_DESC AS DESCRIPTION FROM M_TBLDESIGNATION";
                    break;
                case "PROFESSION":
                    query = "SELECT CF_CODE AS CODE, CF_DESC AS DESCRIPTION FROM M_TBLPROFESSION";
                    break;
                case "COMPANY":
                    query = "SELECT CC_CODE AS CODE, CC_DESC AS DESCRIPTION FROM M_TBLCOMPANY";
                    break;
                case "RELIGION":
                    query = "SELECT CR_CODE AS CODE, CR_DESC AS DESCRIPTION FROM M_TBLRELIGION";
                    break;
                case "ETHNICITY":
                    query = "SELECT CE_CODE AS CODE, CE_DESC AS DESCRIPTION FROM M_TBLETHNICITY";
                    break;
                case "LOYALTY":
                    query = "SELECT CL_CODE AS CODE, CL_DESC AS DESCRIPTION FROM M_TBLLOYALTYGROUPS";
                    break;
            }

            using (SqlConnection conn = new SqlConnection(Globalconfig.ConnectionString))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = null; // reset before binding
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // 1. Add checkbox column
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "SELECT";
                chk.Name = "Select";
                chk.Width = 50;
                dataGridView1.Columns.Add(chk);

                // 2. Add CODE column
                DataGridViewTextBoxColumn codeCol = new DataGridViewTextBoxColumn();
                codeCol.HeaderText = "CODE";
                codeCol.DataPropertyName = "CODE"; // must match alias in query
                codeCol.Name = "CODE";
                codeCol.Width = 50;
                dataGridView1.Columns.Add(codeCol);

                // 3. Add DESCRIPTION column
                DataGridViewTextBoxColumn descCol = new DataGridViewTextBoxColumn();
                descCol.HeaderText = "DESCRIPTION";
                descCol.DataPropertyName = "DESCRIPTION";
                descCol.Name = "DESCRIPTION";
                descCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // stretch
                dataGridView1.Columns.Add(descCol);

                // Bind data
                dataGridView1.DataSource = dt;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SelectedCodes.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Select"].Value != null && Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    SelectedCodes.Add(row.Cells["CODE"].Value.ToString());
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
