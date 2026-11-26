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
        /// Base processed item without a more specific type.
        /// </summary>
        ProcessedItem,

        /// <summary>
        /// A part that was processed at a workstation without a more specific processing type.
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
        /// A part that was processed at a laminating workstation.
        /// </summary>
        ProcessedPartLaminating,

        /// <summary>
        /// A position that was processed.
        /// </summary>
        ProcessedPosition,

        /// <summary>
        /// An assembly group that was processed.
        /// </summary>
        ProcessedAssemblyGroup
    }
}
