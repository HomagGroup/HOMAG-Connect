using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Contracts.Boards
{
    public class BoardEntity
    {
        /// <summary>
        /// Gets or sets the id (#)
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the length. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, PropertyDecimals.MillimeterDecimals, PropertyDecimals.InchDecimals)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the width. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, PropertyDecimals.MillimeterDecimals, PropertyDecimals.InchDecimals)]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the management type.
        /// </summary>
        public ManagementType ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public StorageLocation Location { get; set; }
        
        /// <summary>
        /// Gets or sets the board type properties.
        /// </summary>
        public BoardType BoardType { get; set; }
    }
}