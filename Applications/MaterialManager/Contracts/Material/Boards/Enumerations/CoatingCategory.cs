using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    public enum CoatingCategory
    {
        [Display(Description = "Aluminium")]
        Aluminium,

        [Display(Description = "Glass laminate (PMMA)")]
        LaminatedGlass,

        [Display(Description = "HPL")]
        // ReSharper disable once InconsistentNaming
        Hpl,

        [Display(Description = "Laminates consisting of decorative paper")]
        DecorativePaper,

        [Display(Description = "Melamine (thermoset)")]
        MelamineThermoset,

        [Display(Description = "Painted")]
        Painted,

        [Display(Description = "Protective film")]
        ProtectiveFilm,

        [Display(Description = "Real metal")]
        RealMetal,

        [Display(Description = "Uncoated")]
        Uncoated,

        [Display(Description = "Undefined")]
        Undefined,

        [Display(Description = "Veneer")]
        Veneer
    }
}