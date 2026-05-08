#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// Represents detailed board type information including inventory, allocations, and additional data.
/// </summary>
/// <example>
/// { "boardCode": "P2_Gold_Craft_Oak_19.0", "inventory": [], "allocations": [], "additionalData": [] }
/// </example>
public class BoardTypeDetails : BoardType
{
    /// <summary>
    /// Gets or sets the additional data entries associated with the board type.
    /// </summary>
    /// <example>[]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeDetailsProperties_AdditionalData))]
    [JsonProperty(Order = 84)]
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the allocations for the board type.
    /// </summary>
    /// <example>[]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeDetailsProperties_Allocations))]
    [JsonProperty(Order = 83)]
    public ICollection<BoardTypeAllocation> Allocations { get; set; } = new List<BoardTypeAllocation>();

    /// <summary>
    /// Gets or sets the inventory entries for the board type.
    /// </summary>
    /// <example>[]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeDetailsProperties_Inventory))]
    [JsonProperty(Order = 82)]
    public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();
}