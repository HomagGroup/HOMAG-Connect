﻿using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using System;
using System.Collections;
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
            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "50",
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "23",
                BoardCode = "RP_EG_H3303_ST10_19_2800.0_2070.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntity(MaterialAssistClient, "42");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, ["50", "23"]);
        }
    }
}
