using System;

using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    public class ImageInformation
    {
        public Uri? Url { get; set; } 

        public Size? Size { get; set; } 

        public string? Type { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        public Grain Grain { get; set; } 
    }
}
