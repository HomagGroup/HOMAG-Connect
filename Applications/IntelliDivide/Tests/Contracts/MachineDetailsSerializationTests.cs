using HomagConnect.Base.Contracts;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Configuration;

using Newtonsoft.Json;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Contracts;

/// <summary>
/// Contains serialization tests for <see cref="MachineDetails" />.
/// </summary>
[TestClass]
[UnitTest("IntelliDivide.Contracts")]
public class MachineDetailsSerializationTests
{
    /// <summary>
    /// Verifies that the cutting subtypes and the German configuration keys survive a serialization roundtrip.
    /// </summary>
    [TestMethod]
    public void MachineDetails_SerializeDeserialize_KeepsCuttingSubtypesAndGermanKeys()
    {
        var machineDetails = new MachineDetails
        {
            Name = "SAWTEQ S-300",
            Configuration = new()
            {
                MachineConfiguration = new CuttingMachineConfiguration
                {
                    ConfigurationOptions = { ["conf_saegeart"] = "WS" },
                    ProgramFences = { new ProgramFence { Number = 1, Clamps = { new Clamp { Number = 2, Width = 40, ClampFingers = { new ClampFinger { Width = 40 } } } } } }
                },
                SawMaterialConfiguration = new CuttingSawMaterialConfiguration { Parameters = { ["SaegeblattStaerke"] = "4.4" } },
                SawConfiguration = new CuttingSawConfiguration
                {
                    Parameters = { ["SchnittlaengeMaximal_LS"] = "4300" },
                    FlexTecParameter = new FlexTecParameter { ActiveBufferLength = 3000, MsmComponentConfigurations = { new MsmComponentConfiguration { Name = "MSM" } } }
                }
            }
        };

        var json = JsonConvert.SerializeObject(machineDetails, SerializerSettings.Default);
        var deserialized = JsonConvert.DeserializeObject<MachineDetails>(json, SerializerSettings.Default);

        json.ShouldContain("\"SchnittlaengeMaximal_LS\"");

        deserialized.ShouldNotBeNull();
        deserialized.Name.ShouldBe("SAWTEQ S-300");
        var configuration = deserialized.Configuration;

        var machineConfiguration = configuration.MachineConfiguration.ShouldBeOfType<CuttingMachineConfiguration>();
        machineConfiguration.ConfigurationOptions["conf_saegeart"].ShouldBe("WS");
        var clamp = machineConfiguration.ProgramFences.ShouldHaveSingleItem().Clamps.ShouldHaveSingleItem();
        clamp.Width.ShouldBe(40);
        clamp.ClampFingers.ShouldHaveSingleItem().Width.ShouldBe(40);

        configuration.SawMaterialConfiguration.ShouldBeOfType<CuttingSawMaterialConfiguration>().Parameters["SaegeblattStaerke"].ShouldBe("4.4");

        var sawConfiguration = configuration.SawConfiguration.ShouldBeOfType<CuttingSawConfiguration>();
        sawConfiguration.Parameters["SchnittlaengeMaximal_LS"].ShouldBe("4300");
        sawConfiguration.OptimizationType.ShouldBe(OptimizationType.Cutting);
        sawConfiguration.FlexTecParameter.ActiveBufferLength.ShouldBe(3000);
        sawConfiguration.FlexTecParameter.MsmComponentConfigurations.ShouldHaveSingleItem().Name.ShouldBe("MSM");
    }
}
