using System;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Image information.
    /// </summary>
    public class ImageInformation
    {
        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public Uri? Url { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public Size? Size { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        public string? ContentType { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the grain.
        /// </summary>
        public Grain Grain { get; set; } 
    }
}
