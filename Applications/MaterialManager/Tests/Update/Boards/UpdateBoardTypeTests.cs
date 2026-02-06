using Shouldly;

using HomagConnect.MaterialManager.Samples.Update.Boards;

namespace HomagConnect.MaterialManager.Tests.Update.Boards;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Boards")]
public class UpdateBoardTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType()
    {
        var random = new Random();
        var value = Math.Round(RandomBetween(5.0, 25.0), 2);

        var materialManagerClient = GetMaterialManagerClient();
        const string boardCode = "HPL_F274_9_19.0_2800_2070";
        await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient.Material.Boards, boardCode, value);

        var checkBoard = await materialManagerClient.Material.Boards.GetBoardTypeByBoardCode(boardCode);

        checkBoard.ShouldNotBeNull(
            $"because board type with board code '{boardCode}' should exist after update");

        checkBoard.Costs.ShouldNotBeNull();
        checkBoard.Costs.Value.ShouldBe(value, 0.0001, "because the costs should match");

        
        return;

        double RandomBetween(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }

    /// <summary />
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new UpdateBoardTypeTests();
        await classInstance.EnsureBoardTypeExist("HPL_F274_9_19.0");
    }
}