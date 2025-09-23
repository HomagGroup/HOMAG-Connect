using HomagConnect.MaterialAssist.Samples.Update.Boards;
using Newtonsoft.Json.Linq;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards
{ 
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]

    public class UpdateBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateBoardsTests();
            await classInstance.EnsureBoardTypeExist("MDF_H3171_12_19.0");
            await classInstance.EnsureBoardEntityExist("41111", "MDF_H3171_12_19.0_2800_2070");
        }
        
        [TestMethod]
        public async Task BoardsUpdateBoardEntity()
        {
            Random random = new Random();

            double RandomBetween(double min, double max)
            {
                return random.NextDouble() * (max - min) + min;
            }

            double length = Math.Round(RandomBetween(100.0, 2000.0), 2); 
            double width = Math.Round(RandomBetween(100.0, 2000.0), 2);

            var materialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(materialAssistClient, length, width);

            var checkBoardEntity = await materialAssistClient.GetBoardEntityById("41111");
            Assert.AreEqual(length, checkBoardEntity.Length);
            Assert.AreEqual(width, checkBoardEntity.Width);
        }
    }
}