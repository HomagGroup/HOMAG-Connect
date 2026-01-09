using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Rework;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.Rework
{
    [TestClass]
    [IntegrationTest("ProductionManager.Rework")]
    public class ReworkTests : ProductionManagerTestBase
    {
        /// <summary>
        /// Should retrieve completed reworks without throwing and trace the results.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetCompletedRework_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var completedReworks = await productionManager.GetCompletedReworks().ToListAsync();

            completedReworks.ShouldNotBeNull();
            completedReworks.Trace();
        }

        /// <summary>
        /// Retrieves reworks in a date range filtered by states and verifies all results are within range.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetReworksInRange_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var from = DateTimeOffset.UtcNow.AddDays(-7);
            var to = DateTimeOffset.UtcNow.AddDays(-2);

            var reworks = await productionManager.GetReworks([ReworkState.Transferred, ReworkState.Rejected], from, to).ToListAsync();

            reworks.ShouldNotBeNull();
            
            reworks.Count(r => r.CapturedAt > to).ShouldBe(0);
            reworks.Count(r => r.CapturedAt < from).ShouldBe(0);

            reworks.Trace();
        }
    }
}
