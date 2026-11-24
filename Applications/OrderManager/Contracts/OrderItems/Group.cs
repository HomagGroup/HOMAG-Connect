using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Group of order items, for example the items of a room or of an optimization.
/// </summary>
/// <example>
/// { "type": "Group", "id": "G1", "name": "Kitchen", "source": "orderConfigurator", "quantity": 1, "groupType": "Default", "items": [] }
/// </example>
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
    /// Name of the group. Optional.
    /// </summary>
    /// <example>Kitchen</example>
    [JsonProperty(Order = 10)]
    public string? Name { get; set; }

    /// <summary>
    /// Source of the group, for example <c>orderConfigurator</c>. Optional.
    /// </summary>
    /// <example>orderConfigurator</example>
    [JsonProperty(Order = 11)]
    public string? Source { get; set; }

    /// <summary>
    /// Quantity of the group. Defaults to 1.
    /// </summary>
    /// <example>1</example>
    [DefaultValue(1)]
    [JsonProperty(Order = 13)]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Room information of the group, which can describe several rooms. Optional.
    /// The data structure is defined separately; the position of each configuration position
    /// must match the coordinate system of the room data.
    /// </summary>
    [JsonProperty(Order = 20)]
    public string? RoomInformation { get; set; }

    /// <summary>
    /// Type of the group. Defaults to <see cref="OrderItems.GroupType.Default" />.
    /// </summary>
    /// <example>Default</example>
    [DefaultValue(GroupType.Default)]
    [JsonProperty(Order = 30)]
    public GroupType GroupType { get; set; }

    /// <summary>
    /// <c>true</c> if bill of materials (BOM) data is required for processing the group. Optional.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 40)]
    public bool? NeedsBOMData { get; set; }

    /// <summary>
    /// Origin of the group, indicating where its items were added or edited. Optional.
    /// </summary>
    /// <example>orderConfigurator</example>
    [JsonProperty(Order = 50)]
    public string? Origin { get; set; }
}
