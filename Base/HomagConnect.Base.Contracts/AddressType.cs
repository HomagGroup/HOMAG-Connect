using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts;

[Flags]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum AddressType
{
    /// <summary>
    /// The address type is unknown.
    /// </summary>
    Unknown,

    /// <summary>
    /// The address is a delivery address.
    /// </summary>
    Delivery = 1,

    /// <summary>
    /// The address is a billing address.
    /// </summary>
    Billing = 2
}