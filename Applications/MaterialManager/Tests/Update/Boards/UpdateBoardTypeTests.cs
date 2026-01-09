using FluentAssertions;
using HomagConnect.MaterialManager.Samples.Update.Boards;
using Shouldly;
using System;

namespace HomagConnect.MaterialManager.Tests.Update.Boards;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Boards")]
public class UpdateBoardTypeTests : MaterialManagerTestBase
{

    private readonly Random random = new Random();
    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType()
    {
        var value = Math.Round(RandomBetween(5.0, 25.0), 2);

        var materialManagerClient = GetMaterialManagerClient();
        const string boardCode = "HPL_F274_9_19.0_2800_2070";
        await UpdateBoardTypeSamples.Boards_UpdateBoardType(materialManagerClient.Material.Boards, boardCode, value);

        var checkBoard = await materialManagerClient.Material.Boards.GetBoardTypeByBoardCode(boardCode);

        checkBoard.ShouldNotBeNull(
            $"because board type with board code '{boardCode}' should exist after update");

        checkBoard.Costs.ShouldNotBeNull();
        checkBoard.Costs.Value.ShouldBe(value, 0.0001, "because the costs should match");
    }

    /// <summary />
    [TestMethod]
    public async Task BoardsUpdateBoardType_WithAdditionalData_Succeeds()
    {
        var random = new Random();
        var value = Math.Round(RandomBetween(5.0, 25.0), 2);

        var materialManagerClient = GetMaterialManagerClient();
        const string boardCode = "HPL_F274_9_19.0_2800_2070";
        const string materialCode = "HPL_F274_9_19.0";
        await UpdateBoardTypeSamples.Boards_UpdateBoardType_AdditionalData(materialManagerClient.Material.Boards, materialCode, boardCode);

        var checkBoard = await materialManagerClient.Material.Boards.GetBoardTypeByBoardCode(boardCode);

        checkBoard.Should().NotBeNull(
            $"because board type with board code '{boardCode}' should exist after update");
        checkBoard!.Costs.Should().Be(value,
            $"because board type '{boardCode}' was updated to costs {value}");        
    }

    double RandomBetween(double min, double max)
    {
        return random.NextDouble() * (max - min) + min;
    }

    /// <summary />
    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new UpdateBoardTypeTests();
        await classInstance.EnsureBoardTypeExist("HPL_F274_9_19.0");
    }
}