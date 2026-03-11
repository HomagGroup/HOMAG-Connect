using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Specifies the processed item contract type used in production protocol payloads.
    /// </summary>
    /// <example>ProcessedPart</example>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ProcessedItemType
    {
        /// <summary>
        /// Base processed item.
        /// </summary>
        ProcessedItem,

        /// <summary>
        /// Processed part.
        /// </summary>
        ProcessedPart,

        /// <summary>
        /// Processed part for dividing workstations.
        /// </summary>
        ProcessedPartDividing,

        /// <summary>
        /// Processed part for edgebanding workstations.
        /// </summary>
        ProcessedPartEdgebanding,

        /// <summary>
        /// Processed part for CNC workstations.
        /// </summary>
        ProcessedPartCnc,

        /// <summary>
        /// Processed position.
        /// </summary>
        ProcessedPosition,

        /// <summary>
        /// Processed assembly group.
        /// </summary>
        ProcessedAssemblyGroup
    }
}
