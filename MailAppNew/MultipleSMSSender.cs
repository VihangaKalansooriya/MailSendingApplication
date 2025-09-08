using MailSendingApp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MailAppNew
{
    internal class MultipleSMSSender
    {
        private string Token { get; set; } = "";
        private string SenderId { get; set; } = Globalconfig.SMSID;
        private readonly string Provider ;

        public MultipleSMSSender()
        {
            Provider = Globalconfig.Provider; // "DialogESMS" or "DialogReachAPI"
        }

        /// <summary>
        /// Send SMS based on provider from appsettings.json
        /// </summary>
        public async Task<bool> SendSmsAsync(string mobileNo, string message, string transactionId = null)
        {
            return Provider switch
            {
                "DialogESMS" => await SendViaDialogEsms(mobileNo, message, transactionId),
                "DialogReachAPI" => await SendViaRichCommunicationModern(mobileNo, message, "PromoCampaign", transactionId),
                "HutchBulkSMS" => await SendViaHutchBulkSms(mobileNo, message),
                _ => throw new Exception("Unknown SMS Provider: " + Provider)
            };
        }

        // ------------------- PROVIDER METHODS -------------------

        #region DialogESMS
        private async Task<bool> SendViaDialogEsms(string mobileNo, string message, string transactionId)
        {
            if (string.IsNullOrEmpty(Token))
                await GenerateToken();

            return await SendSmsInternal(mobileNo, message, transactionId ?? DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

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
        #endregion

        #region RichCommunicationAPI
        
        // Request/Response Models
        public class RichSmsMessage
        {
            public string clientRef { get; set; }
            public string number { get; set; }   // 94xxxxxxxxx or comma separated numbers
            public string mask { get; set; }     // Sender ID
            public string text { get; set; }     // SMS Content
            public string campaignName { get; set; } // Required
        }

        public class RichSmsRequest
        {
            public List<RichSmsMessage> messages { get; set; }
        }

        public class RichSmsResponseMessage
        {
            public string Number { get; set; }
            public string ResultDesc { get; set; }
        }

        public class RichSmsResponse
        {
            public int resultCode { get; set; }
            public string resultDesc { get; set; }
            public List<RichSmsResponseMessage> messages { get; set; }
        }

        private string GetMd5HashData(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2")); // lowercase hex
                }
                return sb.ToString();
            }
        }
        private async Task<bool> SendViaRichCommunicationModern(string mobileNo, string message, string campaignName, string clientRef)
        {
            try
            {
                // Prepare request payload
                var smsRequest = new RichSmsRequest
                {
                    messages = new List<RichSmsMessage>
                {
                new RichSmsMessage
                {
                    clientRef = clientRef,
                    number = mobileNo,
                    mask = SenderId,
                    text = message,
                    campaignName = campaignName
                }
                }
                };

                // Create RestSharp client
                var client = new RestClient("https://richcommunication.dialog.lk/api/sms/send");
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");

                // Add custom headers from your old code
                string password = Globalconfig.SMSPassword;
                string hashPassword = GetMd5HashData(password);
                string startTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

                request.AddHeader("USER", Globalconfig.SMSUser);
                request.AddHeader("DIGEST", hashPassword);
                request.AddHeader("CREATED", startTime);

                // Add JSON body
                request.AddJsonBody(smsRequest);

                // Send request asynchronously
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var smsResponse = JsonConvert.DeserializeObject<RichSmsResponse>(response.Content);

                    if (smsResponse != null && smsResponse.resultCode == 0)
                    {
                        foreach (var msg in smsResponse.messages)
                        {
                            Console.WriteLine($"[RichComm] {msg.Number} -> {msg.ResultDesc}");
                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"[RichComm] Failed: {smsResponse?.resultDesc}");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("HTTP Error: " + response.ErrorMessage);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending SMS: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region HutchBulkSMSAPI
        private async Task<bool> SendViaHutchBulkSms(string mobileNos, string message)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string username = Globalconfig.SMSUser;
                string password = Globalconfig.SMSPassword;
                string mask = SenderId; // must be the exact Hutch-approved mask (case-sensitive!)

                // Build full GET URL with parameters
                string url = $"https://bulksms.hutch.lk/sendsmsmultimask.php" +
                             $"?USER={WebUtility.UrlEncode(username)}" +
                             $"&PWD={WebUtility.UrlEncode(password)}" +
                             $"&MASK={WebUtility.UrlEncode(mask)}" +
                             $"&NUM={WebUtility.UrlEncode(mobileNos)}" +
                             $"&MSG={WebUtility.UrlEncode(message)}";

                var client = new RestClient(url);
                var request = new RestRequest("", Method.Get);

                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = response.Content ?? "";
                    //Console.WriteLine($"[Hutch] Response: {content}");
                    Logger.LogInformation($"[Hutch] Response: {content}");

                    // Hutch success response: "success. Reference : 13767261950601"
                    if (content.StartsWith("success", StringComparison.OrdinalIgnoreCase))
                        return true;

                    return false;
                }
                else
                {
                    //Console.WriteLine($"[Hutch] HTTP Error: {response.StatusCode} {response.ErrorMessage}");
                    Logger.LogInformation($"[Hutch] HTTP Error: {response.StatusCode} {response.ErrorMessage}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error sending Hutch SMS: {ex.Message}");
                Logger.LogInformation($"Error sending Hutch SMS: {ex.Message}");
                return false;
            }
        }


        #endregion
        public async Task InsertPromotionSmsAsync(string cusCode, string locCode, DateTime date, string mobileNo, int status, string type, string body, string nic)
        {
            string connectionString = Globalconfig.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                string query = @"
                                INSERT INTO U_TBLPROMOTIONSMS 
                                (PS_CUSCODE, PS_LOCCODE, PS_DATE, PS_MOBILENO, PS_STATUS, PS_TYPE, PS_BODY, PS_NIC)
                                VALUES (@CusCode, @LocCode, @Date, @MobileNo, @Status, @Type, @Body, @Nic)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CusCode", cusCode);
                    cmd.Parameters.AddWithValue("@LocCode", locCode);
                    cmd.Parameters.AddWithValue("@Date", date);
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Body", body);
                    cmd.Parameters.AddWithValue("@Nic", nic);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
