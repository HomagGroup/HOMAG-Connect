using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Kind of an order item. Serialized as the member name and used as discriminator when items are deserialized.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum Type
{
    /// <summary>
    /// Type is not known (fallback for new or unrecognized values).
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
    /// A price.
    /// </summary>
    Price,

    /// <summary>
    /// (Software) Configuration information.
    /// </summary>
    Configuration,

    /// <summary>
    /// Root configuration position of a (software) configuration.
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

    /// <summary>
    /// A board.
    /// </summary>
    Board,

    /// <summary>
    /// An edge band.
    /// </summary>
    EdgeBand,

    /// <summary>
    /// An offcut.
    /// </summary>
    Offcut,

    /// <summary>
    /// A calculation error.
    /// </summary>
    CalculationError,

    /// <summary>
    /// A reclaim.
    /// </summary>
    Reclaim,
}