using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using point_of_sale_system.Models;

namespace point_of_sale_system.DAL
{
    public static class ApiMessages
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string systemId = "776655223";

        private static readonly string sendUrl = "http://dev2.alashiq.com/send.php";
        private static readonly string receiveUrl = $"http://dev2.alashiq.com/message.php?systemId={systemId}";

        /// <summary>
        /// Send message to external API
        /// </summary>
        public static async Task<string> SendMessageAsync(int userId, string username, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return "الرسالة فارغة، لا يمكن الإرسال.";

            string sendUrlWithParams = $"{sendUrl}?systemId={systemId}";

            var parameters = new List<KeyValuePair<string, string>>
    {
        new KeyValuePair<string, string>("user_id", userId.ToString()),
        new KeyValuePair<string, string>("username", username),
        new KeyValuePair<string, string>("message", message)
    };

            var content = new FormUrlEncodedContent(parameters);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            try
            {
                var response = await httpClient.PostAsync(sendUrlWithParams, content);
                string result = await response.Content.ReadAsStringAsync();

                var responseObj = JsonConvert.DeserializeObject<dynamic>(result);
                if (responseObj != null && responseObj.success == true)
                {
                    return "Message sent successfully!";
                }
                else
                {
                    return responseObj?.message?.ToString() ?? "Failed to send message (unknown error)";
                }
            }
            catch (Exception ex)
            {
                return "Connection error: " + ex.Message;
            }
        }

        /// <summary>
        /// Get all messages from external API (Admin only)
        /// </summary>
        public static async Task<List<Messages>> GetMessagesAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(receiveUrl);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine("API Response: " + json);

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(json);

                if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
                {
                    return apiResponse.Data.Messages ?? new List<Messages>();
                }

                return new List<Messages>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("API Error: " + ex.Message);
                return new List<Messages>();
            }
        }

        public class BaseApiResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
        }

    }
}
