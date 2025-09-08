using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSendingApp
{
    internal static class Globalconfig
    {
        public static string ConnectionString { get; set; }
        public static string TransactionTemplate { get; set; }
        public static string PermissionTemplate { get; set; }
        public static string SenderEmail { get; set; }
        public static string SenderPassword { get; set; }
        public static string logfilepath { get; set; }
        public static string repodatabasename { get; set; }
        public static string reportgeneratorpath { get; set; }
        public static string PdfFullPath { get; set; }
        public static string SMTPclient { get; set; }
        public static int Port { get; set; }
        public static string databasename { get; set; }
        public static string AttachedFilePath { get; set; }
        public static string AttachedEXFilePath { get; set; }
        public static string ExcelTemplate { get; set; }
        public static string SMSUser { get; set; }
        public static string SMSPassword { get; set; }
        public static string Mask { get; set; }
        public static string SMSID { get; set; }
        public static string Provider { get; set; }
        public static string KEY { get; set; }
        public static bool EnableButton1 { get; set; }
        public static bool EnableButton2 { get; set; }
        public static bool EnableButton3 { get; set; }
        public static bool EnableButton4 { get; set; }
        public static bool EnableButton5 { get; set; }
    }
}