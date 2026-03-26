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

            Console.WriteLine($@"Current month usage ({DateTime.Now:yyyy-MM}):");

            if (currentUsage != null)
            {
                foreach (var detail in currentUsage)
                {
                    Console.WriteLine($@"{detail.Timestamp:yyyy-MM-dd HH:mm} - {detail.OrderNumber} ({detail.OrderName})");
                    Console.WriteLine($@"  Customer: {detail.CustomerName}");
                    Console.WriteLine($@"  Parts: {detail.QuantityOfParts}, Action: {detail.Action}");
                }

                // Calculate current month statistics
                var totalParts = currentUsage.Sum(d => d.QuantityOfParts);
                var releaseCount = currentUsage.Count(d => d.Action == OrderAction.Release);
                var resetCount = currentUsage.Count(d => d.Action == OrderAction.ResetRelease);

                Console.WriteLine($@"
Current month totals:");
                Console.WriteLine($@"Total parts: {totalParts}");
                Console.WriteLine($@"Releases: {releaseCount}, Resets: {resetCount}");
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

            if (usageDetails != null)
                foreach (var detail in usageDetails)
                {
                    Console.WriteLine($@"{detail.Timestamp:yyyy-MM-dd HH:mm} - {detail.OrderNumber} ({detail.OrderName})");
                    Console.WriteLine($@"  Customer: {detail.CustomerName} ({detail.CustomerNumber})");
                    Console.WriteLine($@"  Parts: {detail.QuantityOfParts}, Action: {detail.Action}, By: {detail.ChangedBy}");
                }

            // Example 2: Get usage details for the last 3 months and calculate totals
            var recentDetails = await productionManager.GetUsageDetails(monthsAgo: 3).ToListAsync();
            if (recentDetails != null)
            {
                var totalParts = recentDetails.Sum(d => d.QuantityOfParts);
                var releaseCount = recentDetails.Count(d => d.Action == OrderAction.Release);
                var resetCount = recentDetails.Count(d => d.Action == OrderAction.ResetRelease);

                Console.WriteLine($@"Total parts: {totalParts}");
                Console.WriteLine($@"Releases: {releaseCount}, Resets: {resetCount}");
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

            if (janUsage != null)
                foreach (var detail in janUsage)
                {
                    Console.WriteLine($@"{detail.Timestamp:yyyy-MM-dd} - {detail.OrderNumber}: {detail.QuantityOfParts} parts");
                }

            // Example 2: Get usage for multiple periods
            var periods = new[] { "2025-01", "2024-12", "2024-11" };

            foreach (var period in periods)
            {
                var usage = await productionManager.GetUsageDetailsForPeriod(period).ToListAsync();
                if (usage != null)
                {
                    var totalParts = usage.Sum(d => d.QuantityOfParts);
                    Console.WriteLine($@"{period}: {totalParts} total parts released");
                }
            }
        }

        /// <summary>
        /// Gets usage overview for the specified number of months.
        /// </summary>
        public static async Task GetUsageOverviewAsync(IProductionManagerClient productionManager)
        {
            // Example 1: Get usage overview for the last 12 months
            var usageOverview = await productionManager.GetUsageOverview(monthsAgo: 12).ToListAsync();
            usageOverview.Trace(nameof(usageOverview));

            if (usageOverview != null)
                foreach (var overview in usageOverview)
                {
                    Console.WriteLine($@"Period: {overview.Period:yyyy-MM}");
                    Console.WriteLine($@"Parts released: {overview.QuantityOfReleasedParts} / {overview.QuantityOfPartsCoveredByTheLicenses}");

                    foreach (var license in overview.Licenses)
                    {
                        Console.WriteLine($@"  License: {license.ApplicationLicenseFullName} (Count: {license.LicenseCount})");
                    }
                }

            // Example 2: Get usage overview for the last 6 months
            var recentOverview = await productionManager.GetUsageOverview(monthsAgo: 6).ToListAsync();

            if (recentOverview != null)
                foreach (var overview in recentOverview)
                {
                    var usagePercentage = overview.QuantityOfPartsCoveredByTheLicenses > 0
                        ? overview.QuantityOfReleasedParts * 100.0 / overview.QuantityOfPartsCoveredByTheLicenses
                        : 0;
                    Console.WriteLine($@"{overview.Period:yyyy-MM}: {usagePercentage:F1}% used");
                }
        }
    }
}