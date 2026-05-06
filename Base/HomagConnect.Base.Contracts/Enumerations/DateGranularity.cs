using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations
{
    /// <summary>
    /// Specifies the level of granularity to use when representing or grouping dates.
    /// </summary>
    /// <remarks>Use this enumeration to indicate whether dates should be considered at the day, week, or
    /// month level. This is commonly used in reporting, data aggregation, or filtering scenarios where the precision of
    /// date-based operations must be controlled.</remarks>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum DateGranularity
    {
        Day,
        Week,
        Month
    }
}
