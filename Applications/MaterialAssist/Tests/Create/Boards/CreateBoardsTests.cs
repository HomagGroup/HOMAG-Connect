using HomagConnect.MaterialAssist.Samples.Create.Boards;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            var test = new CreateBoardsTests();
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Boards;
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypesByBoardCodes(["MDF_H3171_12_11.6_2800.0_1310.0"]));
        }

        [TestMethod]
        public async Task BoardsCreateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "42", "50", "23");
            Assert.IsNotNull(await MaterialAssistClient.GetBoardEntityById("42"));
            Assert.IsNotNull(await MaterialAssistClient.GetBoardEntityById("50"));
            Assert.IsNotNull(await MaterialAssistClient.GetBoardEntityById("23"));
        }

        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardCode = "RP_EG_H3303_ST10_19_2800.0_2070.0";
            await CreateBoardEntitySample.Boards_CreateBoardType(MaterialAssistClient, boardCode);
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypeByBoardCode(boardCode));
        }
        
        [ClassCleanup]
        public static async Task Cleanup()
        {
            var test = new CreateBoardsTests();
            var MaterialAssistClient = test.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "23"]);
            //Assert.IsNull(await MaterialAssistClient.GetBoardEntityById("42"));
            //Assert.IsNull(await MaterialAssistClient.GetBoardEntityById("50"));
            //Assert.IsNull(await MaterialAssistClient.GetBoardEntityById("23"));

            await MaterialManagerClient.DeleteBoardType("RP_EG_H3303_ST10_19_2800.0_2070.0");
            //Assert.IsNull(await MaterialManagerClient.GetBoardTypeByBoardCode("RP_EG_H3303_ST10_19_2800.0_2070.0"));
        }
    }
}
