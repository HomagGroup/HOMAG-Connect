using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Events
{
    /// <summary>
    /// Gets triggered when a ProductionItem has been completed on a Workstation.
    /// </summary>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(ProductionItemCompletedByParentEvent))]
    public class ProductionItemCompletedByParentEvent : ProductionItemCompletedEvent
    {
        /// <summary>
        /// Gets or sets the Identifier of the ProductionItem that was completed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 31)]
        public string ParentIdentifier { get; set; }
    }
}