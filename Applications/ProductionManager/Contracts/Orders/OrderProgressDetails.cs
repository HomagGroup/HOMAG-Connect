using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders
{
    /// <summary>
    /// Information of processed parts for 1 order and 1 workstation
    /// </summary>
    public class OrderProgressDetails
    {
        /// <summary>
        /// The number of the order
        /// </summary>
        [JsonProperty(Order = 1)]
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the group of the workstation
        /// </summary>
        [JsonProperty(Order = 2)]
        public WorkstationGroup Group { get; set; } = WorkstationGroup.None;

        /// <summary>
        /// Gets or sets the Workstation Id
        /// </summary>
        [JsonProperty(Order = 3)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets the quantity of parts of the order which have already been processed at the workstation.
        /// </summary>
        [JsonProperty(Order = 4)]
        public int AggregatedItemQuantity { get; set; }

    }
}