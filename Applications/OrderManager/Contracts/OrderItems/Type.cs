using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Order item type.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum Type
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// A group of items.
    /// </summary>
    Group,

    /// <summary>
    /// A position in the order.
    /// </summary>
    Position,

    /// <summary>
    /// A (hardware) component.
    /// </summary>
    Component,

    /// <summary>
    /// A part.
    /// </summary>
    Part,

    /// <summary>
    /// A (hardware) resource.
    /// </summary>
    Resource,

    //Processing,
    //Packaging,

    /// <summary>
    /// A price information.
    /// </summary>
    Price,

    /// <summary>
    /// (Software) Configuration information.
    /// </summary>
    Configuration,

    /// <summary>
    /// (Software) Root configuration position.
    /// </summary>
    ConfigurationPosition,

    /// <summary>
    /// (Software) configuration group.
    /// </summary>
    ConfigurationGroup,

    /// <summary>
    /// Error information.
    /// </summary>
    ErrorInfo,
}