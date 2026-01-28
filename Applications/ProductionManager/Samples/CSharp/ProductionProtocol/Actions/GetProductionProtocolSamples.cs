using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.ProductionProtocol;

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
            /* 
             * Replace with your workstation ID.
             * Get a list of all workstations in productionManager via the ProductionManagerClient 
             * Choose one workstation and use its ID here. Or you can get all protocols from all workstations as shown in the next sample.
            */

            var workstations = await productionManager.GetWorkstations();

            var protocolList = new List<ProcessedItem>();
            foreach (var workstation in workstations)
            {
                var protocolTask = productionManager.GetProductionProtocol(workstation.Id.ToString(), take:100, skip:0, daysBack:7);
                var response = await protocolTask ?? Array.Empty<ProcessedItem>();
                protocolList.AddRange(response);
            }

            protocolList.Trace();
        }

        /// <summary>
        /// Sample showing how to retrieve the list of all workstations.
        /// </summary>
        public static async Task GetWorkstations(IProductionManagerClient productionManager)
        {
            var workstations = await productionManager.GetWorkstations();

            workstations.Trace();
        }
    }
}
