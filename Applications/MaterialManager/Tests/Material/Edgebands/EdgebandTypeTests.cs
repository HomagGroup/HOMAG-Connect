using FluentAssertions;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Tests;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialManager.Tests.Material.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class EdgebandTypeTests : TestBase
{
    /// <summary />
    protected override Guid UserSecretsFolder { get; set; } = new("7a028258-94b9-4d79-822a-1005e4558b74");

    /// <summary />
    [TestMethod]
    public void EdgebandType_CheckConfiguration_ConfigValid()
    {
        BaseUrl.Should().NotBeNull();
        SubscriptionId.Should().NotBeEmpty();
        AuthorizationKey.Should().NotBeNullOrEmpty();
    }

    /// <summary />
    [TestMethod]
    public void EdgebandType_SwitchUnitSystem_LengthWidthThicknessChanged()
    {
        var edgebandTypeMetric = new EdgebandType()
        {
            Length = 150,
            Thickness = 19,
            Airtec = 4,
            ProtectionFilmThickness = 0.1,
            TotalLengthAvailableWarningLimit = 1000
        };

        Trace(edgebandTypeMetric);

        var edgebandTypeImperial = edgebandTypeMetric.SwitchUnitSystem(UnitSystem.Imperial, true);

        Trace(edgebandTypeImperial);

        Assert.AreEqual(UnitSystem.Imperial, edgebandTypeImperial.UnitSystem);

        Assert.AreNotEqual(edgebandTypeMetric.Length, edgebandTypeImperial.Length);
        Assert.AreNotEqual(edgebandTypeMetric.Thickness, edgebandTypeImperial.Thickness);
        Assert.AreNotEqual(edgebandTypeMetric.ProtectionFilmThickness, edgebandTypeImperial.ProtectionFilmThickness);
        Assert.AreNotEqual(edgebandTypeMetric.Airtec, edgebandTypeImperial.Airtec);
        Assert.AreNotEqual(edgebandTypeMetric.TotalLengthAvailableWarningLimit, edgebandTypeImperial.TotalLengthAvailableWarningLimit);
    }
}