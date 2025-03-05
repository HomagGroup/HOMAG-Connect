using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.IntelliDivide.Tests.Base
{

    [TestClass]
    [TestCategory("IntelliDivide")]
    public class LocalizationTests
    {

        /// <summary>
        /// This test will only succeed on server OR when you add the relevant resx for ja and en manually. 
        /// </summary>
        [IntegrationTest("translation")]
        [TemporaryDisabledOnServer(2025, 3, 10, "DF-Foundation")]
        [TestMethod]
        public void IntelliDivide_Localization_Grain()
        {
            var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(3, displayNames.Count);
            Assert.AreEqual("Keine", displayNames[Grain.None]);

            displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("en"));
            displayNames.Trace();

            Assert.AreEqual(3, displayNames.Count);
            Assert.AreEqual("None", displayNames[Grain.None]);

            displayNames.Trace();
            displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("ja"));

            Assert.AreEqual(3, displayNames.Count);
            Assert.AreEqual("縦", displayNames[Grain.Lengthwise]);

            displayNames.Trace();
        }
        [TestMethod]
        [TemporaryDisabledOnServer(2025, 3, 10, "DF-Foundation")]
        public void Localization_BoardMatCat()
        {
            var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(54, displayNames.Count);
            Assert.AreEqual("Acrylglas (PMMA)", displayNames[BoardMaterialCategory.AcrylicGlass_PMMA]);

            displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("en"));
            displayNames.Trace();

        }
    }
}