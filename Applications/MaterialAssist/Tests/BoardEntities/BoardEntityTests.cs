using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Tests.BoardEntities;

[TestClass]
[TestCategory("MaterialAssist")]
[TestCategory("MaterialAssist.Boards")]
public class BoardEntityTests : MaterialAssistTestBase
{
    [TestMethod]
    public async Task MaterialAssist_BoardEntities_List()
    {
        var client = GetMaterialAssistClient().Boards;

        var boardEntities = await client.GetBoardEntities(5).ConfigureAwait(false) ?? new List<BoardEntity>();

        foreach (var boardEntity in boardEntities)
        {
            boardEntity.Trace();
        }
    }
}