using Newtonsoft.Json;

namespace Allowed.Ethereum.StandardInputJson.Models
{
    public class YulDetails
    {
        [JsonProperty("stackAllocation", NullValueHandling = NullValueHandling.Ignore)]
        public bool StackAllocation { get; set; }
    }
}
