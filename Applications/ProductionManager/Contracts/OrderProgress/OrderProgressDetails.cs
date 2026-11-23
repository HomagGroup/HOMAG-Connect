using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.OrderProgress
{
    /// <summary>
    /// Represents processed part totals for one order at one workstation.
    /// </summary>
    /// <example>
    /// { "orderNumber": "4711", "group": "Saw", "id": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "aggregatedItemQuantity": 84 }
    /// </example>
    public class OrderProgressDetails: ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Order number of the order the progress belongs to. Optional.
        /// </summary>
        /// <example>4711</example>
        [JsonProperty(Order = 1)]
        [Display(ResourceType = typeof(Resources), Name = nameof(OrderNumber))]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Workstation group of the workstation at which the parts were processed. Defaults to None.
        /// </summary>
        /// <example>Saw</example>
        [JsonProperty(Order = 2)]
        [Display(ResourceType = typeof(Resources), Name = nameof(WorkstationGroup))]
        public WorkstationGroup Group { get; set; } = WorkstationGroup.None;

        /// <summary>
        /// Unique identifier of the workstation.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [JsonProperty(Order = 3)]
        public Guid Id { get; set; }

        /// <summary>
        /// Number of parts of the order that have already been processed at the workstation.
        /// </summary>
        /// <example>84</example>
        [JsonProperty(Order = 4)]
        [Display(ResourceType = typeof(Resources), Name = nameof(AggregatedItemQuantity))]
        public int AggregatedItemQuantity { get; set; }

    }
}
