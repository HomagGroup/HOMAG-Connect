using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Specifies how an address is used in an API contract.
/// Multiple values can be combined because this enum uses <see cref="FlagsAttribute" />.
/// </summary>
/// <example>Delivery</example>
[Flags]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum AddressType
{
    /// <summary>
    /// The address usage is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// The address is used for delivery.
    /// </summary>
    Delivery = 1,

    /// <summary>
    /// The address is used for billing.
    /// </summary>
    Billing = 2,

    /// <summary>
    /// The address is used for both delivery and billing.
    /// </summary>
    DeliveryAndBilling = 3
}