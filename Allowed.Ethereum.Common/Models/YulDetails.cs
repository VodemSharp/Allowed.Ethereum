using Newtonsoft.Json;

namespace Allowed.Ethereum.Common.Models
{
    public class YulDetails
    {
        [JsonProperty("stackAllocation", NullValueHandling = NullValueHandling.Ignore)]
        public bool StackAllocation { get; set; }
    }
}
