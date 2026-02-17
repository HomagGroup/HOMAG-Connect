using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.AdditionalData;

public class AdditionalDataPreview
{
    /// <summary>
    /// Gets or sets the preview size.
    /// </summary>
    [JsonProperty(Order = 0)]
    [Display(ResourceType = typeof(AdditionalDataPropertyDisplayNames), Name = nameof(Size))]
    public AdditionalDataPreviewSize Size { get; set; }

    /// <summary>
    /// Gets or sets the preview uri.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Display(ResourceType = typeof(AdditionalDataPropertyDisplayNames), Name = nameof(Uri))]
    public Uri? Uri { get; set; }
}