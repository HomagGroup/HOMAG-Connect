using System.Globalization;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Rework;
using Shouldly;

// ReSharper disable StringLiteralTypo

namespace HomagConnect.ProductionManager.Tests.Rework
{
    /// <summary />
    [TestClass]
    [UnitTest("ProductionManager.Rework")]
    public class ReworkLocalizationTests
    {
        [TestMethod]
        public void Localization_Rework()
        {
            var culture = CultureInfo.GetCultureInfo("de");

            var rework = new Contracts.Rework.Rework();
            var propertyDisplayNames = rework.GetPropertyDisplayNames(culture);

            propertyDisplayNames.ShouldNotBeEmpty(
                "because Rework properties should have localized display names");

            propertyDisplayNames[nameof(rework.CapturedAt)].ShouldBe("Erfasst", "the CapturedAt property should be localized as 'Erfasst' in German");

            propertyDisplayNames.Trace();
        }

        /// <summary />
        [TestMethod]
        public void Localization_ReworkCategory()
        {
            var displayNames = EnumExtensions.GetDisplayNames<ReworkCategory>(CultureInfo.GetCultureInfo("de"));

            displayNames.ShouldNotBeEmpty(
                "because ReworkCategory enum should have localized display names");
            displayNames[ReworkCategory.Dividing].ShouldBe("Aufteilen",
                "because ReworkCategory.Dividing should be localized as 'Aufteilen' in German");

            displayNames.Trace();
        }

        [TestMethod]
        public void Localization_ReworkCreationDetails()
        {
            var culture = CultureInfo.GetCultureInfo("de");

            var creationDetails = new CreationDetails();
            var propertyDisplayNames = creationDetails.GetPropertyDisplayNames(culture);

            propertyDisplayNames.ShouldNotBeEmpty(
                "because CreationDetails properties should have localized display names");

            propertyDisplayNames[nameof(creationDetails.Comment)].ShouldBe("Notizen", "the Comment property should be localized as 'Notizen' in German");

            propertyDisplayNames.Trace();
        }

        /// <summary />
        [TestMethod]
        public void Localization_ReworkReason()
        {
            var displayNames = EnumExtensions.GetDisplayNames<ReworkReason>(CultureInfo.GetCultureInfo("de"));

            displayNames.ShouldNotBeEmpty(
                "because ReworkReason enum should have localized display names");
            displayNames[ReworkReason.Tearouts].ShouldBe("Ausrisse",
                "because ReworkReason.Tearouts should be localized as 'Ausrisse' in German");

            displayNames.Trace();
        }

        [TestMethod]
        public void Localization_ReworkRejectionDetails()
        {
            var culture = CultureInfo.GetCultureInfo("de");

            var rejectionDetails = new RejectionDetails();
            var propertyDisplayNames = rejectionDetails.GetPropertyDisplayNames(culture);

            propertyDisplayNames.ShouldNotBeEmpty(
                "because RejectionDetails properties should have localized display names");

            propertyDisplayNames[nameof(rejectionDetails.Comment)].ShouldBe("Notizen", "the Comment property should be localized as 'Notizen' in German");

            propertyDisplayNames.Trace();
        }

        /// <summary />
        [TestMethod]
        public void Localization_ReworkState()
        {
            var displayNames = EnumExtensions.GetDisplayNames<ReworkState>(CultureInfo.GetCultureInfo("de"));

            displayNames.ShouldNotBeEmpty(
                "because ReworkState enum should have localized display names");
            displayNames[ReworkState.Rejected].ShouldBe("Verweigert",
                "because ReworkState.Rejected should be localized as 'Verweigert' in German");

            displayNames.Trace();
        }
    }
}