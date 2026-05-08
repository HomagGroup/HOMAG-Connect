using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// Library attribute user right
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum MDAttributeUserRight
    {
        /// <summary>
        /// Represents an unspecified or unrecognized value.
        /// </summary>
        /// <remarks>Use this value when the actual value is unknown or cannot be determined. This is
        /// typically used as a default or fallback case.</remarks>
        Unknown,

        /// <summary>
        /// Master
        /// </summary>
        Master,

        /// <summary>
        /// Advanced
        /// </summary>
        Advanced,

        /// <summary>
        /// Simple
        /// </summary>
        Simple,
    }
}
