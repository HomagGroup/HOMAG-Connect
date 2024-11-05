using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Represents a solution with details.
    /// </summary>
    public class SolutionDetails : Solution
    {
        /// <summary>
        /// Gets or sets the exports (<see cref="SolutionExportType" /> of the solution.
        /// </summary>
        [JsonProperty(Order = 70)]
        public IReadOnlyCollection<SolutionExportType> Exports { get; set; } = new List<SolutionExportType>();

        /// <summary>
        /// Gets or sets the material of the solution.
        /// </summary>
        [JsonProperty(Order = 30)]
        public SolutionMaterial Material { get; set; } = new SolutionMaterial();

        /// <summary>
        /// Gets or sets the key figures of the solution.
        /// </summary>
        [JsonProperty(Order = 40)]
        public SolutionFigures KeyFigures { get; set; } = new SolutionFigures();

        /// <summary>
        /// Gets or sets the parts of the solution.
        /// </summary>
        [JsonProperty(Order = 20)]
        public IReadOnlyCollection<SolutionPart> Parts { get; set; } = new List<SolutionPart>();

        /// <summary>
        /// Gets or sets the unit system.
        /// </summary>
        [JsonProperty(Order = 21)]
        public UnitSystem UnitSystem { get; set; }
    }
}