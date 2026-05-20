using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.ProductionProtocolFlow
{
    /// <summary>
    /// Integration tests for Production Protocol Flow functionality.
    /// </summary>
    [TestClass]
    [IntegrationTest("ProductionManager.ProductionProtocolFlow")]
    public class ProductionProtocolFlowIntegrationTests : ProductionManagerTestBase
    {
        /// <summary>
        /// Gets or sets the test context for this test run.
        /// </summary>
        public required TestContext TestContext { get; set; }

        /// <summary>
        /// Tests getting production flow for the last 7 days.
        /// </summary>
        [TestMethod]
        [TemporaryDisabledOnServer(2026, 07, 01, "DF-Insights")]
        public async Task GetProductionFlow_Last7Days_ReturnsData()
        {
            // Arrange
            var productionManagerClient = GetProductionManagerClient();
            var from = DateTime.UtcNow.AddDays(-7);
            var to = DateTime.UtcNow;

            // Act
            var result = await productionManagerClient.GetProductionFlow(from, to);

            // Assert
            result.ShouldNotBeNull();
            result.Trace("Production Flow - Last 7 Days");

            TestContext?.AddResultFile(result.TraceToFile(nameof(GetProductionFlow_Last7Days_ReturnsData)).FullName);
        }

    }
}
