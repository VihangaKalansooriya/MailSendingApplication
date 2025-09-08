using MailSendingApp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MailAppNew
{
    public class DialogSmsService
    {
        private string Token { get; set; } = "";
        private string Mask { get; set; } = "";

        public DialogSmsService()
        {
            GenerateToken();
        }

        private void GenerateToken()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var url = (HttpWebRequest)WebRequest.Create("https://e-sms.dialog.lk/api/v2/user/login");
                url.Method = "POST";
                url.ContentType = "application/json";
                url.Accept = "*/*";
                url.Headers["X-API-VERSION"] = "v1";

                string username = Globalconfig.SMSUser;
                string password = Globalconfig.SMSPassword;

                using (var body = new StreamWriter(url.GetRequestStream()))
                {
                    string json = $"{{\"username\":\"{username}\",\"password\":\"{password}\"}}";
                    body.Write(json);
                }

                var httpResponse = (HttpWebResponse)url.GetResponse();
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    JObject obj = JObject.Parse(result);
                    Token = obj["token"]?.ToString() ?? throw new Exception("Token generation failed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating token: " + ex.Message);
            }
        }

        /// <summary>
        /// Send a single SMS
        /// </summary>
        public bool SendSms(string mobileNo, string message, string transactionId)
        {
            return SendSmsInternal(mobileNo, message, transactionId);
        }

        /// <summary>
        /// Send bulk SMS to multiple numbers
        /// </summary>
        public async Task SendBulkSmsAsync(List<string> mobileNumbers, string message)
        {
            foreach (var number in mobileNumbers)
            {
                string transactionId = Guid.NewGuid().ToString();
                bool success = await Task.Run(() => SendSmsInternal(number, message, transactionId));

                Console.WriteLine($"{number}: {(success ? "Sent ✅" : "Failed ❌")}");
            }
        }

        /// <summary>
        /// Internal method for sending SMS
        /// </summary>
        private bool SendSmsInternal(string mobileNo, string message, string transactionId)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string senderId = Globalconfig.SMSID;

                var url = (HttpWebRequest)WebRequest.Create("https://e-sms.dialog.lk/api/v2/sms");
                url.Method = "POST";
                url.ContentType = "application/json";
                url.Accept = "application/json";

                if (!string.IsNullOrEmpty(Token))
                    url.Headers["Authorization"] = "Bearer " + Token;

                string noList = $"{{\"mobile\":\"{mobileNo}\"}}";
                string json = $"{{\"transaction_id\":\"{transactionId}\",\"sourceAddress\":\"{senderId}\",\"msisdn\":[{noList}],\"message\":\"{message}\"}}";

                using (var body = new StreamWriter(url.GetRequestStream()))
                {
                    body.Write(json);
                }

                var httpResponse = (HttpWebResponse)url.GetResponse();
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();
                    JObject obj = JObject.Parse(result);
                    return obj["status"]?.ToString() == "success";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending SMS to " + mobileNo + ": " + ex.Message);
                return false;
            }
        }
    }
}
