using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Specifies the reason for reclaiming an item in an order. This enumeration is used to categorize the various reasons 
/// why an item may be returned or reclaimed, such as transport damage, scratches, wrong dimensions, or other issues. 
/// The <see cref="Unknown"/> value serves as a fallback for cases where the reclaim reason is not recognized or is newly introduced.
/// When deserializing from JSON, unknown values will be mapped to <see cref="Unknown"/> due to the tolerant enum converter.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(ReclaimReasonDisplayNames))]
public enum ReclaimReason
{
    /// <summary>
    /// Default / unknown reason (fallback if the enum is new or not known)
    /// </summary>
    Unknown,

    /// <summary>
    /// The item was damaged during transport.
    /// </summary>
    TransportDamage,

    /// <summary>
    /// The item has scratches on its surface.
    /// </summary>
    Scratches,

    /// <summary>
    /// The item has tearouts.
    /// </summary>
    Tearouts,

    /// <summary>
    /// The item has a damaged board.
    /// </summary>
    DamagedBoard,

    /// <summary>
    /// The item has damaged edges.
    /// </summary>
    DamagedEdges,

    /// <summary>
    /// The item has surface defects.
    /// </summary>
    SurfaceDefects,

    /// <summary>
    /// The item has incorrect drillings.
    /// </summary>
    IncorrectDrillings,

    /// <summary>
    /// The item has wrong dimensions.
    /// </summary>
    WrongDimensions,

    /// <summary>
    /// The item was delivered incorrectly.
    /// </summary>
    WrongDelivery,

    /// <summary>
    /// The item was not delivered.
    /// </summary>
    NotDelivered,

    /// <summary>
    /// The item has another reclaim reason.
    /// </summary>
    Other
}