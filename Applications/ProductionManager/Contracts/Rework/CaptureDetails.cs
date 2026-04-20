using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Represents the capture details recorded when a rework entry is created.
    /// </summary>
    /// <example>
    /// { "comment": "Panel edge damaged during handling.", "capturedBy": "operator@example.com", "lastWorkstation": "Saw-01", "lastWorkstationId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "attachments": [], "additionalProperties": { "incidentType": "Damage" } }
    /// </example>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CaptureDetails))]
    public class CaptureDetails
    {
        /// <summary>
        /// Gets or sets additional custom properties configured in the application.
        /// JSON properties that are not mapped to typed members are captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "incidentType": "Damage" }</example>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets attachments associated with the rework capture.
        /// </summary>
        /// <example>[]</example>
        [JsonProperty(Order = 80)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Attachments))]
        public Collection<AdditionalDataEntity>? Attachments { get; set; }

        /// <summary>
        /// Gets or sets the comment entered when the rework was captured.
        /// </summary>
        /// <example>Panel edge damaged during handling.</example>
        [JsonProperty(Order = 10)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Comment))]
        public string? Comment { get; set; }

        /// <summary>
        /// Gets or sets the user who captured the rework.
        /// </summary>
        /// <example>operator@example.com</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CapturedBy))]
        [JsonProperty(Order = 20)]
        public string? CapturedBy { get; set; }

        /// <summary>
        /// Gets or sets the last workstation where the rework was captured.
        /// </summary>
        /// <example>Saw-01</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(LastWorkstation))]
        [JsonProperty(Order = 30)]
        public string? LastWorkstation { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the last workstation where the rework was captured.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [JsonProperty(Order = 31)]
        public Guid? LastWorkstationId { get; set; }
    }
}