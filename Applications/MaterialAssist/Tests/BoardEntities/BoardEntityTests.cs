namespace HomagConnect.MaterialAssist.Tests.BoardEntities
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class BoardEntityTests : MaterialAssistTestBase
    {
        /// <summary />
        [TestMethod]
        public async Task MaterialAssist_BoardEntities_List()
        {
            var client = GetMaterialAssistClient().Boards;

            var boardTypes = await client.GetBoardEntities(5);

            foreach (var boardType in boardTypes)
            {
                Trace(boardType);
            }
        }
    }
}