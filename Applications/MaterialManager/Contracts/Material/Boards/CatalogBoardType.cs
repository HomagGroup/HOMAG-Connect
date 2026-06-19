using System;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Represents a board type entry from the Tadamo material catalog, including classification,
    /// dimensions, manufacturer data, and surface information.
    /// </summary>
    public class CatalogBoardType
    {
        /// <summary>
        /// Gets or sets the manufacturer-assigned article number.
        /// </summary>
        /// <example>ART-100200</example>
        public string? ArticleNumber { get; set; }

        /// <summary>
        /// Gets or sets the coating category of the board.
        /// </summary>
        /// <example>MelamineResinCoated</example>
        public CoatingCategory CoatingCategory { get; set; }

        /// <summary>
        /// Gets or sets the decor code.
        /// </summary>
        /// <example>DCR-7788</example>
        public string? DecorCode { get; set; }

        /// <summary>
        /// Gets or sets the decor name.
        /// </summary>
        /// <example>Craft Oak</example>
        public string? DecorName { get; set; }

        /// <summary>
        /// Gets or sets the embossing of the bottom decor side.
        /// </summary>
        /// <example>ST10</example>
        public string? EmbossingBottom { get; set; }

        /// <summary>
        /// Gets or sets the embossing of the top decor side.
        /// </summary>
        /// <example>ST22</example>
        public string? EmbossingTop { get; set; }

        /// <summary>
        /// Gets or sets the grain direction of the board.
        /// </summary>
        /// <example>Lengthwise</example>
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the GTIN (Global Trade Item Number).
        /// </summary>
        /// <example>04012345678901</example>
        public string? Gtin { get; set; }

        /// <summary>
        /// Gets or sets the board length in mm.
        /// </summary>
        /// <example>2800.0</example>
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer name.
        /// </summary>
        /// <example>HOMAG Sample Supplier</example>
        public string? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the material category of the board.
        /// </summary>
        /// <example>ParticleBoard</example>
        public BoardMaterialCategory MaterialCategory { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        /// <example>Gold Craft Oak</example>
        public string? ProductName { get; set; }

        /// <summary>
        /// Gets or sets the board thickness in mm.
        /// </summary>
        /// <example>19.0</example>
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the URI of the product thumbnail image.
        /// </summary>
        /// <example>https://example.com/materials/boards/P2_Gold_Craft_Oak_19.0.png</example>
        public Uri? Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the board width in mm.
        /// </summary>
        /// <example>2070.0</example>
        public double? Width { get; set; }
    }
}