using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots;

/// <summary>
/// Lot details.
/// </summary>
public class LotDetails : Lot
{
    /// <summary>
    /// Gets or sets the part Ids related to the requested lot.
    /// </summary>
    [JsonProperty(Order = 50)]
    public List<Guid> PartIds { get; set; } = [];
}