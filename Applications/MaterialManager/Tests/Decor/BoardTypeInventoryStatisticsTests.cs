using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Decor;

/// <summary />
[TestClass]
[TestCategory("DeploymentTests.MaterialManager")]
[TestCategory("DeploymentTests.MaterialManager.Decor")]
[TemporaryDisabledOnServer(2026, 10, 15, "DF-Insights")]
public class BoardDecorTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2026, 10, 15, "DF-Insights")]
    public async Task Decor_Search_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var decors = await materialClient.Material.Boards.SearchBoardDecors(new BoardTypeDecorMatchSearchRequest
        {
            Material = new BoardType
            {
                BoardCode = "code",
                MaterialCode = "code",
                ManufacturerName = "Egger",
                DecorCode = "egger:aaa_bbb",
                DecorName = "f004",
                EmbossingTop = "st04",
                Width = 1,
                Length = 1
            }
        });

        decors.ShouldNotBeNull($"object required");

        decors.Trace();
    }
}