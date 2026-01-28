using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rework class
    /// </summary>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Rework))]
    public class Rework : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Rework captured at
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CapturedAt))]
        [JsonProperty(Order = 37)]
        public DateTimeOffset? CapturedAt { get; set; }

        /// <summary>
        /// Rework category
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Category))]
        [JsonProperty(Order = 20)]
        public ReworkCategory Category { get; set; }

        /// <summary>
        /// Creation details if available
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CaptureDetails))]
        [JsonProperty(Order = 38)]
        public CaptureDetails? CaptureDetails { get; set; }

        /// <summary>
        /// Description of the rework
        /// </summary>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Identifier of the rework
        /// </summary>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Id))]
        public string? Id { get; set; }

        /// <summary>
        /// Material of the parts to rework
        /// </summary>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Material))]
        public string? Material { get; set; }

        /// <summary>
        /// Name of the order
        /// </summary>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Quantity of parts to rework
        /// </summary>
         [JsonProperty(Order = 5)]
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(QuantityOfReworks))]
        public int QuantityOfReworks { get; set; }

        /// <summary>
        /// Reason for the rework
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Reason))]
        [JsonProperty(Order = 21)]
        public ReworkReason Reason { get; set; }

        /// <summary>
        /// Rejection details if available
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectionDetails))]
        [JsonProperty(Order = 40)]
        public RejectionDetails? RejectionDetails { get; set; } = null;

        /// <summary>
        /// Rework state
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(State))]
        [JsonProperty(Order = 35)]
        public ReworkState State { get; set; }

        /// <summary>
        /// Customer name
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CustomerName))]
        public string? CustomerName { get; set; }

        /// <summary>
        /// Lenght
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Length))]
        public string? Length { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Width))]
        public string? Width { get; set; }

        /// <summary>
        /// Rework id
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Width))]
        public string? ReworkId { get; set; }
    }
}