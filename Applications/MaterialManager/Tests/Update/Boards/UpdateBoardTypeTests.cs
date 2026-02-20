using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Samples.Update.Boards;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update.Boards;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Boards")]
public class UpdateBoardTypeTests : MaterialManagerTestBase
{

    private const string MaterialCode = "HPL_F274_9_19.0";
    private const double length = 2800.0;
    private const double width = 2070.0;
    private string BoardTypeCode = $"{MaterialCode}_{length}_{width}";
    private MaterialManagerClientMaterialBoards materialManagerClient;

    /// <summary>
    /// Initializes the test by setting up the <see cref="MaterialManagerClient"/> and ensuring the board type exists.
    /// </summary>
    [TestInitialize]
    public async Task Init()
    {
        materialManagerClient = GetMaterialManagerClient().Material.Boards;
        await EnsureBoardTypeExist(materialManagerClient, BoardTypeCode, MaterialCode, length, width);
    }
    
    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType()
    {
        var value = Math.Round(RandomBetween(5.0, 25.0), 2);        

        await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient, BoardTypeCode, value);

        var checkBoard = await materialManagerClient.GetBoardTypeByBoardCode(BoardTypeCode);

        checkBoard.ShouldNotBeNull(
            $"because board type with board code '{BoardTypeCode}' should exist after update");

        checkBoard.Costs.ShouldNotBeNull();
        checkBoard.Costs.Value.ShouldBe(value, 0.0001, "because the costs should match");
    }

    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType_WithAdditionalData_Succeeds()
    {
        await UpdateBoardTypeSamples.Boards_UpdateBoardType_AdditionalData(materialManagerClient, MaterialCode, BoardTypeCode);

        var checkBoard = await materialManagerClient.GetBoardTypeByBoardCode(BoardTypeCode);
        checkBoard.ShouldNotBeNull(
            $"because board type with board code '{BoardTypeCode}' should exist after update");
    }
    
}