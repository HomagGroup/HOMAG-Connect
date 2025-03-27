using System.ComponentModel;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Order item group.
/// </summary>
public class Group : Base
{
    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Group;
        }
        set
        {
            // Ignore
        }
    }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the source of the group.
    /// Possible values:
    /// * orderConfiguration
    /// * TODO: ???
    /// </summary>
    [JsonProperty(Order = 11)]
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    [DefaultValue(1)]
    [JsonProperty(Order = 13)]
    public int Quantity { get; set; } = 1;
}