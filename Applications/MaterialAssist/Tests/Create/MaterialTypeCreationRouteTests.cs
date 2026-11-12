using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Create;

[TestClass]
[TestCategory("DeploymentTests.MaterialAssist")]
public class MaterialTypeCreationRouteTests : MaterialAssistTestBase
{
    [TestMethod]
    [TestCategory("DeploymentTests.MaterialAssist.Boards")]
    public async Task CreateBoardType_ThroughMaterialAssist_ReachesApi()
    {
        var boardCode = $"MA_BOARD_{Guid.NewGuid():N}";
        var assistClient = GetMaterialAssistClient();
        var managerClient = GetMaterialManagerClient();

        var boardType = await assistClient.Boards.CreateBoardType(new MaterialManagerRequestBoardType
        {
            BoardCode = boardCode,
            MaterialCode = boardCode,
            Length = 2800,
            Width = 1310,
            Thickness = 19,
            Type = BoardTypeType.Board,
            Grain = Grain.None,
            CoatingCategory = CoatingCategory.Undefined,
            MaterialCategory = BoardMaterialCategory.Undefined
        });

        try
        {
            boardType.BoardCode.ShouldBe(boardCode);
            boardType.MaterialCode.ShouldBe(boardCode);
        }
        finally
        {
            await managerClient.Material.Boards.DeleteBoardType(boardCode);
        }
    }

    [TestMethod]
    [TestCategory("DeploymentTests.MaterialAssist.Edgebands")]
    public async Task CreateEdgebandType_ThroughMaterialAssist_ReachesApi()
    {
        var edgebandCode = $"MA_EDGE_{Guid.NewGuid():N}";
        var assistClient = GetMaterialAssistClient();
        var managerClient = GetMaterialManagerClient();

        var edgebandType = await assistClient.Edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = edgebandCode,
            Height = 20,
            Thickness = 1,
            DefaultLength = 23,
            MaterialCategory = EdgebandMaterialCategory.ABS,
            Process = EdgebandingProcess.Other
        });

        try
        {
            edgebandType.EdgebandCode.ShouldBe(edgebandCode);
        }
        finally
        {
            await managerClient.Material.Edgebands.DeleteEdgebandType(edgebandCode);
        }
    }
}
