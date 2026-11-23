using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders;

// Note: This is preliminary code and is subject to change

/// <summary>
/// An order including its additional data and the production items (bill of materials) it consists of.
/// </summary>
/// <example>
/// { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "orderName": "Kitchen Miller", "orderDate": "2025-04-01T08:30:00+00:00",
/// "orderStatus": "ReadyForProduction", "changedAt": "2025-04-02T09:00:00+00:00", "createdAt": "2025-04-01T08:30:00+00:00",
/// "additionalData": [], "items": [] }
/// </example>
public class OrderDetails : Order
{
    /// <summary>
    /// Additional data entries attached to the order. Optional.
    /// </summary>
    [JsonProperty(Order = 401)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Additional properties configured in productionManager that are not part of the standard contract. Optional.
    /// </summary>
    [JsonProperty(Order = 600)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Bill of materials of the order: the production items (such as positions, assembly groups, and parts) it consists of. Optional.
    /// </summary>
    [JsonProperty(Order = 500)]
    public Collection<ProductionItemBase>? Items { get; set; }
}