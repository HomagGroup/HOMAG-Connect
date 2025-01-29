namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// (Software) Configuration information.
/// </summary>
public class Configuration : Base
{
    /// <inheritdoc />
    public override Type Type
    {
        get
        {
            return Type.Configuration;
        }
    }

    /// <summary>
    /// Contains configuration attributes.
    /// </summary>
    public IDictionary<string, object>? Attributes { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
}