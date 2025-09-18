using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Update.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class UpdateEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateEdgebandsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Edgebands;

            // TODO: ensure data is there
            try
            {
                var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
                {
                    EdgebandCode = "Test_Data_ABS_White_1mm",
                    Height = 20,
                    Thickness = 1.0,
                    DefaultLength = 23.0,
                    MaterialCategory = EdgebandMaterialCategory.Veneer,
                    Process = EdgebandingProcess.Other,
                };
                await MaterialManagerClient.CreateEdgebandType(edgebandTypeRequest);
            }
            catch { }

            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "43",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "44",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "45",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
        }
        
        [TestMethod]
        public async Task EdgebandsUpdateEdgebandEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_UpdateEdgebandEntity(materialAssistClient);
        }
    }
}