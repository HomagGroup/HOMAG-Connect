using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocolFlow
{
    /// <summary>
    /// get all Workstation details (like an aggregated Orderprogress)
    /// plus all edges to the next Workstations with the aggregated quantity on this edge
    /// </summary>
    public class ProductionProtocolFlowDetails : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// List of all Workstations for graphical representation of the flow. The order of the list is not relevant, because the flow is defined by the edges in the Workstation details
        /// </summary>
        /// <example>4711</example>
        [JsonProperty(Order = 1)]
        public IEnumerable<ProductionProtocolFlowNode> Workstations { get; set; } = new List<ProductionProtocolFlowNode>();
    }
}