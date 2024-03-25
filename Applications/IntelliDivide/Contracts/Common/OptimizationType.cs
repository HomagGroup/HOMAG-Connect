using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Common
{
    /// <summary>
    /// Defines the type of the patterns to generate.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum OptimizationType
    {
        /// <summary>
        /// The optimization generates cutting patterns for saws.
        /// </summary>
        Cutting,

        /// <summary>
        /// The optimization generates nesting patterns for nesting machines.
        /// </summary>
        Nesting
    }
}