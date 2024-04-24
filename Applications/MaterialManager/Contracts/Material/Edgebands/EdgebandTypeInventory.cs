using System;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// A edgeband type inventory.
    /// </summary>
    public class EdgebandTypeInventory
    {
        /// <summary>
        /// Gets or sets the edgebands instance code.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        public string? Workstation { get; set; }

        /// <summary>
        /// Gets or sets the current length of the instance data.
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// Gets or set the total length of the instance data.
        /// </summary>
        public double? TotalLength { get; set; }

        /// <summary>
        /// Gets or sets the current thickness.
        /// </summary>
        public double? CurrentThickness { get; set; }
        
        /// <summary>
        /// Gets or sets the creation date of the instance data.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the additional comments of the instance data.
        /// </summary>
        public string? AdditionalCommentsEdgeband { get; set; }
    }
}