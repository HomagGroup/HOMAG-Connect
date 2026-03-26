using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Orders;
using Shouldly;
using System.Globalization;

namespace HomagConnect.ProductionManager.Tests.Orders.Usage
{
    [TestClass]
    [UnitTest("ProductionManager.Orders.Usage.Localization")]
    public class UsageLocalizationTests : ProductionManagerTestBase
    {
        /// <summary>
        /// Tests that OrderAction enum can be localized in German.
        /// </summary>
        [TestMethod]
        public void Localization_OrderAction_German()
        {
            var culture = CultureInfo.GetCultureInfo("de");
            var displayNames = EnumExtensions.GetDisplayNames<OrderAction>(culture);

            displayNames.ShouldNotBeEmpty(
                "because OrderAction enum should have localized display names");

            displayNames[OrderAction.Release].ShouldBe("Freigeben",
                "because OrderAction.Release should be localized as 'Freigeben' in German");
            displayNames[OrderAction.ResetRelease].ShouldBe("Freigabe zurücknehmen",
                "because OrderAction.ResetRelease should be localized as 'Freigabe zurücksetzen' in German");

            displayNames.Trace();
        }

        /// <summary>
        /// Tests that OrderAction enum can be localized in English.
        /// </summary>
        [TestMethod]
        public void Localization_OrderAction_English()
        {
            var culture = CultureInfo.GetCultureInfo("en");
            var displayNames = EnumExtensions.GetDisplayNames<OrderAction>(culture);

            displayNames.ShouldNotBeEmpty(
                "because OrderAction enum should have localized display names");

            displayNames.Trace();
        }

        /// <summary>
        /// Tests that UsageDetails properties can be localized.
        /// </summary>
        [TestMethod]
        public void Localization_UsageDetails_German()
        {
            var culture = CultureInfo.GetCultureInfo("de");

            var usageDetails = new UsageDetails();
            var propertyDisplayNames = usageDetails.GetPropertyDisplayNames(culture);

            propertyDisplayNames.ShouldNotBeEmpty(
                "because UsageDetails properties should have localized display names");

            propertyDisplayNames[nameof(UsageDetails.Timestamp)].ShouldBe("Zeitstempel",
                "because Timestamp property should be localized as 'Zeitstempel' in German");
            propertyDisplayNames[nameof(UsageDetails.OrderName)].ShouldBe("Auftragsname",
                "because OrderName property should be localized as 'Auftragsname' in German");
            propertyDisplayNames[nameof(UsageDetails.CustomerName)].ShouldBe("Kundenname",
                "because CustomerName property should be localized as 'Kundenname' in German");

            propertyDisplayNames.Trace();
        }

        /// <summary>
        /// Tests that UsageOverview properties can be localized.
        /// </summary>
        [TestMethod]
        public void Localization_UsageOverview_German()
        {
            var culture = CultureInfo.GetCultureInfo("de");

            var usageOverview = new UsageOverview();
            var propertyDisplayNames = usageOverview.GetPropertyDisplayNames(culture);

            propertyDisplayNames.ShouldNotBeEmpty(
                "because UsageOverview properties should have localized display names");

            propertyDisplayNames[nameof(UsageOverview.Period)].ShouldBe("Zeitraum",
                "because Period property should be localized as 'Zeitraum' in German");
            propertyDisplayNames[nameof(UsageOverview.Licenses)].ShouldBe("Lizenzen",
                "because Licenses property should be localized as 'Lizenzen' in German");

            propertyDisplayNames.Trace();
        }

        /// <summary>
        /// Tests that UsageDetails can be serialized in localized format.
        /// </summary>
        [TestMethod]
        public void UsageDetails_SerializeLocalized_German()
        {
            var usageDetail = new UsageDetails
            {
                Timestamp = DateTimeOffset.UtcNow,
                OrderNumber = "ORD-2026-0001",
                OrderName = "Test Order",
                CustomerNumber = "CUST-123",
                CustomerName = "Test Customer",
                QuantityOfParts = 100,
                Action = OrderAction.Release,
                ChangedBy = "test.user@example.com",
                Source = "API"
            };

            var culture = CultureInfo.GetCultureInfo("de");
            var localizedJson = usageDetail.SerializeLocalized(culture);

            localizedJson.ShouldNotBeNullOrWhiteSpace();
            Assert.IsTrue(localizedJson.Contains("Auftragsname"),
                "because OrderName should be serialized with German property name");
            Assert.IsTrue(localizedJson.Contains("Kundenname"),
                "because CustomerName should be serialized with German property name");
            Assert.IsTrue(localizedJson.Contains("Freigeben"),
                "because Action should be serialized with German enum value");

            localizedJson.Trace();
        }

        /// <summary>
        /// Tests that UsageOverview can be serialized in localized format.
        /// </summary>
        [TestMethod]
        public void UsageOverview_SerializeLocalized_German()
        {
            var usageOverview = new UsageOverview
            {
                Period = DateTime.UtcNow,
                Licenses = new System.Collections.ObjectModel.Collection<License>
                {
                    new License
                    {
                        ApplicationId = Guid.NewGuid(),
                        ApplicationLicenseFullName = "Test License",
                        LicenseCount = 5
                    }
                },
                QuantityOfPartsCoveredByTheLicenses = 5000,
                QuantityOfReleasedParts = 3000
            };

            var culture = CultureInfo.GetCultureInfo("de");
            var localizedJson = usageOverview.SerializeLocalized(culture);

            localizedJson.ShouldNotBeNullOrWhiteSpace();
            Assert.IsTrue(localizedJson.Contains("Zeitraum"),
                "because Period should be serialized with German property name");
            Assert.IsTrue(localizedJson.Contains("Lizenzen"),
                "because Licenses should be serialized with German property name");

            localizedJson.Trace();
        }
    }
}
