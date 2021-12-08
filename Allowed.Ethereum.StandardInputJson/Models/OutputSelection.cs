using Newtonsoft.Json;

namespace Allowed.Ethereum.StandardInputJson.Models
{
    public class OutputSelection
    {
        [JsonProperty("*", NullValueHandling = NullValueHandling.Ignore)]
        public Empty Empty { get; set; }

        [JsonProperty("def", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string[]> Def { get; set; }
    }
}
