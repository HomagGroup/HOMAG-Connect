using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using Shouldly;
using System.Globalization;

namespace HomagConnect.ProductionManager.Tests.ProductionItems
{
    [TestClass]
    [UnitTest("ProductionManager.ProductionItems")]
    public class ProductionItemLocalizationTests
    {
        public required TestContext TestContext { get; set; }


        [TestMethod]
        public void Localization_ProductionItemFeedback()
        {
            var culture = CultureInfo.GetCultureInfo("de");

            var itemFeedback = new ProductionItemFeedback();
            var propertyDisplayNames = itemFeedback.GetPropertyDisplayNames(culture);

            propertyDisplayNames.ShouldNotBeEmpty(
                "because ProductionItems properties should have localized display names");

            propertyDisplayNames[nameof(itemFeedback.From)].ShouldBe("Von", "the From property should be localized as 'Von' in German");

            propertyDisplayNames.Trace();
        }

        /// <summary />
        [TestMethod]
        public void Localization_ProductionItemFeedbackForExcel()
        {
            var itemFeedback = new ProductionItemFeedback
            {
                Identifier = "12345",
                WorkstationId = Guid.NewGuid(),
                Workstation = "Workstation 1",
                From = "Machine 1",
                Timestamp = DateTimeOffset.UtcNow,
                Quantity = 10,
                Action = ProductionItemFeedbackAction.InProduction
            };

            var culture = CultureInfo.GetCultureInfo("de-DE");
            var serializedObjectLocalized = itemFeedback.SerializeLocalized(culture);

            TestContext.Write(serializedObjectLocalized);
        }
    }
}
