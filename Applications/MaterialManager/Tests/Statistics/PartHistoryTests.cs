using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Statistics;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Statistics;

/// <summary />
[TestClass]
public class PartHistoryTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Statistics.PartHistory")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public void Localization_PartHistory()
    {
        var culture = CultureInfo.GetCultureInfo("de");

        var partHistory = new PartHistory();
        var propertyDisplayNames = partHistory.GetPropertyDisplayNames(culture);

        propertyDisplayNames.ShouldNotBeEmpty(
            "because PartHistory properties should have localized display names");

        propertyDisplayNames[nameof(partHistory.OptimizationName)].ShouldBe("Optimierung", "the OptimizationName property should be localized as 'Optimierung' in German");

        propertyDisplayNames.Trace();
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Statistics.PartHistory")]
    public void Localization_PartHistoryPart()
    {
        var culture = CultureInfo.GetCultureInfo("de");

        var partHistory = new PartHistoryPart();
        var propertyDisplayNames = partHistory.GetPropertyDisplayNames(culture);

        propertyDisplayNames.ShouldNotBeEmpty(
            "because PartHistory properties should have localized display names");

        propertyDisplayNames[nameof(partHistory.Length)].ShouldBe("Länge", "the Length property should be localized as 'Länge' in German");

        propertyDisplayNames.Trace();
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Statistics.PartHistory")]
    public async Task Statistics_GetPartHistory_ByDays_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var statistics = (await materialClient.Material.Boards.GetPartHistoryAsync(60, 10).ConfigureAwait(false) ?? Array.Empty<PartHistory>()).ToArray();

        statistics.ShouldNotBeNull(
            "because part history for the last 60 days with max 10 results should be available");

        statistics.Trace();
    }
}