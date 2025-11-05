using FluentAssertions;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

namespace HomagConnect.MaterialAssist.Tests.Integration.Boards;

[TemporaryDisabledOnServer(2025, 12, 30, "DF-Material")]
[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateUpdateStoreAllocateBoards : MaterialAssistTestBase
{
    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureBoardTypeExist("HPL_Anthracite_Marble_12.0");
        await EnsureBoardTypeExist("P2_F204_75_38.0", 4100, 600);
        await EnsureBoardTypeExist("HPL_Natural_4.0", 2790, 2060, 4);
    }

    [TestMethod]
    public async Task Boards_CreateUpdateStoreAllocate()
    {
        // board data
        var board1 = new { id = "91", boardCode = "HPL_Anthracite_Marble_12.0_2800_2070" };
        var board2 = new { id = "92", boardCode = "P2_F204_75_38.0_4100_600" };
        var board3 = new { id = "93", boardCode = "HPL_Natural_4.0_2790_2060" };

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

        await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board1.boardCode);
        var boardTypeUpdate = new MaterialManagerUpdateBoardType
        {
            Thickness = 12.0,
        };
        await materialManagerClient.UpdateBoardType(board1.boardCode, boardTypeUpdate);
        var updatedBoardType1 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board1.boardCode);

        updatedBoardType1.Should().NotBeNull(
            $"because board type with board code '{board1.boardCode}' should exist after update");
        updatedBoardType1!.Thickness.Should().Be(12.0,
            $"because board type '{board1.boardCode}' was updated to thickness 12.0");

        await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board2.boardCode);
        var boardTypeUpdate2 = new MaterialManagerUpdateBoardType
        {
            BoardCode = "P2_F204_75_38.0_4200_610",
            Length = 4200.0,
            Width = 610.0,
            Thickness = 38.0
        };
        await materialManagerClient.UpdateBoardType(board2.boardCode, boardTypeUpdate2);
        var updatedBoard2 = new { id = "92", boardCode = "P2_F204_75_38.0_4200_610" };
        var updatedBoardType2 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(updatedBoard2.boardCode);

        updatedBoardType2.Should().NotBeNull(
            $"because board type with board code '{updatedBoard2.boardCode}' should exist after update");
        updatedBoardType2!.Length.Should().Be(4200.0,
            $"because board type '{updatedBoard2.boardCode}' was updated to length 4200.0");
        updatedBoardType2.Width.Should().Be(610.0,
            $"because board type '{updatedBoard2.boardCode}' was updated to width 610.0");
        updatedBoardType2.Thickness.Should().Be(38.0,
            $"because board type '{updatedBoard2.boardCode}' was updated to thickness 38.0");

        await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);
        var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
        {
            Thickness = 4.0,
            MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
        };
        await materialManagerClient.UpdateBoardType(board3.boardCode, boardTypeUpdate3);
        var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);

        updatedBoardType3.Should().NotBeNull(
            $"because board type with board code '{board3.boardCode}' should exist after update");
        updatedBoardType3!.MaterialCategory.Should().Be(BoardMaterialCategory.CompactPanels_HPL,
            $"because board type '{board3.boardCode}' was updated to material category CompactPanels_HPL");

        /*
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
            Id = board1.id,
            Length = 2800,
            Width = 2070,
            StorageLocation = firstStorageLocation,
            Workstation = firstWorkstation
        };
        await materialAssistClient.StoreBoardEntity(storeBoardEntity);
        var boardEntity = await materialAssistClient.GetBoardEntityById(board1.id);

        var storeBoardEntity2 = new MaterialAssistStoreBoardEntity()
        {
            Id = board2.id,
            Length = 4100,
            Width = 600,
            StorageLocation = firstStorageLocation,
            Workstation = firstWorkstation
        };
        await materialAssistClient.StoreBoardEntity(storeBoardEntity2);
        var boardEntity2 = await materialAssistClient.GetBoardEntityById(board2.id);

        var storeBoardEntity3 = new MaterialAssistStoreBoardEntity()
        {
            Id = board3.id,
            Length = 2790,
            Width = 2060,
            StorageLocation = firstStorageLocation,
            Workstation = firstWorkstation
        };
        await materialAssistClient.StoreBoardEntity(storeBoardEntity3);
        var boardEntity3 = await materialAssistClient.GetBoardEntityById(board3.id);
        */

        // create board type allocations
        var boardTypeAllocationRequest = new BoardTypeAllocationRequest
        {
            BoardTypeCode = board1.boardCode,
            CreatedBy = "Boards_CreateUpdateAllocate",
            Name = "DeploymentTestAllocation1",
            Quantity = 1,
            Source = "HOMAG Connect",
            Workstation = "001"
        };
        await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest);

        var boardTypeAllocationRequest2 = new BoardTypeAllocationRequest
        {
            BoardTypeCode = updatedBoard2.boardCode,
            CreatedBy = "Boards_CreateUpdateAllocate",
            Name = "DeploymentTestAllocation2",
            Quantity = 1,
            Source = "HOMAG Connect",
            Workstation = "001"
        };
        await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest2);

        var boardTypeAllocationRequest3 = new BoardTypeAllocationRequest
        {
            BoardTypeCode = board3.boardCode,
            CreatedBy = "Boards_CreateUpdateAllocate",
            Name = "DeploymentTestAllocation3",
            Quantity = 2,
            Source = "HOMAG Connect",
            Workstation = "001"
        };
        await materialManagerClient.CreateBoardTypeAllocation(boardTypeAllocationRequest3);

        var allAllocationNames = (await materialManagerClient.GetBoardTypeAllocationsByAllocationNames(new List<string> { "DeploymentTestAllocation1", "DeploymentTestAllocation2", "DeploymentTestAllocation3" }, 1000) ?? Array.Empty<BoardTypeAllocation>()).ToArray();

        allAllocationNames.Should().NotBeNull(
            "because GetBoardTypeAllocationsByAllocationNames should return a collection of allocations");
        allAllocationNames.Should().Contain(a => a.Name == "DeploymentTestAllocation1",
            "because allocation 'DeploymentTestAllocation1' was created for board type '{0}'", board1.boardCode);
        allAllocationNames.Should().Contain(a => a.Name == "DeploymentTestAllocation2",
            "because allocation 'DeploymentTestAllocation2' was created for board type '{0}'", updatedBoard2.boardCode);
        allAllocationNames.Should().Contain(a => a.Name == "DeploymentTestAllocation3",
            "because allocation 'DeploymentTestAllocation3' was created for board type '{0}'", board3.boardCode);
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        await materialAssistClient.DeleteBoardEntity("91");
        await materialAssistClient.DeleteBoardEntity("92");
        await materialAssistClient.DeleteBoardEntity("93");
        var materialManagerClient = GetMaterialManagerClient().Material;
        await materialManagerClient.Boards.DeleteBoardType("HPL_Anthracite_Marble_12.0_2800_2070");
        await materialManagerClient.Boards.DeleteBoardType("P2_F204_75_38.0_4200_610");
        await materialManagerClient.Boards.DeleteBoardType("HPL_Natural_4.0_2790_2060");
    }
}