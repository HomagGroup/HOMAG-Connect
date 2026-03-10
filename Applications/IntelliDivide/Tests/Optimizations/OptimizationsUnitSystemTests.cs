using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Contracts.Result;
using HomagConnect.IntelliDivide.Tests.Base;

using Shouldly;

namespace HomagConnect.IntelliDivide.Tests.Optimizations;

/// <summary />
[TestClass]
public class OptimizationsUnitSystemTests : IntelliDivideTestBase
{
    /// <summary />
    [TestMethod]
    [UnitTest("IntelliDivide.Optimizations.UnitSystem")]
    public void SolutionDetails_UnitSystem_Imperial()
    {
        const UnitSystem sourceUnitSystem = UnitSystem.Imperial;
        const UnitSystem targetUnitSystem = UnitSystem.Metric;
        const double conversionFactorImperialToMetric = 2;

        var solutionDetailsImperial = new SolutionDetails(sourceUnitSystem);
        solutionDetailsImperial.KeyFigures.Production.Handling.BookWeightMax = 100;
        solutionDetailsImperial.KeyFigures.Production.Handling.BookWeightAverage = 50;

        solutionDetailsImperial.UnitSystem.ShouldBe(sourceUnitSystem);

        var solutionDetailsMetric = solutionDetailsImperial.SwitchUnitSystem(targetUnitSystem, false);

        solutionDetailsMetric.UnitSystem.ShouldBe(targetUnitSystem);
        solutionDetailsMetric.KeyFigures.Production.Handling.BookWeightMax
            .ShouldBe(solutionDetailsImperial.KeyFigures.Production.Handling.BookWeightMax / conversionFactorImperialToMetric);
        solutionDetailsMetric.KeyFigures.Production.Handling.BookWeightAverage
            .ShouldBe(solutionDetailsImperial.KeyFigures.Production.Handling.BookWeightAverage / conversionFactorImperialToMetric);
    }
}