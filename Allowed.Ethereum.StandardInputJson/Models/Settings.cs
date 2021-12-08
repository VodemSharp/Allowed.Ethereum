using Newtonsoft.Json;

namespace Allowed.Ethereum.StandardInputJson.Models
{
    public class Settings
    {
        [JsonProperty("remappings", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Remappings { get; set; }

        [JsonProperty("optimizer", NullValueHandling = NullValueHandling.Ignore)]
        public Optimizer Optimizer { get; set; }
    }
}
