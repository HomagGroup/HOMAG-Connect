using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems
{
    /// <summary>
    /// Retrieves the ProductionItemFeedback 
    /// </summary>
    public class ProductionItemFeedback : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets the identifier, which is either the barcode (if available) or the id.
        /// </summary>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Identifier))]
        public string? Identifier { get; set; }

        /// <summary>
        /// Gets or sets the WorkstationId - can be null if the feedback is not related to a specific workstation, e.g. if it is provided by a system without workstation association.
        /// </summary>
        [JsonProperty(Order = 2)]
        [Display(AutoGenerateField = false)]
        public Guid? WorkstationId { get; set; }

        /// <summary>
        /// Gets or sets the Workstation name
        /// </summary>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(Workstation))]
        public string? Workstation { get; set; }

        /// <summary>
        /// Gets or sets the Workstation name
        /// </summary>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(WorkstationType))]
        public WorkstationType WorkstationType { get; set; }

        /// <summary>
        /// Gets or sets the source of the feedback information, e.g. the name of the machine, person or system which provided the feedback.
        /// </summary>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(From))]
        public string? From { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the feedback was provided.
        /// </summary>
        [JsonProperty(Order = 5)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Timestamp))]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gtes or sets the quantity related to the feedback, e.g. the quantity produced, processed or affected.
        /// </summary>
        [JsonProperty(Order = 6)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(Quantity))]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the action related to the feedback
        /// </summary>
        [JsonProperty(Order = 7)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(Action))]
        public ProductionItemFeedbackAction Action { get; set; } = ProductionItemFeedbackAction.Unknown;
    }
}
