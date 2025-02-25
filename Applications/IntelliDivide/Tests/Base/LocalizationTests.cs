using System.Globalization;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;

namespace HomagConnect.IntelliDivide.Tests.Base
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    public class LocalizationTests
    {
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
    }
}