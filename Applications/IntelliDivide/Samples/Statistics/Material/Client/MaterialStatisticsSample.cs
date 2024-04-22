using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Samples.Helper;

namespace HomagConnect.IntelliDivide.Samples.Statistics.Material.Client
{
    /// <summary />
    public static class MaterialStatisticsSample
    {
        /// <summary />
        public static async Task RetrieveMaterialStatistics(Guid subscriptionId, string authorizationKey)
        {
            // Create new instance of the intelliDivide client:
            var client = new IntelliDivideClient(subscriptionId, authorizationKey);

            // Define the parameters:

            DateTime from = DateTime.Now.AddMonths(-3);
            DateTime to = DateTime.Now.AddDays(-1);
            int take = 1000;

            // Get the data
            var materialStatistics = await client.GetMaterialStatisticsAsync(from, to, take).ToListAsync();

            // Use the retrieved data
            var totalBoardsUsedInSquareMeter = materialStatistics.Sum(m => m.BoardsUsed);
            var totalOffcutGrowthInSquareMeter = materialStatistics.Sum(m => m.OffcutsGrowth);

            totalBoardsUsedInSquareMeter.Trace(nameof(totalBoardsUsedInSquareMeter));
            totalOffcutGrowthInSquareMeter.Trace(nameof(totalOffcutGrowthInSquareMeter));

            materialStatistics.Trace();
        }
    }
}