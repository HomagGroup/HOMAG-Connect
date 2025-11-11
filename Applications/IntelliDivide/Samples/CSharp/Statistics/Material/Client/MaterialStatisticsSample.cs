using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Client;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var from = DateTime.Now.AddMonths(-3);
            var to = DateTime.Now.AddDays(-1);
            const int take = 1000;

            // Get the data
            var materialStatistics = await client.GetMaterialStatistics(from, to, take).ToListAsync();
            if (materialStatistics == null || materialStatistics.Count <= 0)
            {
                Assert.Fail("No material statistics could be found.");
            }

            // Use the retrieved data
            var totalBoardsUsedInSquareMeter = materialStatistics.Sum(m => m.BoardsUsedArea);
            var totalOffcutGrowthInSquareMeter = materialStatistics.Sum(m => m.OffcutsGrowthArea);

            totalBoardsUsedInSquareMeter.Trace(nameof(totalBoardsUsedInSquareMeter));
            totalOffcutGrowthInSquareMeter.Trace(nameof(totalOffcutGrowthInSquareMeter));

            materialStatistics.Trace();
        }
    }
}