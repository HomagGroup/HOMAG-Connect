using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.DecorMatches;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Decor;

/// <summary />
[TestClass]
[TestCategory("DeploymentTests.MaterialManager")]
[TestCategory("DeploymentTests.MaterialManager.Decor")]
[TemporaryDisabledOnServer(2026, 10, 15, "DF-Insights")]
public class EdgebandDecorTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task Decor_Search_NoException()
    {
        var materialClient = GetMaterialManagerClient();

        var decors = await materialClient.Material.Edgebands.SearchBoardDecors(new EdgebandTypeDecorMatchSearchRequest
        {
            Material = new EdgebandType()
            {
                    EdgebandCode = "code",
                    ManufacturerName = "Egger",
                    DecorCode = "egger:aaa_bbb",
                    DecorName = "f004",
                    DecorEmbossingCode = "st04"
            }
        });

        decors.ShouldNotBeNull($"object required");

        decors.Trace();
    }
}
