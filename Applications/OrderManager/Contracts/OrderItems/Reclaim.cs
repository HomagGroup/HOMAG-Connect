
// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Represents a reclaim item in the order.
/// </summary>
public class Reclaim : Base
{
    /// <summary>
    /// Gets or sets the reason for reclaiming the item.
    /// </summary>
    public ReclaimReason Reason { get; set; }

    /// <summary>
    /// Gets or sets the comment for the reclaim item.
    /// </summary>
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
