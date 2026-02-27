using Shouldly;

using HomagConnect.MaterialAssist.Samples.Update.Boards;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class UpdateBoardsTests : MaterialAssistTestBase
{

    private const string _MaterialCodeMdfH3171 = "MDF_H3171_12_19.0";
    private const string _BoardCodeMdfH3171 = _MaterialCodeMdfH3171 + "_2800_2070";
    private const string _BoardEntityCode = "834";
    [TestMethod]
    public async Task BoardsUpdateBoardEntity()
    {
        var random = new Random();

        var length = Math.Round(RandomBetween(100.0, 2000.0), 2);
        var width = Math.Round(RandomBetween(100.0, 2000.0), 2);

        var materialAssistClient = GetMaterialAssistClient().Boards;
        await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(materialAssistClient, length, width);

        var boardEntity = await materialAssistClient.GetBoardEntityByCode("834");

        boardEntity.ShouldNotBeNull(
            $"because board entity '{_BoardEntityCode}' should exist after update");
        Assert.AreEqual(length, boardEntity!.Length, 0.0001,
            $"because board entity '{_BoardEntityCode}' was updated to length {length}");
        Assert.AreEqual(width, boardEntity.Width, 0.0001,
            $"because board entity '{_BoardEntityCode}' was updated to width {width}");
        return;
    }

    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureBoardTypeExist(_MaterialCodeMdfH3171);
        await EnsureBoardEntityExist(_BoardEntityCode, _BoardCodeMdfH3171);
    }
}