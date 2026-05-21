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

            var edgeItemTypeSummary = new Dictionary<ProductionItemType, int>
            {
                { ProductionItemType.Part, 80 }
            };

            var node1 = new ProductionProtocolFlowNode
            {
                InputWorkstation = new Workstation
                {
                    Id = workstation1Id,
                    Name = "CNC Machine 1",
                    Type = WorkstationType.CNC,
                    Group = WorkstationGroup.CNC
                },
                OutputWorkstations = new List<ProductionProtocolFlowEdge>
                {
                    new ProductionProtocolFlowEdge
                    {
                        OutputWorkstation = new Workstation
                        {
                            Id = workstation2Id,
                            Name = "Edgebander 1",
                            Type = WorkstationType.Edgebanding,
                            Group = WorkstationGroup.Edgebanding
                        }
                    }
                }
            };

            // Populate ItemTypeSummary for node1
            node1.ItemTypeSummary[ProductionItemType.Part] = 150;
            node1.ItemTypeSummary[ProductionItemType.AssemblyGroup] = 25;

            // Populate ItemTypeSummary for the edge
            node1.OutputWorkstations.First().ItemTypeSummary[ProductionItemType.Part] = 80;

            var node2 = new ProductionProtocolFlowNode
            {
                InputWorkstation = new Workstation
                {
                    Id = workstation2Id,
                    Name = "Edgebander 1",
                    Type = WorkstationType.Edgebanding,
                    Group = WorkstationGroup.Edgebanding
                }
            };

            // Populate ItemTypeSummary for node2
            node2.ItemTypeSummary[ProductionItemType.Part] = 100;

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
            deserializedNode1.InputWorkstation.ShouldNotBeNull();
            deserializedNode1.InputWorkstation.Id.ShouldBe(workstation1Id);
            deserializedNode1.InputWorkstation.Name.ShouldBe("CNC Machine 1");
            deserializedNode1.InputWorkstation.Type.ShouldBe(WorkstationType.CNC);
            deserializedNode1.InputWorkstation.Group.ShouldBe(WorkstationGroup.CNC);
            deserializedNode1.ItemTypeSummary.ShouldNotBeNull();
            deserializedNode1.ItemTypeSummary[ProductionItemType.Part].ShouldBe(150);
            deserializedNode1.ItemTypeSummary[ProductionItemType.AssemblyGroup].ShouldBe(25);
            deserializedNode1.OutputWorkstations.ShouldNotBeNull();
            deserializedNode1.OutputWorkstations.Count().ShouldBe(1);

            var deserializedEdge = deserializedNode1.OutputWorkstations.First();
            deserializedEdge.OutputWorkstation.ShouldNotBeNull();
            deserializedEdge.OutputWorkstation.Id.ShouldBe(workstation2Id);
            deserializedEdge.OutputWorkstation.Name.ShouldBe("Edgebander 1");
            deserializedEdge.OutputWorkstation.Type.ShouldBe(WorkstationType.Edgebanding);
            deserializedEdge.OutputWorkstation.Group.ShouldBe(WorkstationGroup.Edgebanding);
            deserializedEdge.ItemTypeSummary.ShouldNotBeNull();
            deserializedEdge.ItemTypeSummary[ProductionItemType.Part].ShouldBe(80);
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
            node.ItemTypeSummary[ProductionItemType.Part] = 100;

            var serialized = JsonConvert.SerializeObject(node, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowNode>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.ItemTypeSummary.ShouldNotBeNull();
            deserialized.ItemTypeSummary[ProductionItemType.Part].ShouldBe(100);
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
                InputWorkstation = new Workstation
                {
                    Id = workstationId,
                    Name = "Test Workstation",
                    Type = WorkstationType.CNC,
                    Group = WorkstationGroup.CNC
                },
                OutputWorkstations = new List<ProductionProtocolFlowEdge>()
            };

            // Populate ItemTypeSummary
            node.ItemTypeSummary[ProductionItemType.Part] = 200;
            node.ItemTypeSummary[ProductionItemType.AssemblyGroup] = 50;

            node.Trace();

            var serialized = JsonConvert.SerializeObject(node, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowNode>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.InputWorkstation.ShouldNotBeNull();
            deserialized.InputWorkstation.Id.ShouldBe(workstationId);
            deserialized.InputWorkstation.Name.ShouldBe("Test Workstation");
            deserialized.InputWorkstation.Type.ShouldBe(WorkstationType.CNC);
            deserialized.InputWorkstation.Group.ShouldBe(WorkstationGroup.CNC);
            deserialized.ItemTypeSummary.ShouldNotBeNull();
            deserialized.ItemTypeSummary.Count.ShouldBe(2);
            deserialized.ItemTypeSummary[ProductionItemType.Part].ShouldBe(200);
            deserialized.ItemTypeSummary[ProductionItemType.AssemblyGroup].ShouldBe(50);
            deserialized.OutputWorkstations.ShouldNotBeNull();
            deserialized.OutputWorkstations.ShouldBeEmpty();
        }

        #endregion

        #region ProductionProtocolFlowEdge Serialization Tests

        /// <summary>
        /// Tests basic serialization and deserialization of ProductionProtocolFlowEdge
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowEdge_SerializeDeserialize_Basic()
        {
            var edge = new ProductionProtocolFlowEdge();

            var serialized = JsonConvert.SerializeObject(edge, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowEdge>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
        }

        /// <summary>
        /// Tests serialization with all properties set
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowEdge_SerializeDeserialize_AllProperties()
        {
            var workstationId = Guid.NewGuid();

            var edge = new ProductionProtocolFlowEdge
            {
                OutputWorkstation = new Workstation
                {
                    Id = workstationId,
                    Name = "Output Workstation",
                    Type = WorkstationType.Edgebanding,
                    Group = WorkstationGroup.Edgebanding
                }
            };

            // Populate ItemTypeSummary
            edge.ItemTypeSummary[ProductionItemType.Part] = 75;
            edge.ItemTypeSummary[ProductionItemType.AssemblyGroup] = 15;

            edge.Trace();

            var serialized = JsonConvert.SerializeObject(edge, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowEdge>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.OutputWorkstation.ShouldNotBeNull();
            deserialized.OutputWorkstation.Id.ShouldBe(workstationId);
            deserialized.OutputWorkstation.Name.ShouldBe("Output Workstation");
            deserialized.OutputWorkstation.Type.ShouldBe(WorkstationType.Edgebanding);
            deserialized.OutputWorkstation.Group.ShouldBe(WorkstationGroup.Edgebanding);
            deserialized.ItemTypeSummary.ShouldNotBeNull();
            deserialized.ItemTypeSummary.Count.ShouldBe(2);
            deserialized.ItemTypeSummary[ProductionItemType.Part].ShouldBe(75);
            deserialized.ItemTypeSummary[ProductionItemType.AssemblyGroup].ShouldBe(15);
        }

        /// <summary>
        /// Tests serialization with multiple item types
        /// </summary>
        [TestMethod]
        public void ProductionProtocolFlowEdge_SerializeDeserialize_MultipleItemTypes()
        {
            var edge = new ProductionProtocolFlowEdge();

            // Populate ItemTypeSummary
            edge.ItemTypeSummary[ProductionItemType.Part] = 100;
            edge.ItemTypeSummary[ProductionItemType.AssemblyGroup] = 20;

            var serialized = JsonConvert.SerializeObject(edge, SerializerSettings.Default);
            var deserialized = JsonConvert.DeserializeObject<ProductionProtocolFlowEdge>(serialized, SerializerSettings.Default);

            deserialized.ShouldNotBeNull();
            deserialized.ItemTypeSummary.ShouldNotBeNull();
            deserialized.ItemTypeSummary.Count.ShouldBe(2);
            deserialized.ItemTypeSummary[ProductionItemType.Part].ShouldBe(100);
            deserialized.ItemTypeSummary[ProductionItemType.AssemblyGroup].ShouldBe(20);
        }

        #endregion
    }
}
