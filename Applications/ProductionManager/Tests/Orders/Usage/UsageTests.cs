using HomagConnect.Base.Extensions;
using Shouldly;

using HomagConnect.Base;
using HomagConnect.ProductionManager.Contracts.Orders;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.Orders.Usage
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class UsageTests : ProductionManagerTestBase
    {
        /// <summary />
        [TestMethod]
        public void UsageDetails_Trace()
        {
            var timestamp = DateTimeOffset.UtcNow;
            var order = new UsageDetails()
            {
                OrderName = "Mini Schrank",
                CustomerName = "Boris Wehrle",
                CustomerNumber = "4711",
                Timestamp = timestamp,
                Source = "Test Source",
                QuantityOfParts = 10
            };

            order.Trace();


            var serialized = JsonConvert.SerializeObject(order, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageDetails>(serialized, SerializerSettings.Default);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.Source);

            deserialized.ShouldNotBe(null);
            deserialized.ShouldBeEquivalentTo(order);

            deserialized.Action.ShouldBe(OrderAction.Release);
        }

        /// <summary />
        [TestMethod]
        public void UsageOverview_Trace()
        {
            var datetime = DateTime.UtcNow;;
            var overview = new UsageOverview()
            {
               Period = datetime,
               Licenses = new ()
               {
                   new()
                   {
                       ApplicationId = Guid.Empty,
                       ApplicationLicenseFullName = "TestApp",
                       LicenseCount = 1
                   }
               },
               QuantityOfPartsCoveredByTheLicenses = 10000,
               QuantityOfReleasedParts = 9500
            };

            overview.Trace();


            var serialized = JsonConvert.SerializeObject(overview, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageOverview>(serialized, SerializerSettings.Default);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.Licenses);

            deserialized.ShouldNotBe(null);
            deserialized.ShouldBeEquivalentTo(overview);

            deserialized.Licenses.GetType().ShouldBe(overview.Licenses.GetType());
        }
    }

}