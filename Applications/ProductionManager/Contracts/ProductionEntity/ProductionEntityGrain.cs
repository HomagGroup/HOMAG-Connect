using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity
{
    /// <summary>
    /// Production entity grain
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ProductionEntityGrain
    {
        /// <summary>
        /// No grain
        /// </summary>
        NoGrain = 0,
        /// <summary>
        /// Length wise
        /// </summary>
        Lengthwise = 1,
        /// <summary>
        /// Cross wise
        /// </summary>
        Crosswise = 2
    }
}
