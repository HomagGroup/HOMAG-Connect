using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// A catalog edgeband type is a manufacturer-sourced edge material that can be added to materialManager with its
/// identifying and product information.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>
/// .
/// </summary>
/// <example>
/// { "articleNumber": "ART-200300", "decorCode": "DCR-7788", "decorEmbossingCode": "ST22",
/// "decorName": "Craft Oak", "defaultLength": 500.0, "functionLayerThickness": 0.1,
/// "gtin": "04012345678901", "height": 23.0, "manufacturerName": "Example Supplier",
/// "materialCategory": "ABS", "process": "HotmeltGlue", "productName": "Craft Oak ABS Edgeband",
/// "protectionFilmThickness": 0.05, "thickness": 1.0,
/// "thumbnail": "https://example.com/materials/edgebands/ABS_Craft_Oak_23x1.png" }
/// </example>
public class CatalogEdgebandType
{
    /// <summary>
    /// Article number assigned to the edgeband product by its manufacturer, when available.
    /// </summary>
    /// <example>ART-200300</example>
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Cost associated with the edgeband product, when available.
    /// </summary>
    /// <example>0.85</example>
    public double? Costs { get; set; }

    /// <summary>
    /// Code identifying the edgeband's decorative design, when available.
    /// </summary>
    /// <example>DCR-7788</example>
    public string? DecorCode { get; set; }

    /// <summary>
    /// Surface texture code for the edgeband's decorative finish, when available.
    /// </summary>
    /// <example>ST22</example>
    public string? DecorEmbossingCode { get; set; }

    /// <summary>
    /// Name of the edgeband's decorative design, when available.
    /// </summary>
    /// <example>Craft Oak</example>
    public string? DecorName { get; set; }

    /// <summary>
    /// Catalog coil length in m, when available.
    /// </summary>
    /// <example>500.0</example>
    public double? DefaultLength { get; set; }

    /// <summary>
    /// Thickness of the edgeband's functional layer in mm, when available.
    /// </summary>
    /// <example>0.1</example>
    public double? FunctionLayerThickness { get; set; }

    /// <summary>
    /// Global Trade Item Number identifying the edgeband product, when available.
    /// </summary>
    /// <example>04012345678901</example>
    public string? Gtin { get; set; }

    /// <summary>
    /// Edgeband height in mm, when available.
    /// </summary>
    /// <example>23.0</example>
    public double? Height { get; set; }

    /// <summary>
    /// Name of the edgeband manufacturer, when available.
    /// </summary>
    /// <example>Example Supplier</example>
    public string? ManufacturerName { get; set; }

    /// <summary>
    /// Material from which the edgeband is made.
    /// </summary>
    /// <example>ABS</example>
    public EdgebandMaterialCategory MaterialCategory { get; set; }

    /// <summary>
    /// Process used to attach the edgeband to a board.
    /// </summary>
    /// <example>HotmeltGlue</example>
    public EdgebandingProcess Process { get; set; }

    /// <summary>
    /// Product name used in the manufacturer's catalog, when available.
    /// </summary>
    /// <example>Craft Oak ABS Edgeband</example>
    public string? ProductName { get; set; }

    /// <summary>
    /// Thickness of the protective film in mm, when available.
    /// </summary>
    /// <example>0.05</example>
    public double? ProtectionFilmThickness { get; set; }

    /// <summary>
    /// Edgeband thickness in mm, when available.
    /// </summary>
    /// <example>1.0</example>
    public double? Thickness { get; set; }

    /// <summary>
    /// Address of the product's thumbnail image, when available.
    /// </summary>
    /// <example>https://example.com/materials/edgebands/ABS_Craft_Oak_23x1.png</example>
    public Uri? Thumbnail { get; set; }
}