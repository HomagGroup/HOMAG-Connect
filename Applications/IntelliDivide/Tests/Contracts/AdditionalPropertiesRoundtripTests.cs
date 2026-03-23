using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.Base.TestBase.Helpers;
using HomagConnect.IntelliDivide.Contracts.Result;

namespace HomagConnect.IntelliDivide.Tests.Contracts;

/// <summary>
/// Contains roundtrip serialization tests for IntelliDivide contracts that support <c>AdditionalProperties</c>.
/// </summary>
[TestClass]
[UnitTest("IntelliDivide.Contracts")]
public class AdditionalPropertiesRoundtripTests
{
    /// <summary>
    /// Gets or sets the MSTest context for the current test execution.
    /// </summary>
    public TestContext? TestContext { get; set; }

    /// <summary>
    /// Verifies that roundtrip serialization does not introduce unexpected extension-data entries for IntelliDivide contracts that support <c>AdditionalProperties</c>.
    /// </summary>
    [TestMethod]
    public void IntelliDivide_SerializeDeserialize_RoundtripDoesNotAddAdditionalProperties_ForAllContractsSupportingAdditionalProperties()
    {
        RoundtripTestSerializationValidator.SerializeDeserialize_RoundtripDoesNotAddAdditionalProperties_ForAllContracts(
            typeof(Solution).Assembly,
            "HomagConnect.IntelliDivide",
            TestContext);
    }
}
