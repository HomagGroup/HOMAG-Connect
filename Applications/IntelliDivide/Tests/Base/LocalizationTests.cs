using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.IntelliDivide.Tests.Base
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class LocalizationTests
    {
        /// <summary>
        /// This test will only succeed on server OR when you add the relevant resx for ja and en manually.
        /// </summary>
        [IntegrationTest("translation")]
        [TestMethod]
        public void IntelliDivide_Localization_Grain()
        {
            var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(3, displayNames.Count);
            Assert.AreEqual("Keine", displayNames[Grain.None]);

            displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("en"));
            displayNames.Trace();

            Assert.AreEqual(3, displayNames.Count);

            displayNames.Trace();
            displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("ja"));

            Assert.AreEqual(3, displayNames.Count);

            displayNames.Trace();
        }

        /// <summary>
        /// This test will only succeed on server OR when you add the relevant resx for ja and en manually.
        /// </summary>
        [IntegrationTest("translation")]
        [TestMethod]
        public void IntelliDivide_Localization_Properties()
        {
            var culture = CultureInfo.GetCultureInfo("de");
            var part = new OptimizationRequestPart();

            var propertyDisplayNames = part.GetPropertyDisplayNames(culture);

            Assert.IsTrue(propertyDisplayNames.Count > 0);
            Assert.IsTrue(propertyDisplayNames.ContainsKey(nameof(part.LaminateTop)));
            Assert.IsTrue(propertyDisplayNames.ContainsKey(nameof(part.LaminateBottom)));
            Assert.IsTrue(propertyDisplayNames.ContainsKey(nameof(part.AllowedRotationAngle)));

            // Attribute defined on interface

            Assert.AreEqual(@"Belag oben", propertyDisplayNames[nameof(part.LaminateTop)]);
            Assert.AreEqual(@"Belag unten", propertyDisplayNames[nameof(part.LaminateBottom)]);

            // Attribute defined on class
            Assert.AreEqual(@"Drehwinkel", propertyDisplayNames[nameof(part.AllowedRotationAngle)]);

            var laminateTopDisplayName = part.GetPropertyDisplayName(nameof(ILaminatingProperties.LaminateTop), culture);
            Assert.AreEqual(@"Belag oben", laminateTopDisplayName);

            var laminateBottomDisplayName = part.GetPropertyDisplayName(nameof(ILaminatingProperties.LaminateBottom), culture);
            Assert.AreEqual(@"Belag unten", laminateBottomDisplayName);

            var allowedRotationAngle = part.GetPropertyDisplayName(nameof(OptimizationRequestPart.AllowedRotationAngle), culture);
            Assert.AreEqual(@"Drehwinkel", allowedRotationAngle);

            propertyDisplayNames.Trace();
        }

        [TestMethod]
        public void Localization_BoardMaterialCategory()
        {
            var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

            Assert.IsTrue(displayNames.Count > 0);

            Assert.AreEqual("Acrylglas (PMMA)", displayNames[BoardMaterialCategory.AcrylicGlass_PMMA]);

            displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("en"));
            displayNames.Trace();
        }
    }
}