using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Base
{
    /// <summary />
    [TestClass]
    [UnitTest("IntelliDivide.Localization")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class LocalizationTests
    {
        /// <summary>
        /// This test will only succeed on server OR when you add the relevant resx for ja and en manually.
        /// </summary>
        [TestMethod]
        public void IntelliDivide_Localization_Grain()
        {
            var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));

            displayNames.Count.ShouldBe(3, "There are 3 types of grain available.");
            displayNames[Grain.None].ShouldBe("Keine", "The German display name for Grain.None is 'Keine'.");

            displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("en"));

            displayNames.Trace();
            displayNames.Count.ShouldBe(3, "There are 3 types of grain available.");
            displayNames.Trace();

            displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("ja"));

            displayNames.Count.ShouldBe(3, "There are 3 types of grain available.");

            displayNames.Trace();
        }

        /// <summary />
        [TestMethod]
        public void IntelliDivide_Localization_OptimizationStatus()
        {
            var cultureInfo = CultureInfo.GetCultureInfo("de-DE");
            var displayNames = EnumExtensions.GetDisplayNames<OptimizationStatus>(cultureInfo);

            displayNames.ShouldNotBeNull();
            displayNames.Count.ShouldBeGreaterThanOrEqualTo(1);
            displayNames[OptimizationStatus.Queued].ShouldBe("Gestartet (In Warteschlange)", "The German display name for OptimizationStatus.Queued is 'Gestartet (In Warteschlange)'.");

            displayNames.Trace($"{nameof(OptimizationStatus)}: {cultureInfo}");
        }

        /// <summary>
        /// This test will only succeed on server OR when you add the relevant resx for ja and en manually.
        /// </summary>
        [TestMethod]
        public void IntelliDivide_Localization_Properties()
        {
            var culture = CultureInfo.GetCultureInfo("de");
            var part = new OptimizationRequestPart();

            var propertyDisplayNames = part.GetPropertyDisplayNames(culture);

            propertyDisplayNames.Count.ShouldBeGreaterThan(0, "There should be a display name for part properties in the given culture.");
            propertyDisplayNames.ShouldContainKey(nameof(part.LaminateTop), "The property LaminateTop should have a display name in the given culture.");
            propertyDisplayNames.ShouldContainKey(nameof(part.LaminateBottom), "The property LaminateBottom should have a display name in the given culture.");
            propertyDisplayNames.ShouldContainKey(nameof(part.AllowedRotationAngle), "The property AllowedRotationAngle should have a display name in the given culture.");

            // Attribute defined on interface
            propertyDisplayNames[nameof(part.LaminateTop)].ShouldBe(@"Belag oben", "That is the translation for LaminateTop in the given culture.");
            propertyDisplayNames[nameof(part.LaminateBottom)].ShouldBe(@"Belag unten", "That is the translation for LaminateBottom in the given culture.");

            // Attribute defined on class
            propertyDisplayNames[nameof(part.AllowedRotationAngle)].ShouldBe(@"Drehwinkel", "That is the translation for AllowedRotationAngle in the given culture.");

            var laminateTopDisplayName = part.GetPropertyDisplayName(nameof(ILaminatingProperties.LaminateTop), culture);
            laminateTopDisplayName.ShouldBe(@"Belag oben", "That is the translation for LaminateTop in the given culture.");

            var laminateBottomDisplayName = part.GetPropertyDisplayName(nameof(ILaminatingProperties.LaminateBottom), culture);
            laminateBottomDisplayName.ShouldBe(@"Belag unten", "That is the translation for LaminateBottom in the given culture.");

            var allowedRotationAngle = part.GetPropertyDisplayName(nameof(OptimizationRequestPart.AllowedRotationAngle), culture);
            allowedRotationAngle.ShouldBe(@"Drehwinkel", "That is the translation for AllowedRotationAngle in the given culture.");

            propertyDisplayNames.Trace();
        }

        [TestMethod]
        public void Localization_BoardMaterialCategory()
        {
            var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

            displayNames.Count.ShouldBeGreaterThan(0, "There should be at least one display name for BoardMaterialCategory in the given culture.");
            displayNames[BoardMaterialCategory.AcrylicGlass_PMMA].ShouldBe("Acrylglas (PMMA)", "That is the translation for AcrylicGlass_PMMA in the given culture.");

            displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("en"));
            displayNames.Trace();
        }
    }
}