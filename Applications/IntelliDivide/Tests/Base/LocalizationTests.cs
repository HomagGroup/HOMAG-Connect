﻿using System.Globalization;

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

        [TestMethod]
        public void Localization_BoardMaterialCategory()
        {
            var displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("de"));

            Assert.AreEqual(54, displayNames.Count);
            Assert.AreEqual("Acrylglas (PMMA)", displayNames[BoardMaterialCategory.AcrylicGlass_PMMA]);

            displayNames = EnumExtensions.GetDisplayNames<BoardMaterialCategory>(CultureInfo.GetCultureInfo("en"));
            displayNames.Trace();

        }
    }
}