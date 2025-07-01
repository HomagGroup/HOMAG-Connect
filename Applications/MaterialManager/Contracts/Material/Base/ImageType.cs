using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Image types
    /// </summary>
    [ResourceManager(typeof(ImageTypeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ImageType
    {
        /// <summary>
        /// The main picture.
        /// </summary>
        [Display(Description = "Picture")] 
        Picture,

        /// <summary>
        /// A technical drawing.
        /// </summary>
        [Display(Description = "Drawing")] 
        Drawing,

        /// <summary>
        /// A technical picture used for measurement that also shows identifier for measurement values.
        /// </summary>
        [Display(Description = "Measurement")] 
        Measurement,

        /// <summary>
        /// A specification sheet usually taken by the user from the manufacturer and uploaded
        /// </summary>
        [Display(Description = "Spec Sheet")] 
        SpecSheet,

        /// <summary>
        /// A pdf containing a label used for printing
        /// </summary>
        [Display(Description = "Label")] 
        Label,

        /// <summary>
        /// Image used for 3D rendering of f.e. cabinets
        /// </summary>
        [Display(Description = "Texture")] 
        Texture
    }
}