using HomagConnect.Base;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.ProductionProtocol;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Tests.ProductionProtocol
{
    /// <summary />
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.ProductionProtocol")]
    public class ProductionProtocolTests
    {
        /// <summary />
        public required TestContext TestContext { get; set; }

        /// <summary />
        [TestMethod]
        public void ProductionProtocol_Cutting_Part_Serialization()
        {
            var processedPartCutting = new ProcessedPartCutting
            {
                Timestamp = DateTimeOffset.Now,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                BoardCode = "P2_Gold_Craft_Oak_19.0_2800.0_2070.0"
            };

            TestContext.AddResultFile(processedPartCutting.TraceToFile("processedPartCutting").FullName);

            var processedPartCuttingSerialized = JsonConvert.SerializeObject(processedPartCutting, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartCuttingSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPartCutting.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPartCutting;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPartCutting.Timestamp, processedPartDeserialized.Timestamp);
        }

        /// <summary />
        [TestMethod]
        public void ProductionProtocol_CNC_Part_Serialization()
        {
            var completedAt = DateTimeOffset.Now;
            var startedAt = DateTimeOffset.Now.AddMinutes(-10);
            var processedPartCnc = new ProcessedPartCnc()
            {
                Timestamp = completedAt,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                CompletedAt = completedAt,
                StartedAt = startedAt,
                ProgramName = "Test.mpr",
                OrderName = "TestOrder",
                OrderId = Guid.NewGuid(),
                ProgramDuration = completedAt - startedAt
            };

            TestContext.AddResultFile(processedPartCnc.TraceToFile("ProcessedPartCnc").FullName);

            var processedPartCncSerialized = JsonConvert.SerializeObject(processedPartCnc, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartCncSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPartCnc.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPartCnc;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPartCnc.Timestamp, processedPartDeserialized.Timestamp);
            Assert.AreEqual(processedPartCnc.CompletedAt, processedPartDeserialized.CompletedAt);
            Assert.AreEqual(processedPartCnc.CustomerName, processedPartDeserialized.CustomerName);
            Assert.AreEqual(processedPartCnc.MachineType, MachineType.Cnc);
            Assert.AreEqual(processedPartCnc.Preview, processedPartDeserialized.Preview);
        }
    }
}