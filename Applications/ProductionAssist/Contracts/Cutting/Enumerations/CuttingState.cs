using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Cutting.Enumerations
{
    /// <summary>
    /// Cutting State
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum CuttingState
    {
        /// <summary>
        /// Not yet started
        /// </summary>
        NotStarted,

        /// <summary>
        /// Started (at least one part produced)
        /// </summary>
        Started,

        /// <summary>
        /// Finished (all parts produced)
        /// </summary>
        Finished,

        /// <summary>
        /// Archived
        /// </summary>
        Archived
    }
}