using MailSendingApp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MailAppNew
{
    internal class SmsSender
    {
        private string Token { get; set; } = "";
        private string SenderId { get; set; } = Globalconfig.SMSID;

        public SmsSender()
        {
            
        }
        public async Task InitializeAsync()
        {
            await GenerateToken();
        }


        /// <summary>
        /// Generate token from e-SMS API
        /// </summary>
        private async Task GenerateToken()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://e-sms.dialog.lk/api/v1/login");
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                var username = Globalconfig.SMSUser;
                var password = Globalconfig.SMSPassword;
                request.AddJsonBody(new { username, password });

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var obj = JObject.Parse(response.Content);
                    if (obj["status"]?.ToString() == "success")
                    {
                        Token = obj["token"]?.ToString();
                        return;
                    }
                }

                throw new Exception("Failed to get token: " + response.Content);
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating token: " + ex.Message);
            }
        }

        /// <summary>
        /// Send a single SMS
        /// </summary>
        public async Task<bool> SendSmsAsync(string mobileNo, string message, string transactionId = null)
        {
            if (string.IsNullOrEmpty(Token))
                await GenerateToken();

            return await SendSmsInternal(mobileNo, message, transactionId ?? DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        /// <summary>
        /// Send bulk SMS to multiple numbers
        /// </summary>
        public async Task SendBulkSmsAsync(List<string> mobileNumbers, string message)
        {
            var tasks = mobileNumbers.Select(number =>
                Task.Run(() =>
                {
                    // Use numeric-only transaction ID
                    string txnId = DateTime.Now.ToString("yyyyMMddHHmmss");
                    return SendSmsInternal(number, message, txnId);
                })
            );

            bool[] results = await Task.WhenAll(tasks);

            for (int i = 0; i < mobileNumbers.Count; i++)
            {
                Console.WriteLine($"{mobileNumbers[i]}: {(results[i] ? "Sent ✅" : "Failed ❌")}");
            }
        }



        /// <summary>
        /// Internal SMS sending
        /// </summary>
        private async Task<bool> SendSmsInternal(string mobileNo, string message, string transactionId)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://e-sms.dialog.lk/api/v2/sms");
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"Bearer {Token}");
                request.AddHeader("Accept", "application/json");

                var body = new
                {
                    transaction_id = transactionId,
                    sourceAddress = SenderId,
                    msisdn = new[] { new { mobile = mobileNo } },
                    message = message
                };

                request.AddJsonBody(body);
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var obj = JObject.Parse(response.Content);
                    return obj["status"]?.ToString() == "success";
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending SMS to {mobileNo}: {ex.Message}");
                return false;
            }
        }
    }
}
