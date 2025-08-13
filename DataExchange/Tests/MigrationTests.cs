using System.IO.Compression;

using HomagConnect.Base.Extensions;
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
        using var projectOriginalDirectory = DisposableTempDirectory.Create();
        var fileInfo = new FileInfo("TestData\\project-01.zip");

        var (projectOriginal, projectFilesOriginal) = ProjectPersistenceManager.Load(new ZipArchive(fileInfo.OpenRead(), ZipArchiveMode.Read), projectOriginalDirectory.DirectoryInfo, false);

        Assert.IsNotNull(projectOriginal.Orders);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        TestContext.AddResultFile(fileInfo.FullName);

        using var projectMigratedDirectory = DisposableTempDirectory.Create();
        var (projectMigrated, projectFilesMigrated) = ProjectPersistenceManager.Load(new ZipArchive(fileInfo.OpenRead(), ZipArchiveMode.Read), projectMigratedDirectory.DirectoryInfo,  true);

        Assert.IsNotNull(projectMigrated.Orders);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        var output = new FileInfo($"project-01.migrated.{Guid.NewGuid()}.zip");

        using var projectReloadedDirectory = DisposableTempDirectory.Create();
        projectMigrated.SaveToZipArchive(output, projectFilesMigrated);
        var (projectReloaded, projectFilesReloaded) = ProjectPersistenceManager.Load(new ZipArchive(output.OpenRead(), ZipArchiveMode.Read), projectReloadedDirectory.DirectoryInfo, true);

        Assert.IsNotNull(projectReloaded.Orders);
        Assert.IsNotNull(projectReloaded.Orders[0].Properties);
        Assert.IsNotNull(projectReloaded.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectReloaded.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        TestContext.AddResultFile(output.FullName);
    }
}