using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    public enum GluingCategory
    {
        [Display(Description = "Hot-melt glue")]
        HotMeltGlue,

        [Display(Description = "Zero-joint")]
        ZeroJoint,

        [Display(Description = "Other")]
        Other
    }
}