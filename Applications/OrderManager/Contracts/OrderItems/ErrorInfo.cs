// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// 
/// </summary>
public class ErrorInfo : Base
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.ErrorInfo;
        }
        set
        {
            // Ignore
        }
    }

    /// <summary>
    /// Optional an error information or text
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Optional an error category
    /// Possible values are:
    /// * Error
    /// * Warning
    /// </summary>
    public string? Category { get; set; }
}
