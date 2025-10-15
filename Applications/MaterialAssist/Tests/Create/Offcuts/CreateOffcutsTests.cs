using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateOffcutsTests : MaterialAssistTestBase
    {
        [TestInitialize]
        public async Task Initialize()
        {
            await EnsureBoardTypeExist("EG_H3303_ST10_19", 1000, 500, 19, true);
        }

        [TestMethod]
        public async Task BoardsCreateOffcutEntity()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(materialAssistClient, "11114");

            var offcutEntity = await materialAssistClient.GetBoardEntityById("11114");
            Assert.AreEqual("11114", offcutEntity.Id);
            Assert.AreEqual(ManagementType.Single, offcutEntity.ManagementType);
            Assert.AreEqual(1, offcutEntity.Quantity);
            Assert.AreEqual(1000.0, offcutEntity.Length);
            Assert.AreEqual(500.0, offcutEntity.Width);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await materialAssistClient.DeleteBoardEntity("11114");
        }
    }
}
