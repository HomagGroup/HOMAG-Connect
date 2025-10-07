using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Integration
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]

    public class CreateUpdateAllocateBoards : MaterialAssistTestBase
    {
        [TestInitialize]
        public  async Task ClassInit()
        {
            await EnsureBoardTypeExist("HPL_F274_9_19.0");
            await EnsureBoardTypeExist("P2_F204_75_38.0", 4100, 600);
            await EnsureBoardTypeExist("HPL_Natural_4.0", 2790, 2060);
        }

        [TestMethod]
        public async Task Boards_CreateUpdateAllocate()
        {
            // board data
            var board1 = new { id = "93", boardCode = "HPL_F274_9_19.0_2800_2070" };
            var board2 = new { id = "91", boardCode = "P2_F204_75_38.0_4100_600" };
            var board3 = new { id = "92", boardCode = "HPL_Natural_4.0_2790_2060" };

            // create board entities
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = board1.id,
                BoardCode = board1.boardCode,
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest);

            var boardEntityRequest2 = new MaterialAssistRequestBoardEntity()
            {
                Id = board2.id,
                BoardCode = board2.boardCode,
                ManagementType = ManagementType.Stack,
                Quantity = 10
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest2);

            var boardEntityRequest3 = new MaterialAssistRequestBoardEntity()
            {
                Id = board3.id,
                BoardCode = board3.boardCode,
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 2
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest3);

            // update board types
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board1.boardCode);

            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                BoardCode = "HPL_F274_9_12.0_2800_2070",
                Thickness = 12.0,
            };
            await materialManagerClient.UpdateBoardType(board1.boardCode, boardTypeUpdate);
            var updatedBoard1 = new { id = "93", boardCode = "HPL_F274_9_12.0_2800_2070" };
            var updatedBoardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(updatedBoard1.boardCode);
            Assert.AreEqual(12.0, updatedBoardType1.Thickness);

            var boardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board2.boardCode);
            var boardTypeUpdate2 = new MaterialManagerUpdateBoardType
            {
                BoardCode = "P2_F204_75_38.0_4200_610",
                Length = 4200.0,
                Width = 610.0,
            };
            await materialManagerClient.UpdateBoardType(board2.boardCode, boardTypeUpdate2);
            var updatedBoard2 = new { id = "91", boardCode = "P2_F204_75_38.0_4200_610" };
            var updatedBoardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(updatedBoard2.boardCode);
            Assert.AreEqual(4200.0, updatedBoardType2.Length);
            Assert.AreEqual(610.0, updatedBoardType2.Width);

            var boardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);
            var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
            {
                MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
            };
            await materialManagerClient.UpdateBoardType(board3.boardCode, boardTypeUpdate3);
            var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);
            Assert.AreEqual(BoardMaterialCategory.CompactPanels_HPL, updatedBoardType3.MaterialCategory);

            // create board type allocations
            var boardTypeAllocationRequest = new BoardTypeAllocationRequest
            {
                BoardTypeCode = updatedBoard1.boardCode,
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation",
                Quantity = 1,
                Source = "HOMAG Connect",
                Workstation = "001"
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest);

            var boardTypeAllocationRequest2 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = updatedBoard2.boardCode,
                CreatedBy = "DeploymentTest",
                Name = "2DeploymentTestAllocation2",
                Quantity = 1,
                Source = "HOMAG Connect",
                Workstation = "001"
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest2);

            var boardTypeAllocationRequest3 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = board3.boardCode,
                CreatedBy = "DeploymentTest",
                Name = "3DeploymentTestAllocation3",
                Quantity = 2,
                Source = "HOMAG Connect",
                Workstation = "001"
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest3);

            var allAllocationNames = await materialManagerClient.GetBoardTypeAllocations(1000);
            Assert.IsNotNull(allAllocationNames);
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "2DeploymentTestAllocation2"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "3DeploymentTestAllocation3"));

            /*StatusCode: 500, ReasonPhrase: 'Internal Server Error'
             
            // needed for storing
            // Get the first workstation
            var workstations = await materialAssistClient.GetWorkstations().ConfigureAwait(false);
            var firstWorkstation = workstations.FirstOrDefault();
            if (firstWorkstation == null)
            {
                Assert.Inconclusive("No workstations found.");
                return;
            }
            // Get the first storage location for the workstation
            var storageLocations = await materialAssistClient.GetStorageLocations(firstWorkstation.Id.ToString()).ConfigureAwait(false);
            var firstStorageLocation = storageLocations.FirstOrDefault();
            if (firstStorageLocation == null)
            {
                Assert.Inconclusive("No storage locations found for the workstation.");
                return;
            }

            // store board entities
            var storeBoardEntity = new MaterialAssistStoreBoardEntity()
            {
                Id = "93",
                Length = 2800,
                Width = 2070,
                StorageLocation = firstStorageLocation,
                Workstation = firstWorkstation
            };
            await materialAssistClient.StoreBoardEntity(storeBoardEntity);
            var boardEntity = await materialAssistClient.GetBoardEntityById("93");

            var storeBoardEntity2 = new MaterialAssistStoreBoardEntity()
            {
                Id = "91",
                Length = 4100,
                Width = 600,
                StorageLocation = firstStorageLocation,
                Workstation = firstWorkstation
            };
            await materialAssistClient.StoreBoardEntity(storeBoardEntity2);
            var boardEntity2 = await materialAssistClient.GetBoardEntityById("91");

            var storeBoardEntity3 = new MaterialAssistStoreBoardEntity()
            {
                Id = "92",
                Length = 2790,
                Width = 2060,
                StorageLocation = firstStorageLocation,
                Workstation = firstWorkstation
            };
            await materialAssistClient.StoreBoardEntity(storeBoardEntity3);
            var boardEntity3 = await materialAssistClient.GetBoardEntityById("92");
            */
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await materialAssistClient.DeleteBoardEntity("93");
            await materialAssistClient.DeleteBoardEntity("91");
            await materialAssistClient.DeleteBoardEntity("92");
            var materialManagerClient = GetMaterialManagerClient().Material;
            await materialManagerClient.Boards.DeleteBoardType("HPL_F274_9_12.0_2800_2070");
            await materialManagerClient.Boards.DeleteBoardType("P2_F204_75_38.0_4200_610");
            await materialManagerClient.Boards.DeleteBoardType("HPL_Natural_4.0_2790_2060");
        }
    }
}
