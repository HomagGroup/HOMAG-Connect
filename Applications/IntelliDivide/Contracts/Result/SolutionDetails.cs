using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    public class SolutionDetails : Solution
    {
        [JsonProperty(Order = 70)]
        public IReadOnlyCollection<SolutionExportType> Exports { get; set; } = new List<SolutionExportType>();

        [JsonProperty(Order = 30)]
        public SolutionMaterial Material { get; set; } = new SolutionMaterial();

        [JsonProperty(Order = 20)]
        public IReadOnlyCollection<SolutionPart> Parts { get; set; } = new List<SolutionPart>();
    }
}