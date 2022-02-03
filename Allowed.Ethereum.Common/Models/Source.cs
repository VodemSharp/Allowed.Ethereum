using Newtonsoft.Json;

namespace Allowed.Ethereum.Common.Models
{
    public class Source
    {
        [JsonProperty("keccak256", NullValueHandling = NullValueHandling.Ignore)]
        public string Keccak256 { get; set; }

        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }
    }
}
