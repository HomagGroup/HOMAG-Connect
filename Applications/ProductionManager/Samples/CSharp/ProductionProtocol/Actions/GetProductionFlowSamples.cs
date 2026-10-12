using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.CSharp.ProductionProtocol.Actions
{
    /// <summary>
    /// Sample class which shows how to get the workstations yield   for a specified duration.
    /// </summary>
    public static class GetWorkstationsYieldSamples
    {
        /// <summary>
        /// Gets the workstations yield for the last 7 days.
        /// </summary>
        public static async Task GetWorkstationsYieldLast7Days(IProductionManagerClient productionManager)
        {
            // Get workstations yield for the last 7 days
            var from = DateTime.UtcNow.AddDays(-7);
            var to = DateTime.UtcNow;

            var workstationsYield = await productionManager.GetWorkstationsYield(from, to);

            if (workstationsYield != null)
            {
                var workstationCount = workstationsYield.Count();
                var totalItems = workstationsYield  
                    .SelectMany(w => w.Yields.Select(kv => kv.Value))
                    .Sum();

                var summary = new
                {
                    Period = $"{from:yyyy-MM-dd} to {to:yyyy-MM-dd}",
                    WorkstationCount = workstationCount,
                    TotalItems = totalItems
                };

                summary.Trace("Workstations Yield Summary");
            }

            workstationsYield.Trace(nameof(workstationsYield));
        }
    }
}
