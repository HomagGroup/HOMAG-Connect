using HomagConnect.MaterialManager.Samples.Update.Boards;

namespace HomagConnect.MaterialManager.Tests.Update.Boards
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Boards")]
    public class UpdateBoardTypeTests : MaterialManagerTestBase
    {
        /// <summary />
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            //TODO: check for the exact board type
            var classInstance = new UpdateBoardTypeTests();
            await classInstance.EnsureBoardTypeExist("Test_Data_HPL_F274_9_15.0");
        }

        [TestMethod]
        public async Task BoardsUpdateBoardType()
        {
            // TODO: update a random value and assert
            var materialManagerClient = GetMaterialManagerClient();
            var boardCode = "Test_Data_HPL_F274_9_15.0_2800.0_2070.0"; 
            await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient.Material.Boards, boardCode);
        }
    }
}
