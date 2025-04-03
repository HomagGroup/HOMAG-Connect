using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.DataExchange.Tests;

/// <summary />
[TestClass]
[TestCategory("DataExchange")]
[TestCategory("DataExchange.Migration")]
public class MigrationTests
{
    /// <summary />
    public required TestContext TestContext { get; set; }

    /// <summary />
    [TestMethod]
    public void Migrate_ProjectUsingAddressFields()
    {
        var fileInfo = new FileInfo("TestData\\project-01.xml");

        var projectOriginal = ProjectPersistenceManager.Load(fileInfo, false);

        Assert.IsNotNull(projectOriginal.Orders);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        var projectMigrated = ProjectPersistenceManager.Load(fileInfo, true);

        Assert.IsNotNull(projectMigrated.Orders);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        var output = new FileInfo("project-01.migrated.xml");

        projectMigrated.Save(output);

        TestContext.AddResultFile(output.FullName);
    }
}