using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.Orders;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.Orders.Usage
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.Orders")]
    public class UsageTests : ProductionManagerTestBase
    {
        #region UsageDetails Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of UsageDetails
        /// </summary>
        [TestMethod]
        public void UsageDetails_SerializeDeserialize_Basic()
        {
            var timestamp = DateTimeOffset.UtcNow;
            var order = new UsageDetails
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

        /// <summary>
        /// Tests serialization with all properties set
        /// </summary>
        [TestMethod]
        public void UsageDetails_SerializeDeserialize_AllProperties()
        {
            var timestamp = DateTimeOffset.Parse("2026-03-20T14:30:00+01:00");
            var usageDetails = new UsageDetails
            {
                Timestamp = timestamp,
                OrderNumber = "ORD-2026-12345",
                OrderName = "Kitchen Cabinet Project",
                CustomerNumber = "CUST-98765",
                CustomerName = "Möbelhaus Schmidt GmbH",
                QuantityOfParts = 234,
                Action = OrderAction.ResetRelease,
                ChangedBy = "max.mustermann@example.com",
                Source = "Web Portal"
            };

            var serialized = JsonConvert.SerializeObject(usageDetails, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageDetails>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Timestamp.ShouldBe(timestamp);
            deserialized.OrderNumber.ShouldBe("ORD-2026-12345");
            deserialized.OrderName.ShouldBe("Kitchen Cabinet Project");
            deserialized.CustomerNumber.ShouldBe("CUST-98765");
            deserialized.CustomerName.ShouldBe("Möbelhaus Schmidt GmbH");
            deserialized.QuantityOfParts.ShouldBe(234);
            deserialized.Action.ShouldBe(OrderAction.ResetRelease);
            deserialized.ChangedBy.ShouldBe("max.mustermann@example.com");
            deserialized.Source.ShouldBe("Web Portal");

            serialized.Trace();
        }

        /// <summary>
        /// Tests serialization with nullable Source property set to null
        /// </summary>
        [TestMethod]
        public void UsageDetails_SerializeDeserialize_NullableSource()
        {
            var usageDetails = new UsageDetails
            {
                Timestamp = DateTimeOffset.UtcNow,
                OrderNumber = "ORD-001",
                OrderName = "Test Order",
                CustomerNumber = "CUST-001",
                CustomerName = "Test Customer",
                QuantityOfParts = 50,
                Action = OrderAction.Release,
                ChangedBy = "user@example.com",
                Source = null
            };

            var serialized = JsonConvert.SerializeObject(usageDetails, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageDetails>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Source.ShouldBeNull();
        }

        /// <summary>
        /// Tests JSON property order for UsageDetails
        /// </summary>
        [TestMethod]
        public void UsageDetails_SerializeDeserialize_PropertyOrder()
        {
            var usageDetails = new UsageDetails
            {
                Timestamp = DateTimeOffset.UtcNow,
                OrderNumber = "ORD-001",
                OrderName = "Test Order",
                CustomerNumber = "CUST-001",
                CustomerName = "Test Customer",
                QuantityOfParts = 50,
                Action = OrderAction.Release,
                ChangedBy = "user@example.com",
                Source = "API"
            };

            var serialized = JsonConvert.SerializeObject(usageDetails, SerializerSettings.Default);
            var jObject = JObject.Parse(serialized);
            var properties = jObject.Properties().Select(p => p.Name).ToArray();

            // Verify JSON property order matches the Order attribute
            properties[0].ShouldBe("timestamp");
            properties[1].ShouldBe("orderNumber");
            properties[2].ShouldBe("orderName");
            properties[3].ShouldBe("customerNumber");
            properties[4].ShouldBe("customerName");
            properties[5].ShouldBe("quantityOfParts");
            properties[6].ShouldBe("action");
            properties[7].ShouldBe("changedBy");
            properties[8].ShouldBe("source");
        }

        /// <summary>
        /// Tests OrderAction enum serialization
        /// </summary>
        [TestMethod]
        public void UsageDetails_SerializeDeserialize_OrderActionEnum()
        {
            // Test with Release action
            var releaseDetails = new UsageDetails
            {
                Timestamp = DateTimeOffset.UtcNow,
                OrderNumber = "ORD-001",
                OrderName = "Test Order",
                CustomerNumber = "CUST-001",
                CustomerName = "Test Customer",
                QuantityOfParts = 50,
                Action = OrderAction.Release,
                ChangedBy = "user@example.com"
            };

            var serializedRelease = JsonConvert.SerializeObject(releaseDetails, SerializerSettings.Default);
            serializedRelease.Trace(); // Debug output

            // Verify the JSON contains Release (formatted JSON has spaces)
            Assert.IsTrue(serializedRelease.Contains("\"Release\""),
                $"Serialized JSON should contain Release enum value. Actual JSON: {serializedRelease}");

            // Deserialize and verify
            var deserializedRelease = JsonConvert.DeserializeObject<UsageDetails>(serializedRelease, SerializerSettings.Default);
            deserializedRelease.ShouldNotBeNull();
            deserializedRelease.Action.ShouldBe(OrderAction.Release,
                "Deserialized Action should be Release");

            // Test with ResetRelease action
            var resetDetails = new UsageDetails
            {
                Timestamp = DateTimeOffset.UtcNow,
                OrderNumber = "ORD-002",
                OrderName = "Test Order 2",
                CustomerNumber = "CUST-002",
                CustomerName = "Test Customer 2",
                QuantityOfParts = 75,
                Action = OrderAction.ResetRelease,
                ChangedBy = "user@example.com"
            };

            var serializedReset = JsonConvert.SerializeObject(resetDetails, SerializerSettings.Default);
            serializedReset.Trace(); // Debug output

            // Verify the JSON contains ResetRelease (formatted JSON has spaces)
            Assert.IsTrue(serializedReset.Contains("\"ResetRelease\""),
                $"Serialized JSON should contain ResetRelease enum value. Actual JSON: {serializedReset}");

            // Deserialize and verify
            var deserializedReset = JsonConvert.DeserializeObject<UsageDetails>(serializedReset, SerializerSettings.Default);
            deserializedReset.ShouldNotBeNull();
            deserializedReset.Action.ShouldBe(OrderAction.ResetRelease,
                "Deserialized Action should be ResetRelease");

            // Verify that both values round-trip correctly
            var reserializedRelease = JsonConvert.SerializeObject(deserializedRelease, SerializerSettings.Default);
            var rereserializedRelease = JsonConvert.DeserializeObject<UsageDetails>(reserializedRelease, SerializerSettings.Default);
            rereserializedRelease?.Action.ShouldBe(OrderAction.Release,
                "Action should remain Release after re-serialization");

            var reserializedReset = JsonConvert.SerializeObject(deserializedReset, SerializerSettings.Default);
            var rereserializedReset = JsonConvert.DeserializeObject<UsageDetails>(reserializedReset, SerializerSettings.Default);
            rereserializedReset?.Action.ShouldBe(OrderAction.ResetRelease,
                "Action should remain ResetRelease after re-serialization");
        }

        #endregion

        #region UsageOverview Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of UsageOverview
        /// </summary>
        [TestMethod]
        public void UsageOverview_SerializeDeserialize_Basic()
        {
            var datetime = DateTime.UtcNow;
            var overview = new UsageOverview
            {
                Period = datetime,
                Licenses = new()
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

        /// <summary>
        /// Tests serialization with all properties set
        /// </summary>
        [TestMethod]
        public void UsageOverview_SerializeDeserialize_AllProperties()
        {
            var period = new DateTime(2026, 3, 1, 0, 0, 0, DateTimeKind.Utc);
            var applicationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");

            var overview = new UsageOverview
            {
                Period = period,
                Licenses = new Collection<License>
                {
                    new License
                    {
                        ApplicationId = applicationId,
                        ApplicationLicenseFullName = "productionManager Pro",
                        LicenseCount = 5
                    },
                    new License
                    {
                        ApplicationId = Guid.Parse("660e8400-e29b-41d4-a716-446655440001"),
                        ApplicationLicenseFullName = "productionManager Basic",
                        LicenseCount = 10
                    }
                },
                QuantityOfPartsCoveredByTheLicenses = 50000,
                QuantityOfReleasedParts = 32145
            };

            var serialized = JsonConvert.SerializeObject(overview, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageOverview>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Period.ShouldBe(period);
            deserialized.Licenses.ShouldNotBeNull();
            deserialized.Licenses.Count.ShouldBe(2);
            deserialized.Licenses.First().ApplicationId.ShouldBe(applicationId);
            deserialized.Licenses.First().ApplicationLicenseFullName.ShouldBe("productionManager Pro");
            deserialized.Licenses.First().LicenseCount.ShouldBe(5);
            deserialized.QuantityOfPartsCoveredByTheLicenses.ShouldBe(50000);
            deserialized.QuantityOfReleasedParts.ShouldBe(32145);

            serialized.Trace();
        }

        /// <summary>
        /// Tests serialization with empty licenses collection
        /// </summary>
        [TestMethod]
        public void UsageOverview_SerializeDeserialize_EmptyLicenses()
        {
            var overview = new UsageOverview
            {
                Period = DateTime.UtcNow,
                Licenses = new Collection<License>(),
                QuantityOfPartsCoveredByTheLicenses = 0,
                QuantityOfReleasedParts = 0
            };

            var serialized = JsonConvert.SerializeObject(overview, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageOverview>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Licenses.ShouldNotBeNull();
            deserialized.Licenses.Count.ShouldBe(0);
        }

        /// <summary>
        /// Tests JSON property order for UsageOverview
        /// </summary>
        [TestMethod]
        public void UsageOverview_SerializeDeserialize_PropertyOrder()
        {
            var overview = new UsageOverview
            {
                Period = DateTime.UtcNow,
                Licenses = new Collection<License>
                {
                    new License
                    {
                        ApplicationId = Guid.NewGuid(),
                        ApplicationLicenseFullName = "Test License",
                        LicenseCount = 1
                    }
                },
                QuantityOfPartsCoveredByTheLicenses = 5000,
                QuantityOfReleasedParts = 3000
            };

            var serialized = JsonConvert.SerializeObject(overview, SerializerSettings.Default);
            var jObject = JObject.Parse(serialized);
            var properties = jObject.Properties().Select(p => p.Name).ToArray();

            // Verify JSON property order matches the Order attribute
            properties[0].ShouldBe("period");
            properties[1].ShouldBe("licenses");
            properties[2].ShouldBe("quantityOfPartsCoveredByTheLicenses");
            properties[3].ShouldBe("quantityOfReleasedParts");
        }

        /// <summary>
        /// Tests that usage statistics are correctly represented
        /// </summary>
        [TestMethod]
        public void UsageOverview_SerializeDeserialize_UsageStatistics()
        {
            var overview = new UsageOverview
            {
                Period = DateTime.UtcNow,
                Licenses = new Collection<License>
                {
                    new License
                    {
                        ApplicationId = Guid.NewGuid(),
                        ApplicationLicenseFullName = "productionManager Premium",
                        LicenseCount = 3
                    }
                },
                QuantityOfPartsCoveredByTheLicenses = 30000,
                QuantityOfReleasedParts = 25000
            };

            var serialized = JsonConvert.SerializeObject(overview, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<UsageOverview>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();

            // Verify usage ratio
            var usagePercentage = (double)deserialized.QuantityOfReleasedParts / deserialized.QuantityOfPartsCoveredByTheLicenses * 100;
            usagePercentage.ShouldBe(83.33, 0.01);

            // Verify remaining capacity
            var remainingParts = deserialized.QuantityOfPartsCoveredByTheLicenses - deserialized.QuantityOfReleasedParts;
            remainingParts.ShouldBe(5000);
        }

        #endregion
    }
}