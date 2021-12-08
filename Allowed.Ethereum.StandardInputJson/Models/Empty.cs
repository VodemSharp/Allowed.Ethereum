using Newtonsoft.Json;

namespace Allowed.Ethereum.StandardInputJson.Models
{
    public class Empty
    {
        [JsonProperty("*", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Fluffy { get; set; }

        [JsonProperty("", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Purple { get; set; }
    }
}
