using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.AdditionalData;

public class AdditionalDataPreview
{
    /// <summary>
    /// Gets or sets the preview size.
    /// </summary>
    [JsonProperty(Order = 0)]
    public AdditionalDataPreviewSize Size { get; set; }

    /// <summary>
    /// Gets or sets the preview uri.
    /// </summary>
    [JsonProperty(Order = 1)]
    public Uri? Uri { get; set; }
}