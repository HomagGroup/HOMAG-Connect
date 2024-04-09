using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    public enum EdgebandMaterialCategory
    {
        [Display(Description = "ABS")]
        Abs,

        [Display(Description = "Acrylic")]
        Acrylic,

        [Display(Description = "Aluminum")]
        Aluminum,

        [Display(Description = "Melamine")]
        Melamine,

        [Display(Description = "Others")]
        Others,

        [Display(Description = "PMMA")]
        Pmma,

        [Display(Description = "PP")]
        Pp,

        [Display(Description = "PVC")]
        Pvc,

        [Display(Description = "Real wood")]
        RealWood,

        [Display(Description = "Veneer")]
        Veneer
    }
}
