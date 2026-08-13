using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Granularity of the evaluations
    /// </summary>
    [ResourceManager(typeof(GranularityDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum Granularity
    {
        /// <summary>
        /// Data is aggregated by hour
        /// </summary>
        Hour,

        /// <summary>
        /// Data is aggregated by day
        /// </summary>
        Day,

        /// <summary>
        /// Data is aggregated by week
        /// </summary>
        Week,

        /// <summary>
        /// Data is aggregated by month
        /// </summary>
        Month
    }
}
