using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rejection details
    /// </summary>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectionDetails))]
    public class RejectionDetails
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Attachments
        /// </summary>
        [JsonProperty(Order = 80)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Attachments))]
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Rejection comment
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Comment))]
        [JsonProperty(Order = 70)]
        public string? Comment { get; set; }

        /// <summary>
        /// Date and time when the item was rejected
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectedAt))]
        [JsonProperty(Order = 10)]
        public DateTimeOffset? RejectedAt { get; set; }

        /// <summary>
        /// Rejected by user
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectedBy))]
        [JsonProperty(Order =20)]
        public string? RejectedBy { get; set; }

        /// <summary />
        [Obsolete("Use RejectedAt instead.", true)]
        public DateTimeOffset? RejectedOn
        {
            get
            {
               return null;
            }
            set
            {
                RejectedAt = value;
            }
        }
    }
}