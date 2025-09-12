using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "16");
        }
        /*
        [TestMethod]
        public async Task create()
        {
            var client = GetMaterialManagerClient().Material.Edgebands;
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "ABS_White_2mm",
                Height = 20,
                Thickness = 1.0,
                DefaultLength = 23.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            await client.CreateEdgebandType(edgebandTypeRequest);
        }
        */
        [TestCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity("16");
            /*
            var client = GetMaterialManagerClient().Material.Edgebands;
            await client.DeleteEdgebandType("ABS_White_2mm");*/
        }
    }
}
