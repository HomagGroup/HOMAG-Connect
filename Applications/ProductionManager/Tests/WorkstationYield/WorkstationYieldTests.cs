using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using HomagConnect.ProductionManager.Contracts.WorkstationYield;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.WorkstationYield
{
    /// <summary>
    /// Tests for Production Protocol Flow functionality.
    /// </summary>
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.WorkstationYield")]
    public class WorkstationYieldTests 
    {
        #region WorkstationYield Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of WorkstationYields
        /// </summary>
        [TestMethod]
        public void WorkstationYields_SerializeDeserialize_Basic()
        {
            var yield = new WorkstationsYield();

            var serialized = JsonConvert.SerializeObject(yield, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<WorkstationsYield>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Yields.ShouldNotBeNull();
            deserialized.Yields.ShouldBeEmpty();
        }

        /// <summary>
        /// Tests serialization with complete data structure
        /// </summary>
        [TestMethod]
        public void WorkstationYields_SerializeDeserialize_Complete()
        {
            var workstation1Id = Guid.NewGuid();
            var workstation2Id = Guid.NewGuid();

            var node1 = new Contracts.WorkstationYield.WorkstationYield
            {
                Workstation = new Workstation
                {
                    Id = workstation1Id,
                    Name = "CNC Machine 1",
                    Type = WorkstationType.CNC,
                    Group = WorkstationGroup.CNC
                }
            };

            // Populate Yields for node1
            node1.Yields = new List<KeyValuePair<ProductionItemType, int>>
            {
                new KeyValuePair<ProductionItemType, int>(ProductionItemType.Part, 150),
                new KeyValuePair<ProductionItemType, int>(ProductionItemType.AssemblyGroup, 25)
            };
            
            var node2 = new Contracts.WorkstationYield.WorkstationYield
            {
                Workstation = new Workstation
                {
                    Id = workstation2Id,
                    Name = "Edgebander 1",
                    Type = WorkstationType.Edgebanding,
                    Group = WorkstationGroup.Edgebanding
                }
            };

            // Populate Yields for node2
            node2.Yields = new List<KeyValuePair<ProductionItemType, int>>
            {
                new(ProductionItemType.Part, 100)
            };

            var yield = new WorkstationsYield();
            ((List<Contracts.WorkstationYield.WorkstationYield>)yield.Yields).Add(node1);
            ((List<Contracts.WorkstationYield.WorkstationYield>)yield.Yields).Add(node2);

            yield.Trace();

            var serialized = JsonConvert.SerializeObject(yield, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<WorkstationsYield>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Yields.ShouldNotBeNull();
            deserialized.Yields.Count().ShouldBe(2);

            var deserializedNode1 = deserialized.Yields.First();
            deserializedNode1.Workstation.ShouldNotBeNull();
            deserializedNode1.Workstation.Id.ShouldBe(workstation1Id);
            deserializedNode1.Workstation.Name.ShouldBe("CNC Machine 1");
            deserializedNode1.Workstation.Type.ShouldBe(WorkstationType.CNC);
            deserializedNode1.Workstation.Group.ShouldBe(WorkstationGroup.CNC);
            deserializedNode1.Yields.ShouldNotBeNull();
            deserializedNode1.Yields.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.Part).Value.ShouldBe(150);
            deserializedNode1.Yields.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.AssemblyGroup).Value.ShouldBe(25);
        }

        #endregion

        #region WorkstationYieldNode Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of WorkstationYieldNode
        /// </summary>
        [TestMethod]
        public void WorkstationYieldNode_SerializeDeserialize_Basic()
        {
            var node = new Contracts.WorkstationYield.WorkstationYield();
            node.Yields = new List<KeyValuePair<ProductionItemType, int>>
            {
                new(ProductionItemType.Part, 100)
            };

            var serialized = JsonConvert.SerializeObject(node, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<Contracts.WorkstationYield.WorkstationYield>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Yields.ShouldNotBeNull();
            deserialized.Yields.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.Part).Value.ShouldBe(100);
        }

        /// <summary>
        /// Tests serialization with all properties set
        /// </summary>
        [TestMethod]
        public void WorkstationYieldNode_SerializeDeserialize_AllProperties()
        {
            var workstationId = Guid.NewGuid();

            var node = new Contracts.WorkstationYield.WorkstationYield
            {
                Workstation = new Workstation
                {
                    Id = workstationId,
                    Name = "Test Workstation",
                    Type = WorkstationType.CNC,
                    Group = WorkstationGroup.CNC
                }
            };

            // Populate Yields
            node.Yields = new List<KeyValuePair<ProductionItemType, int>>
            {
                new(ProductionItemType.Part, 200),
                new(ProductionItemType.AssemblyGroup, 50)
            };

            node.Trace();

            var serialized = JsonConvert.SerializeObject(node, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<Contracts.WorkstationYield.WorkstationYield>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Workstation.ShouldNotBeNull();
            deserialized.Workstation.Id.ShouldBe(workstationId);
            deserialized.Workstation.Name.ShouldBe("Test Workstation");
            deserialized.Workstation.Type.ShouldBe(WorkstationType.CNC);
            deserialized.Workstation.Group.ShouldBe(WorkstationGroup.CNC);
            deserialized.Yields.ShouldNotBeNull();
            deserialized.Yields.Count().ShouldBe(2);
            deserialized.Yields.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.Part).Value.ShouldBe(200);
            deserialized.Yields.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.AssemblyGroup).Value.ShouldBe(50);
        }

        #endregion
    }
}
