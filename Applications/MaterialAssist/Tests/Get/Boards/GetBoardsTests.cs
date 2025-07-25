using HomagConnect.Base.TestBase;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialAssist.Samples.Get.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Get.Boards
{
    public class GetBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public async Task Initialize()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "42", "50", "23");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntities(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntityById()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var id = "42";
            await GetBoardEntitiesSamples.Boards_GetBoardEntityById(MaterialAssistClient, id);
        }

        public async Task BoardsGetBoardEntitiesById()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            List<string> ids = ["42", "50", "23"];
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(MaterialAssistClient, ids);
        }

        public async Task BoardsGetBoardEntitiesByBoardCode()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardCode = "MDF_H3171_12_11.6_2800.0_1310.0"; // make it boardCode
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(MaterialAssistClient, boardCode);
        }

        public async Task BoardsGetBoardEntitiesByBoardCodes()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            List<string> allBoardCodes = ["MDF_H3171_12_11.6_2800.0_1310.0", "RP_EG_H3303_ST10_19_2800.0_2070.0"];
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(MaterialAssistClient, allBoardCodes);
        }

        public async Task BoardsGetBoardEntitiesByMaterialCode()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var materialCode = "EG_H3303_ST10_19"; // make it materialCode 
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(MaterialAssistClient, materialCode);
        }

        public async Task BoardsGetBoardEntitiesByMaterialCodes()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            List<string> allMaterialCodes = ["EG_H3303_ST10_19"];
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(MaterialAssistClient, allMaterialCodes);
        }

        public async Task BoardsGetStorageLocations()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetStorageLocations(MaterialAssistClient);
        }

        public async Task BoardsGetWorkstations()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetWorkstations(MaterialAssistClient);
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, ["42", "50", "23"]);
        }
    }
}
