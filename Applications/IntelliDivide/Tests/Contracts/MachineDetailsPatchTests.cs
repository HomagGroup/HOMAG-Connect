using HomagConnect.Base.Contracts;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts.Configuration;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Contracts;

/// <summary>
/// Contains tests for building patches of <see cref="MachineDetails" />.
/// </summary>
[TestClass]
[UnitTest("IntelliDivide.Contracts")]
public class MachineDetailsPatchTests
{
    [TestMethod]
    public void PatchBuilder_NestedAndTopLevelProperties_BuildsNestedJson()
    {
        var patch = PatchBuilder<MachineDetails>.For()
            .Set(m => m.Name, "SAWTEQ S-300 patched")
            .Set(m => m.Configuration.SawConfiguration.Parameters, new Dictionary<string, string> { ["SchnitthoeheMaximal"] = "95" })
            .Set(m => m.Configuration.MachineConfiguration.ConfigurationOptions, new Dictionary<string, string> { ["option_dreht"] = "1" });

        var json = JsonConvert.SerializeObject(patch.Build(), Formatting.None);

        json.ShouldBe("{\"name\":\"SAWTEQ S-300 patched\",\"configuration\":{\"sawConfiguration\":{\"parameters\":{\"SchnitthoeheMaximal\":\"95\"}},\"machineConfiguration\":{\"configurationOptions\":{\"option_dreht\":\"1\"}}}}");
    }

    [TestMethod]
    public void PatchBuilder_UnsetProperties_AreOmitted()
    {
        var patch = PatchBuilder<MachineDetails>.For().Set(m => m.Name, "SAWTEQ S-300 patched");

        patch.Build().Keys.ShouldBe(new[] { "name" });
    }
}
