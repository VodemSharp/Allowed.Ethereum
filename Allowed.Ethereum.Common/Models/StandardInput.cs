using Newtonsoft.Json;

namespace Allowed.Ethereum.Common.Models
{
    public class StandardInput
    {
        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("sources", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Source> Sources { get; set; }

        [JsonProperty("settings", NullValueHandling = NullValueHandling.Ignore)]
        public Settings Settings { get; set; }

        [JsonProperty("evmVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string EvmVersion { get; set; }

        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Metadata Metadata { get; set; }

        [JsonProperty("libraries", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Dictionary<string, string>> Libraries { get; set; }

        [JsonProperty("outputSelection", NullValueHandling = NullValueHandling.Ignore)]
        public OutputSelection OutputSelection { get; set; }
    }
}
