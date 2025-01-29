using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Order item type.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum Type
{
    Unknown,
    Group,
    Position,
    Component,
    Part,
    Resource,

    //Processing,
    //Packaging,
    Price,
    Configuration
}