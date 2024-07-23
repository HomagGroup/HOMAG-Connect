
using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Order details
/// </summary>
public class OrderDetails : Order
{
    /// <summary>
    /// Gets or sets the bill of materials.
    /// </summary>
    [JsonProperty(Order = 500)]
    public Collection<ProductionEntity.ProductionEntity>? BillOfMaterials { get; set; }
}