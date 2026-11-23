using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Represents the rejection details recorded for a rework item.
    /// </summary>
    /// <example>
    /// { "rejectedAt": "2025-03-14T08:42:15+00:00", "rejectedBy": "operator@example.com", "comment": "Rejected due to visible surface damage.", "attachments": [], "additionalProperties": { "rejectionSource": "QualityCheck" } }
    /// </example>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectionDetails))]
    public class RejectionDetails
    {
        /// <summary>
        /// Additional properties configured in productionManager that are not part of the standard contract. Optional.
        /// </summary>
        /// <example>{ "rejectionSource": "QualityCheck" }</example>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Attachments that document the rejection. Optional.
        /// </summary>
        /// <example>[]</example>
        [JsonProperty(Order = 80)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Attachments))]
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Comment entered for the rejection. Optional.
        /// </summary>
        /// <example>Rejected due to visible surface damage.</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Comment))]
        [JsonProperty(Order = 70)]
        public string? Comment { get; set; }

        /// <summary>
        /// Date and time the rework was rejected. Optional.
        /// </summary>
        /// <example>2025-03-14T08:42:15+00:00</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectedAt))]
        [JsonProperty(Order = 10)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset? RejectedAt { get; set; }

        /// <summary>
        /// User who rejected the rework. Optional.
        /// </summary>
        /// <example>operator@example.com</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectedBy))]
        [JsonProperty(Order =20)]
        public string? RejectedBy { get; set; }
    }
}
