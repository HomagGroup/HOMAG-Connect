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
    /// * orderConfigurator
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

    /// <summary>
    /// An optional definition of the room information.
    /// This can also be mutiple rooms.
    /// The data structure is sperately defined.
    /// The position of each ConfigurationPosition must match 
    /// with the corrdinate system of the room data.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? RoomInformation { get; set; }

    /// <summary>
    /// Gets or sets the type of group.
    /// </summary>
    [DefaultValue(GroupType.Default)]
    [JsonProperty(Order = 30)]
    public GroupType GroupType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Bill of Materials (BOM) data is required for processing.
    /// </summary>
    [JsonProperty(Order = 40)]
    public bool? NeedsBOMData { get; set; }
}
