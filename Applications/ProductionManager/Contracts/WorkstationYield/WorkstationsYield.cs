using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.WorkstationYield
{
    /// <summary>
    /// get all Workstation details (like an aggregated Orderprogress)
    /// </summary>
    public class WorkstationsYield : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// List of all Workstations for graphical representation of the yield. 
        /// </summary>
        /// <example>4711</example>
        [JsonProperty(Order = 1)]
        public IEnumerable<WorkstationYield> Yields { get; set; } = new List<WorkstationYield>();
    }
}