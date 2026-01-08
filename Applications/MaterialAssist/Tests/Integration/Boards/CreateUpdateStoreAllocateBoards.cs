using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

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

        updatedBoardType1.ShouldNotBeNull(
            $"because board type with board code '{board1.boardCode}' should exist after update");

        updatedBoardType1.Thickness.ShouldNotBeNull();
        updatedBoardType1.Thickness.Value.ShouldBe(12.0, 0.0001, "because the update value should be 12.0");

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

        updatedBoardType2.ShouldNotBeNull(
            $"because board type with board code '{updatedBoard2.boardCode}' should exist after update");

        updatedBoardType2.Thickness.ShouldNotBeNull();
        updatedBoardType2.Thickness.Value.ShouldBe(38.0, 0.0001, "because the update value should be 38.0");

        updatedBoardType2.Length.ShouldNotBeNull();
        updatedBoardType2.Length.Value.ShouldBe(4200.0, 0.0001, "because the update value should be 4200.0");

        updatedBoardType2.Width.ShouldNotBeNull();
        updatedBoardType2.Width.Value.ShouldBe(610.0, 0.0001, "because the update value should be 610.0");
        
        await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);
        var boardTypeUpdate3 = new MaterialManagerUpdateBoardType
        {
            Thickness = 4.0,
            MaterialCategory = BoardMaterialCategory.CompactPanels_HPL,
        };
        await materialManagerClient.UpdateBoardType(board3.boardCode, boardTypeUpdate3);
        var updatedBoardType3 = await materialManagerClient.GetBoardTypeByBoardCodeIncludingDetails(board3.boardCode);

        updatedBoardType3.ShouldNotBeNull(
            $"because board type with board code '{board3.boardCode}' should exist after update");
        updatedBoardType3!.MaterialCategory.ShouldBe(BoardMaterialCategory.CompactPanels_HPL,
            $"because board type '{board3.boardCode}' was updated to material category CompactPanels_HPL");
        
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

        allAllocationNames.ShouldNotBeNull(
            "because GetBoardTypeAllocationsByAllocationNames should return a collection of allocations");
        allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation1").ShouldBeTrue();
        allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation2").ShouldBeTrue();
        allAllocationNames.Any(a => a.Name == "DeploymentTestAllocation3").ShouldBeTrue();
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