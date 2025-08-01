﻿using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards
{
    [TestClass]
    public class CreateBoardsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task BoardsCreateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "42", "50", "23");
        }

        
        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardCode = "RP_EG_H3303_ST10_19_2800.0_2070.0";
            await CreateBoardEntitySample.Boards_CreateBoardType(MaterialAssistClient, boardCode);
        }
        

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "23"]);

            var MaterialManagerClient = GetMaterialManagerClient();
            await MaterialManagerClient.Material.Boards.DeleteBoardType("RP_EG_H3303_ST10_19_2800.0_2070.0");
        }
    }
}
