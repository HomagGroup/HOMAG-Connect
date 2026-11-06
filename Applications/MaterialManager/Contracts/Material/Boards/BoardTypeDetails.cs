#nullable enable

using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// Board type details bring together a material's stock, production allocations, and supporting information for managing
/// it in materialManager.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-platten">Learn more about board types</see>.
/// </summary>
/// <example>
/// {
/// "boardCode": "BOARD-001-2800x2070-19",
/// "materialCode": "MAT-001-19",
/// "length": 2800.0,
/// "width": 2070.0,
/// "thickness": 19.0,
/// "unitSystem": "Metric",
/// "inventory": [],
/// "allocations": [],
/// "additionalData": []
/// }
/// </example>
public class BoardTypeDetails : BoardType
{
    /// <summary>
    /// Supporting data associated with the board type, such as documents or images.
    /// </summary>
    /// <example>[]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeDetailsProperties_AdditionalData))]
    [JsonProperty(Order = 84)]
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Production reservations associated with this board type.
    /// </summary>
    /// <example>[]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeDetailsProperties_Allocations))]
    [JsonProperty(Order = 83)]
    public ICollection<BoardTypeAllocation> Allocations { get; set; } = new List<BoardTypeAllocation>();

    /// <summary>
    /// Inventory entries associated with this board type.
    /// </summary>
    /// <example>[]</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeDetailsProperties_Inventory))]
    [JsonProperty(Order = 82)]
    public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();
}