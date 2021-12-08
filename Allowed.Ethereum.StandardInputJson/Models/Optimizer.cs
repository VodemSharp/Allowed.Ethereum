﻿using Newtonsoft.Json;

namespace Allowed.Ethereum.StandardInputJson.Models
{
    public class Optimizer
    {
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Enabled { get; set; }

        [JsonProperty("runs", NullValueHandling = NullValueHandling.Ignore)]
        public int Runs { get; set; }
    }
}
