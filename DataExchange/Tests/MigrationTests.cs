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

        using var originalZipArchive = new ZipArchive(fileInfo.OpenRead(), ZipArchiveMode.Read);
        var (projectOriginal, projectFilesOriginal) = ProjectPersistenceManager.Load(originalZipArchive, projectOriginalDirectory.DirectoryInfo, false);

        Assert.IsNotNull(projectOriginal.Orders);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties);
        Assert.IsNotNull(projectOriginal.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        TestContext.AddResultFile(fileInfo.FullName);

        using var projectMigratedDirectory = DisposableTempDirectory.Create();
        using var migratedZipArchive = new ZipArchive(fileInfo.OpenRead(), ZipArchiveMode.Read);
        var (projectMigrated, projectFilesMigrated) = ProjectPersistenceManager.Load(migratedZipArchive, projectMigratedDirectory.DirectoryInfo, true);

        Assert.IsNotNull(projectMigrated.Orders);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties);
        Assert.IsNotNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectMigrated.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        var output = new FileInfo($"project-01.migrated.{Guid.NewGuid()}.zip");

        using var projectReloadedDirectory = DisposableTempDirectory.Create();
        projectMigrated.SaveToZipArchive(output, projectFilesMigrated);
        using var reloadedZipArchive = new ZipArchive(output.OpenRead(), ZipArchiveMode.Read);
        var (projectReloaded, projectFilesReloaded) = ProjectPersistenceManager.Load(reloadedZipArchive, projectReloadedDirectory.DirectoryInfo, true);

        Assert.IsNotNull(projectReloaded.Orders);
        Assert.IsNotNull(projectReloaded.Orders[0].Properties);
        Assert.IsNotNull(projectReloaded.Orders[0].Properties.FirstOrDefault(p => p.Name == "Street"));
        Assert.IsNull(projectReloaded.Orders[0].Properties.FirstOrDefault(p => p.Name == "AddressField1"));

        TestContext.AddResultFile(output.FullName);
    }
}