using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Category of a position, such as a standard position or one for material and processing. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum PositionType
{
       /// <summary>
    /// Standard position.
    /// </summary>
    Standard,

    /// <summary>
    /// Position for material and processing.
    /// </summary>
    MaterialAndProcessing,

    /// <summary>
    /// Position whose price has been generated.
    /// </summary>
    PriceGenerated,
}