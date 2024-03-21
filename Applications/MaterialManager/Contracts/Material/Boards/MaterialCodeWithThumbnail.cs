using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A material code with thumbnail.
    /// </summary>
    public class MaterialCodeWithThumbnail: IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [JsonProperty(Order = 1)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or set the thumbnail uri.
        /// </summary>
        [JsonProperty(Order = 2)]
        public Uri? Thumbnail { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}