using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class CreateOffcutsTests : MaterialAssistTestBase
{
    private const string _OffcutEntityId = "11114";
    private MaterialAssistClientBoards _MaterialAssistClientBoards;
    private MaterialManagerClientMaterialBoards _MaterialManagerClientMaterialBoards;

    [TemporaryDisabledOnServer(2026,2,28, "DF-Material")]
    [TestMethod]
    public async Task BoardsCreateOffcutEntity()
    {
        try
        {
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(_MaterialAssistClientBoards, _OffcutEntityId);
        }
        catch (Exception )
        {
            // do nothing, the entity might already exist
        }

        var offcutEntity = await _MaterialAssistClientBoards.GetBoardEntityByCode(_OffcutEntityId);

        offcutEntity.ShouldNotBeNull(
            $"because offcut entity with ID '{_OffcutEntityId}' should be created successfully");
        offcutEntity!.Id.ShouldBe(_OffcutEntityId,
            $"because we created offcut entity with ID '{_OffcutEntityId}'");
        offcutEntity.BoardType.BoardTypeType.ShouldBe(BoardTypeType.Offcut,
            $"because entity '{_OffcutEntityId}' was created as an offcut");
        offcutEntity.ManagementType.ShouldBe(ManagementType.Single,
            $"because offcut entity '{_OffcutEntityId}' was created with ManagementType.Single");
        offcutEntity.Quantity.ShouldBe(1,
            "because Single management type must have quantity of 1");
        offcutEntity.Length.ShouldBe(1000.0,
            $"because offcut entity '{_OffcutEntityId}' was created with length 1000.0");
        offcutEntity.Width.ShouldBe(500.0,
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