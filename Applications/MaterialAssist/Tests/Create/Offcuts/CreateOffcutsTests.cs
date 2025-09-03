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
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntity("22");

            var MaterialManagerClient = GetMaterialManagerClient();
            await MaterialManagerClient.Material.Boards.DeleteBoardType("XEG_H3303_ST10_19_1200.0_460.0");
        }

    }
}
