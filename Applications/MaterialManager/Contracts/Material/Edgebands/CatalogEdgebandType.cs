using System;

using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// Represents an edgeband type entry from the Tadamo material catalog, including classification,
    /// dimensions, manufacturer data, and processing information.
    /// </summary>
    public class CatalogEdgebandType
    {
        /// <summary>
        /// Gets or sets the manufacturer-assigned article number.
        /// </summary>
        /// <example>ART-200300</example>
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the cost per meter of the edgeband.
        /// </summary>
        /// <example>0.85</example>
        public double? Costs { get; set; }

        /// <summary>
        /// Gets or sets the decor code.
        /// </summary>
        /// <example>DCR-7788</example>
        public string? DecorCode { get; set; }

        /// <summary>
        /// Gets or sets the decor embossing code.
        /// </summary>
        /// <example>ST22</example>
        public string? DecorEmbossingCode { get; set; }

        /// <summary>
        /// Gets or sets the decor name.
        /// </summary>
        /// <example>Craft Oak</example>
        public string? DecorName { get; set; }

        /// <summary>
        /// Gets or sets the default roll length in m.
        /// </summary>
        /// <example>500.0</example>
        public double? DefaultLength { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the function layer in mm.
        /// </summary>
        /// <example>0.1</example>
        public double? FunctionLayerThickness { get; set; }

        /// <summary>
        /// Gets or sets the GTIN (Global Trade Item Number).
        /// </summary>
        /// <example>04012345678901</example>
        public string? Gtin { get; set; }

        /// <summary>
        /// Gets or sets the edgeband height in mm.
        /// </summary>
        /// <example>23.0</example>
        public double? Height { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer name.
        /// </summary>
        /// <example>HOMAG Sample Supplier</example>
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the material category of the edgeband.
        /// </summary>
        /// <example>ABS</example>
        public EdgebandMaterialCategory MaterialCategory { get; set; }

        /// <summary>
        /// Gets or sets the edgebanding process type.
        /// </summary>
        /// <example>HotAir</example>
        public EdgebandingProcess Process { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <example>White ABS Edgeband</example>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the protection film in mm.
        /// </summary>
        /// <example>0.05</example>
        public double? ProtectionFilmThickness { get; set; }

        /// <summary>
        /// Gets or sets the edgeband thickness in mm.
        /// </summary>
        /// <example>1.0</example>
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the URI of the product thumbnail image.
        /// </summary>
        /// <example>https://example.com/materials/edgebands/ABS_White_23x1.png</example>
        public Uri? Thumbnail { get; set; }
    }
}