using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Attributes;
using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Events
{
    /// <summary>
    /// Event that occurs when a production item has been completed at a workstation.
    /// </summary>
    /// <example>
    /// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionAssist.ProductionItemCompletedByParentEvent", "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "identifier": "PART-1001", "quantity": 1, "parentIdentifier": "ASM-100" }
    /// </example>
    [AppEvent(nameof(ProductionAssist) + "." + nameof(ProductionItemCompletedByParentEvent))]
    public class ProductionItemCompletedByParentEvent : ProductionItemCompletedEvent
    {
        /// <summary>
        /// Identifier of the parent production item whose completion caused this item to be completed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 31)]
        public string ParentIdentifier { get; set; }
    }
}
