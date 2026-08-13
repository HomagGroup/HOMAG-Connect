using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Samples.Update.Boards;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Update.Boards;

/// <summary />
[TestClass]
[TestCategory("DeploymentTests.MaterialManager")]
[TestCategory("DeploymentTests.MaterialManager.Boards")]
public class UpdateBoardTypeTests : MaterialManagerTestBase
{

    private const string _MaterialCode = "HPL_F274_9_19.0";
    private const double _Length = 2800.0;
    private const double _Width = 2070.0;
    private readonly string _BoardTypeCode = $"{_MaterialCode}_{_Length}_{_Width}";
    private MaterialManagerClientMaterialBoards _MaterialManagerClient = null!;

    /// <summary>
    /// Initializes the test by setting up the <see cref="MaterialManagerClient"/> and ensuring the board type exists.
    /// </summary>
    [TestInitialize]
    public async Task Init()
    {
        _MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
        await EnsureBoardTypeExist(_MaterialManagerClient, _BoardTypeCode, _MaterialCode, _Length, _Width);
    }
    
    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType()
    {
        var value = Math.Round(RandomBetween(5.0, 25.0), 2);        

        await UpdateBoardTypeSamples.Boards_UpdateBoardType(_MaterialManagerClient, _BoardTypeCode, value);

        var checkBoard = await _MaterialManagerClient.GetBoardTypeByBoardCode(_BoardTypeCode);

        checkBoard.ShouldNotBeNull(
            $"because board type with board code '{_BoardTypeCode}' should exist after update");

        checkBoard.Costs.ShouldNotBeNull();
        checkBoard.Costs.Value.ShouldBe(value, 0.0001, "because the costs should match");
    }

    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType_WithAdditionalData_Succeeds()
    {
        await UpdateBoardTypeSamples.Boards_UpdateBoardType_AdditionalData(_MaterialManagerClient, _MaterialCode, _BoardTypeCode);

        var checkBoard = await _MaterialManagerClient.GetBoardTypeByBoardCode(_BoardTypeCode);
        checkBoard.ShouldNotBeNull(
            $"because board type with board code '{_BoardTypeCode}' should exist after update");
    }
    
}