using FluentAssertions;

using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialAssist.Tests.Create.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateBoardsTests : MaterialAssistTestBase
{
    private const string _SingleCode = "11111";
    private const string _StackCode = "11112";
    private const string _GoodsInStockCode = "11113";
    private MaterialAssistClientBoards _MaterialAssistClientBoards;

    [TestMethod]
    public async Task BoardsCreateBoardEntity()
    {
        try
        {
            await CreateBoardEntitySample.Boards_CreateBoardEntity(_MaterialAssistClientBoards, _SingleCode, _StackCode, _GoodsInStockCode);
        }
        catch (Exception)
        {
            // do nothing, the entities might already exist
        }

        // Verify Single board entity
        var boardEntity1 = await _MaterialAssistClientBoards.GetBoardEntityByCode(_SingleCode);
        boardEntity1.Should().NotBeNull(
            $"because board entity with ID '{_SingleCode}' should be created successfully");
        boardEntity1!.Id.Should().Be(_SingleCode,
            $"because we created board entity with ID '{_SingleCode}'");
        boardEntity1.ManagementType.Should().Be(ManagementType.Single,
            $"because board entity '{_SingleCode}' was created with ManagementType.Single");
        boardEntity1.Quantity.Should().Be(1,
            "because Single management type must have quantity of 1");

        // Verify Stack board entity
        var boardEntity2 = await _MaterialAssistClientBoards.GetBoardEntityByCode(_StackCode);
        boardEntity2.Should().NotBeNull(
            $"because board entity with ID '{_StackCode}' should be created successfully");
        boardEntity2!.Id.Should().Be(_StackCode,
            $"because we created board entity with ID '{_StackCode}'");
        boardEntity2.ManagementType.Should().Be(ManagementType.Stack,
            $"because board entity '{_StackCode}' was created with ManagementType.Stack");
        boardEntity2.Quantity.Should().Be(5,
            "because Stack management type was created with quantity of 5");

        // Verify GoodsInStock board entity
        var boardEntity3 = await _MaterialAssistClientBoards.GetBoardEntityByCode(_GoodsInStockCode);
        boardEntity3.Should().NotBeNull(
            $"because board entity with ID '{_GoodsInStockCode}' should be created successfully");
        boardEntity3!.Id.Should().Be(_GoodsInStockCode,
            $"because we created board entity with ID '{_GoodsInStockCode}'");
        boardEntity3.ManagementType.Should().Be(ManagementType.GoodsInStock,
            $"because board entity '{_GoodsInStockCode}' was created with ManagementType.GoodsInStock");
        boardEntity3.Quantity.Should().Be(5,
            "because GoodsInStock management type was created with quantity of 5");
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        await CleanupBoards();
    }

    [TestInitialize]
    public async Task Initialize()
    {
        _MaterialAssistClientBoards = GetMaterialAssistClient().Boards;
        await EnsureBoardTypeExist("MDF_H3171_12_19.0");
        await EnsureBoardTypeExist("EG_H3303_ST10_19");
    }

    private async Task CleanupBoards()
    {
        await _MaterialAssistClientBoards.DeleteBoardEntities([_SingleCode, _StackCode, _GoodsInStockCode]);
    }
}