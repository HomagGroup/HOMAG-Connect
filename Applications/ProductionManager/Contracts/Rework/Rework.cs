using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Represents a rework request for parts that have to be produced again, including category, reason, quantity, state, and the capture or rejection details.
    /// <see href="https://docs.homag.cloud/docs/en/productionmanager-tutorial-rework">Learn more about rework</see>.
    /// </summary>
    /// <example>
    /// { "id": "PART-1001", "description": "Visible edge damage", "orderName": "Kitchen Order", "material": "P2_Gold_Craft_Oak_19.0", "quantityOfReworks": 2, "category": "Edgebanding", "reason": "PartDamaged", "state": "Pending", "capturedAt": "2025-03-14T08:42:15+00:00" }
    /// </example>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Rework))]
    public class Rework : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Additional properties configured in productionManager that are not part of the standard contract. Optional.
        /// </summary>
        /// <example>{ "externalSource": "MES" }</example>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Date and time the rework was captured. Optional.
        /// </summary>
        /// <example>2025-03-14T08:42:15+00:00</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CapturedAt))]
        [JsonProperty(Order = 37)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset? CapturedAt { get; set; }

        /// <summary>
        /// Category that classifies the rework by the production step it relates to.
        /// </summary>
        /// <example>Edgebanding</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Category))]
        [JsonProperty(Order = 20)]
        public ReworkCategory Category { get; set; }

        /// <summary>
        /// Details recorded when the rework was captured. Optional.
        /// </summary>
        /// <example>{ "comment": "Captured during quality inspection." }</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CaptureDetails))]
        [JsonProperty(Order = 38)]
        public CaptureDetails? CaptureDetails { get; set; }

        /// <summary>
        /// Description of the rework. Optional.
        /// </summary>
        /// <example>Visible edge damage</example>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Identifier of the part the rework was created for. Optional.
        /// </summary>
        /// <example>PART-1001</example>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Id))]
        public string? Id { get; set; }

        /// <summary>
        /// Material code of the parts to be reworked. Optional.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Material))]
        public string? Material { get; set; }

        /// <summary>
        /// Name of the order the rework belongs to. Optional.
        /// </summary>
        /// <example>Kitchen Order</example>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Number of parts that require rework.
        /// </summary>
        /// <example>2</example>
        [JsonProperty(Order = 5)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(QuantityOfReworks))]
        [Range(0, int.MaxValue)]
        public int QuantityOfReworks { get; set; }

        /// <summary>
        /// Reason why the rework is needed.
        /// </summary>
        /// <example>PartDamaged</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Reason))]
        [JsonProperty(Order = 21)]
        public ReworkReason Reason { get; set; }

        /// <summary>
        /// Details of the rejection. Only set when the rework was rejected.
        /// </summary>
        /// <example>{ "comment": "Rejected after final review." }</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectionDetails))]
        [JsonProperty(Order = 40)]
        public RejectionDetails? RejectionDetails { get; set; } = null;

        /// <summary>
        /// Current state of the rework in its approval workflow.
        /// </summary>
        /// <example>Pending</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(State))]
        [JsonProperty(Order = 35)]
        public ReworkState State { get; set; }

        /// <summary>
        /// Name of the customer the rework relates to. Optional.
        /// </summary>
        /// <example>Muster GmbH</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CustomerName))]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Length of the part. Optional.
        /// </summary>
        /// <example>720.0</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Length))]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public decimal? Length { get; set; }

        /// <summary>
        /// Width of the part. Optional.
        /// </summary>
        /// <example>480.0</example>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Width))]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public decimal? Width { get; set; }

        /// <summary>
        /// Unique identifier of the rework. Optional.
        /// </summary>
        /// <example>RW-2025-0001</example>
        public string? ReworkId { get; set; }
    }
}
