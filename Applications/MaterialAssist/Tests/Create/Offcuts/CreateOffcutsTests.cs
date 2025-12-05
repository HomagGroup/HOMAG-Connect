using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateOffcutsTests : MaterialAssistTestBase
{
    private const string _OffcutEntityId = "11114";
    private MaterialAssistClientBoards _MaterialAssistClientBoards;
    private MaterialManagerClientMaterialBoards _MaterialManagerClientMaterialBoards;

    [TestMethod]
    public async Task BoardsCreateOffcutEntity()
    {
        await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(_MaterialAssistClientBoards, _OffcutEntityId);

        var offcutEntity = await _MaterialAssistClientBoards.GetBoardEntityByCode(_OffcutEntityId);

        offcutEntity.Should().NotBeNull(
            $"because offcut entity with ID '{_OffcutEntityId}' should be created successfully");
        offcutEntity!.Id.Should().Be(_OffcutEntityId,
            $"because we created offcut entity with ID '{_OffcutEntityId}'");
        offcutEntity.BoardType.BoardTypeType.Should().Be(BoardTypeType.Offcut,
            $"because entity '{_OffcutEntityId}' was created as an offcut");
        offcutEntity.ManagementType.Should().Be(ManagementType.Single,
            $"because offcut entity '{_OffcutEntityId}' was created with ManagementType.Single");
        offcutEntity.Quantity.Should().Be(1,
            "because Single management type must have quantity of 1");
        offcutEntity.Length.Should().Be(1000.0,
            $"because offcut entity '{_OffcutEntityId}' was created with length 1000.0");
        offcutEntity.Width.Should().Be(500.0,
            $"because offcut entity '{_OffcutEntityId}' was created with width 500.0");
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        // Clean up board entity
        try
        {
            await _MaterialAssistClientBoards.DeleteBoardEntity(_OffcutEntityId);
        }
        catch
        {
            // Ignore cleanup errors if entity doesn't exist
        }

        // Clean up parent board type (the regular board type created in Initialize)
        try
        {
            await _MaterialManagerClientMaterialBoards.DeleteBoardType("EG_H3303_ST10_19_2800_2070");
        }
        catch
        {
            // Ignore cleanup errors if board type doesn't exist
        }

        // Clean up offcut board type (automatically created when offcut entity is created)
        try
        {
            await _MaterialManagerClientMaterialBoards.DeleteBoardType("XEG_H3303_ST10_19_1000.0_500.0");
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
        _MaterialAssistClientBoards = GetMaterialAssistClient().Boards;
        _MaterialManagerClientMaterialBoards = GetMaterialManagerClient().Material.Boards;
        await EnsureBoardTypeExist("EG_H3303_ST10_19");
    }
}