using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems
{
    /// <summary>
    /// Feedback reported for a production item, for example that it was cut, labeled, or placed in a sorting shelf.
    /// </summary>
    /// <example>
    /// { "identifier": "4711000001", "workstationId": "5b1c2d3e-4f50-4a61-8b72-9c0d1e2f3a4b", "workstation": "Saw 1",
    /// "workstationType": "Cutting", "from": "Saw 1", "timestamp": "2025-04-03T07:15:00+00:00", "quantity": 1, "action": "Completed" }
    /// </example>
    public class ProductionItemFeedback : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Identifier of the production item the feedback refers to: the barcode if one exists, otherwise the item's id.
        /// </summary>
        /// <example>4711000001</example>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Identifier))]
        public string? Identifier { get; set; }

        /// <summary>
        /// Unique identifier of the workstation that reported the feedback. Empty when the feedback is not related to a workstation,
        /// for example when it comes from a system without workstation association.
        /// </summary>
        /// <example>5b1c2d3e-4f50-4a61-8b72-9c0d1e2f3a4b</example>
        [JsonProperty(Order = 2)]
        [Display(AutoGenerateField = false)]
        public Guid? WorkstationId { get; set; }

        /// <summary>
        /// Name of the workstation that reported the feedback. Optional.
        /// </summary>
        /// <example>Saw 1</example>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(Workstation))]
        public string? Workstation { get; set; }

        /// <summary>
        /// Type of the workstation that reported the feedback.
        /// </summary>
        /// <example>Cutting</example>
        [JsonProperty(Order = 3)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(WorkstationType))]
        public WorkstationType WorkstationType { get; set; }

        /// <summary>
        /// Name of the machine, person, or system that provided the feedback. Optional.
        /// </summary>
        /// <example>Saw 1</example>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(From))]
        public string? From { get; set; }

        /// <summary>
        /// Date and time the feedback was provided.
        /// </summary>
        /// <example>2025-04-03T07:15:00+00:00</example>
        [JsonProperty(Order = 5)]
        [Display(ResourceType = typeof(Resources), Name = nameof(Timestamp))]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Number of pieces the feedback applies to, for example the number produced, processed, or affected.
        /// </summary>
        /// <example>1</example>
        [JsonProperty(Order = 6)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(Quantity))]
        public int Quantity { get; set; }

        /// <summary>
        /// Action the feedback reports. Defaults to <see cref="ProductionItemFeedbackAction.Unknown" />.
        /// </summary>
        /// <example>Completed</example>
        [JsonProperty(Order = 7)]
        [Display(ResourceType = typeof(ProductionItemFeedbackDisplayNames), Name = nameof(Action))]
        public ProductionItemFeedbackAction Action { get; set; } = ProductionItemFeedbackAction.Unknown;
    }
}
