using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    /// <summary>
    /// Edgeband material category.
    /// </summary>
    public enum EdgebandMaterialCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        /// <summary>
        /// ABS.
        /// </summary>
        [Display(Description = "ABS")]
        ABS,

        /// <summary>
        /// Acrylic.
        /// </summary>
        [Display(Description = "Acrylic")]
        Acrylic,

        /// <summary>
        /// Aluminum.
        /// </summary>
        [Display(Description = "Aluminum")]
        Aluminum,

        /// <summary>
        /// Melamine.
        /// </summary>
        [Display(Description = "Melamine")]
        Melamine,

        /// <summary>
        /// Others.
        /// </summary>
        [Display(Description = "Others")]
        Others,

        /// <summary>
        /// PMMA.
        /// </summary>
        [Display(Description = "PMMA")]
        PMMA,

        /// <summary>
        /// PP.
        /// </summary>
        [Display(Description = "PP")]
        PP,

        /// <summary>
        /// PVC.
        /// </summary>
        [Display(Description = "PVC")]
        PVC,

        /// <summary>
        /// Real wood.
        /// </summary>
        [Display(Description = "Real wood")]
        RealWood,

        /// <summary>
        /// Veneer.
        /// </summary>
        [Display(Description = "Veneer")]
        Veneer

        // ReSharper restore InconsistentNaming
        // ReSharper restore IdentifierTypo
    }
}
