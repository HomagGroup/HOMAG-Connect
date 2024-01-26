using System;
using System.Diagnostics;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    [DebuggerDisplay("Id={Id}, OptimizationId={OptimizationId}, Name={Name}, QuantityOfParts={QuantityOfParts}")]
    public class Solution : IExtensibleDataObject
    {
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        [JsonProperty(Order = 2)]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(Order = 3)]
        public Guid OptimizationId { get; set; }

        [JsonProperty(Order = 5)]
        public SolutionOverview Overview { get; set; } = new SolutionOverview();

        [JsonProperty(Order = 80)]
        public double TotalScore { get; set; }

        [JsonProperty(Order = 99)]
        public ExtensionDataObject? ExtensionData { get; set; }
    }
}