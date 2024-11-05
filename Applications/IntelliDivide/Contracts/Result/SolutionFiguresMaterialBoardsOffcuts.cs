using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides the key figures for material boards and offcuts.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionFiguresMaterialBoardsOffcuts : IExtensibleDataObject
    {
        /// <summary>
        /// Gets the total percentage of waste.
        /// </summary>
        [JsonProperty(Order = 1)]
        public double Waste { get; set; }

        /// <summary>
        /// Gets the percentage of waste, including offcuts, based on board area.
        /// </summary>
        [JsonProperty(Order = 2)]
        public double WasteWithOffcutsByBoard { get; set; }

        /// <summary>
        /// Gets the percentage of waste, including offcuts, based on parts area.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double WasteWithOffcutsByParts { get; set; }

        /// <summary>
        /// Gets the required board area in m² or ft².
        /// </summary>
        [JsonProperty(Order = 4)]
        public double RequiredBoardArea { get; set; }

        /// <summary>
        /// Gets the number of whole boards used.
        /// </summary>
        [JsonProperty(Order = 5)]
        public int WholeBoards { get; set; }

        /// <summary>
        /// Gets the total number of offcuts.
        /// </summary>
        [JsonProperty(Order = 10)]
        public int OffcutsTotal { get; set; }

        /// <summary>
        /// Gets offcuts produced.
        /// </summary>
        [JsonProperty(Order = 11)]
        public int OffcutsProduced { get; set; }

        /// <summary>
        /// Gets offcuts required.
        /// </summary>
        [JsonProperty(Order = 12)]
        public int OffcutsRequired { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 99)]
        public ExtensionDataObject ExtensionData { get; set; }
    }
}
