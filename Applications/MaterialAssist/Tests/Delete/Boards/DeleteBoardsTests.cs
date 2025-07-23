using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Delete.Boards
{
    public class DeleteBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public async Task Initialize()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "42", "50", "23");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardId = "42";
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntity(MaterialAssistClient, boardId);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            List<string> boardIds = ["50", "23"];
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, boardIds);
        }
    }
}
