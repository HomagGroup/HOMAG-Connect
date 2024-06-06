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
    public async Task GetHistoricalValue()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetTimeSeriesFromMachine(machines.First()?.MachineNumber ?? "123", "Test",
            DateTime.Now.AddDays(-5), DateTime.Now);

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
}