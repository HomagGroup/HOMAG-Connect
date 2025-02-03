using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

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
    [JsonProperty(Order = 10)]
    public string? LibraryId { get; set; }

    /// <summary>
    /// The id of this configuration module
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? ModuleId { get; set; }

    /// <summary>
    /// An optional position of this module (x, y, z)
    /// </summary>
    [JsonProperty(Order = 30)]
    public double[]? Position { get; set; }

    /// <summary>
    /// An optional rotation of this module (x, y, z)
    /// </summary>
    [JsonProperty(Order = 40)]
    public double[]? Rotation { get; set; }

    /// <summary>
    /// Contains configuration attributes.
    /// </summary>
    [JsonProperty(Order = 50)]
    public IDictionary<string, object>? Attributes { get; set; } = new Dictionary<string, object>(StringComparer.Ordinal);
}