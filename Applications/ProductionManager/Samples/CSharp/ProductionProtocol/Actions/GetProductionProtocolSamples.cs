using System.Diagnostics;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.ProductionProtocol.Actions
{
    /// <summary>
    /// Sample class which shows how to get the Production Protocol for one workstation.
    /// </summary>
    public static class GetProductionProtocolSamples
    {
        /// <summary>
        /// Gets all productionprotocol for a workstation.
        /// </summary>
        public static async Task GetProductionProtocol(IProductionManagerClient productionManager)
        {
            var workstationId = "8892b810-ac7a-468f-9153-c1a4d6536463"; // Replace with actual workstation ID. It must be a GUID string.
            var response = await productionManager.GetProductionProtocol(workstationId).ToListAsync();
            response.Trace();
            var protocol = response.Select(x => x.Timestamp).ToList();
            protocol.Trace(nameof(protocol));
        }
    }
}
