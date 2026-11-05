using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards;

/// <summary>
/// A catalog board type is a manufacturer-sourced sheet material that can be added to materialManager with its identifying
/// and product information.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-platten">Learn more about board types</see>.
/// </summary>
/// <remarks>Catalog dimensions are expressed in millimeters.</remarks>
/// <example>
/// { "articleNumber": "ART-100200", "coatingCategory": "MelamineThermoset",
/// "decorCode": "DCR-7788", "decorName": "Craft Oak", "embossingBottom": "ST10",
/// "embossingTop": "ST22", "grain": "Lengthwise", "gtin": "04012345678901",
/// "length": 2800.0, "manufacturerName": "Example Supplier", "materialCategory": "Chipboard",
/// "productName": "Gold Craft Oak", "thickness": 19.0,
/// "thumbnail": "https://example.com/materials/boards/P2_Gold_Craft_Oak_19.0.png", "width": 2070.0 }
/// </example>
public class CatalogBoardType : IDimensionProperties
{
    /// <summary>
    /// Article number assigned to the board product by its manufacturer, when available.
    /// </summary>
    /// <example>ART-100200</example>
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Surface coating of the board product.
    /// </summary>
    /// <example>MelamineThermoset</example>
    public CoatingCategory CoatingCategory { get; set; }

    /// <summary>
    /// Code identifying the board's decorative design, when available.
    /// </summary>
    /// <example>DCR-7788</example>
    public string? DecorCode { get; set; }

    /// <summary>
    /// Name of the board's decorative design, when available.
    /// </summary>
    /// <example>Craft Oak</example>
    public string? DecorName { get; set; }

    /// <summary>
    /// Surface texture code for the bottom decor side, when available.
    /// </summary>
    /// <example>ST10</example>
    public string? EmbossingBottom { get; set; }

    /// <summary>
    /// Surface texture code for the top decor side, when available.
    /// </summary>
    /// <example>ST22</example>
    public string? EmbossingTop { get; set; }

    /// <summary>
    /// Grain orientation relative to the board's length and width.
    /// </summary>
    /// <example>Lengthwise</example>
    public Grain Grain { get; set; }

    /// <summary>
    /// Global Trade Item Number identifying the board product, when available.
    /// </summary>
    /// <example>04012345678901</example>
    public string? Gtin { get; set; }

    /// <inheritdoc />
    public double? Length { get; set; }

    /// <summary>
    /// Name of the board manufacturer, when available.
    /// </summary>
    /// <example>Example Supplier</example>
    public string? ManufacturerName { get; set; }

    /// <summary>
    /// Base material from which the board is made.
    /// </summary>
    /// <example>Chipboard</example>
    public BoardMaterialCategory MaterialCategory { get; set; }

    /// <summary>
    /// Product name used in the manufacturer's catalog, when available.
    /// </summary>
    /// <example>Gold Craft Oak</example>
    public string? ProductName { get; set; }

    /// <inheritdoc />
    public double? Thickness { get; set; }

    /// <summary>
    /// Address of the product's thumbnail image, when available.
    /// </summary>
    /// <example>https://example.com/materials/boards/P2_Gold_Craft_Oak_19.0.png</example>
    public Uri? Thumbnail { get; set; }

    /// <inheritdoc />
    public double? Width { get; set; }
}