using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    public enum GluingCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        [Display(Description = "Hot-melt glue")]
        HotmeltGlue,

        [Display(Description = "Zero-joint")]
        Zerojoint,

        [Display(Description = "Other")]
        Other

        // ReSharper restore InconsistentNaming
        // ReSharper restore IdentifierTypo
    }
}