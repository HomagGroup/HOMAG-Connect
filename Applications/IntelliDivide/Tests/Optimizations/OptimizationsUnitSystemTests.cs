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

 
    }

}