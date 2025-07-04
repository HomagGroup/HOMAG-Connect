using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Board type allocation details.
    /// </summary>
    public class BoardTypeAllocationDetails
    {

        /// <summary>
        /// Gets or sets the boardType code.
        /// </summary>
        [Required]
        public string BoardTypeCode { get; set; }


        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }


        /// <summary>
        /// Gets or sets the pattern number.
        /// </summary>
        public int? PatternNumber { get; set; }

        /// <summary>
        /// Gets or sets the additional data for pattern images.
        /// </summary>
        public AdditionalDataEntity AdditionalData { get; set; }
    }
}
