using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using Newtonsoft.Json;
using Shouldly;
using System.Globalization;

namespace HomagConnect.ProductionManager.Tests.Orders.Usage
{
    /// <summary>
    /// Integration tests for usage statistics endpoints
    /// </summary>
    [TestClass]
    [IntegrationTest("ProductionManager.Orders.Usage")]
    public class UsageIntegrationTests : ProductionManagerTestBase
    {
        /// <summary>
        /// Tests retrieving usage overview and traces the result
        /// </summary>
        [TestMethod]
        public async Task UsageOverview_Trace()
        {
            var productionManagerClient = GetProductionManagerClient();
            var usageOverview = await productionManagerClient.GetUsageOverview(monthsAgo: 12).ToListAsync();

            usageOverview.ShouldNotBeNull();
            usageOverview.Trace();

            TestContext?.AddResultFile(usageOverview.TraceToFile(nameof(UsageOverview_Trace)).FullName);
        }

        /// <summary>
        /// Tests retrieving usage overview with localized output
        /// </summary>
        [TestMethod]
        public async Task UsageOverview_TraceLocalized()
        {
            var productionManagerClient = GetProductionManagerClient();
            var usageOverview = await productionManagerClient.GetUsageOverview(monthsAgo: 12).ToListAsync();

            usageOverview.ShouldNotBeNull();

            var culture = CultureInfo.GetCultureInfo("de-DE");
            var serializedObjectLocalized = usageOverview.SerializeLocalized(culture);
            var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

            dynamic.ShouldNotBeNull();

            TestContext?.AddResultFile(dynamic.TraceToFile(nameof(UsageOverview_TraceLocalized)).FullName);
        }

        /// <summary>
        /// Tests retrieving usage details and traces the result
        /// </summary>
        [TestMethod]
        public async Task UsageDetails_Trace()
        {
            var productionManagerClient = GetProductionManagerClient();
            var usageDetails = await productionManagerClient.GetUsageDetails(monthsAgo: 12).ToListAsync();

            usageDetails.ShouldNotBeNull();
            usageDetails.Trace();

            TestContext?.AddResultFile(usageDetails.TraceToFile(nameof(UsageDetails_Trace)).FullName);
        }

        /// <summary>
        /// Tests retrieving usage details with localized output
        /// </summary>
        [TestMethod]
        public async Task UsageDetails_TraceLocalized()
        {
            var productionManagerClient = GetProductionManagerClient();
            var usageDetails = await productionManagerClient.GetUsageDetails(monthsAgo: 12).ToListAsync();

            usageDetails.ShouldNotBeNull();

            var culture = CultureInfo.GetCultureInfo("de-DE");
            var serializedObjectLocalized = usageDetails.SerializeLocalized(culture);
            var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

            dynamic.ShouldNotBeNull();

            TestContext?.AddResultFile(dynamic.TraceToFile(nameof(UsageDetails_TraceLocalized)).FullName);
        }

        /// <summary>
        /// Tests retrieving usage details for a specific period
        /// </summary>
        [TestMethod]
        public async Task UsageDetailsForPeriod_Trace()
        {
            var productionManagerClient = GetProductionManagerClient();
            var period = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            var usageDetails = await productionManagerClient.GetUsageDetailsForPeriod(period).ToListAsync();

            usageDetails.ShouldNotBeNull();
            usageDetails.Trace();

            TestContext?.AddResultFile(usageDetails.TraceToFile(nameof(UsageDetailsForPeriod_Trace)).FullName);
        }

        /// <summary>
        /// Tests retrieving current month's usage details
        /// </summary>
        [TestMethod]
        public async Task CurrentUsage_Trace()
        {
            var productionManagerClient = GetProductionManagerClient();
            var currentUsage = await productionManagerClient.GetCurrentUsage().ToListAsync();

            currentUsage.ShouldNotBeNull();
            currentUsage.Trace();

            TestContext?.AddResultFile(currentUsage.TraceToFile(nameof(CurrentUsage_Trace)).FullName);
        }

        /// <summary>
        /// Tests retrieving current month's usage details with localized output
        /// </summary>
        [TestMethod]
        public async Task CurrentUsage_TraceLocalized()
        {
            var productionManagerClient = GetProductionManagerClient();
            var currentUsage = await productionManagerClient.GetCurrentUsage().ToListAsync();

            currentUsage.ShouldNotBeNull();

            var culture = CultureInfo.GetCultureInfo("de-DE");
            var serializedObjectLocalized = currentUsage.SerializeLocalized(culture);
            var dynamic = JsonConvert.DeserializeObject(serializedObjectLocalized);

            dynamic.ShouldNotBeNull();

            TestContext?.AddResultFile(dynamic.TraceToFile(nameof(CurrentUsage_TraceLocalized)).FullName);
        }

        /// <summary>
        /// Tests that usage overview returns data for valid month range
        /// </summary>
        [TestMethod]
        public async Task UsageOverview_ValidMonthRange()
        {
            var productionManagerClient = GetProductionManagerClient();

            // Test with 6 months
            var overview6Months = await productionManagerClient.GetUsageOverview(monthsAgo: 6).ToListAsync();
            overview6Months.ShouldNotBeNull();

            // Test with 12 months (default)
            var overview12Months = await productionManagerClient.GetUsageOverview(monthsAgo: 12).ToListAsync();
            overview12Months.ShouldNotBeNull();

            // Test with 24 months (maximum)
            var overview24Months = await productionManagerClient.GetUsageOverview(monthsAgo: 24).ToListAsync();
            overview24Months.ShouldNotBeNull();
        }

        /// <summary>
        /// Tests that usage details returns data for valid month range
        /// </summary>
        [TestMethod]
        public async Task UsageDetails_ValidMonthRange()
        {
            var productionManagerClient = GetProductionManagerClient();

            // Test with 3 months
            var details3Months = await productionManagerClient.GetUsageDetails(monthsAgo: 3).ToListAsync();
            details3Months.ShouldNotBeNull();

            // Test with 12 months (default)
            var details12Months = await productionManagerClient.GetUsageDetails(monthsAgo: 12).ToListAsync();
            details12Months.ShouldNotBeNull();
        }

        /// <summary>
        /// Tests that usage details for period returns valid data
        /// </summary>
        [TestMethod]
        public async Task UsageDetailsForPeriod_ValidPeriods()
        {
            var productionManagerClient = GetProductionManagerClient();

            // Test current month
            var currentPeriod = DateTime.Now.ToString("yyyy-MM");
            var currentDetails = await productionManagerClient.GetUsageDetailsForPeriod(currentPeriod).ToListAsync();
            currentDetails.ShouldNotBeNull();

            // Test last month
            var lastMonthPeriod = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            var lastMonthDetails = await productionManagerClient.GetUsageDetailsForPeriod(lastMonthPeriod).ToListAsync();
            lastMonthDetails.ShouldNotBeNull();
        }
    }
}
