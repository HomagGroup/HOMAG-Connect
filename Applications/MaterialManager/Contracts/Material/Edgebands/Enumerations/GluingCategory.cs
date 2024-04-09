using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    public enum GluingCategory
    {
        [Display(Description = "Hot-melt glue")]
        hotmelt,

        [Display(Description = "Zero-joint")]
        zerojoint,

        [Display(Description = "Other")]
        others
    }
}