using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;
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
                MaterialCategory = BoardMaterialCategory.Undefined,
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
                MaterialCategory = EdgebandMaterialCategory.PVC,
                Process = EdgebandingProcess.Other,
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

        [TestMethod]
        public async Task updateStorageLocationBoards()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;

            var storageLocation = new StorageLocation()
            {
                Name = "001",
            };
            await MaterialAssistClient.StoreBoardEntity("42", 4100, 650, storageLocation);
            var boardEntity = await MaterialAssistClient.GetBoardEntityById("42");
            Assert.IsNotNull(boardEntity.Location);
            Assert.AreEqual("001", boardEntity.Location.Name);

            var storageLocation2 = new StorageLocation()
            {
                Name = "002",
            };
            await MaterialAssistClient.StoreBoardEntity("22", 4100, 600, storageLocation2);
            var boardEntity2 = await MaterialAssistClient.GetBoardEntityById("22");
            Assert.IsNotNull(boardEntity2.Location);
            Assert.AreEqual("002", boardEntity2.Location.Name);

            var storageLocation3 = new StorageLocation()
            {
                Name = "003",
            };
            await MaterialAssistClient.StoreBoardEntity("37", 2790, 2060, storageLocation3);
            var boardEntity3 = await MaterialAssistClient.GetBoardEntityById("37");
            Assert.IsNotNull(boardEntity3.Location);
            Assert.AreEqual("003", boardEntity3.Location.Name);
        }

        [TestMethod]
        public async Task updateStorageLocationEdgebands()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "18",
                Length = 75,
                StorageLocation = new StorageLocation()
                {
                    LocationId = "001-01",
                },
            };
            await MaterialAssistClient.StoreEdgebandEntity(edgebandEntityStore);
            var edgebandEntity = await MaterialAssistClient.GetEdgebandEntityById("18");
            Assert.IsNotNull(edgebandEntity.Location);
            Assert.AreEqual("001-01", edgebandEntity.Location.LocationId);

            var edgebandEntityStore2 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "59",
                Length = 75,
                StorageLocation = new StorageLocation()
                {
                    Name = "Location2",
                },
            };
            await MaterialAssistClient.StoreEdgebandEntity(edgebandEntityStore2);
            var edgebandEntity2 = await MaterialAssistClient.GetEdgebandEntityById("59");
            Assert.IsNotNull(edgebandEntity2.Location);
            Assert.AreEqual("Location2", edgebandEntity2.Location.Name);

            var edgebandEntityStore3 = new MaterialAssistStoreEdgebandEntity()
            {
                Id = "63",
                Length = 225,
                StorageLocation = new StorageLocation()
                {
                    Name = "Lager1",
                },
            };
            await MaterialAssistClient.StoreEdgebandEntity(edgebandEntityStore3);
            var edgebandEntity3 = await MaterialAssistClient.GetEdgebandEntityById("63");
            Assert.IsNotNull(edgebandEntity3.Location);
            Assert.AreEqual("Lager1", edgebandEntity3.Location.Name);
        }

        [TestMethod]
        public async Task getAndUpdateBoardTypes()
        {
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardType1 = await MaterialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_F274_9_12.0_4100.0_650.0");
            
            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                Thickness = 19.5,
            };
            await MaterialManagerClient.UpdateBoardType("HPL_F274_9_12.0_4100.0_650.0", boardTypeUpdate);
            var updatedBoardType1 = await MaterialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_F274_9_12.0_4100.0_650.0");
            Assert.AreEqual(19.5, updatedBoardType1.Thickness);

            var boardType2 = await MaterialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("P2_F204_75_38.0_4100.0_600.0");
            var boardTypeUpdate2 = new MaterialManagerUpdateBoardType
            {
                BoardCode = "P2_F204_75_38.0_4200.0_610.0",
                Length = 4200.0,
                Width = 610.0,
            };
            await MaterialManagerClient.UpdateBoardType("P2_F204_75_38.0_4100.0_600.0", boardTypeUpdate2);
            var updatedBoardType2 = await MaterialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("P2_F204_75_38.0_4200.0_610.0");
            Assert.AreEqual(4200.0, updatedBoardType2.Length);
            Assert.AreEqual(610.0, updatedBoardType2.Width);

            var boardType3 = await MaterialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
            {
                MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
            };
            await MaterialManagerClient.UpdateBoardType("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0", boardTypeUpdate3);
            var updatedBoardType3 = await MaterialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            Assert.AreEqual(BoardMaterialCategory.CompactPanels_HPL, updatedBoardType3.MaterialCategory);
        }

        [TestMethod]
        public async Task getAndUpdateEdgebandTypes()
        {
            var MaterialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            var edgebandType1 = await MaterialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_Multiplex schwarz_1.00_23.0_NN");
            var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
            {
                EdgebandCode = "ABS_Multiplex schwarz_1.2_23.0_NN",
                Thickness = 1.2,
            };
            await MaterialManagerClient.UpdateEdgebandType("ABS_Multiplex schwarz_1.00_23.0_NN", edgebandTypeUpdate);
            var updatedEdgebandType1 = await MaterialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_Multiplex schwarz_1.2_23.0_NN");
            Assert.AreEqual(1.2, updatedEdgebandType1.Thickness);

            var edgebandType2 = await MaterialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("NN_Schwarz_16.50_24.5_HM");
            var edgebandTypeUpdate2 = new MaterialManagerUpdateEdgebandType
            {
                DefaultLength = 75.0,
            };
            await MaterialManagerClient.UpdateEdgebandType("NN_Schwarz_16.50_24.5_HM", edgebandTypeUpdate2);
            var updatedEdgebandType2 = await MaterialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("NN_Schwarz_16.50_24.5_HM");
            Assert.AreEqual(75.0, updatedEdgebandType2.DefaultLength);

            var edgebandType3 = await MaterialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
            var edgebandTypeUpdate3 = new MaterialManagerUpdateEdgebandType
            {
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.HotmeltGlue,
            };
            await MaterialManagerClient.UpdateEdgebandType("ABS_A_Dash_of_Freedom_1.00_100.0_HM", edgebandTypeUpdate3);
            var updatedEdgebandType3 = await MaterialManagerClient.GetEdgebandTypeByEdgebandCodeIncludingDetails("ABS_A_Dash_of_Freedom_1.00_100.0_HM");
            Assert.AreEqual(EdgebandMaterialCategory.ABS, updatedEdgebandType3.MaterialCategory);
            Assert.AreEqual(EdgebandingProcess.HotmeltGlue, updatedEdgebandType3.Process);
        }

        [TestMethod]
        public async Task allocateBoards()
            {
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;

            var boardTypeAllocationRequest = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_F274_9_12.0_4100.0_650.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation",
                Quantity = 2,
                };
            await MaterialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest);

            var boardTypeAllocationRequest2 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "P2_F204_75_38.0_4100.0_600.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation2",
                Quantity = 3,
            };
            await MaterialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest2);

            var boardTypeAllocationRequest3 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation3",
                Quantity = 2,
            };
            await MaterialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest3);

            var allAllocationNames = await MaterialManagerClient.GetBoardTypeAllocations(1000);
            Assert.IsNotNull(allAllocationNames);
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation2"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation3"));
        }

        //allocation for edgebands not implemented yet

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialManagerClient = GetMaterialManagerClient().Material;
            var MaterialAssistClient = GetMaterialAssistClient();

            //board type allocations
            await MaterialManagerClient.Boards.DeleteBoardTypeAllocations(new List<string>
            {
                "DeploymentTestAllocation",
                "DeploymentTestAllocation2",
                "DeploymentTestAllocation3"
            });

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
