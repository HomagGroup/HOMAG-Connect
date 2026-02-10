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
        const UnitSystem unitSystem = UnitSystem.Imperial;
        var solutionDetails = new SolutionDetails(unitSystem);

        ValidateUnitSystem(solutionDetails, unitSystem);
    }

    /// <summary />
    [TestMethod]
    [UnitTest("IntelliDivide.Optimizations.UnitSystem")]
    public void SolutionDetails_UnitSystem_Imperial_SwitchToMetric()
    {
        var solutionDetailsImperial = new SolutionDetails(UnitSystem.Imperial);
        var solutionDetailsMetric = solutionDetailsImperial.SwitchUnitSystem(UnitSystem.Metric, true);

        ValidateUnitSystem(solutionDetailsImperial, UnitSystem.Imperial);
        ValidateUnitSystem(solutionDetailsMetric, UnitSystem.Metric);
    }

    private static void ValidateUnitSystem(SolutionDetails imperialSolutionDetails, UnitSystem unitSystem)
    {
        imperialSolutionDetails.UnitSystem.ShouldBe(unitSystem);

        imperialSolutionDetails.Overview.UnitSystem.ShouldBe(unitSystem);

        imperialSolutionDetails.Overview.Figures.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.Overview.Figures.Costs.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.Overview.Figures.Production.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.Overview.Figures.Material.UnitSystem.ShouldBe(unitSystem);

        imperialSolutionDetails.KeyFigures.Production.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.KeyFigures.Production.Output.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.KeyFigures.Production.Handling.UnitSystem.ShouldBe(unitSystem);

        imperialSolutionDetails.KeyFigures.Material.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.KeyFigures.Material.BoardsAndOffcuts.UnitSystem.ShouldBe(unitSystem);
        imperialSolutionDetails.KeyFigures.Material.Edgebands.UnitSystem.ShouldBe(unitSystem);
    }
}