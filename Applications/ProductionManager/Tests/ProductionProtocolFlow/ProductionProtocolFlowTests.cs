using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Contracts.OrderProgress;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using HomagConnect.ProductionManager.Contracts.ProductionProtocolFlow;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.ProductionManager.Tests.ProductionProtocolFlow
{
    /// <summary>
    /// Tests for Production Protocol Flow functionality.
    /// </summary>
    [TestClass]
    [TestCategory("ProductionManager")]
    [TestCategory("ProductionManager.ProductionProtocolFlow")]
    public class ProductionProtocolFlowTests
    {
        #region ProductionProtocolFlowDetails Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of ProductionProtocolFlowDetails
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowDetails_SerializeDeserialize_Basic()
        {
            var flowDetails = new ProductionProtocolFlowDetails();

            var serialized = JsonConvert.SerializeObject(flowDetails, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowDetails>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Workstations.ShouldNotBeNull();
            deserialized.Workstations.ShouldBeEmpty();
        }

        /// <summary>
        /// Tests serialization with complete data structure
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowDetails_SerializeDeserialize_Complete()
        {
            var workstation1Id = Guid.NewGuid();
            var workstation2Id = Guid.NewGuid();

            var itemTypeSummary1 = new Dictionary<ProductionItemType, int>
            {
                { ProductionItemType.Part, 150 },
                { ProductionItemType.AssemblyGroup, 25 }
            };

            var itemTypeSummary2 = new Dictionary<ProductionItemType, int>
            {
                { ProductionItemType.Part, 100 }
            };

            var node1 = new ProductionProtocolFlowNode
            {
                Workstation = new Workstation
                {
                    Id = workstation1Id,
                    Name = "CNC Machine 1",
                    Type = WorkstationType.CNC,
                    Group = WorkstationGroup.CNC
                }
            };

            // Populate ItemTypeSummary for node1
            node1.ItemTypeSummary = new List<KeyValuePair<ProductionItemType, int>>
            {
                new KeyValuePair<ProductionItemType, int>(ProductionItemType.Part, 150),
                new KeyValuePair<ProductionItemType, int>(ProductionItemType.AssemblyGroup, 25)
            };
            
            var node2 = new ProductionProtocolFlowNode
            {
                Workstation = new Workstation
                {
                    Id = workstation2Id,
                    Name = "Edgebander 1",
                    Type = WorkstationType.Edgebanding,
                    Group = WorkstationGroup.Edgebanding
                }
            };

            // Populate ItemTypeSummary for node2
            node2.ItemTypeSummary = new List<KeyValuePair<ProductionItemType, int>>
            {
                new(ProductionItemType.Part, 100)
            };

            var flowDetails = new ProductionProtocolFlowDetails();
            ((List<ProductionProtocolFlowNode>)flowDetails.Workstations).Add(node1);
            ((List<ProductionProtocolFlowNode>)flowDetails.Workstations).Add(node2);

            flowDetails.Trace();

            var serialized = JsonConvert.SerializeObject(flowDetails, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowDetails>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Workstations.ShouldNotBeNull();
            deserialized.Workstations.Count().ShouldBe(2);

            var deserializedNode1 = deserialized.Workstations.First();
            deserializedNode1.Workstation.ShouldNotBeNull();
            deserializedNode1.Workstation.Id.ShouldBe(workstation1Id);
            deserializedNode1.Workstation.Name.ShouldBe("CNC Machine 1");
            deserializedNode1.Workstation.Type.ShouldBe(WorkstationType.CNC);
            deserializedNode1.Workstation.Group.ShouldBe(WorkstationGroup.CNC);
            deserializedNode1.ItemTypeSummary.ShouldNotBeNull();
            deserializedNode1.ItemTypeSummary.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.Part).Value.ShouldBe(150);
            deserializedNode1.ItemTypeSummary.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.AssemblyGroup).Value.ShouldBe(25);
        }

        #endregion

        #region ProductionProtocolFlowNode Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of ProductionProtocolFlowNode
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowNode_SerializeDeserialize_Basic()
        {
            var node = new ProductionProtocolFlowNode();
            node.ItemTypeSummary = new List<KeyValuePair<ProductionItemType, int>>
            {
                new(ProductionItemType.Part, 100)
            };

            var serialized = JsonConvert.SerializeObject(node, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowNode>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.ItemTypeSummary.ShouldNotBeNull();
            deserialized.ItemTypeSummary.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.Part).Value.ShouldBe(100);
        }

        /// <summary>
        /// Tests serialization with all properties set
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowNode_SerializeDeserialize_AllProperties()
        {
            var workstationId = Guid.NewGuid();

            var node = new ProductionProtocolFlowNode
            {
                Workstation = new Workstation
                {
                    Id = workstationId,
                    Name = "Test Workstation",
                    Type = WorkstationType.CNC,
                    Group = WorkstationGroup.CNC
                }
            };

            // Populate ItemTypeSummary
            node.ItemTypeSummary = new List<KeyValuePair<ProductionItemType, int>>
            {
                new(ProductionItemType.Part, 200),
                new(ProductionItemType.AssemblyGroup, 50)
            };

            node.Trace();

            var serialized = JsonConvert.SerializeObject(node, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowNode>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.Workstation.ShouldNotBeNull();
            deserialized.Workstation.Id.ShouldBe(workstationId);
            deserialized.Workstation.Name.ShouldBe("Test Workstation");
            deserialized.Workstation.Type.ShouldBe(WorkstationType.CNC);
            deserialized.Workstation.Group.ShouldBe(WorkstationGroup.CNC);
            deserialized.ItemTypeSummary.ShouldNotBeNull();
            deserialized.ItemTypeSummary.Count().ShouldBe(2);
            deserialized.ItemTypeSummary.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.Part).Value.ShouldBe(200);
            deserialized.ItemTypeSummary.First(edgeItemTypeSummary=>edgeItemTypeSummary.Key == ProductionItemType.AssemblyGroup).Value.ShouldBe(50);
        }

        #endregion
    }
}
