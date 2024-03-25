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
        public static string SmtpServer { get; set; }
        public static string TransactionTemplate { get; set; }
        public static string PermissionTemplate { get; set; }
        public static string SenderEmail { get; set; }
        public static string SenderPassword { get; set; }
        public static string logfilepath { get; set; }
        public static string repodatabasename { get; set; }
        public static string reportgeneratorpath { get; set; }
        public static string PdfFullPath { get; set; }

    }
}