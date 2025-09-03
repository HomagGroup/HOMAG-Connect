using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateOffcutsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            var test = new CreateOffcutsTests();
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Boards;
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypeByBoardCode("EG_H3303_ST10_19_1200.0_460.0"));
        }

        [TestMethod]
        public async Task BoardsCreateOffcutEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(MaterialAssistClient, "22");
        }

        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardCode = "XEG_H3303_ST10_19_1200.0_460.0";
            var materialCode = "EG_H3303_ST10_19";
            await CreateOffcutEntitiesSamples.Boards_CreateBoardType(MaterialAssistClient, boardCode, materialCode);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var test = new CreateOffcutsTests();
            var MaterialAssistClient = test.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntity("22");
            Assert.IsNull(await MaterialAssistClient.GetBoardEntityById("22"));
            await MaterialManagerClient.DeleteBoardType("XEG_H3303_ST10_19_1200.0_460.0");
            Assert.IsNull(await MaterialManagerClient.GetBoardTypeByBoardCode("XEG_H3303_ST10_19_1200.0_460.0"));
        }
    }
}
