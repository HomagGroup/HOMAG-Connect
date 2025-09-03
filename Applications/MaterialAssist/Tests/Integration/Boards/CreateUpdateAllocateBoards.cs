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

namespace HomagConnect.MaterialAssist.Tests.Integration.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]

    public class CreateUpdateAllocateBoards : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task CreateBoardTypes()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;

            var boardTypeRequest = new MaterialManagerRequestBoardType
            {
                MaterialCode = "HPL_F274_9_12.0",
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                Length = 4100.0,
                Width = 650.0,
                Thickness = 12.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            await materialManagerClient.CreateBoardType(boardTypeRequest);
            var boardTypeResponse = await materialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest.BoardCode);
            Assert.IsNotNull(boardTypeResponse);
            Assert.AreEqual(4100.0, boardTypeResponse.Length);
            Assert.AreEqual(650.0, boardTypeResponse.Width);
            Assert.AreEqual(12.0, boardTypeResponse.Thickness);

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
            await materialManagerClient.CreateBoardType(boardTypeRequest2);
            var boardTypeResponse2 = await materialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest2.BoardCode);
            Assert.IsNotNull(boardTypeResponse2);
            Assert.AreEqual(4100.0, boardTypeRequest2.Length);
            Assert.AreEqual(650.0, boardTypeResponse2.Width);
            Assert.AreEqual(38.0, boardTypeResponse2.Thickness);

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
            await materialManagerClient.CreateBoardType(boardTypeRequest3);
            var boardTypeResponse3 = await materialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest3.BoardCode);
            Assert.IsNotNull(boardTypeResponse3);
            Assert.AreEqual(2790.0, boardTypeRequest3.Length);
            Assert.AreEqual(2060.0, boardTypeResponse3.Width);
            Assert.AreEqual(4.0, boardTypeResponse3.Thickness);
        }

        public async Task CreateBoardEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "HPL_F274_9_12.0_4100.0_650.0",
                ManagementType = ManagementType.Single,
                Quantity = 5
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest);
            Assert.IsNotNull(await materialAssistClient.GetBoardEntityById(boardEntityRequest.Id));

            var boardEntityRequest2 = new MaterialAssistRequestBoardEntity()
            {
                Id = "22",
                BoardCode = "P2_F204_75_38.0_4100.0_600.0",
                ManagementType = ManagementType.Stack,
                Quantity = 10
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest2);
            Assert.IsNotNull(await materialAssistClient.GetBoardEntityById(boardEntityRequest2.Id));

            var boardEntityRequest3 = new MaterialAssistRequestBoardEntity()
            {
                Id = "37",
                BoardCode = "HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 2
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest3);
            Assert.IsNotNull(await materialAssistClient.GetBoardEntityById(boardEntityRequest3.Id));
        }

        public async Task UpdateStorageLocationBoards()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;

            var storageLocation = new StorageLocation()
            {
                Name = "001",
            };
            await materialAssistClient.StoreBoardEntity("42", 4100, 650, storageLocation);
            var boardEntity = await materialAssistClient.GetBoardEntityById("42");
            Assert.IsNotNull(boardEntity.Location);
            Assert.AreEqual("001", boardEntity.Location.Name);

            var storageLocation2 = new StorageLocation()
            {
                Name = "002",
            };
            await materialAssistClient.StoreBoardEntity("22", 4100, 600, storageLocation2);
            var boardEntity2 = await materialAssistClient.GetBoardEntityById("22");
            Assert.IsNotNull(boardEntity2.Location);
            Assert.AreEqual("002", boardEntity2.Location.Name);

            var storageLocation3 = new StorageLocation()
            {
                Name = "003",
            };
            await materialAssistClient.StoreBoardEntity("37", 2790, 2060, storageLocation3);
            var boardEntity3 = await materialAssistClient.GetBoardEntityById("37");
            Assert.IsNotNull(boardEntity3.Location);
            Assert.AreEqual("003", boardEntity3.Location.Name);
        }

        public async Task GetAndUpdateBoardTypes()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_F274_9_12.0_4100.0_650.0");
            
            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                Thickness = 19.5,
            };
            await materialManagerClient.UpdateBoardType("HPL_F274_9_12.0_4100.0_650.0", boardTypeUpdate);
            var updatedBoardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_F274_9_12.0_4100.0_650.0");
            Assert.AreEqual(19.5, updatedBoardType1.Thickness);

            var boardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("P2_F204_75_38.0_4100.0_600.0");
            var boardTypeUpdate2 = new MaterialManagerUpdateBoardType
            {
                BoardCode = "P2_F204_75_38.0_4200.0_610.0",
                Length = 4200.0,
                Width = 610.0,
            };
            await materialManagerClient.UpdateBoardType("P2_F204_75_38.0_4100.0_600.0", boardTypeUpdate2);
            var updatedBoardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("P2_F204_75_38.0_4200.0_610.0");
            Assert.AreEqual(4200.0, updatedBoardType2.Length);
            Assert.AreEqual(610.0, updatedBoardType2.Width);

            var boardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
            {
                MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
            };
            await materialManagerClient.UpdateBoardType("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0", boardTypeUpdate3);
            var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            Assert.AreEqual(BoardMaterialCategory.CompactPanels_HPL, updatedBoardType3.MaterialCategory);
        }

        public async Task AllocateBoards()
            {
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;

            var boardTypeAllocationRequest = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_F274_9_12.0_4100.0_650.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation",
                Quantity = 2,
                };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest);

            var boardTypeAllocationRequest2 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "P2_F204_75_38.0_4100.0_600.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation2",
                Quantity = 3,
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest2);

            var boardTypeAllocationRequest3 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation3",
                Quantity = 2,
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest3);

            var allAllocationNames = await materialManagerClient.GetBoardTypeAllocations(1000);
            Assert.IsNotNull(allAllocationNames);
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation2"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation3"));
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var materialManagerClient = GetMaterialManagerClient().Material;
            var materialAssistClient = GetMaterialAssistClient();

            //board type allocations
            await materialManagerClient.Boards.DeleteBoardTypeAllocations(new List<string>
            {
                "DeploymentTestAllocation",
                "DeploymentTestAllocation2",
                "DeploymentTestAllocation3"
            });

            //board entities
            await materialAssistClient.Boards.DeleteBoardEntity("42");
            Assert.IsNull(await materialAssistClient.Boards.GetBoardEntityById("42"));

            await materialAssistClient.Boards.DeleteBoardEntity("22");
            Assert.IsNull(await materialAssistClient.Boards.GetBoardEntityById("22"));

            await materialAssistClient.Boards.DeleteBoardEntity("37");
            Assert.IsNull(await materialAssistClient.Boards.GetBoardEntityById("37"));

            //board types
            await materialManagerClient.Boards.DeleteBoardType("HPL_F274_9_12.0_4100.0_650.0");
            Assert.IsNull(await materialManagerClient.Boards.GetBoardTypeByBoardCode("HPL_F274_9_12.0_4100.0_650.0"));

            await materialManagerClient.Boards.DeleteBoardType("P2_F204_75_38.0_4100.0_600.0");
            Assert.IsNull(await materialManagerClient.Boards.GetBoardTypeByBoardCode("P2_F204_75_38.0_4100.0_600.0"));

            await materialManagerClient.Boards.DeleteBoardType("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            Assert.IsNull(await materialManagerClient.Boards.GetBoardTypeByBoardCode("HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0"));
        }
    }
}
