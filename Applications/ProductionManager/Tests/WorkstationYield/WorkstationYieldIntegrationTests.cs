using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.WorkstationYield
{
    /// <summary>
    /// Integration tests for Workstations Yield functionality.
    /// </summary>
    [TestClass]
    [IntegrationTest("ProductionManager.WorkstationYield")]
    public class WorkstationYieldIntegrationTests : ProductionManagerTestBase
    {
        /// <summary>
        /// Gets or sets the test context for this test run.
        /// </summary>
        public required TestContext TestContext { get; set; }

        /// <summary>
        /// Tests getting production flow for the last 7 days.
        /// </summary>
        [TestMethod]
        [TemporaryDisabledOnServer(2026, 08, 01, "DF-Insights")]
        public async Task GetWorkstationsYield_Last7Days_ReturnsData()
        {
            // Arrange
            var productionManagerClient = GetProductionManagerClient();
            var from = DateTime.UtcNow.AddDays(-7);
            var to = DateTime.UtcNow;

            // Act
            var result = await productionManagerClient.GetWorkstationsYield(from, to);

            // Assert
            result.ShouldNotBeNull();
            result.Trace("Workstations Yield - Last 7 Days");

            TestContext?.AddResultFile(result.TraceToFile(nameof(GetWorkstationsYield_Last7Days_ReturnsData)).FullName);
        }

    }
}
