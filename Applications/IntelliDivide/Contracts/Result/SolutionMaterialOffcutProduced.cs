using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialOffcutProduced : IExtensibleDataObject
    {
        [JsonProperty(Order = 7)]
        public double Costs { get; set; }

        [JsonProperty(Order = 6)]
        public int Demand { get; set; }

        [JsonProperty(Order = 3)]
        public double Length { get; set; }

        [JsonProperty(Order = 1)]
        public string MaterialCode { get; set; } = string.Empty;

        [JsonProperty(Order = 5)]
        public double Thickness { get; set; }

        [JsonProperty(Order = 4)]
        public double Width { get; set; }

        [JsonProperty(Order = 99)]
        public ExtensionDataObject? ExtensionData { get; set; }
    }
}