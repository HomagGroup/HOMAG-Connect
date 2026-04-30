using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Rework;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.Rework
{
    [TestClass]
    [IntegrationTest("ProductionManager.Rework")]
    [TemporaryDisabledOnServer(2026,06,15, "DF-Production")]
    public class ReworkTests : ProductionManagerTestBase
    {
        /// <summary>
        /// Should retrieve requested reworks without throwing and trace the results.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetRequestedReworks_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var completedReworks = await productionManager.GetRequestedReworks()!.ToListAsync();

            completedReworks.ShouldNotBeNull();
            completedReworks.Trace();
        }

        /// <summary>
        /// Should retrieve approved reworks without throwing and trace the results.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetApprovedReworks_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var completedReworks = await productionManager.GetApprovedReworks()!.ToListAsync();

            completedReworks.ShouldNotBeNull();
            completedReworks.Trace();
        }

        /// <summary>
        /// Should retrieve completed reworks without throwing and trace the results.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetCompletedReworks_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var completedReworks = await productionManager.GetCompletedReworks()!.ToListAsync();

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

            var reworks = await productionManager.GetReworks([ReworkState.Transferred, ReworkState.Rejected], from, to)!.ToListAsync();

            reworks.ShouldNotBeNull();

            reworks.Count(r => r.CapturedAt > to).ShouldBe(0);
            reworks.Count(r => r.CapturedAt < from).ShouldBe(0);

            reworks.Trace();
        }

        /// <summary>
        /// Should retrieve reworks using date range without throwing.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetReworks_WithDateRange_NoException()
        {
            var productionManager = GetProductionManagerClient();
            var from = DateTime.UtcNow.AddDays(-30);
            var to = DateTime.UtcNow;

            var reworks = await productionManager.GetReworks(from: from, to: to).ToListAsync();

            reworks.ShouldNotBeNull();
            reworks.Trace();
        }

        /// <summary>
        /// Should retrieve reworks using daysBack parameter without throwing.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetReworks_WithDaysBack_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var reworks = await productionManager.GetReworks(daysBack: 30).ToListAsync();

            reworks.ShouldNotBeNull();
            reworks.Trace();
        }

        /// <summary>
        /// Should retrieve reworks with state filter without throwing.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetReworks_WithStateFilter_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var reworks = await productionManager.GetReworks(
                daysBack: 30, 
                state: ReworkState.Transferred).ToListAsync();

            reworks.ShouldNotBeNull();
            reworks.Trace();
        }

        /// <summary>
        /// Should retrieve reworks with pagination without throwing.
        /// </summary>
        [TestMethod]
        public async Task Rework_GetReworks_WithPagination_NoException()
        {
            var productionManager = GetProductionManagerClient();

            var reworks = await productionManager.GetReworks(
                daysBack: 30,
                take: 10,
                skip: 0).ToListAsync();

            reworks.ShouldNotBeNull();
            reworks.Count.ShouldBeLessThanOrEqualTo(10);
            reworks.Trace();
        }      

    }
}
