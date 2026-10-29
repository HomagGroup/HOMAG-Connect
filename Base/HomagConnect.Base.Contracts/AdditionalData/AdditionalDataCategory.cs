using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Well known categories which can be used for <see cref="AdditionalDataEntity.Category" />. The property remains a
/// string, so any other category value stays valid.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum AdditionalDataCategory
{
    /// <summary />
    Unknown,

    /// <summary>
    /// Decor of a material (Type=Decor).
    /// </summary>
    Decor,

    /// <summary>
    /// Small preview image of the entity.
    /// </summary>
    Thumbnail,

    /// <summary>
    /// Image of the entity.
    /// </summary>
    Image,

    /// <summary>
    /// Data used by the Roomle configurator.
    /// </summary>
    Roomle,

    /// <summary>
    /// Image of a surface (Type=Image).
    /// </summary>
    [Display(Description = "Surface")]
    Surface,

    /// <summary>
    /// Image of a configuration attribute value (Type=Image).
    /// </summary>
    [Display(Description = "Attribute value image")]
    AttributeValueImage,

    /// <summary>
    /// Plan of a room (Type=Image or Type=Pdf).
    /// </summary>
    [Display(Description = "Room plan")]
    RoomPlan,

    /// <summary>
    /// Image of a single article / position (Type=Image).
    /// </summary>
    [Display(Description = "Article image")]
    ArticleImage,

    /// <summary>
    /// Image of a module (Type=Image).
    /// </summary>
    [Display(Description = "Module image")]
    ModuleImage
}