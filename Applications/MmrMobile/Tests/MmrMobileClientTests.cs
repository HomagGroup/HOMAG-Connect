using System.IO.Compression;

using FluentAssertions;

namespace HomagConnect.MmrMobile.Tests;

/// <summary />
[TestClass]
[TestCategory("MmrMobile")]
public class MmrMobileClientTests : MmrTestBase
{
    /// <summary />
    [TestMethod]
    public async Task GetCurrentValue()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetCurrentValuesFromMachine(machines.First()?.MachineNumber ?? "123", "Test");

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary />
    [TestMethod]
    public async Task GetHistoricalEventSeries()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetAlertEventsFromMachine(machines.First()?.MachineNumber ?? "123",
            DateTime.Now.AddDays(-5), DateTime.Now, 1000);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary />
    [TestMethod]
    public async Task GetHistoricalValue()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetTimeSeriesFromMachine(machines.First()?.MachineNumber ?? "123", "Test",
            DateTime.Now.AddDays(-5), DateTime.Now, 1000);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary />
    [TestMethod]
    public async Task GetHistoricalValueFail()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        try
        {
            await client.GetTimeSeriesFromMachine(machines.First()?.MachineNumber ?? "123", "Test",
                DateTime.Now.AddDays(-5), DateTime.Now, 1001);
            Assert.Fail();
        }
        catch (Exception)
        {
            // expected exception to be ignored
        }
    }

    /// <summary />
    [TestMethod]
    public async Task GetMachinesForMmr()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var result = await client.GetMachines();

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary />
    [TestMethod]
    public async Task GetNodesForMachine()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetNodesOfMachine(machines.First()?.MachineNumber ?? "123");

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary />
    [TestMethod]
    public async Task GetPointInTimeValue()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result =
            await client.GetPointInTimeValuesFromMachine(machines.First()?.MachineNumber ?? "123", "Test",
                DateTime.Now);

        // Assert
        result.Should().NotBeNull();
    }

    /// <summary />
    [TestMethod]
    public async Task UploadProductionProtocol()
    {
        var rnd = new Random(Guid.NewGuid().GetHashCode());
        var projectDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"MmrMobileTestProject{Guid.NewGuid().ToString()}"));
        var fileName = Path.Combine(projectDirectory.FullName, "testdata");
        var destinationArchiveFileName = $"{Path.GetTempPath()}\\test.zip";
        // Arrange
        var client = GetMmrMobileClient();
        if (!projectDirectory.Exists)
        {
            projectDirectory.Create();
        }

        //create a file with all properties set
        await File.WriteAllTextAsync($"{fileName}.hol",
            $"{DateTime.Now.ToString("yyyyMMddHHmmss")},BRD,Run{rnd.Next(1000, 9999)},Pattern{rnd.Next(1000, 9999)},{rnd.Next(1, 5)},{rnd.Next(1, 5)},BoardName,MaterialName,1200.0,1400.0,10.0");
        if (File.Exists(destinationArchiveFileName))
        {
            File.Delete(destinationArchiveFileName);
        }

        ZipFile.CreateFromDirectory(projectDirectory.FullName, destinationArchiveFileName);

        using var fileStream = new FileStream(destinationArchiveFileName, FileMode.Open, FileAccess.Read);

        // Act
        await client.UploadProductionProtocol(fileStream, $"hgt{rnd.Next(240730001, 240739999)}");

        projectDirectory.Delete(true);
    }
}