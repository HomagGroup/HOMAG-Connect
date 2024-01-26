using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests;

[TestClass]
public class GetTestContextTests : IntelliDivideTestBase
{
    public TestContext TestContext { get; set; }

    [TestMethod]
    public void GetBaseUrlFromContext()
    {
        TestContext.WriteLine("BaseUrl:" + TestContext.Properties["BaseUrl"]);
    }
}