using FluentAssertions;

using HomagConnect.MaterialAssist.Samples.Get.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Tests.Get.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class GetBoardsTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task BoardsGetBoardEntities()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var result = await GetBoardEntitiesSamples.Boards_GetBoardEntities(materialAssistClient);

        result.Should().NotBeNull(
            "because GetBoardEntities should return a collection of board entities");
        result.Count.Should().BeGreaterOrEqualTo(3,
            "because at least 3 board entities (733, 734, 735) should exist in the system");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByBoardCode()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        const string boardCode = "MDF_H3171_12_19.0_2800_2070";
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(materialAssistClient, boardCode) ?? Array.Empty<BoardEntity>()).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByBoardCode should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(2,
            $"because at least 2 board entities with board code '{boardCode}' should exist");
        result.Should().Contain(be => be.Id == "733",
            $"because board entity '733' has board code '{boardCode}'");
        result.Should().Contain(be => be.Id == "734",
            $"because board entity '734' has board code '{boardCode}'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByBoardCodes()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var boardCodes = new List<string> { "MDF_H3171_12_19.0_2800_2070", "EG_H3303_ST10_19_2800_2070" };
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(materialAssistClient, boardCodes)).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByBoardCodes should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(3,
            $"because at least 3 board entities with board codes '{string.Join(", ", boardCodes)}' should exist");
        result.Should().Contain(be => be.Id == "733",
            "because board entity '733' has board code 'MDF_H3171_12_19.0_2800_2070'");
        result.Should().Contain(be => be.Id == "734",
            "because board entity '734' has board code 'MDF_H3171_12_19.0_2800_2070'");
        result.Should().Contain(be => be.Id == "735",
            "because board entity '735' has board code 'EG_H3303_ST10_19_2800_2070'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesById()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var requestedIds = new List<string> { "733", "734", "735" };
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(materialAssistClient, requestedIds)).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesById should return a collection of board entities");
        result.Should().HaveCount(3,
            "because we requested 3 specific board entity IDs: 733, 734, and 735");
        result.Should().Contain(be => be.Id == "733",
            "because board entity with ID '733' was requested");
        result.Should().Contain(be => be.Id == "734",
            "because board entity with ID '734' was requested");
        result.Should().Contain(be => be.Id == "735",
            "because board entity with ID '735' was requested");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByMaterialCode()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        const string materialCode = "EG_H3303_ST10_19";
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(materialAssistClient, materialCode) ?? Array.Empty<BoardEntity>()).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByMaterialCode should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(1,
            $"because at least 1 board entity with material code '{materialCode}' should exist");
        result.Should().Contain(be => be.Id == "735",
            $"because board entity '735' has material code '{materialCode}'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntitiesByMaterialCodes()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var materialCodes = new List<string> { "EG_H3303_ST10_19", "MDF_H3171_12_19.0" };
        var result = (await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(materialAssistClient, materialCodes)).ToArray();

        result.Should().NotBeNull(
            "because GetBoardEntitiesByMaterialCodes should return a collection of board entities");
        result.Should().HaveCountGreaterOrEqualTo(3,
            $"because at least 3 board entities with material codes '{string.Join(", ", materialCodes)}' should exist");
        result.Should().Contain(be => be.Id == "733",
            "because board entity '733' has material code 'MDF_H3171_12_19.0'");
        result.Should().Contain(be => be.Id == "734",
            "because board entity '734' has material code 'MDF_H3171_12_19.0'");
        result.Should().Contain(be => be.Id == "735",
            "because board entity '735' has material code 'EG_H3303_ST10_19'");
    }

    [TestMethod]
    public async Task BoardsGetBoardEntityById()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        var result = await GetBoardEntitiesSamples.Boards_GetBoardEntityById(materialAssistClient, "733");

        result.Should().NotBeNull(
            "because board entity with ID '733' should exist");
        result?.Id.Should().Be("733",
            "because we retrieved board entity by ID '733'");
    }

    [TestMethod]
    public async Task BoardsGetStorageLocation()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;

        var act = async () => await GetBoardEntitiesSamples.Boards_GetStorageLocation(materialAssistClient);

        await act.Should().NotThrowAsync(
            "because GetStorageLocation should retrieve a storage location successfully");
    }

    [TestMethod]
    public async Task BoardsGetStorageLocations()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;

        var act = async () => await GetBoardEntitiesSamples.Boards_GetStorageLocations(materialAssistClient);

        await act.Should().NotThrowAsync(
            "because GetStorageLocations should retrieve storage locations successfully");
    }

    [TestMethod]
    public async Task BoardsGetWorkstations()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;

        var act = async () => await GetBoardEntitiesSamples.Boards_GetWorkstations(materialAssistClient);

        await act.Should().NotThrowAsync(
            "because GetWorkstations should retrieve workstations successfully");
    }

    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new GetBoardsTests();

        await classInstance.EnsureBoardTypeExist("MDF_H3171_12_19.0");
        await classInstance.EnsureBoardTypeExist("EG_H3303_ST10_19");

        await classInstance.EnsureBoardEntityExist("733", "MDF_H3171_12_19.0_2800_2070");
        await classInstance.EnsureBoardEntityExist("734", "MDF_H3171_12_19.0_2800_2070", ManagementType.Stack, 5);
        await classInstance.EnsureBoardEntityExist("735", "EG_H3303_ST10_19_2800_2070", ManagementType.GoodsInStock, 5);
    }
}