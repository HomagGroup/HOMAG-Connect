using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Processed item type enumeration
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ProcessedItemType
    {
        /// <summary>
        /// Processed item in general
        /// </summary>
        ProcessedItem,

        /// <summary>
        /// Processed part in general
        /// </summary>
        ProcessedPart,

        /// <summary>
        /// Processed part in dividing workstations
        /// </summary>
        ProcessedPartDividing,

        /// <summary>
        /// Processed part in edgebanding workstations
        /// </summary>
        ProcessedPartEdgebanding,

        /// <summary>
        /// Processed part in CNC workstations
        /// </summary>
        ProcessedPartCnc,

        /// <summary>
        /// Processed position
        /// </summary>
        ProcessedPosition,

        /// <summary>
        /// Processed assembly group
        /// </summary>
        ProcessedAssemblyGroup
    }
}
