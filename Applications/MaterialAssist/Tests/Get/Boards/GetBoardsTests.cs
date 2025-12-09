using FluentAssertions;

using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Samples.Get.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialAssist.Tests.Get.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class GetBoardsTests : MaterialAssistTestBase
{
    private const string _SingleCode = "733";
    private const string _StackCode = "734";
    private const string _GoodsInStockCode = "735";
    private const string _BoardCodeMdfH3171 = _MaterialCodeMdfH3171 + "_2800_2070";
    private const string _BoardCodeEgH3303 = _MaterialCodeEgH3303 + "_2800_2070";
    private const string _MaterialCodeMdfH3171 = "MDF_H3171_12_19.0";
    private const string _MaterialCodeEgH3303 = "EG_H3303_ST10_19";
    private MaterialAssistClientBoards _MaterialAssistClientBoards;

    [TestMethod]
    public async Task BoardsGetBoardEntities()
    {
        var result = await GetBoardEntitiesSamples.Boards_GetBoardEntities(_MaterialAssistClientBoards);

        result.Should().NotBeNull(
            "because GetBoardEntities should return a collection of board entities");
        result.Count.Should().BeGreaterOrEqualTo(3,
            $"because at least 3 board entities ({_SingleCode}, {_StackCode}, {_GoodsInStockCode}) should exist in the system");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByBoardCode()
    {
        const string boardCode = _BoardCodeMdfH3171;
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(_MaterialAssistClientBoards, boardCode) ?? []).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByBoardCode should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(2,
            $"because at least 2 board entities with board code '{boardCode}' should exist");
        result.Should().Contain(be => be.Id == _SingleCode,
            $"because board entity '{_SingleCode}' has board code '{boardCode}'");
        result.Should().Contain(be => be.Id == _StackCode,
            $"because board entity '{_StackCode}' has board code '{boardCode}'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByBoardCodes()
    {
        var boardCodes = new List<string> { _BoardCodeMdfH3171, _BoardCodeEgH3303 };
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(_MaterialAssistClientBoards, boardCodes)).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByBoardCodes should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(3,
            $"because at least 3 board entities with board codes '{string.Join(", ", boardCodes)}' should exist");
        result.Should().Contain(be => be.Id == _SingleCode,
            $"because board entity '{_SingleCode}' has board code '{_BoardCodeMdfH3171}'");
        result.Should().Contain(be => be.Id == _StackCode,
            $"because board entity '{_StackCode}' has board code '{_BoardCodeMdfH3171}'");
        result.Should().Contain(be => be.Id == _GoodsInStockCode,
            $"because board entity '{_GoodsInStockCode}' has board code '{_BoardCodeEgH3303}'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesById()
    {
        var requestedIds = new List<string> { _SingleCode, _StackCode, _GoodsInStockCode };
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByCodes(_MaterialAssistClientBoards, requestedIds)).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesById should return a collection of board entities");
        result.Should().HaveCount(3,
            $"because we requested 3 specific board entity IDs: {_SingleCode}, {_StackCode}, and {_GoodsInStockCode}");
        result.Should().Contain(be => be.Id == _SingleCode,
            $"because board entity with ID '{_SingleCode}' was requested");
        result.Should().Contain(be => be.Id == _StackCode,
            $"because board entity with ID '{_StackCode}' was requested");
        result.Should().Contain(be => be.Id == _GoodsInStockCode,
            $"because board entity with ID '{_GoodsInStockCode}' was requested");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByMaterialCode()
    {
        const string materialCode = _MaterialCodeEgH3303;
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(_MaterialAssistClientBoards, materialCode) ?? []).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByMaterialCode should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(1,
            $"because at least 1 board entity with material code '{materialCode}' should exist");
        result.Should().Contain(be => be.Id == _GoodsInStockCode,
            $"because board entity '{_GoodsInStockCode}' has material code '{materialCode}'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByMaterialCodes()
    {
        var materialCodes = new List<string> { _MaterialCodeEgH3303, _MaterialCodeMdfH3171 };
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(_MaterialAssistClientBoards, materialCodes)).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByMaterialCodes should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(3,
            $"because at least 3 board entities with material codes '{string.Join(", ", materialCodes)}' should exist");
        result.Should().Contain(be => be.Id == _SingleCode,
            $"because board entity '{_SingleCode}' has material code '{_MaterialCodeMdfH3171}'");
        result.Should().Contain(be => be.Id == _StackCode,
            $"because board entity '{_StackCode}' has material code '{_MaterialCodeMdfH3171}'");
        result.Should().Contain(be => be.Id == _GoodsInStockCode,
            $"because board entity '{_GoodsInStockCode}' has material code '{_MaterialCodeEgH3303}'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntityByCode()
    {
        var result = await GetBoardEntitiesSamples.Boards_GetBoardEntityByCode(_MaterialAssistClientBoards, _SingleCode);

        result.Should().NotBeNull(
            $"because board entity with ID '{_SingleCode}' should exist");
        result?.Id.Should().Be(_SingleCode,
            $"because we retrieved board entity by ID '{_SingleCode}'");
    }

    [TestMethod]
    public async Task BoardsGetStorageLocation()
    {
        var act = async () => await GetBoardEntitiesSamples.Boards_GetStorageLocation(_MaterialAssistClientBoards);

        await act.Should().NotThrowAsync(
            "because GetStorageLocation should retrieve a storage location successfully");
    }

    [TestMethod]
    public async Task BoardsGetStorageLocations()
    {
        var act = async () => await GetBoardEntitiesSamples.Boards_GetStorageLocations(_MaterialAssistClientBoards);

        await act.Should().NotThrowAsync(
            "because GetStorageLocations should retrieve storage locations successfully");
    }

    [TestMethod]
    public async Task BoardsGetWorkstations()
    {
        var act = async () => await GetBoardEntitiesSamples.Boards_GetWorkstations(_MaterialAssistClientBoards);

        await act.Should().NotThrowAsync(
            "because GetWorkstations should retrieve workstations successfully");
    }

    [TestInitialize]
    public async Task Initialize()
    {
        _MaterialAssistClientBoards = GetMaterialAssistClient().Boards;
        await EnsureBoardTypeExist(_MaterialCodeMdfH3171);
        await EnsureBoardTypeExist(_MaterialCodeEgH3303);

        await EnsureBoardEntityExist(_SingleCode, _BoardCodeMdfH3171);
        await EnsureBoardEntityExist(_StackCode, _BoardCodeMdfH3171, ManagementType.Stack, 5);
        await EnsureBoardEntityExist(_GoodsInStockCode, _BoardCodeEgH3303, ManagementType.GoodsInStock, 5);
    }
}