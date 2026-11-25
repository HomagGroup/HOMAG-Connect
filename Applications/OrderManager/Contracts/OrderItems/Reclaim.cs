
// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Reclaim (complaint) of an order item.
/// </summary>
/// <example>
/// { "type": "Reclaim", "reason": "Scratches", "comment": "Scratch on the front panel." }
/// </example>
public class Reclaim : Base
{
    /// <summary>
    /// Reason for the reclaim.
    /// </summary>
    /// <example>Scratches</example>
    public ReclaimReason Reason { get; set; }

    /// <summary>
    /// Additional free-text comment. Optional.
    /// </summary>
    /// <example>Scratch on the front panel.</example>
    public string? Comment { get; set; }

    /// <inheritdoc />
    public override Type Type
    {
        get
        {
            return Type.Reclaim;
        }
        set
        {
            // Ignore
        }
    }
}
