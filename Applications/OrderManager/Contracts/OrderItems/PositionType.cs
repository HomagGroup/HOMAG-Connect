using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Specifies the type of position used to categorize items, 
/// such as standard positions or those related to material and processing.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum PositionType
{
       /// <summary>
    /// Standard position
    /// </summary>
    Standard,

    /// <summary>
    /// A position representing a material and processing item
    /// </summary>
    MaterialAndProcessing,
}