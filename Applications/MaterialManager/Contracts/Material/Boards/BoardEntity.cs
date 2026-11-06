#nullable enable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// A board inventory entry connects a physical board format to its quantity and storage location for day-to-day stock
/// management.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-platten">Learn more about board types</see>.
/// </summary>
/// <example>
/// {
/// "id": "B-1001",
/// "boardType": {
/// "boardCode": "BOARD-001-2800x2070-19",
/// "materialCode": "MAT-001-19",
/// "thickness": 19.0,
/// "materialCategory": "Chipboard",
/// "coatingCategory": "MelamineThermoset",
/// "standardQuality": "RawCut",
/// "width": 2070.0,
/// "length": 2800.0,
/// "grain": "Lengthwise",
/// "unitSystem": "Metric"
/// },
/// "comments": "Reserved for production.",
/// "creationDate": "2025-04-01T08:30:00+00:00",
/// "length": 2800.0,
/// "width": 2070.0,
/// "location": { "locationId": "rack-a-01", "name": "Main Buffer 03" },
/// "managementType": "GoodsInStock",
/// "quantity": 12
/// }
/// </example>
public class BoardEntity
{
    /// <summary>
    /// Material and format definition for the boards represented by this inventory entry.
    /// </summary>
    /// <example>
    /// { "boardCode": "BOARD-001-2800x2070-19", "materialCode": "MAT-001-19", "thickness": 19.0, "width": 2070.0,
    /// "length": 2800.0, "unitSystem": "Metric" }
    /// </example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_BoardType))]
    public BoardType BoardType { get; set; }

    /// <summary>
    /// Notes about this inventory entry.
    /// </summary>
    /// <example>Reserved for production.</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Comments))]
    [DefaultValue("")]
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when the inventory entry was created, when known.
    /// </summary>
    /// <example>2025-04-01T08:30:00+00:00</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_CreationDate))]
    public DateTimeOffset? CreationDate { get; set; }

    /// <summary>
    /// Identifier of this inventory entry, not its board type code.
    /// </summary>
    /// <example>B-1001</example>
    [Key]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Length of one board represented by this entry.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2800.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Length))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Length { get; set; }

    /// <summary>
    /// Storage compartment containing this inventory entry.
    /// </summary>
    /// <example>{ "locationId": "rack-a-01", "name": "Main Buffer 03" }</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Location))]
    public StorageLocation Location { get; set; }

    /// <summary>
    /// Whether the boards are tracked individually, as a stack, or as goods in stock.
    /// </summary>
    /// <example>GoodsInStock</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_ManagementType))]
    public ManagementType ManagementType { get; set; }

    /// <summary>
    /// Number of boards represented by this inventory entry.
    /// </summary>
    /// <example>12</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Quantity))]
    public int Quantity { get; set; }

    /// <summary>
    /// Width of one board represented by this entry.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2070.0</example>
    [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Width))]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Width { get; set; }
}