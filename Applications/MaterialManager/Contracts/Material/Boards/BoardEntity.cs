#nullable enable
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Represents a board inventory entry including dimensions, quantity, and storage information.
    /// </summary>
    /// <example>
    /// { "id": "B-1001", "boardType": { "boardCode": "P2_Gold_Craft_Oak_19.0", "materialCode": "P2_Gold_Craft_Oak", "thickness": 19.0, "materialCategory": "ParticleBoard", "coatingCategory": "MelamineResinCoated", "standardQuality": "P2", "width": 2070.0, "length": 2800.0, "grain": "Lengthwise", "unitSystem": "Metric" }, "comments": "Reserved for production", "creationDate": "2025-04-01T08:30:00+00:00", "length": 2800.0, "location": { "barcode": "COMP-0004711", "locationId": "LOC-01-02-03", "name": "Main Buffer 03" }, "managementType": "StockMaterial", "quantity": 12, "width": 2070.0 }
    /// </example>
    public class BoardEntity
    {
        /// <summary>
        /// Gets or sets the board type properties.
        /// </summary>
        /// <example>{}</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_BoardType))]
        public BoardType BoardType { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <example>Reserved for production</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Comments))]
        [DefaultValue("")]
        public string Comments { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <example>2025-04-01T08:30:00+00:00</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_CreationDate))]
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the board entry.
        /// </summary>
        /// <example>B-1001</example>
        [Key]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the board length.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>2800.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Length))]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <example>{}</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Location))]
        public StorageLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the management type.
        /// </summary>
        /// <example>StockMaterial</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_ManagementType))]
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <example>12</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Quantity))]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the board width.
        /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
        /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
        /// </summary>
        /// <example>2070.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Width))]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Width { get; set; }
    }
}