using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using HomagConnect.MaterialAssist.Tests.Update.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Delete.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class DeleteEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialie(TestContext testContext)
        {
            var classInstance = new DeleteEdgebandsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;

            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType()
            {
                EdgebandCode = "ABS_White_5mm",
                Height = 20,
                Thickness = 5.0,
                DefaultLength = 500.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandEntity = await MaterialAssistClient.CreateEdgebandType(edgebandTypeRequest);

            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "42",
                EdgebandCode = "ABS_White_5mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 500,
                CurrentThickness = 5.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "50",
                EdgebandCode = "ABS_White_5mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 500,
                CurrentThickness = 5.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "23",
                EdgebandCode = "ABS_White_5mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 500,
                CurrentThickness = 5.0
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

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new DeleteEdgebandsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Edgebands;
            await MaterialManagerClient.DeleteEdgebandType("ABS_White_5mm");
        }

    }
}
