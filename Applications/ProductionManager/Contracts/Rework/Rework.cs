using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rework class
    /// </summary>
    [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Rework))]
    public class Rework
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 90)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Rework captured at
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CapturedAt))]
        public DateTimeOffset? CapturedAt { get; set; }

        /// <summary>
        /// Rework category
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Category))]
        public ReworkCategory Category { get; set; }

        /// <summary>
        /// Creation details if available
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(CreationDetails))]
        public CreationDetails? CreationDetails { get; set; }

        /// <summary>
        /// Description of the rework
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Identifier of the rework
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Id))]
        public string? Id { get; set; }

        /// <summary>
        /// Material of the parts to rework
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Material))]
        public string? Material { get; set; }

        /// <summary>
        /// Name of the order
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(OrderName))]
        public string? OrderName { get; set; }

        /// <summary>
        /// Quantity of parts to rework
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(QuantityOfReworks))]
        public int QuantityOfReworks { get; set; }

        /// <summary>
        /// Reason for the rework
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(Reason))]
        public ReworkReason Reason { get; set; }

        /// <summary>
        /// Rejection details if available
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(RejectionDetails))]
        public RejectionDetails? RejectionDetails { get; set; } = null;

        /// <summary>
        /// Rework state
        /// </summary>
        [Display(ResourceType = typeof(ReworkPropertyDisplayNames), Name = nameof(State))]
        public ReworkState State { get; set; }
    }
}