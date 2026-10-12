using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations
{
    /// <summary>
    /// The edgeband material category identifies the material an edge band is made from, such as ABS, PVC, or real wood.
    /// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>.
    /// </summary>
    [ResourceManager(typeof(EdgebandMaterialCategoryDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
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
