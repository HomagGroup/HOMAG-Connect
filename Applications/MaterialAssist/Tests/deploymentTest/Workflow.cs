using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Tests;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.deploymentTest
{
    [TestClass]
    public class Workflow : MaterialAssistTestBase
    {
        public async Task createClient()
        {
        var MaterailAssistClient = GetMaterialAssistClient();
        var MaterialManagerClient = GetMaterialManagerClient();
        }
    
        [TestMethod]
        public async Task createBoardTypes()
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                MaterialCode = "HPL_F274_9_12.0",
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                Length = 4100.0,
                Width = 650.0,
                Thickness = 19.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            await MaterialManagerClient.CreateBoardType(boardTypeRequest);

            var boardTypeRequest2 = new MaterialManagerRequestBoardType
            {
                MaterialCode = "P2_F204_75_38.0",
                BoardCode = "P2_F204_75_38.0_4100.0_600.0",
                Length = 4100.0,
                Width = 600.0,
                Thickness = 38.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Chipboard,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            await MaterialManagerClient.CreateBoardType(boardTypeRequest2);

            var boardTypeRequest3 = new MaterialManagerRequestBoardType
            {
                MaterialCode = "HPL_Natural_Carini_Walnut_4.0",
                BoardCode = "HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
                Length = 2790.0,
                Width = 2060.0,
                Thickness = 4.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            await MaterialManagerClient.CreateBoardType(boardTypeRequest3);

            var boardTypeResponse = await MaterialManagerClient.GetBoardType(boardTypeRequest.BoardCode);
            Assert.IsNotNull(boardTypeResponse);
        }

        [TestMethod]
        public async Task assertCreateBoardTypes()
        {
            await MaterialManagerClient.GetBoardTypeByBoardCode("HPL_F274_9_12.0_4100.0_650.0");
            await MaterialManagerClient.GetBoardTypeByBoardCode("P2_F204_75_38.0_4100.0_600.0");
            await MaterialManagerClient.GetBoardTypeByBoardCode("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
        }

        [TestMethod]
        public async Task createEdgebandTypes()
        {
            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "ABS_Multiplex schwarz_1.00_23.0_NN",
                Height = 23.0,
                Thickness = 1.0,
                DefaultLength = 75.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.Other,
            };
            await MaterialManagerClient.CreateEdgebandType(edgebandTypeRequest);

            var edgebandTypeRequest2 = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "NN_Schwarz_16.50_24.5_HM",
                Height = 24.5,
                Thickness = 16.5,
                DefaultLength = 0.0,
                MaterialCategory = EdgebandMaterialCategory.Others,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await MaterialManagerClient.CreateEdgebandType(edgebandTypeRequest2);

            var edgebandTypeRequest3 = new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = "ABS_A_Dash_of_Freedom_1.00_100.0_HM",
                Height = 100.0,
                Thickness = 1.0,
                DefaultLength = 225.0,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await MaterialManagerClient.CreateEdgebandType(edgebandTypeRequest3);
        }

        [TestMethod]
        public async Task assertCreateEdgebandTypes()
        {
            await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("ABS_Multiplex schwarz_1.00_23.0_NN");
            await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("NN_Schwarz_16.50_24.5_HM");
            await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
        }

        [TestMethod]
        public async Task createBoardEntities()
        {
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                ManagementType = ManagementType.Single,
                Quantity = 5
            };
            await MaterialAssistClient.CreateBoardEntity(boardEntityRequest);

            var boardEntityRequest2 = new MaterialAssistRequestBoardEntity()
            {
                Id = "22",
                BoardCode = "HP2_F204_75_38.0_4100.0_600.0",
                ManagementType = ManagementType.Stack,
                Quantity = 10
            };
            await MaterialAssistClient.CreateBoardEntity(boardEntityRequest2);

            var boardEntityRequest3 = new MaterialAssistRequestBoardEntity()
            {
                Id = "37",
                BoardCode = "HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 2
            };
            await MaterialAssistClient.CreateBoardEntity(boardEntityRequest3);
        }

        [TestMethod]
        public async Task createEdgebandEntities()
        {
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "18",
                EdgebandCode = "ABS_Multiplex schwarz_1.00_23.0_NN",
                ManagementType = ManagementType.Single,
                Quantity = 2,
                Length = 75.0,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "59",
                EdgebandCode = "NN_Schwarz_16.50_24.5_HM",
                ManagementType = ManagementType.Stack,
                Quantity = 5,
                Length = 75.0,
                CurrentThickness = 16.5
                };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);
            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "63",
                EdgebandCode = "ABS_A_Dash_of_Freedom_1.00_100.0_HM",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 3,
                Length = 225.0,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
        }
    }