using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Data;
using System.Diagnostics;
using System.Transactions;
using Newtonsoft.Json;
using MailSendingApp;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Serilog.Core;
using System.Net.Http.Json;

public class mailapp
{

    public async Task ProcessEmailAsync(string recipientEmail, int id)
    {
        try
        {
            MailMessage mail = null;
            SmtpClient smtpClient = new SmtpClient(Globalconfig.SmtpServer)
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(Globalconfig.SenderEmail, Globalconfig.SenderPassword)
            };

            Dictionary<string, object> argsval = new Dictionary<string, object>();

            using (SqlConnection dbConnection = new SqlConnection(Globalconfig.ConnectionString))
            {
                dbConnection.Open();
                string query = "SELECT TB_TYPE, TB_RUNNO, TB_TRTYPE, TB_URL FROM M_TBLMAILDETAILS WHERE TB_ID = @ID";

                using (SqlCommand command = new SqlCommand(query, dbConnection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            argsval.Add("reportName", "transaction");
                            argsval.Add("userType", "ADMIN");
                            argsval.Add("code", reader["TB_RUNNO"].ToString());
                            argsval.Add("menuCode", reader["TB_TYPE"].ToString());
                            argsval.Add("db", Globalconfig.repodatabasename);

                            string trType = reader["TB_TRTYPE"].ToString();
                            string url = reader["TB_URL"].ToString();
                            if (trType == "T")
                            {
                                mail = new MailMessage(Globalconfig.SenderEmail, recipientEmail)
                                {
                                    Subject = $"{argsval["menuCode"]} Report Attached.",
                                    Body = Globalconfig.TransactionTemplate.Replace("{menuCode}", argsval["menuCode"].ToString())
                                              .Replace("{id}", id.ToString())
                                              .Replace("{trType}", trType)
                                };
                            }
                            else if (trType == "P")
                            {
                                mail = new MailMessage(Globalconfig.SenderEmail, recipientEmail)
                                {
                                    Subject = $" Request for Permission {argsval["menuCode"]} Report",
                                    Body = Globalconfig.PermissionTemplate.Replace("{menuCode}", argsval["menuCode"].ToString())
                                             .Replace("{id}", id.ToString())
                                             .Replace("{trType}", trType)
                                             .Replace("{url}", url)
                                };
                            }
                        }
                    }
                }
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = Globalconfig.reportgeneratorpath,
                Arguments = JsonConvert.SerializeObject(JsonConvert.SerializeObject(argsval)),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                try
                {
                    process.Start();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    Logger.LogError("Error running ReportGenerator.exe", ex);
                }

                var output = await process.StandardOutput.ReadToEndAsync();
                string resultString = output.Replace("\r", "").Replace("\n", "");
                Logger.LogInformation("Generated File Name: " + resultString);
                string pdfFullPath = Path.Combine(Globalconfig.PdfFullPath, $"{resultString}.pdf");
                Attachment attachment = new Attachment(pdfFullPath, MediaTypeNames.Application.Pdf);
                mail.Attachments.Add(attachment);
                await smtpClient.SendMailAsync(mail);
                UpdateStatus(id);
                Logger.LogInformation("Email sent ID: " + id + " and update status 1");
                MessageBox.Show("Email sent successfully.ID NO: " + id, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Failed! Please, Check The Run No is Correct.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Logger.LogError("Error attaching or sending email", ex);
        }

    }

    static void UpdateStatus(int id)
    {
        string updateQuery = "UPDATE M_TBLMAILDETAILS SET TB_STATUS = 1 WHERE TB_ID = @ID";

        using (SqlConnection con = new SqlConnection(Globalconfig.ConnectionString))
        {
            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
            {
                con.Open();
                updateCommand.Parameters.AddWithValue("@ID", id);
                updateCommand.ExecuteNonQuery();
                con.Close();
            }
        }
    }

}