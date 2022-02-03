using Newtonsoft.Json;

namespace Allowed.Ethereum.Common.Models
{
    public class Settings
    {
        [JsonProperty("remappings", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Remappings { get; set; }

        [JsonProperty("optimizer", NullValueHandling = NullValueHandling.Ignore)]
        public Optimizer Optimizer { get; set; }
    }
}
