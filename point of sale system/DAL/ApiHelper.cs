using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace point_of_sale_system.DAL
{
    public class AboutData
    {
        public string title { get; set; }
        public string description { get; set; }
    }

    public class AboutResponse
    {
        public AboutData data { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }

    public static class ApiHelper
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task<AboutData> GetAboutApiDataAsync()
        {
            try
            {
                string url = "https://dev2.alashiq.com/about.php";
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();

                AboutResponse apiResponse = JsonConvert.DeserializeObject<AboutResponse>(json);
                return apiResponse.data;
            }
            catch (Exception ex)
            {
                return new AboutData
                {
                    title = "false",
                    description = ex.Message
                };
            }
        }
    }
}
