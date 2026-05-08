using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots;

/// <summary>
/// Represents lot details including the related part identifiers.
/// </summary>
/// <example>
/// { "id": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "name": "Lot-2025-04-01", "quantityOfParts": 24, "status": "Planned", "partIds": [ "PART-1001", "PART-1002" ] }
/// </example>
public class LotDetails : Lot
{
    /// <summary>
    /// Gets or sets the part identifiers assigned to the lot.
    /// </summary>
    /// <example>[ "PART-1001", "PART-1002" ]</example>
    [JsonProperty(Order = 50)]
    public List<string> PartIds { get; set; } = [];
}