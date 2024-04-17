using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    /// <summary>
    /// Gluing category.
    /// </summary>
    public enum GluingCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        /// <summary>
        /// Hot-melt glue.
        /// </summary>
        [Display(Description = "Hot-melt glue")]
        HotmeltGlue,

        /// <summary>
        /// Zero-joint.
        /// </summary>
        [Display(Description = "Zero-joint")]
        Zerojoint,

        /// <summary>
        /// Other.
        /// </summary>
        [Display(Description = "Other")]
        Other

        // ReSharper restore InconsistentNaming
        // ReSharper restore IdentifierTypo
    }
}