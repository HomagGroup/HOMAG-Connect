using System.IO.Compression;

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
    public void Migrate_ProjectUsingAddressFields_Xml()
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

        projectMigrated.SaveToXmlFile(output);

        TestContext.AddResultFile(output.FullName);
    }

    /// <summary />
    [TestMethod]
    public void Migrate_ProjectUsingAddressFields_Zip()
    {
        var fileInfo = new FileInfo("TestData\\project-01.zip");

        var (projectOriginal, projectFilesOriginal) = ProjectPersistenceManager.Load(new ZipArchive(fileInfo.OpenRead(), ZipArchiveMode.Read), false);

        Assert.IsNotNull(projectOriginal.Orders);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        TestContext.AddResultFile(fileInfo.FullName);

        var extractDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "Project", Guid.NewGuid().ToString()));
        var (projectMigrated, projectFilesMigrated) = ProjectPersistenceManager.Load(new ZipArchive(fileInfo.OpenRead(), ZipArchiveMode.Read),  true);

        Assert.IsNotNull(projectMigrated.Orders);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        var output = new FileInfo($"project-01.migrated.{Guid.NewGuid()}.zip");

        projectMigrated.SaveToZipArchive(output, projectFilesMigrated);
        var (projectReloaded, projectFilesReloaded) = ProjectPersistenceManager.Load(new ZipArchive(output.OpenRead(), ZipArchiveMode.Read), true);

        Assert.IsNotNull(projectReloaded.Orders);
        Assert.IsNotNull(projectReloaded.Orders[0].Properties);
        Assert.IsNotNull(projectReloaded.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectReloaded.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        TestContext.AddResultFile(output.FullName);
    }
}