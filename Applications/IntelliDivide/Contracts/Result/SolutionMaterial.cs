using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterial : IExtensibleDataObject
    {
        [JsonProperty(Order = 1)]
        public IReadOnlyCollection<SolutionMaterialBoard>? Boards { get; set; }

        [JsonProperty(Order = 4)]
        public IReadOnlyCollection<SolutionMaterialEdgeband>? Edgebands { get; set; }

        [JsonProperty(Order = 2)]
        public IReadOnlyCollection<SolutionMaterialOffcut>? Offcuts { get; set; }

        [JsonProperty(Order = 3)]
        public IReadOnlyCollection<SolutionMaterialOffcutProduced>? OffcutsProduced { get; set; }

        [JsonProperty(Order = 99)]
        public ExtensionDataObject? ExtensionData { get; set; }
    }
}