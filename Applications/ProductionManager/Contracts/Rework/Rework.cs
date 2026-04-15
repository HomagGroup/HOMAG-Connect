using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Represents a rework item including classification, quantities, status, and related capture or rejection details.
    /// </summary>
    /// <example>
    /// { "id": "PART-1001", "description": "Visible edge damage", "material": "P2_Gold_Craft_Oak_19.0", "orderName": "Kitchen Order", "quantityOfReworks": 2, "category": "Damage", "reason": "HandlingError", "state": "Open", "capturedAt": "2025-03-14T08:42:15+00:00", "customerName": "Muster GmbH", "length": 720.0, "width": 480.0, "reworkId": "RW-2025-0001" }
    /// </example>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Rework))]
    public class Rework : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets or sets additional custom properties configured in the application.
        /// JSON properties that are not mapped to typed members are captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "externalSource": "MES" }</example>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the rework was captured.
        /// </summary>
        /// <example>2025-03-14T08:42:15+00:00</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CapturedAt))]
        [JsonProperty(Order = 37)]
        public DateTimeOffset? CapturedAt { get; set; }

        /// <summary>
        /// Gets or sets the category used to classify the rework.
        /// </summary>
        /// <example>Damage</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Category))]
        [JsonProperty(Order = 20)]
        public ReworkCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the capture details recorded when the rework was created, if available.
        /// </summary>
        /// <example>{ "comment": "Captured during quality inspection." }</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CaptureDetails))]
        [JsonProperty(Order = 38)]
        public CaptureDetails? CaptureDetails { get; set; }

        /// <summary>
        /// Gets or sets the description of the rework.
        /// </summary>
        /// <example>Visible edge damage</example>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the part for which the rework was created.
        /// </summary>
        /// <example>PART-1001</example>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Id))]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the material of the parts to be reworked.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Material))]
        public string? Material { get; set; }

        /// <summary>
        /// Gets or sets the name of the related order.
        /// </summary>
        /// <example>Kitchen Order</example>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Gets or sets the number of parts that require rework.
        /// </summary>
        /// <example>2</example>
        [JsonProperty(Order = 5)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(QuantityOfReworks))]
        public int QuantityOfReworks { get; set; }

        /// <summary>
        /// Gets or sets the reason assigned to the rework.
        /// </summary>
        /// <example>HandlingError</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Reason))]
        [JsonProperty(Order = 21)]
        public ReworkReason Reason { get; set; }

        /// <summary>
        /// Gets or sets the rejection details, if the rework item was rejected.
        /// </summary>
        /// <example>{ "comment": "Rejected after final review." }</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectionDetails))]
        [JsonProperty(Order = 40)]
        public RejectionDetails? RejectionDetails { get; set; } = null;

        /// <summary>
        /// Gets or sets the current state of the rework item.
        /// </summary>
        /// <example>Open</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(State))]
        [JsonProperty(Order = 35)]
        public ReworkState State { get; set; }

        /// <summary>
        /// Gets or sets the customer name associated with the rework.
        /// </summary>
        /// <example>Muster GmbH</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CustomerName))]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the part length.
        /// </summary>
        /// <example>720.0</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Length))]
        public decimal? Length { get; set; }

        /// <summary>
        /// Gets or sets the part width.
        /// </summary>
        /// <example>480.0</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Width))]
        public decimal? Width { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the rework item.
        /// </summary>
        /// <example>RW-2025-0001</example>
        public string? ReworkId { get; set; }
    }
}