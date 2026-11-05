using System.Globalization;

using HomagConnect.OrderManager.Contracts.Customers;
using HomagConnect.OrderManager.Contracts.Extensions;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Tests.Customers
{
    [TestClass]
    [TestCategory("OrderManager")]
    [TestCategory("OrderManager.Customers")]
    public sealed class LocaleLocalizationTests
    {
        [TestMethod]
        public void Locale_NumericValuesAndDefault_AreUnchanged()
        {
            Assert.AreEqual(Locale.deDE, default);
            Assert.AreEqual(-1, (int)Locale.Unknown);
            Assert.AreEqual(0, (int)Locale.deDE);
            Assert.AreEqual(1, (int)Locale.deAt);
            Assert.AreEqual(2, (int)Locale.deCh);
            Assert.AreEqual(3, (int)Locale.enGB);
            Assert.AreEqual(4, (int)Locale.enUS);
            Assert.AreEqual(5, (int)Locale.esES);
            Assert.AreEqual(6, (int)Locale.nlNL);
            Assert.AreEqual(7, (int)Locale.nlBE);
        }

        [TestMethod]
        public void Locale_Deserialize_UnknownValue_ReturnsUnknown()
        {
            Assert.AreEqual(Locale.Unknown, JsonConvert.DeserializeObject<Locale>("\"xxYY\""));
            Assert.AreEqual(Locale.deAt, JsonConvert.DeserializeObject<Locale>("\"deAt\""));
        }

        [TestMethod]
        [DataRow("deDE", "de-DE")]
        [DataRow("deAt", "de-AT")]
        [DataRow("zhHans", "zh-Hans")]
        [DataRow("zhHansCN", "zh-Hans-CN")]
        [DataRow("srLatnRS", "sr-Latn-RS")]
        [DataRow("es419", "es-419")]
        [DataRow("filPH", "fil-PH")]
        public void Locale_ToCultureName_ReturnsBcp47Name(string locale, string expected)
        {
            Assert.AreEqual(expected, LocaleExtensions.ToCultureName(locale));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow("de")]
        [DataRow("d")]
        [DataRow("DEde")]
        [DataRow("de-AT")]
        [DataRow("deABC")]
        [DataRow("es41")]
        public void Locale_ToCultureName_InvalidName_Throws(string locale)
        {
            Assert.ThrowsExactly<ArgumentException>(() => LocaleExtensions.ToCultureName(locale));
        }

        [TestMethod]
        public void Locale_ToCultureInfo_AllValuesConvert()
        {
            Assert.AreEqual(CultureInfo.InvariantCulture, Locale.Unknown.ToCultureInfo());

            foreach (var locale in Enum.GetValues<Locale>().Where(l => l != Locale.Unknown))
            {
                var culture = locale.ToCultureInfo();
                var name = locale.ToString();

                Assert.AreEqual($"{name.Substring(0, 2).ToLowerInvariant()}-{name.Substring(2).ToUpperInvariant()}", culture.Name);
            }
        }
    }
}
