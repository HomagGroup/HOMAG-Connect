using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Creation details
    /// </summary>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CreationDetails))]
    public class CreationDetails
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Attachments.
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Attachments))]
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Creation comment.
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Comment))]
        public string? Comment { get; set; }
    }
}