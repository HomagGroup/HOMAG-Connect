using System.Runtime.Serialization;

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
public class AdditionalDataEntity : IExtensibleDataObject
{
    /// <summary>
    /// Gets or sets the additional data type.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual AdditionalDataType Type { get; set; }

    /// <summary>
    /// Gets or sets the additional data name
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Name { get; set; }

    /// <summary>
    /// Previews
    /// </summary>
    [JsonProperty(Order = 2)]
    public Dictionary<AdditionalDataPreviewSize, Uri>? PreviewUris { get; set; }

    /// <summary>
    /// Gets or sets the download uri.
    /// </summary>
    public Uri? DownloadUri { get; set; }

    #region (90) IExtensibleDataObject Members

    /// <intheritdoc />
    [JsonProperty(Order = 90)]
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}