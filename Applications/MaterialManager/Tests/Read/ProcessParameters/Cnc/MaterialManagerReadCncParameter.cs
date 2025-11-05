using FluentAssertions;

using HomagConnect.MaterialManager.Samples.Read.ProcessParameters.Cnc;

namespace HomagConnect.MaterialManager.Tests.Read.ProcessParameters.Cnc;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Cnc.Read.Parameters")]
public class MaterialManagerReadCncParameter : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task GetMillingParameterToolGroups_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadCncParameters.GetMillingParameterToolGroups(materialManager.Processing.Cnc);

        await act.Should().NotThrowAsync(
            "because GetMillingParameterToolGroups should retrieve milling parameter tool groups successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task GetMillingParameterMaterialGroups_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadCncParameters.GetMillingParameterMaterialGroups(materialManager.Processing.Cnc);

        await act.Should().NotThrowAsync(
            "because GetMillingParameterMaterialGroups should retrieve milling parameter material groups successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task GetMillingParameterGroups_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadCncParameters.GetMillingParameterGroups(materialManager.Processing.Cnc);

        await act.Should().NotThrowAsync(
            "because GetMillingParameterGroups should retrieve milling parameter groups successfully");
    }
}