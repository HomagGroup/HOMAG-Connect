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
        /// Gets or sets additional custom properties configured in the application.
        /// JSON properties that are not mapped to typed members are captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "rejectionSource": "QualityCheck" }</example>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets attachments associated with the rejection.
        /// </summary>
        /// <example>[]</example>
        [JsonProperty(Order = 80)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Attachments))]
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Gets or sets the comment entered for the rejection.
        /// </summary>
        /// <example>Rejected due to visible surface damage.</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Comment))]
        [JsonProperty(Order = 70)]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the item was rejected.
        /// </summary>
        /// <example>2025-03-14T08:42:15+00:00</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectedAt))]
        [JsonProperty(Order = 10)]
        public DateTimeOffset? RejectedAt { get; set; }

        /// <summary>
        /// Gets or sets the user who rejected the item.
        /// </summary>
        /// <example>operator@example.com</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectedBy))]
        [JsonProperty(Order =20)]
        public string? RejectedBy { get; set; }
    }
}