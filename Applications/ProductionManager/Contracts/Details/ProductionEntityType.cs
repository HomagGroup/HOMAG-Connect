using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Details;

[JsonConverter(typeof(TolerantEnumConverter))]
public enum ProductionEntityType
{
    Unknown,
    OrderItem,
    ProductionOrder,
    Component,
    Ressource
}