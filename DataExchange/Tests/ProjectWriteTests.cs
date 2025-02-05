using HomagConnect.DataExchange.Samples;

namespace HomagConnect.DataExchange.Tests;

/// <summary />
[TestClass]
[TestCategory("DataExchange")]
[TestCategory("DataExchange.Project.Write")]
public class ProjectWriteTests
{
    /// <summary />
    public TestContext? TestContext { get; set; }

    [TestMethod]
    public void Project_WriteZip_TypicalProperties()
    {
        var projectFile = DataExchangeSamples.GetProjectHavingTypicalProperties();

        Assert.IsNotNull(projectFile);
        Assert.IsTrue(projectFile.Exists);

        TestContext?.AddResultFile(projectFile.FullName);
    }
}