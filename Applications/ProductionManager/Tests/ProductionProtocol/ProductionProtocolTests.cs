﻿using HomagConnect.Base;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
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
            var processedPartCutting = new ProcessedPartDividing
            {
                Timestamp = DateTimeOffset.Now,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                OrderName = "TestOrder",
                BoardCode = "P2_Gold_Craft_Oak_19.0_2800.0_2070.0"
            };

            TestContext.AddResultFile(processedPartCutting.TraceToFile("processedPartCutting").FullName);

            var processedPartCuttingSerialized = JsonConvert.SerializeObject(processedPartCutting, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartCuttingSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPartCutting.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPartDividing;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPartCutting.Timestamp, processedPartDeserialized.Timestamp);
            Assert.AreEqual(processedPartCutting.MachineType, MachineType.Cutting);
        }

        /// <summary />
        [TestMethod]
        public void ProductionProtocol_Base_Part_Serialization()
        {
            var processedPart = new ProcessedPart
            {
                Timestamp = DateTimeOffset.Now,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                OrderName = "TestOrder",
            };

            TestContext.AddResultFile(processedPart.TraceToFile("processedPart").FullName);

            var processedPartSerialized = JsonConvert.SerializeObject(processedPart, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPart.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPart;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPart.Timestamp, processedPartDeserialized.Timestamp);
        }

        /// <summary />
        [TestMethod]
        public void ProductionProtocol_Base_Position_Serialization()
        {
            var processedPart = new ProcessedPart
            {
                Timestamp = DateTimeOffset.Now,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                OrderName = "TestOrder"
            };

            TestContext.AddResultFile(processedPart.TraceToFile("processedPart").FullName);

            var processedPartCuttingSerialized = JsonConvert.SerializeObject(processedPart, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartCuttingSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPart.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPart;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPart.Timestamp, processedPartDeserialized.Timestamp);
        }

        /// <summary />
        [TestMethod]
        public void ProductionProtocol_Nesting_Part_Serialization()
        {
            var processedPartNesting = new ProcessedPartDividing(MachineType.Nesting)
            {
                Timestamp = DateTimeOffset.Now,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                OrderName = "TestOrder",
                BoardCode = "P2_Gold_Craft_Oak_19.0_2800.0_2070.0",
            };

            TestContext.AddResultFile(processedPartNesting.TraceToFile("processedPartNesting").FullName);

            var processedPartCuttingSerialized = JsonConvert.SerializeObject(processedPartNesting, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartCuttingSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPartNesting.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPartDividing;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPartNesting.Timestamp, processedPartDeserialized.Timestamp);
            Assert.AreEqual(processedPartNesting.MachineType, MachineType.Nesting);
        }

        /// <summary />
        [TestMethod]
        public void ProductionProtocol_WrongMachineType_Part_Serialization()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new ProcessedPartDividing(MachineType.Edgebanding)
                {
                    Timestamp = DateTimeOffset.Now,
                    SubscriptionId = Guid.NewGuid(),
                    Description = "BTH-CAB-END-LEFT",
                    Length = 162,
                    Width = 600,
                    MaterialCode = "P2_Gold_Craft_Oak_19.0",
                    Quantity = 2,
                    OrderName = "TestOrder",
                    BoardCode = "P2_Gold_Craft_Oak_19.0_2800.0_2070.0"
                }
            );
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
        
        /// <summary />
        [TestMethod]
        public void ProductionProtocol_Edgebanding_Part_Serialization()
        {
            var completedAt = DateTimeOffset.Now;
            var processedPartEdgebanding = new ProcessedPartEdgebanding()
            {
                Timestamp = completedAt,
                SubscriptionId = Guid.NewGuid(),
                Description = "BTH-CAB-END-LEFT",
                Length = 162,
                Width = 600,
                MaterialCode = "P2_Gold_Craft_Oak_19.0",
                Quantity = 2,
                OrderName = "TestOrder",
                OrderId = Guid.NewGuid(),
                EdgeBack = "PP_Eiche_hell_1.30_19.0_HM"
            };

            TestContext.AddResultFile(processedPartEdgebanding.TraceToFile("ProcessedPartEdgebanding").FullName);

            var processedPartEdgebandingSerialized = JsonConvert.SerializeObject(processedPartEdgebanding, SerializerSettings.Default);
            var processedItemDeserialized = JsonConvert.DeserializeObject<ProcessedItem>(processedPartEdgebandingSerialized);

            Assert.IsNotNull(processedItemDeserialized);
            Assert.AreEqual(processedPartEdgebanding.GetType(), processedItemDeserialized.GetType());

            var processedPartDeserialized = processedItemDeserialized as ProcessedPartEdgebanding;

            Assert.IsNotNull(processedPartDeserialized);
            Assert.AreEqual(processedPartEdgebanding.Timestamp, processedPartDeserialized.Timestamp);
            Assert.AreEqual(processedPartEdgebanding.CustomerName, processedPartDeserialized.CustomerName);
            Assert.AreEqual(processedPartEdgebanding.OrderName, processedPartDeserialized.OrderName);
            Assert.AreEqual(processedPartEdgebanding.EdgeBack, processedPartDeserialized.EdgeBack);
            Assert.AreEqual(processedPartEdgebanding.MachineType, MachineType.Edgebanding);
        }
    }
}