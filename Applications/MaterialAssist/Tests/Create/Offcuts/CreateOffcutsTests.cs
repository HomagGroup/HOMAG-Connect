using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts;

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
        offcutEntity.BoardType.BoardTypeType.Should().Be(BoardTypeType.Offcut,
            "because entity '11114' was created as an offcut");
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
        var materialManagerClient = GetMaterialManagerClient().Material.Boards;

        // Clean up board entity
        try
        {
            await materialAssistClient.DeleteBoardEntity("11114");
        }
        catch
        {
            // Ignore cleanup errors if entity doesn't exist
        }

        // Clean up parent board type (the regular board type created in Initialize)
        try
        {
            await materialManagerClient.DeleteBoardType("EG_H3303_ST10_19_2800_2070");
        }
        catch
        {
            // Ignore cleanup errors if board type doesn't exist
        }

        // Clean up offcut board type (automatically created when offcut entity is created)
        try
        {
            await materialManagerClient.DeleteBoardType("XEG_H3303_ST10_19_1000.0_500.0");
        }
        catch
        {
            // Ignore cleanup errors if board type doesn't exist
        }
    }

    [TestInitialize]
    public async Task Initialize()
    {
        // Create a regular board type (not an offcut type) that the offcut entity will reference
        await EnsureBoardTypeExist("EG_H3303_ST10_19");
    }
}