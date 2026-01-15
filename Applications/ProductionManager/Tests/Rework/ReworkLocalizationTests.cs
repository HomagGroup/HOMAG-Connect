using System.Globalization;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.Rework;

using Shouldly;

// ReSharper disable StringLiteralTypo

namespace HomagConnect.ProductionManager.Tests.Rework;

/// <summary />
[TestClass]
[UnitTest("ProductionManager.Rework")]
public class ReworkLocalizationTests
{
    public required TestContext TestContext { get; set; }

    [TestMethod]
    public void Localization_Rework()
    {
        var culture = CultureInfo.GetCultureInfo("de");

        var rework = new Contracts.Rework.Rework();
        var propertyDisplayNames = rework.GetPropertyDisplayNames(culture);

        propertyDisplayNames.ShouldNotBeEmpty(
            "because Rework properties should have localized display names");

        propertyDisplayNames[nameof(rework.CapturedAt)].ShouldBe("Erfasst am", "the CapturedAt property should be localized as 'Erfasst' in German");

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
    public void Localization_ReworkCaptureDetails()
    {
        var culture = CultureInfo.GetCultureInfo("de");

        var creationDetails = new CaptureDetails();
        var propertyDisplayNames = creationDetails.GetPropertyDisplayNames(culture);

        propertyDisplayNames.ShouldNotBeEmpty(
            "because CaptureDetails properties should have localized display names");

        propertyDisplayNames[nameof(creationDetails.Comment)].ShouldBe("Notizen", "the Comment property should be localized as 'Notizen' in German");

        propertyDisplayNames.Trace();
    }

    /// <summary />
    [TestMethod]
    public void Localization_ReworkForExcel()
    {
        var rework = new Contracts.Rework.Rework
        {
            CapturedAt = DateTimeOffset.Parse("2026-01-08T10:40:11.5441193+01:00", CultureInfo.InvariantCulture),
            Category = ReworkCategory.Dividing,
            CaptureDetails = new CaptureDetails
            {
                CapturedBy = "Boris Wehrle",
                LastWorkstation = "Workstation 1",
                Comment = "Happens often"
            },
            Description = "Left door",
            Id = "10621053",
            Material = "P2_Cloudy_blue_19.0",
            OrderName = "My first order",
            QuantityOfReworks = 1,
            Reason = ReworkReason.WaveCut,
            RejectionDetails = new RejectionDetails
            {
                Comment = "The quality is sufficient.",
                RejectedBy = "Boris Wehrle",
                RejectedAt = DateTimeOffset.Parse("2026-01-08T10:40:24.7076173+01:00", CultureInfo.InvariantCulture)
            },
            State = ReworkState.Rejected
        };

        rework.Trace();

        var culture = CultureInfo.GetCultureInfo("de-DE");

        var serializedObjectLocalized = rework.SerializeLocalized(culture);

        TestContext.Write(serializedObjectLocalized);
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

    [TestMethod]
    public void Localization_ReworksForExcel()
    {
        var reworks = new List<Contracts.Rework.Rework>
        {
            new()
            {
                CapturedAt = DateTimeOffset.Parse("2026-01-08T10:40:11.5441193+01:00", CultureInfo.InvariantCulture),
                Category = ReworkCategory.Dividing,
                CaptureDetails = new CaptureDetails
                {
                    Comment = "Happens often"
                },
                Description = "Left door",
                Id = "10621053",
                Material = "P2_Cloudy_blue_19.0",
                OrderName = "My first order",
                QuantityOfReworks = 1,
                Reason = ReworkReason.WaveCut,
                RejectionDetails = new RejectionDetails
                {
                    Comment = "The quality is sufficient.",
                    RejectedBy = "Boris Wehrle",
                    RejectedAt = DateTimeOffset.Parse("2026-01-08T10:40:24.7076173+01:00", CultureInfo.InvariantCulture)
                },
                State = ReworkState.Rejected
            },
            new()
            {
                CapturedAt = DateTimeOffset.Parse("2026-01-08T11:00:00+01:00", CultureInfo.InvariantCulture),
                Category = ReworkCategory.CNC,
                CaptureDetails = new CaptureDetails    
                {
                    Comment = "Check alignment"
                },
                Description = "Right panel",
                Id = "10621054",
                Material = "P2_Cloudy_blue_19.0",
                OrderName = "My second order",
                QuantityOfReworks = 2,
                Reason = ReworkReason.WaveCut,
                RejectionDetails = new RejectionDetails
                {
                    Comment = "Surface acceptable.",
                    RejectedBy = "Boris Wehrle",
                    RejectedAt = DateTimeOffset.Parse("2026-01-08T11:05:00+01:00", CultureInfo.InvariantCulture)
                },
                State = ReworkState.Rejected
            },
            new()
            {
                CapturedAt = DateTimeOffset.Parse("2026-01-08T11:30:00+01:00", CultureInfo.InvariantCulture),
                Category = ReworkCategory.Edgebanding,
                CaptureDetails = new CaptureDetails 
                {
                    Comment = "Glue issue"
                },
                Description = "Top shelf",
                Id = "10621055",
                Material = "P2_Cloudy_blue_19.0",
                OrderName = "My third order",
                QuantityOfReworks = 1,
                Reason = ReworkReason.WaveCut,
                RejectionDetails = new RejectionDetails
                {
                    Comment = "Edges not smooth.",
                    RejectedBy = "Boris Wehrle",
                    RejectedAt = DateTimeOffset.Parse("2026-01-08T11:35:00+01:00", CultureInfo.InvariantCulture)
                },
            }
        };

        reworks.Trace();

        var culture = CultureInfo.GetCultureInfo("de-DE");

        var serializedObjectLocalized = reworks.SerializeLocalized(culture);

        TestContext.Write(serializedObjectLocalized);
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