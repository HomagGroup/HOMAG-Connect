using System.ComponentModel.DataAnnotations;

using HomagConnect.ProductionManager.Contracts.Lots;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders;

/// <summary>
/// Reference from an order to a lot that contains parts of the order, including the order's part quantity and planned dates in that lot.
/// </summary>
/// <example>
/// { "lotId": "9c1e7a52-3b64-4f08-a1d2-5e6f7a8b9c0d", "lotName": "Lot 2025-04-02", "startDatePlanned": "2025-04-03T00:00:00+00:00",
/// "completionDatePlanned": "2025-04-04T00:00:00+00:00", "quantityOfParts": 60 }
/// </example>
public class OrderLotReference
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="LotReference" /> class.
    /// </summary>
    public OrderLotReference() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="LotReference" /> class.
    /// </summary>
    public OrderLotReference(Guid lotId, string? lotName)
    {
        LotId = lotId;
        LotName = lotName;
    }

    #endregion

    /// <summary>
    /// Unique identifier of the lot.
    /// </summary>
    /// <example>9c1e7a52-3b64-4f08-a1d2-5e6f7a8b9c0d</example>
    [JsonProperty(Order = 1)]
    public Guid LotId { get; set; }

    /// <summary>
    /// Name of the lot. Optional.
    /// </summary>
    /// <example>Lot 2025-04-02</example>
    [JsonProperty(Order = 2)]
    [StringLength(100)]
    public string? LotName { get; set; }

    /// <summary>
    /// Number of parts of this order that are in the lot.
    /// </summary>
    /// <example>60</example>
    [JsonProperty(Order = 231)]
    [Range(0, int.MaxValue)]
    public int QuantityOfParts { get; set; }

    /// <summary>
    /// Date on which production of the lot is planned to start. Optional.
    /// </summary>
    /// <example>2025-04-03T00:00:00+00:00</example>
    [JsonProperty(Order = 30)]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Date on which production of the lot is planned to be completed. Optional.
    /// </summary>
    /// <example>2025-04-04T00:00:00+00:00</example>
    [JsonProperty(Order = 31)]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Converts a Lot to <see cref="LotReference" />.
    /// </summary>
    public static implicit operator OrderLotReference(Lot lot)
    {
        return new OrderLotReference(lot.Id, lot.Name);
    }
}
