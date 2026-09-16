using System.Globalization;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches.Enumerations;

using Newtonsoft.Json.Linq;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures.DecorMatches;

/// <summary>
/// Localization tests for the decor match search contract types (request/response enum display names, and localized JSON serialization), independent of any client or server logic.
/// </summary>
[TestClass]
[UnitTest("MaterialManager")]
public class DecorMatchLocalizationTests
{
    /// <summary>
    /// Tests that DecorMatchMaterialType enum values can be localized in German.
    /// </summary>
    [TestMethod]
    public void Localization_DecorMatchMaterialType_German()
    {
        var culture = CultureInfo.GetCultureInfo("de");
        var displayNames = EnumExtensions.GetDisplayNames<DecorMatchMaterialType>(culture);

        displayNames.ShouldNotBeEmpty(
            "because DecorMatchMaterialType enum should have localized display names");

        displayNames[DecorMatchMaterialType.Board].ShouldBe("Platte",
            "because DecorMatchMaterialType.Board should be localized as 'Platte' in German");
        displayNames[DecorMatchMaterialType.Edgeband].ShouldBe("Kante",
            "because DecorMatchMaterialType.Edgeband should be localized as 'Kante' in German");

        displayNames.Trace();
    }

    /// <summary>
    /// Tests that DecorMatchRankBasis enum values can be localized in German.
    /// </summary>
    [TestMethod]
    public void Localization_DecorMatchRankBasis_German()
    {
        var culture = CultureInfo.GetCultureInfo("de");
        var displayNames = EnumExtensions.GetDisplayNames<DecorMatchRankBasis>(culture);

        displayNames.ShouldNotBeEmpty(
            "because DecorMatchRankBasis enum should have localized display names");

        displayNames[DecorMatchRankBasis.Confidence].ShouldBe("Konfidenz",
            "because DecorMatchRankBasis.Confidence should be localized as 'Konfidenz' in German");
        displayNames[DecorMatchRankBasis.UsageFrequency].ShouldBe("Nutzungshäufigkeit",
            "because DecorMatchRankBasis.UsageFrequency should be localized as 'Nutzungshäufigkeit' in German");

        displayNames.Trace();
    }

    /// <summary>
    /// Tests that DecorMatchSearchRequest properties that declare a Display attribute can be localized.
    /// </summary>
    [TestMethod]
    public void Localization_DecorMatchSearchRequest_German()
    {
        var culture = CultureInfo.GetCultureInfo("de");

        var request = new DecorMatchSearchRequest
        {
            MaterialType = DecorMatchMaterialType.Board,
            Material = new JObject()
        };

        var propertyDisplayNames = request.GetPropertyDisplayNames(culture);

        propertyDisplayNames.ShouldNotBeEmpty(
            "because DecorMatchSearchRequest properties should have display names");

        propertyDisplayNames[nameof(DecorMatchSearchRequest.SearchTerm)].ShouldBe("Suchbegriff",
            "because SearchTerm should be localized as 'Suchbegriff' in German");

        propertyDisplayNames.Trace();
    }

    /// <summary>
    /// Tests that DecorMatchCandidate properties that declare a Display attribute can be localized.
    /// </summary>
    [TestMethod]
    public void Localization_DecorMatchCandidate_German()
    {
        var culture = CultureInfo.GetCultureInfo("de");

        var candidate = new DecorMatchCandidate();
        var propertyDisplayNames = candidate.GetPropertyDisplayNames(culture);

        propertyDisplayNames.ShouldNotBeEmpty(
            "because DecorMatchCandidate properties should have display names");

        propertyDisplayNames[nameof(DecorMatchCandidate.LocalizedName)].ShouldBe("Bezeichnung",
            "because LocalizedName should be localized as 'Bezeichnung' in German");

        propertyDisplayNames.Trace();
    }

    /// <summary>
    /// Tests that a DecorMatchCandidate can be serialized using localized property names and enum values.
    /// </summary>
    [TestMethod]
    public void DecorMatchCandidate_SerializeLocalized_German()
    {
        var candidate = new DecorMatchCandidate
        {
            DecorId = "dec-001",
            LocalizedName = "Weiß",
            TextureUrl = "https://example.com/textures/dec-001",
            ConfidenceScore = 0.95,
            IsRecommended = true,
            Previews = new List<AdditionalDataPreview>
            {
                new() { Size = AdditionalDataPreviewSize.Small, Uri = new Uri("https://example.com/small.jpg") }
            }
        };

        var culture = CultureInfo.GetCultureInfo("de");
        var localizedJson = candidate.SerializeLocalized(culture);

        localizedJson.ShouldNotBeNullOrWhiteSpace();
        Assert.IsTrue(localizedJson.Contains("Bezeichnung"),
            "because LocalizedName should be serialized with the German property name");

        localizedJson.Trace();
    }
}
