using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the key figures for the materials of a solution.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionFiguresMaterial : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the material key figures for boards and offcuts.
        /// </summary>
        [JsonProperty(Order = 10)]
        public SolutionFiguresMaterialBoardsOffcuts BoardsAndOffcuts { get; set; } = new SolutionFiguresMaterialBoardsOffcuts();

        /// <summary>
        /// Gets a list of material key figures for waste per material code.
        /// </summary>
        [JsonProperty(Order = 20)]
        public IReadOnlyCollection<SolutionFiguresMaterialWasteOffcuts> WasteOffcutsPerMaterial { get; set; } = new List<SolutionFiguresMaterialWasteOffcuts>();

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
