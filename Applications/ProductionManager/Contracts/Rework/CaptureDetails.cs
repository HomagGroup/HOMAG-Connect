using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Creation details
    /// </summary>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CaptureDetails))]
    public class CaptureDetails
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
        [JsonProperty(Order = 80)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Attachments))]
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Creation comment.
        /// </summary>
        [JsonProperty(Order = 10)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Comment))]
        public string? Comment { get; set; }

        /// <summary>
        /// Created by user
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CapturedBy))]
        [JsonProperty(Order = 20)]
        public string? CapturedBy { get; set; }

        /// <summary>
        /// Last workstation where the rework was captured
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(LastWorkstation))]
        [JsonProperty(Order = 30)]
        public string? LastWorkstation { get; set; }

        /// <summary>
        /// Last workstation id where the rework was captured
        /// </summary>
        [JsonProperty(Order = 31)]
        public Guid? LastWorkstationId { get; set; }
    }
}