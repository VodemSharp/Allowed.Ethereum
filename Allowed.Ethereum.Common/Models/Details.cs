using Newtonsoft.Json;

namespace Allowed.Ethereum.Common.Models
{
    public class Details
    {
        [JsonProperty("peephole", NullValueHandling = NullValueHandling.Ignore)]
        public bool Peephole { get; set; }

        [JsonProperty("jumpdestRemover", NullValueHandling = NullValueHandling.Ignore)]
        public bool JumpdestRemover { get; set; }

        [JsonProperty("orderLiterals", NullValueHandling = NullValueHandling.Ignore)]
        public bool OrderLiterals { get; set; }

        [JsonProperty("deduplicate", NullValueHandling = NullValueHandling.Ignore)]
        public bool Deduplicate { get; set; }

        [JsonProperty("cse", NullValueHandling = NullValueHandling.Ignore)]
        public bool Cse { get; set; }

        [JsonProperty("constantOptimizer", NullValueHandling = NullValueHandling.Ignore)]
        public bool ConstantOptimizer { get; set; }

        [JsonProperty("yul", NullValueHandling = NullValueHandling.Ignore)]
        public bool Yul { get; set; }

        [JsonProperty("yulDetails", NullValueHandling = NullValueHandling.Ignore)]
        public YulDetails YulDetails { get; set; }
    }
}
