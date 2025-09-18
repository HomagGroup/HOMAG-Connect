using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
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
            var classInstance = new UpdateBoardTypeTests();
            await classInstance.EnsureBoardTypeExist("Test_Data_HPL_F274_9_15.0");
        }

        [TestMethod]
        public async Task BoardsUpdateBoardType()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var boardCode = "Test_Data_HPL_F274_9_15.0_2800.0_2070.0"; 
            await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient.Material.Boards, boardCode);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new UpdateBoardTypeTests();
            var materialManagerClient = classInstance.GetMaterialManagerClient();
            await materialManagerClient.Material.Boards.DeleteBoardType("Test_Data_HPL_F274_9_15.0_2800.0_2070.0");
        }
    }
}
