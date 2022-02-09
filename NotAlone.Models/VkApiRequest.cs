using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NotAlone.Models
{
    public class VkApiRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("object")]
        public JsonElement Object { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }
    }
}