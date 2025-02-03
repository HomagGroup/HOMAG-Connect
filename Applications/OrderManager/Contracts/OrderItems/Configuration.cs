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
    /// An optional library id
    /// </summary>
    public string? LibraryId { get; set; }

    /// <summary>
    /// The id of this configuration module
    /// </summary>
    public string? ModuleId { get; set; }

    /// <summary>
    /// An optional position of this module (x, y, z)
    /// </summary>
    public double[]? Position { get; set; }

    /// <summary>
    /// An optional rotation of this module (x, y, z)
    /// </summary>
    public double[]? Rotation { get; set; }

    /// <summary>
    /// Contains configuration attributes.
    /// </summary>
    public IDictionary<string, object>? Attributes { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
}