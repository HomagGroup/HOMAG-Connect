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
        }

        [TestMethod]
        public async Task Boards_CreateUpdateAllocate()
        {
            // create board types
            var materialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardTypeRequest = new MaterialManagerRequestBoardType
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
            await materialManagerClient.CreateBoardType(boardTypeRequest);
            var boardTypeResponse = await materialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest.BoardCode);
            Assert.AreEqual(4100.0, boardTypeResponse.Length);
            Assert.AreEqual(600.0, boardTypeResponse.Width);
            Assert.AreEqual(38.0, boardTypeResponse.Thickness);

            var boardTypeRequest2 = new MaterialManagerRequestBoardType
            {
                MaterialCode = "HPL_Natural_4.0",
                BoardCode = "HPL_Natural_4.0_2790.0_2060.0",
                Length = 2790.0,
                Width = 2060.0,
                Thickness = 4.0,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
            };
            await materialManagerClient.CreateBoardType(boardTypeRequest2);
            var boardTypeResponse2 = await materialManagerClient.GetBoardTypeByBoardCode(boardTypeRequest2.BoardCode);
            Assert.AreEqual(2790.0, boardTypeResponse2.Length);
            Assert.AreEqual(2060.0, boardTypeResponse2.Width);
            Assert.AreEqual(4.0, boardTypeResponse2.Thickness);

            // create board entities
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = "93",
                BoardCode = "HPL_F274_9_19.0_2800_2070",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest);

            var boardEntityRequest2 = new MaterialAssistRequestBoardEntity()
            {
                Id = "91",
                BoardCode = "P2_F204_75_38.0_4100.0_600.0",
                ManagementType = ManagementType.Stack,
                Quantity = 10
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest2);

            var boardEntityRequest3 = new MaterialAssistRequestBoardEntity()
            {
                Id = "92",
                BoardCode = "HPL_Natural_4.0_2790.0_2060.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 2
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequest3);

            /* store function not on prd yet
            // store board entities
            var storeBoardEntity = new MaterialAssistStoreBoardEntity()
            {
                Id = "93",
                Length = 2800,
                Width = 2070,
                StorageLocation = { Name = "001" },
            };
            await materialAssistClient.StoreBoardEntity(storeBoardEntity);
            var boardEntity = await materialAssistClient.GetBoardEntityById("93");
            Assert.AreEqual("001", boardEntity.Location.Name);

            var storeBoardEntity2 = new MaterialAssistStoreBoardEntity()
            {
                Id = "91",
                Length = 4100,
                Width = 600,
                StorageLocation = { Name = "002" },
            };
            await materialAssistClient.StoreBoardEntity(storeBoardEntity2);
            var boardEntity2 = await materialAssistClient.GetBoardEntityById("91");
            Assert.AreEqual("002", boardEntity2.Location.Name);

            var storeBoardEntity3 = new MaterialAssistStoreBoardEntity()
            {
                Id = "92",
                Length = 2790,
                Width = 2060,
                StorageLocation = { Name = "003" },
            };
            await materialAssistClient.StoreBoardEntity(storeBoardEntity3);
            var boardEntity3 = await materialAssistClient.GetBoardEntityById("92");
            Assert.AreEqual("003", boardEntity3.Location.Name);
            */

            // update board types
            var boardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_F274_9_19.0_2800_2070");

            var boardTypeUpdate = new MaterialManagerUpdateBoardType
            {
                BoardCode = "HPL_F274_9_12.0_2800_2070",
                Thickness = 12.0,
            };
            await materialManagerClient.UpdateBoardType("HPL_F274_9_19.0_2800_2070", boardTypeUpdate);
            var updatedBoardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_F274_9_12.0_2800_2070");
            Assert.AreEqual(12.0, updatedBoardType1.Thickness);

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

            var boardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_Natural_4.0_2790.0_2060.0");
            var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
            {
                MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
            };
            await materialManagerClient.UpdateBoardType("HPL_Natural_4.0_2790.0_2060.0", boardTypeUpdate3);
            var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails("HPL_Natural_4.0_2790.0_2060.0");
            Assert.AreEqual(BoardMaterialCategory.CompactPanels_HPL, updatedBoardType3.MaterialCategory);

            // create board type allocations
            var boardTypeAllocationRequest = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_F274_9_12.0_2800_2070",
                CreatedBy = "DeploymentTest",
                Name = "DeploymentTestAllocation",
                Quantity = 1,
                Source = "test",
                Workstation = "001"
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest);

            var boardTypeAllocationRequest2 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "P2_F204_75_38.0_4200.0_610.0",
                CreatedBy = "DeploymentTest",
                Name = "2DeploymentTestAllocation2",
                Quantity = 1,
                Source = "test",
                Workstation = "001"
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest2);

            var boardTypeAllocationRequest3 = new BoardTypeAllocationRequest
            {
                BoardTypeCode = "HPL_Natural_4.0_2790.0_2060.0",
                CreatedBy = "DeploymentTest",
                Name = "3DeploymentTestAllocation3",
                Quantity = 2,
                Source = "test",
                Workstation = "001"
            };
            await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest3);

            var allAllocationNames = await materialManagerClient.GetBoardTypeAllocations(1000);
            Assert.IsNotNull(allAllocationNames);
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "2DeploymentTestAllocation2"));
            Assert.IsTrue(allAllocationNames.Any(a => a.Name == "3DeploymentTestAllocation3"));
        }
        
        [TestCleanup]
        public async Task Cleanup()
        {
            var materialManagerClient = GetMaterialManagerClient().Material;
            await materialManagerClient.Boards.DeleteBoardType("HPL_F274_9_12.0_2800_2070");
            await materialManagerClient.Boards.DeleteBoardType("P2_F204_75_38.0_4200.0_610.0");
            await materialManagerClient.Boards.DeleteBoardType("HPL_Natural_4.0_2790.0_2060.0");
        }
    }
}
