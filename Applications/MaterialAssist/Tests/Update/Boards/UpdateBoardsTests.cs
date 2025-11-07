using FluentAssertions;

using HomagConnect.MaterialAssist.Samples.Update.Boards;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class UpdateBoardsTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task BoardsUpdateBoardEntity()
    {
        var random = new Random();

        var length = Math.Round(RandomBetween(100.0, 2000.0), 2);
        var width = Math.Round(RandomBetween(100.0, 2000.0), 2);

        var materialAssistClient = GetMaterialAssistClient().Boards;
        await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(materialAssistClient, length, width);

        var boardEntity = await materialAssistClient.GetBoardEntityById("834");

        boardEntity.Should().NotBeNull(
            "because board entity '834' should exist after update");
        boardEntity!.Length.Should().Be(length,
            $"because board entity '834' was updated to length {length}");
        boardEntity.Width.Should().Be(width,
            $"because board entity '834' was updated to width {width}");
        return;

        double RandomBetween(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }

    [ClassInitialize]
    public static async Task Initialize(TestContext testContext)
    {
        var classInstance = new UpdateBoardsTests();
        await classInstance.EnsureBoardTypeExist("MDF_H3171_12_19.0");
        await classInstance.EnsureBoardEntityExist("834", "MDF_H3171_12_19.0_2800_2070");
    }
}