using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Delete.Edgebands
{
    public class DeleteEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public async Task Initialie()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "42",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "50",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "23",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
        }
        
        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, "42");
        }

        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntities(MaterialAssistClient, ["50", "23"]);
        }

    }
}
