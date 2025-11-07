using FluentAssertions;

using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts;

[TemporaryDisabledOnServer(2025, 12, 30, "DF-Material")]
[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateOffcutsTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task BoardsCreateOffcutEntity()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(materialAssistClient, "11114");

        var offcutEntity = await materialAssistClient.GetBoardEntityById("11114");

        offcutEntity.Should().NotBeNull(
            "because offcut entity with ID '11114' should be created successfully");
        offcutEntity!.Id.Should().Be("11114",
            "because we created offcut entity with ID '11114'");
        offcutEntity.ManagementType.Should().Be(ManagementType.Single,
            "because offcut entity '11114' was created with ManagementType.Single");
        offcutEntity.Quantity.Should().Be(1,
            "because Single management type must have quantity of 1");
        offcutEntity.Length.Should().Be(1000.0,
            "because offcut entity '11114' was created with length 1000.0");
        offcutEntity.Width.Should().Be(500.0,
            "because offcut entity '11114' was created with width 500.0");
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        var materialAssistClient = GetMaterialAssistClient().Boards;
        await materialAssistClient.DeleteBoardEntity("11114");
    }

    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureBoardTypeExist("EG_H3303_ST10_19", 1000, 500, 19, true);
    }
}