using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialAssist.Samples.Get.Boards;
using HomagConnect.MaterialAssist.Samples.Update.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards
{
    public class UpdateBoardsTests : MaterialAssistTestBase
    {

        [ClassInitialize]
        public async Task Initialize()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "42", "50", "23");
        }

        [TestMethod]
        public async Task BoardsUpdateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsStoreBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_StoreBoardEntity(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveAllBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveAllBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveSubsetBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveSubsetBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveSingleBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveSingleBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, ["42", "50", "23"]);
        }
    }
}
