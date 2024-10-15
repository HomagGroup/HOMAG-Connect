using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots;

/// <summary>
/// Lot reference
/// </summary>
public class LotReference
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LotReference" /> class.
    /// </summary>
    public LotReference() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LotReference" /> class.
    /// </summary>
    public LotReference(Guid lotId, string? lotName)
    {
        LotId = lotId;
        LotName = lotName;
    }

    #endregion

    /// <summary>
    /// Gets or sets the lot id.
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid LotId { get; set; }

    /// <summary>
    /// Gets or sets the lot name.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? LotName { get; set; }

    /// <summary>
    /// Gets or sets the planned production start date of the lot.
    /// </summary>
    [JsonProperty(Order = 30)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the planned production completion date of the lot.
    /// </summary>
    [JsonProperty(Order = 31)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Converts a Lot to <see cref="LotReference" />.
    /// </summary>
    public static implicit operator LotReference(Lot lot)
    {
        return new LotReference(lot.Id, lot.Name);
    }
}