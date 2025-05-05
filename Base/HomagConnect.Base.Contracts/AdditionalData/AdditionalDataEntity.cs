using System.Collections.ObjectModel;
using System.Diagnostics;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.AdditionalData;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(AdditionalDataImage), AdditionalDataType.Image)]
[JsonSubtypes.KnownSubType(typeof(AdditionalDataTexture), AdditionalDataType.Texture)]
[JsonSubtypes.KnownSubType(typeof(AdditionalDataCNCProgram), AdditionalDataType.CNCProgram)]
[JsonSubtypes.KnownSubType(typeof(AdditionalDataThreeD), AdditionalDataType.ThreeD)]
[JsonSubtypes.KnownSubType(typeof(AdditionalDataPdf), AdditionalDataType.Pdf)]
[JsonSubtypes.KnownSubType(typeof(AdditionalDataZip), AdditionalDataType.Zip)]
[DebuggerDisplay("Type={Type}, Name={Name}")]
public class AdditionalDataEntity
{
    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonExtensionData]
    [JsonProperty(Order = 30)]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    /// <remarks>
    /// Possible values:
    /// * OverviewImage (Type=Image) => per group
    /// * AboveImage (Type=Image) => per group
    /// * ThreeDModel (Type=ThreeD) => per group or per position
    /// * ArticleImage (Type=Image) => per position
    /// </remarks>
    [JsonProperty(Order = 1)]
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the download file name.
    /// </summary>
    [JsonProperty(Order = 10)]
    public string? DownloadFileName { get; set; }

    /// <summary>
    /// Gets or sets the download uri.
    /// </summary>
    [JsonProperty(Order = 11)]
    public Uri? DownloadUri { get; set; }

    /// <summary>
    /// Gets or sets the additional data name
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? Name { get; set; }

    /// <summary>
    /// Previews
    /// </summary>
    [JsonProperty(Order = 20)]
    public Collection<AdditionalDataPreview>? Previews { get; set; }

    /// <summary>
    /// Gets or sets the additional data type.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual AdditionalDataType Type { get; set; }

    /// <summary>
    /// Creates a new instance of the <see cref="AdditionalDataEntity" /> class based on the file extension.
    /// </summary>
    public static AdditionalDataEntity CreateInstance(string fileExtension)
    {
        if (fileExtension is ".png" or ".jpg" or ".gif" or ".bmp")
        {
            return new AdditionalDataImage();
        }

        if (fileExtension is ".mpr" or ".mprx" or ".mprxe")
        {
            return new AdditionalDataCNCProgram();
        }

        if (fileExtension is ".3ds" or ".3ds.zip")
        {
            return new AdditionalDataThreeD();
        }

        if (fileExtension is ".pdf")
        {
            return new AdditionalDataPdf();
        }

        if (fileExtension is ".zip")
        {
            return new AdditionalDataZip();
        }

        return new AdditionalDataEntity();
    }
}