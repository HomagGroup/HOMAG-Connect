using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Request
{
    public class MaterialAssistEdgebandEntity
    {
        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        [StringLength(300)]
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets the current thickness.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
        [Range(0.01, 99.99)]
        public double CurrentThickness { get; set; }

        /// <summary>
        /// Gets or sets the board code to which the entity should be assigned.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string EdgebandCode { get; set; }

        /// <summary>
        /// Gets or sets the custom id.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the management type.
        /// </summary>
        public ManagementType? ManagementType { get; set; }

        /// <summary>
        /// Gets or sets the quantity code.
        /// </summary>
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}