// Note: This is preliminary code and is subject to change

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Error or warning information attached to an order item, for example from a failed calculation.
/// </summary>
/// <example>
/// { "type": "ErrorInfo", "text": "No price found for position 3.", "category": "Warning" }
/// </example>
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
    /// Text of the error or warning. Optional.
    /// </summary>
    /// <example>No price found for position 3.</example>
    [JsonProperty(Order = 2)]
    public string? Text { get; set; }

    /// <summary>
    /// Category of the error information, either <c>Error</c> or <c>Warning</c>. Optional.
    /// </summary>
    /// <example>Warning</example>
    [JsonProperty(Order = 3)]
    public string? Category { get; set; }
}
