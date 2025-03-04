using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Tests.Material
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    public class LocalizationTests
    {
        /// <summary />
        [TestMethod]
        public void MaterialManager_Localization_Grain()
        {
            var displayNames = EnumExtensions.GetDisplayNames<Grain>(CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(3, displayNames.Count);
            Assert.AreEqual("Keine", displayNames[Grain.None]);

            displayNames.Trace();
        }

        /// <summary />
        [TestMethod]
        [TemporaryDisabledOnServer(2025, 3, 10, "DF-Foundation")]
        public void MaterialManager_Localization_BoardMaterialCategory()
        {
            var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

            Assert.IsTrue(displayNames.Count > 0);
            Assert.AreEqual("Spanplatte", displayNames[BoardMaterialCategory.Chipboard]);

            displayNames.Trace();
        }
    }
}