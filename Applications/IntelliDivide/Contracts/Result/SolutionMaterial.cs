using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Represents the material used in a solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterial : IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the boards used in the solution.
        /// </summary>
        [JsonProperty(Order = 1)]
        public IReadOnlyCollection<SolutionMaterialBoard> Boards { get; set; }

        /// <summary>
        /// Gets or sets the edgebands used in the solution.
        /// </summary>
        [JsonProperty(Order = 4)]
        public IReadOnlyCollection<SolutionMaterialEdgeband> Edgebands { get; set; }

        /// <summary>
        /// Gets or sets the offcuts used in the solution.
        /// </summary>
        [JsonProperty(Order = 2)]
        public IReadOnlyCollection<SolutionMaterialOffcut> Offcuts { get; set; }

        /// <summary>
        /// Gets or sets the offcuts produced in the solution.
        /// </summary>
        [JsonProperty(Order = 3)]
        public IReadOnlyCollection<SolutionMaterialOffcutProduced> OffcutsProduced { get; set; }

        /// <summary>
        /// Gets or sets the templates used in the solution.
        /// </summary>
        [JsonProperty(Order = 5)]
        public IReadOnlyCollection<SolutionMateriaTemplate> Templates { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}