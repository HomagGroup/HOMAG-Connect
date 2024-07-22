using System.Collections.ObjectModel;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Details;

/// <summary>
/// Order details
/// </summary>
public class OrderDetails : Order
{
    /// <summary>
    /// Gets or sets the bill of materials.
    /// </summary>
    [JsonProperty(Order = 500)]
    public Collection<ProductionEntity>? BillOfMaterials { get; set; }
}