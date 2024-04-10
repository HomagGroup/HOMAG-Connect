using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    public enum EdgebandMaterialCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        [Display(Description = "ABS")]
        ABS,

        [Display(Description = "Acrylic")]
        Acrylic,

        [Display(Description = "Aluminum")]
        Aluminum,

        [Display(Description = "Melamine")]
        Melamine,

        [Display(Description = "Others")]
        Others,

        [Display(Description = "PMMA")]
        PMMA,

        [Display(Description = "PP")]
        PP,

        [Display(Description = "PVC")]
        PVC,

        [Display(Description = "Real wood")]
        RealWood,

        [Display(Description = "Veneer")]
        Veneer

        // ReSharper restore InconsistentNaming
        // ReSharper restore IdentifierTypo
    }
}
