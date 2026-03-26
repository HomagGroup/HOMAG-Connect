using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts;
using HomagConnect.ProductionManager.Contracts.Orders;

namespace HomagConnect.ProductionManager.Samples.Orders.Usage
{
    /// <summary>
    /// Sample class which shows how to get order usage statistics.
    /// </summary>
    public static class GetOrderUsageSamples
    {
        /// <summary>
        /// Gets current month's usage details.
        /// </summary>
        public static async Task GetCurrentUsageAsync(IProductionManagerClient productionManager)
        {
            // Get current month's usage details
            var currentUsage = await productionManager.GetCurrentUsage().ToListAsync();
            currentUsage.Trace(nameof(currentUsage));

            if (currentUsage != null)
            {
                // Calculate current month statistics
                var totalParts = currentUsage.Sum(d => d.QuantityOfParts);
                var releaseCount = currentUsage.Count(d => d.Action == OrderAction.Release);
                var resetCount = currentUsage.Count(d => d.Action == OrderAction.ResetRelease);

                var statistics = new
                {
                    Period = $"{DateTime.Now:yyyy-MM}",
                    TotalParts = totalParts,
                    ReleaseCount = releaseCount,
                    ResetCount = resetCount
                };

                statistics.Trace("Current month statistics");
            }
        }

        /// <summary>
        /// Gets usage details for the specified number of months.
        /// </summary>
        public static async Task GetUsageDetailsAsync(IProductionManagerClient productionManager)
        {
            // Example 1: Get usage details for the last 12 months
            var usageDetails = await productionManager.GetUsageDetails(monthsAgo: 12).ToListAsync();
            usageDetails.Trace(nameof(usageDetails));

            // Example 2: Get usage details for the last 3 months and calculate totals
            var recentDetails = await productionManager.GetUsageDetails(monthsAgo: 3).ToListAsync();
            if (recentDetails != null)
            {
                var totalParts = recentDetails.Sum(d => d.QuantityOfParts);
                var releaseCount = recentDetails.Count(d => d.Action == OrderAction.Release);
                var resetCount = recentDetails.Count(d => d.Action == OrderAction.ResetRelease);

                var statistics = new
                {
                    TotalParts = totalParts,
                    ReleaseCount = releaseCount,
                    ResetCount = resetCount
                };

                statistics.Trace("Last 3 months statistics");
            }
        }

        /// <summary>
        /// Gets usage details for a specific period.
        /// </summary>
        public static async Task GetUsageDetailsForPeriodAsync(IProductionManagerClient productionManager)
        {
            // Example 1: Get usage details for January 2025
            var janUsage = await productionManager.GetUsageDetailsForPeriod("2025-01").ToListAsync();
            janUsage.Trace(nameof(janUsage));

            // Example 2: Get usage for multiple periods
            var periods = new[] { "2025-01", "2024-12", "2024-11" };

            var periodStatistics = new List<object>();

            foreach (var period in periods)
            {
                var usage = await productionManager.GetUsageDetailsForPeriod(period).ToListAsync();
                if (usage != null)
                {
                    var totalParts = usage.Sum(d => d.QuantityOfParts);
                    periodStatistics.Add(new { Period = period, TotalParts = totalParts });
                }
            }

            periodStatistics.Trace("Period statistics");
        }

        /// <summary>
        /// Gets usage overview for the specified number of months.
        /// </summary>
        public static async Task GetUsageOverviewAsync(IProductionManagerClient productionManager)
        {
            // Example 1: Get usage overview for the last 12 months
            var usageOverview = await productionManager.GetUsageOverview(monthsAgo: 12).ToListAsync();
            usageOverview.Trace(nameof(usageOverview));

            // Example 2: Get usage overview for the last 6 months
            var recentOverview = await productionManager.GetUsageOverview(monthsAgo: 6).ToListAsync();

            if (recentOverview != null)
            {
                var usagePercentages = recentOverview.Select(overview => new
                {
                    Period = $"{overview.Period:yyyy-MM}",
                    UsagePercentage = overview.QuantityOfPartsCoveredByTheLicenses > 0
                        ? overview.QuantityOfReleasedParts * 100.0 / overview.QuantityOfPartsCoveredByTheLicenses
                        : 0
                }).ToList();

                usagePercentages.Trace("Last 6 months usage percentages");
            }
        }
    }
}