using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Describes a board entity.
    /// </summary>
    public class BoardEntity
    {
        /// <summary>
        /// Gets or sets the board type properties.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_BoardType))]
        public BoardType BoardType { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Comments))]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_CreationDate))]
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the id (#)
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Id))]
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the length. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Length))]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Location))]
        public StorageLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the management type.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_ManagementType))]
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Quantity))]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the width. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardEntityProperties_Width))]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double Width { get; set; }
    }
}