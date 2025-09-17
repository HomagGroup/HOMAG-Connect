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
                MaterialCode = "Test_Data_HPL_F274_9_12.0",
                BoardCode = "Test_Data_HPL_F274_9_12.0_4100.0_650.0",
                Length = 4100.0,
                Width = 650.0,
                Thickness = 12.5,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            await materialManagerClient.CreateBoardType(boardTypeRequest);
            var boardTypeResponse = await materialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest.BoardCode);
            Assert.AreEqual(4100.0, boardTypeResponse.Length);
            Assert.AreEqual(650.0, boardTypeResponse.Width);
            Assert.AreEqual(12.0, boardTypeResponse.Thickness);

            var boardTypeRequest2 = new MaterialManagerRequestBoardType
            {
                MaterialCode = "Test_Data_P2_F204_75_38.0",
                BoardCode = "Test_Data_P2_F204_75_38.0_4100.0_600.0",
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
            Assert.AreEqual(4100.0, boardTypeRequest2.Length);
            Assert.AreEqual(650.0, boardTypeResponse2.Width);
            Assert.AreEqual(38.0, boardTypeResponse2.Thickness);

            var boardTypeRequest3 = new MaterialManagerRequestBoardType
            {
                MaterialCode = "Test_Data_HPL_Natural_Carini_Walnut_4.0",
                BoardCode = "Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
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
            Assert.AreEqual(2790.0, boardTypeRequest3.Length);
            Assert.AreEqual(2060.0, boardTypeResponse3.Width);
            Assert.AreEqual(4.0, boardTypeResponse3.Thickness);
        }

        public async Task CreateBoardEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = "90",
                BoardCode = "Test_Data_HPL_F274_9_12.0_4100.0_650.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest);
            Assert.IsNotNull(await materialAssistClient.GetBoardEntityById(boardEntityRequest.Id));

            var boardEntityRequest2 = new MaterialAssistRequestBoardEntity()
            {
                Id = "91",
                BoardCode = "Test_Data_P2_F204_75_38.0_4100.0_600.0",
                ManagementType = ManagementType.Stack,
                Quantity = 10
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest2);
            Assert.IsNotNull(await materialAssistClient.GetBoardEntityById(boardEntityRequest2.Id));

            var boardEntityRequest3 = new MaterialAssistRequestBoardEntity()
            {
                Id = "92",
                BoardCode = "Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
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
            await materialAssistClient.StoreBoardEntity("90", 4100, 650, storageLocation);
            var boardEntity = await materialAssistClient.GetBoardEntityById("90");
            Assert.AreEqual("001", boardEntity.Location.Name);

            var storageLocation2 = new StorageLocation()
            {
                Name = "002",
            };
            await materialAssistClient.StoreBoardEntity("91", 4100, 600, storageLocation2);
            var boardEntity2 = await materialAssistClient.GetBoardEntityById("91");
            Assert.AreEqual("002", boardEntity2.Location.Name);

            var storageLocation3 = new StorageLocation()
            {
                Name = "003",
            };
            await materialAssistClient.StoreBoardEntity("92", 2790, 2060, storageLocation3);
            var boardEntity3 = await materialAssistClient.GetBoardEntityById("92");
            Assert.AreEqual("003", boardEntity3.Location.Name);
        }

        public async Task GetAndUpdateBoardTypes()
        {
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("Test_Data_HPL_F274_9_12.0_4100.0_650.0");
            
            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                Thickness = 12.0,
            };
            await materialManagerClient.UpdateBoardType("Test_Data_HPL_F274_9_12.0_4100.0_650.0", boardTypeUpdate);
            var updatedBoardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("Test_Data_HPL_F274_9_12.0_4100.0_650.0");
            Assert.AreEqual(12.0, updatedBoardType1.Thickness);

            var boardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("Test_Data_P2_F204_75_38.0_4100.0_600.0");
            var boardTypeUpdate2 = new MaterialManagerUpdateBoardType
            {
                BoardCode = "Test_Data_P2_F204_75_38.0_4200.0_610.0",
                Length = 4200.0,
                Width = 610.0,
            };
            await materialManagerClient.UpdateBoardType("Test_Data_P2_F204_75_38.0_4100.0_600.0", boardTypeUpdate2);
            var updatedBoardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("Test_Data_P2_F204_75_38.0_4200.0_610.0");
            Assert.AreEqual(4200.0, updatedBoardType2.Length);
            Assert.AreEqual(610.0, updatedBoardType2.Width);

            var boardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
            {
                MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
            };
            await materialManagerClient.UpdateBoardType("Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0", boardTypeUpdate3);
            var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
            Assert.AreEqual(BoardMaterialCategory.CompactPanels_HPL, updatedBoardType3.MaterialCategory);
        }

        public async Task AllocateBoards()
            {
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;

            var boardTypeAllocationRequest = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "Test_Data_HPL_F274_9_12.0_4100.0_650.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation",
                Quantity = 1,
                };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest);

            var boardTypeAllocationRequest2 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "Test_Data_P2_F204_75_38.0_4100.0_600.0",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation2",
                Quantity = 3,
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest2);

            var boardTypeAllocationRequest3 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0",
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
        /*
        //board type allocations
        await materialManagerClient.Boards.DeleteBoardTypeAllocations(new List<string>
            {
                "DeploymentTestAllocation",
                "DeploymentTestAllocation2",
                "DeploymentTestAllocation3"
            });*/

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new CreateUpdateAllocateBoards();

            var materialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            await materialAssistClient.DeleteBoardEntity("42");
            await materialAssistClient.DeleteBoardEntity("22");
            await materialAssistClient.DeleteBoardEntity("37");

            var materialManagerClient = classInstance.GetMaterialManagerClient().Material;
            await materialManagerClient.Boards.DeleteBoardType("Test_Data_HPL_F274_9_12.0_4100.0_650.0");
            await materialManagerClient.Boards.DeleteBoardType("Test_Data_P2_F204_75_38.0_4100.0_600.0");
            await materialManagerClient.Boards.DeleteBoardType("Test_Data_HPL_Natural_Carini_Walnut_4.0_2790.0_2060.0");
        }
    }
}
