using HomagConnect.MaterialAssist.Samples.Create.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task EdgebandsCreateEdgebandType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandType(MaterialAssistClient, "White Edgeband 1mm");
        }

        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "42");
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var MaterialManagerClient = GetMaterialManagerClient().Material.Edgebands;

            await MaterialAssistClient.DeleteEdgebandEntity("42");
            try
            {
                await MaterialAssistClient.GetEdgebandEntityById("42");
                throw new Exception("Edgeband entity was not deleted. Cleanup failed");
            }
            catch {/* Expected exception */}

            await MaterialManagerClient.DeleteEdgebandType("White Edgeband 1mm");
            try
            {
                await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("White Edgeband 1mm");
                throw new Exception("Edgeband type was not deleted. Cleanup failed");
            }
            catch {/* Expected exception */}
        }
    }
}
