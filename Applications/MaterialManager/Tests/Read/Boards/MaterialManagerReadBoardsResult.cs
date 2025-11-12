using FluentAssertions;

using HomagConnect.MaterialManager.Samples.Read.Boards;

namespace HomagConnect.MaterialManager.Tests.Read.Boards;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Board.Read.Results")]
public class MaterialManagerReadBoardsResult : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task GetLocations_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();
        var boardCodes = new[] { "P2_Graphitschwarz_19.0_2800_2070", "P2_Lichtgrau_19_2800_2070" };

        var act = async () => await MaterialManagerReadBoardsResults.GetLocations(materialManager.Material.Boards, boardCodes);

        await act.Should().NotThrowAsync(
            $"because GetLocations should retrieve locations for board codes '{string.Join(", ", boardCodes)}' successfully");
    }
    
    /// <summary />
    [TestMethod]
    public async Task GetMaterialCodes_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadBoardsResults.GetMaterialCodes(materialManager.Material.Boards);

        await act.Should().NotThrowAsync(
            "because GetMaterialCodes should retrieve material codes successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task GetThumbnails_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadBoardsResults.GetThumbnails(materialManager.Material.Boards);

        await act.Should().NotThrowAsync(
            "because GetThumbnails should retrieve thumbnails successfully");
    }
    
    /// <summary />
    [TestMethod]
    public async Task GetBoardTypes_ChangedSince_NoException()
    {
        var materialManager = GetMaterialManagerClient();
        var result = await materialManager.Material.Boards.GetBoardTypes(DateTimeOffset.UtcNow.AddDays(-2), 2);
        Assert.IsNotNull(result, "because GetBoardTypes should return a result for changed since filter");
    }
}