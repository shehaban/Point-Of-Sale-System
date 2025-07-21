using Newtonsoft.Json;
using System.Collections.Generic;

namespace point_of_sale_system.Models
{
    public class Data
    {
        [JsonProperty("messages")]
        public List<Messages> Messages { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}
