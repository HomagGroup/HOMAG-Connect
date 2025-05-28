using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots;

/// <summary>
/// Lot details.
/// </summary>
public class LotDetails : Lot
{
    /// <summary>
    /// Gets or sets the production orders in the lot.
    /// </summary>
    [JsonProperty(Order = 50)]
    public Part[]? ProductionOrders { get; set; }
}