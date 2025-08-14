using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.DeploymentTest
{
    public class firstWorkflow : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task createBoardTypes()
        {
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;

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
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest.BoardCode));
        

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
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest2.BoardCode));

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
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest3.BoardCode));
        }

        [TestMethod]
        public async Task createEdgebandTypes()
        {
            var MaterialManagerClient = GetMaterialManagerClient().Material.Edgebands;

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
            Assert.IsNotNull(await MaterialManagerClient.GetEdgebandTypeByEdgebandCode(edgebandTypeRequest.EdgebandCode));

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
            Assert.IsNotNull(await MaterialManagerClient.GetEdgebandTypeByEdgebandCode(edgebandTypeRequest2.EdgebandCode));

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
            Assert.IsNotNull(await MaterialManagerClient.GetEdgebandTypeByEdgebandCode(edgebandTypeRequest3.EdgebandCode));
        }

        [TestMethod]
        public async Task createBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                ManagementType = ManagementType.Single,
                Quantity = 5
            };
            await MaterialAssistClient.CreateBoardEntity(boardEntityRequest);
            Assert.IsNotNull(await MaterialAssistClient.GetBoardEntityById(boardEntityRequest.Id));

            var boardEntityRequest2 = new MaterialAssistRequestBoardEntity()
            {
                Id = "22",
                BoardCode = "HP2_F204_75_38.0_4100.0_600.0",
                ManagementType = ManagementType.Stack,
                Quantity = 10
            };
            await MaterialAssistClient.CreateBoardEntity(boardEntityRequest2);
            Assert.IsNotNull(await MaterialAssistClient.GetBoardEntityById(boardEntityRequest2.Id));

            var boardEntityRequest3 = new MaterialAssistRequestBoardEntity()
            {
                Id = "37",
                BoardCode = "HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 2
            };
            await MaterialAssistClient.CreateBoardEntity(boardEntityRequest3);
            Assert.IsNotNull(await MaterialAssistClient.GetBoardEntityById(boardEntityRequest3.Id));
        }

        [TestMethod]
        public async Task createEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
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
            Assert.IsNotNull(await MaterialAssistClient.GetEdgebandEntityById(edgebandEntityRequest.Id));

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
            Assert.IsNotNull(await MaterialAssistClient.GetEdgebandEntityById(edgebandEntityRequest2.Id));

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
            Assert.IsNotNull(await MaterialAssistClient.GetEdgebandEntityById(edgebandEntityRequest3.Id));
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialManagerClient = GetMaterialManagerClient().Material;
            var MaterialAssistClient = GetMaterialAssistClient();

            //board entities
            await MaterialAssistClient.Boards.DeleteBoardEntity("42");
            Assert.IsNull(await MaterialAssistClient.Boards.GetBoardEntityById("42"));

            await MaterialAssistClient.Boards.DeleteBoardEntity("22");
            Assert.IsNull(await MaterialAssistClient.Boards.GetBoardEntityById("22"));

            await MaterialAssistClient.Boards.DeleteBoardEntity("37");
            Assert.IsNull(await MaterialAssistClient.Boards.GetBoardEntityById("37"));

            //edgeband entities
            await MaterialAssistClient.Edgebands.DeleteEdgebandEntity("18");
            Assert.IsNull(await MaterialAssistClient.Edgebands.GetEdgebandEntityById("18"));

            await MaterialAssistClient.Edgebands.DeleteEdgebandEntity("59");
            Assert.IsNull(await MaterialAssistClient.Edgebands.GetEdgebandEntityById("59"));

            await MaterialAssistClient.Edgebands.DeleteEdgebandEntity("63");
            Assert.IsNull(await MaterialAssistClient.Edgebands.GetEdgebandEntityById("63"));

            //board types
            await MaterialManagerClient.Boards.DeleteBoardType("HPL_F274_9_12.0_4100.0_650.0");
            Assert.IsNull(await MaterialManagerClient.Boards.GetBoardTypeByBoardCode("HPL_F274_9_12.0_4100.0_650.0"));

            await MaterialManagerClient.Boards.DeleteBoardType("P2_F204_75_38.0_4100.0_600.0");
            Assert.IsNull(await MaterialManagerClient.Boards.GetBoardTypeByBoardCode("P2_F204_75_38.0_4100.0_600.0"));

            await MaterialManagerClient.Boards.DeleteBoardType("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            Assert.IsNull(await MaterialManagerClient.Boards.GetBoardTypeByBoardCode("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0"));

            //edgeband types
            await MaterialManagerClient.Edgebands.DeleteEdgebandType("ABS_Multiplex schwarz_1.00_23.0_NN");
            Assert.IsNull(await MaterialManagerClient.Edgebands.GetEdgebandTypeByEdgebandCode("ABS_Multiplex schwarz_1.00_23.0_NN"));

            await MaterialManagerClient.Edgebands.DeleteEdgebandType("NN_Schwarz_16.50_24.5_HM");
            Assert.IsNull(await MaterialManagerClient.Edgebands.GetEdgebandTypeByEdgebandCode("NN_Schwarz_16.50_24.5_HM"));

            await MaterialManagerClient.Edgebands.DeleteEdgebandType("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
            Assert.IsNull(await MaterialManagerClient.Edgebands.GetEdgebandTypeByEdgebandCode("ABS_A_Dash_of_Freedom_1.00_100.0_HM"));
        }
    }
}
