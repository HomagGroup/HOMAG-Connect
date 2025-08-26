using HomagConnect.Base.Contracts.Events;
using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Events;

/// <summary />
[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Optimizations")]
public class OptimizationEventTests : IntelliDivideTestBase
{
    /// <summary />
    [TestMethod]
    public void Events_ListIntelliDivideEvents()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var derivedTypes = TypeFinder.FindDerivedTypes<AppEvent>(assemblies).ToArray();

        Assert.IsNotNull(derivedTypes);
        Assert.IsTrue(derivedTypes.Length > 0);

        derivedTypes.Trace();
    }
}