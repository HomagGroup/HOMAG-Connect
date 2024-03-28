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
        HPL,

        [Display(Description = "Laminates consisting of decorative paper")]
        DecorativePaper,

        [Display(Description = "Melamine (thermoset)")]
        MelamineThermoSet,

        [Display(Description = "Painted")]
        Painted,

        [Display(Description = "Protective film")]
        ProtectiveFilm,

        [Display(Description = "Real metal")]
        RealMetal,

        [Display(Description = "Uncoated")]
        UnCoated,

        [Display(Description = "Undefined")]
        UnDefined,

        [Display(Description = "Veneer")]
        Veneer
    }
}