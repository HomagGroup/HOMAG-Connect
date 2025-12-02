using FluentAssertions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateBoardsTests : MaterialAssistTestBase
{

    [TemporaryDisabledOnServer(2025, 12, 5, "DF-Material")]
    [TestMethod]
    public async Task BoardsCreateBoardEntity()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        await CreateBoardEntitySample.Boards_CreateBoardEntity(materialAssistClient, "11111", "11112", "11113");

        // Verify Single board entity
        var boardEntity1 = await materialAssistClient.GetBoardEntityById("11111");
        boardEntity1.Should().NotBeNull(
            "because board entity with ID '11111' should be created successfully");
        boardEntity1!.Id.Should().Be("11111",
            "because we created board entity with ID '11111'");
        boardEntity1.ManagementType.Should().Be(ManagementType.Single,
            "because board entity '11111' was created with ManagementType.Single");
        boardEntity1.Quantity.Should().Be(1,
            "because Single management type must have quantity of 1");

        // Verify Stack board entity
        var boardEntity2 = await materialAssistClient.GetBoardEntityById("11112");
        boardEntity2.Should().NotBeNull(
            "because board entity with ID '11112' should be created successfully");
        boardEntity2!.Id.Should().Be("11112",
            "because we created board entity with ID '11112'");
        boardEntity2.ManagementType.Should().Be(ManagementType.Stack,
            "because board entity '11112' was created with ManagementType.Stack");
        boardEntity2.Quantity.Should().Be(5,
            "because Stack management type was created with quantity of 5");

        // Verify GoodsInStock board entity
        var boardEntity3 = await materialAssistClient.GetBoardEntityById("11113");
        boardEntity3.Should().NotBeNull(
            "because board entity with ID '11113' should be created successfully");
        boardEntity3!.Id.Should().Be("11113",
            "because we created board entity with ID '11113'");
        boardEntity3.ManagementType.Should().Be(ManagementType.GoodsInStock,
            "because board entity '11113' was created with ManagementType.GoodsInStock");
        boardEntity3.Quantity.Should().Be(5,
            "because GoodsInStock management type was created with quantity of 5");
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        await materialAssistClient.DeleteBoardEntity("11111");
        await materialAssistClient.DeleteBoardEntity("11112");
        await materialAssistClient.DeleteBoardEntity("11113");
    }

    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureBoardTypeExist("MDF_H3171_12_19.0");
        await EnsureBoardTypeExist("EG_H3303_ST10_19");
    }
}