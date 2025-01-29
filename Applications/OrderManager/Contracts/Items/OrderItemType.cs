using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Items;

[JsonConverter(typeof(TolerantEnumConverter))]
public enum OrderItemType
{
    Unknown,
    OrderGroup,
    OrderItem,
    Component,
    Part,
    Resource,

    //Processing,
    //Packaging,
    Price,
    Configuration
}