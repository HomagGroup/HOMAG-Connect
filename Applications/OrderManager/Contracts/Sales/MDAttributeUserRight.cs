using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Sales
{
    /// <summary>
    /// User right required to see or change a library attribute. Serialized as the member name.
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
        /// Attribute is available to master users only.
        /// </summary>
        Master,

        /// <summary>
        /// Attribute is available to advanced users and above.
        /// </summary>
        Advanced,

        /// <summary>
        /// Attribute is available to all users.
        /// </summary>
        Simple,
    }
}
