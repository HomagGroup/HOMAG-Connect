using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// Enum for the coating category
    /// </summary>
    public enum CoatingCategory
    {
        // ReSharper disable InconsistentNaming
        // ReSharper disable IdentifierTypo

        /// <summary>
        /// Aluminium.
        /// </summary>
        [Display(Description = "Aluminium")]
        Aluminium,

        /// <summary>
        /// Glass laminate (PMMA).
        /// </summary>
        [Display(Description = "Glass laminate (PMMA)")]
        LaminatedGlass_PMMA,

        /// <summary>
        /// HPL.
        /// </summary>
        [Display(Description = "HPL")]
        HPL,

        /// <summary>
        /// Laminates consisting of decorative paper.
        /// </summary>
        [Display(Description = "Laminates consisting of decorative paper")]
        DecorativePaper,

        /// <summary>
        /// Melamine (thermoset).
        /// </summary>
        [Display(Description = "Melamine (thermoset)")]
        MelamineThermoset,

        /// <summary>
        /// Painted.
        /// </summary>
        [Display(Description = "Painted")]
        Painted,

        /// <summary>
        /// Protective film.
        /// </summary>
        [Display(Description = "Protective film")]
        ProtectiveFilm,

        /// <summary>
        /// Real metal.
        /// </summary>
        [Display(Description = "Real metal")]
        RealMetal,

        /// <summary>
        /// Uncoated.
        /// </summary>
        [Display(Description = "Uncoated")]
        Uncoated,

        /// <summary>
        /// Undefined.
        /// </summary>
        [Display(Description = "Undefined")]
        Undefined,

        /// <summary>
        /// Veneer.
        /// </summary>
        [Display(Description = "Veneer")]
        Veneer

        // ReSharper restore InconsistentNaming
        // ReSharper restore IdentifierTypo
    }
}