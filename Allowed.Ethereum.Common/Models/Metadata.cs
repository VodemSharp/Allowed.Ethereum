using Newtonsoft.Json;

namespace Allowed.Ethereum.Common.Models
{
    public class Metadata
    {
        [JsonProperty("useLiteralContent", NullValueHandling = NullValueHandling.Ignore)]
        public bool UseLiteralContent { get; set; }
    }
}
