using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    public enum EdgebandMaterialCategory
    {
        [Display(Description = "ABS")]
        Abs,

        [Display(Description = "Acrylic")]
        Acr,

        [Display(Description = "Aluminum")]
        Aluminum,

        [Display(Description = "Melamine")]
        Mel,

        [Display(Description = "Others")]
        Others,

        [Display(Description = "PMMA")]
        Pmma,

        [Display(Description = "PP")]
        Pp,

        [Display(Description = "PVC")]
        Pvc,

        [Display(Description = "Real wood")]
        Sw,

        [Display(Description = "Veneer")]
        Veneer
    }
}
