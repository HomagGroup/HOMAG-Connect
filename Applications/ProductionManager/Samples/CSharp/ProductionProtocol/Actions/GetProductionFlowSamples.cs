using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.ProductionProtocolFlow;

namespace HomagConnect.ProductionManager.Samples.ProductionProtocol.Actions
{
    /// <summary>
    /// Sample class which shows how to get the Production Flow for a specified duration.
    /// </summary>
    public static class GetProductionFlowSamples
    {
        /// <summary>
        /// Gets the production flow for the last 7 days.
        /// </summary>
        public static async Task GetProductionFlowLast7Days(IProductionManagerClient productionManager)
        {
            // Get production flow for the last 7 days
            var from = DateTime.UtcNow.AddDays(-7);
            var to = DateTime.UtcNow;

            var productionFlow = await productionManager.GetProductionFlow(from, to);

            if (productionFlow?.Workstations != null)
            {
                var workstationCount = productionFlow.Workstations.Count();
                var totalItems = productionFlow.Workstations
                    .SelectMany(w => w.ItemTypeSummary.Values)
                    .Sum();

                var summary = new
                {
                    Period = $"{from:yyyy-MM-dd} to {to:yyyy-MM-dd}",
                    WorkstationCount = workstationCount,
                    TotalItems = totalItems
                };

                summary.Trace("Production Flow Summary");
            }

            productionFlow.Trace(nameof(productionFlow));
        }
    }
}
