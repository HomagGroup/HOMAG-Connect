using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Image sizes.
    /// </summary>
    [ResourceManager(typeof(ImageSizeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ImageSize
    {
        /// <summary>
        /// Small size.
        /// </summary
        [Display(Description = "Small")]
        Small,

        /// <summary>
        /// Medium size.
        /// </summary>
        [Display(Description = "Medium")] 
        Medium,

        /// <summary>
        /// Large size.
        /// </summary>
        [Display(Description = "Larger")] 
        Larger,

        /// <summary>
        /// Thumbnail size.
        /// </summary>
        [Display(Description = "Thumbnail Square")]
        ThumbnailSquare,

        /// <summary>
        /// Texture size.
        /// </summary>
        [Display(Description = "Texture Square")] 
        TextureSquare,

        /// <summary>
        /// Original size.
        /// </summary>
        // Original size should remain last
        [Display(Description = "Original")] 
        Original
    }
}