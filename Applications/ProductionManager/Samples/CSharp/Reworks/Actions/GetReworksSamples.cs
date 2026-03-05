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
            var response = await productionManager.GetCompletedReworks().ToListAsync();
            response.Trace();
            var reworkIds = response.Select(x => x.Id).ToList();
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

            // Example 3: Get reworks by identifier with pagination
            var reworksByIdentifier = await productionManager.GetReworks(
                daysBack: 14,
                identifier: "10800332",
                take: 50,
                skip: 0).ToListAsync();
            reworksByIdentifier.Trace(nameof(reworksByIdentifier));

            if (reworksLastWeek != null)
            {
                var reworkIds = reworksLastWeek.Select(x => x.Id).ToList();
                reworkIds.Trace(nameof(reworkIds));
            }
        }

        /// <summary>
        /// Gets rework history using date range and filters.
        /// </summary>
        public static async Task GetReworkHistoryAsync(IProductionManagerClient productionManager)
        {
            // Example 1: Get rework history from the last 14 days
            var historyLastTwoWeeks = await productionManager.GetReworkHistory(daysBack: 14).ToListAsync();
            historyLastTwoWeeks.Trace(nameof(historyLastTwoWeeks));

            // Example 2: Get rework history with specific date range
            var from = DateTime.UtcNow.AddMonths(-1);
            var to = DateTime.UtcNow;
            var historyByDateRange = await productionManager.GetReworkHistory(
                from: from,
                to: to,
                take: 100).ToListAsync();
            historyByDateRange.Trace(nameof(historyByDateRange));

            // Example 3: Get rework history by rework ID
            var historyByReworkId = await productionManager.GetReworkHistory(
                daysBack: 30,
                reworkId: "a880d29f-e158-424f-8ec3-196c579ce43e",
                take: 10).ToListAsync();
            historyByReworkId.Trace(nameof(historyByReworkId));

            if (historyLastTwoWeeks != null)
            {
                var reworkIds = historyLastTwoWeeks.Select(x => x.Id).ToList();
                reworkIds.Trace(nameof(reworkIds));
            }
        }
    }
}
