using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Extensions.Configuration;
using Serilog;
using MailSendingApp;
using Serilog.Core;
using static System.Windows.Forms.DataFormats;

//***************************************************************************
// File: Email Sending Application UI
// Author: Vihanga Kalansooriya
// Date: March 26, 2024
// Contact: techsupport.02@24x7retail.com
//***************************************************************************

namespace MailAppNew
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json").Build();

            Globalconfig.ConnectionString = config.GetConnectionString("DefaultConnection");
            Globalconfig.SenderEmail = config.GetSection("EmailConfiguration")["SenderEmail"];
            Globalconfig.SenderPassword = config.GetSection("EmailConfiguration")["SenderPassword"];
            Globalconfig.Port = int.Parse(config.GetSection("EmailConfiguration")["Port"]);
            Globalconfig.SMTPclient = config.GetSection("EmailConfiguration")["SMTPclient"];
            Globalconfig.TransactionTemplate = config.GetSection("EmailTemplates")["TransactionTemplate"];
            Globalconfig.PermissionTemplate = config.GetSection("EmailTemplates")["PermissionTemplate"];
            Globalconfig.logfilepath = config.GetSection("LogfilePath")["logfilepath"];
            Globalconfig.repodatabasename = config.GetSection("RepGenDatabaseConfiguration")["repodatabasename"];
            Globalconfig.reportgeneratorpath = config.GetSection("RepoGenPath")["reportgeneratorpath"];
            Globalconfig.PdfFullPath = config.GetSection("AppSettings")["PdfFullPath"];
            Globalconfig.databasename = config.GetSection("DatabaseConfiguration")["databasename"];
            Globalconfig.ExcelTemplate = config.GetSection("EmailTemplates")["ExcelTemplate"];
            Globalconfig.AttachedFilePath = config.GetSection("PDFfilePath")["AttachedFilePath"];
            Globalconfig.AttachedEXFilePath = config.GetSection("EXCELfilePath")["AttachedEXFilePath"];
            Globalconfig.SMSUser = config.GetSection("SMSGateway")["User"];
            Globalconfig.SMSPassword = config.GetSection("SMSGateway")["Password"];
            Globalconfig.Mask = config.GetSection("SMSGateway")["Mask"];
            Globalconfig.SMSID = config.GetSection("SMSGateway")["SMSID"];

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Globalconfig.logfilepath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            int daysThreshold = 7;
            ClearOldFiles(daysThreshold);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Form2 form2 = new Form2();
                Form3 form3 = new Form3();
                Application.Run(new Form1());
                if (form2.IsDatabaseConnected())
                {
                    Application.Run(form3);
                }
                else
                {
                    MessageBox.Show("Failed! Connect to the Server Database. Please Check Your Internet Connection.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Program.cs :", ex);
            }
        }

        static void ClearOldFiles(int daysThreshold)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(Globalconfig.logfilepath);

                if (!directory.Exists)
                {
                    Console.WriteLine("Directory not found.");
                    return;
                }

                DateTime thresholdDate = DateTime.Now.AddDays(-daysThreshold);

                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.LastWriteTime < thresholdDate)
                    {
                        file.Delete();
                        Console.WriteLine($"Deleted old file: {file.FullName}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Clear Old Files :", ex);
            }
        }
    }
}