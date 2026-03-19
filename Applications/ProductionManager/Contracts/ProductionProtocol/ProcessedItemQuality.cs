using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol
{
    /// <summary>
    /// Marks the quality of the part processed at a workstation, which is relevant for the overall quality of the produced item.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))] 
    [ResourceManager(typeof(ProcessedItemQualityDisplayNames))]
    public enum ProcessedItemQuality
    {
        /// <summary>
        /// Good quality, no rework necessary.
        /// </summary>
        Good,

        /// <summary>
        /// Rework necessary to meet quality standards.
        /// </summary>
        Rework,

        /// <summary>
        /// Uncertain quality, part was reproduced but no rework was performed or system had overreported
        /// This can be used to mark parts that were reproduced due to quality issues but the quality of the reproduced part is not yet confirmed.
        /// </summary>
        Uncertain
    }
}
