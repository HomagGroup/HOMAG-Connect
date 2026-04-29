using System.Diagnostics;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Rework;

namespace HomagConnect.ProductionManager.Samples.Reworks.Actions
{
    /// <summary>
    /// Sample class which shows how to get reworks.
    /// </summary>
    public static class GetReworksSamples
    {
        /// <summary>
        /// Gets all completed reworks for a customer.
        /// </summary>
        public static async Task GetCompletedReworksAsync(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetCompletedReworks()!.ToListAsync();
            response.Trace();
            var reworkIds = response!.Select(x => x.Id).ToList();
            reworkIds.Trace(nameof(reworkIds));
        }

        /// <summary>
        /// Gets all requested reworks for a customer.
        /// </summary>
        public static async Task GetRequestedReworksAsync(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetRequestedReworks()!.ToListAsync();
            response.Trace();
            var reworkIds = response!.Select(x => x.Id).ToList();
            reworkIds.Trace(nameof(reworkIds));
        }

        /// <summary>
        /// Gets all approved reworks for a customer.
        /// </summary>
        public static async Task GetApprovedReworksAsync(IProductionManagerClient productionManager)
        {
            var response = await productionManager.GetApprovedReworks()!.ToListAsync();
            response.Trace();
            var reworkIds = response!.Select(x => x.Id).ToList();
            reworkIds.Trace(nameof(reworkIds));
        }

        /// <summary>
        /// Gets reworks using date range and filters.
        /// </summary>
        public static async Task GetReworksAsync(IProductionManagerClient productionManager)
        {
            // Example 1: Get reworks from the last 7 days
            var reworksLastWeek = await productionManager.GetReworks(daysBack: 7).ToListAsync();
            reworksLastWeek.Trace(nameof(reworksLastWeek));

            // Example 2: Get reworks with specific date range and state filter
            var from = DateTime.UtcNow.AddDays(-30);
            var to = DateTime.UtcNow;
            var reworksFiltered = await productionManager.GetReworks(
                from: from,
                to: to,
                state: ReworkState.Transferred,
                take: 100).ToListAsync();
            reworksFiltered.Trace(nameof(reworksFiltered));

            if (reworksLastWeek != null)
            {
                var reworkIds = reworksLastWeek.Select(x => x.Id).ToList();
                reworkIds.Trace(nameof(reworkIds));
            }
        }
    }
}
