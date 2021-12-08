using Newtonsoft.Json;

namespace Allowed.Ethereum.StandardInputJson.Models
{
    public class Source
    {
        [JsonProperty("keccak256", NullValueHandling = NullValueHandling.Ignore)]
        public string Keccak256 { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }
    }
}
