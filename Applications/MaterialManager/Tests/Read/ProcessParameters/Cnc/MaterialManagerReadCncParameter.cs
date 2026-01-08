using Shouldly;

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

        await Should.NotThrowAsync(act,
            "because GetMillingParameterToolGroups should retrieve milling parameter tool groups successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task GetMillingParameterMaterialGroups_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadCncParameters.GetMillingParameterMaterialGroups(materialManager.Processing.Cnc);

        await Should.NotThrowAsync(act,
            "because GetMillingParameterMaterialGroups should retrieve milling parameter material groups successfully");
    }

    /// <summary />
    [TestMethod]
    public async Task GetMillingParameterGroups_GetResult_NoException()
    {
        var materialManager = GetMaterialManagerClient();

        var act = async () => await MaterialManagerReadCncParameters.GetMillingParameterGroups(materialManager.Processing.Cnc);

        await Should.NotThrowAsync(act,
            "because GetMillingParameterGroups should retrieve milling parameter groups successfully");
    }
}