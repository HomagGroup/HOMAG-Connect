using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots;

/// <summary>
/// Represents a lightweight reference to a lot.
/// </summary>
/// <example>
/// { "lotId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "lotName": "Lot-2025-04-01", "startDatePlanned": "2025-04-14T06:00:00+00:00", "completionDatePlanned": "2025-04-14T14:00:00+00:00" }
/// </example>
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
    /// <param name="lotId">The unique identifier of the lot.</param>
    /// <param name="lotName">The display name of the lot.</param>
    public LotReference(Guid lotId, string? lotName)
    {
        LotId = lotId;
        LotName = lotName;
    }

    #endregion

    /// <summary>
    /// Gets or sets the unique identifier of the lot.
    /// </summary>
    /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
    [JsonProperty(Order = 1)]
    public Guid LotId { get; set; }

    /// <summary>
    /// Gets or sets the display name of the lot.
    /// </summary>
    /// <example>Lot-2025-04-01</example>
    [JsonProperty(Order = 2)]
    public string? LotName { get; set; }

    /// <summary>
    /// Gets or sets the planned production start date of the lot.
    /// </summary>
    /// <example>2025-04-14T06:00:00+00:00</example>
    [JsonProperty(Order = 30)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the planned production completion date of the lot.
    /// </summary>
    /// <example>2025-04-14T14:00:00+00:00</example>
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