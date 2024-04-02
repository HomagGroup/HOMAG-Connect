using FluentAssertions;
using HomagConnect.MmrMobile.Client;
using System.Net.Http.Headers;

namespace HomagConnect.MmrMobile.Tests;

/// <summary>
/// 
/// </summary>
[TestClass]
[TestCategory("MmrMobile")]

public class MmrMobileClientTests : MmrTestBase
{
    /// <summary>
    /// 
    /// </summary>
    protected Guid TestSubscriptionId { get; } = new("62b8fee0-1b35-41c1-b03a-b947304a0d58");

    /// <summary>
    /// getmachines
    /// </summary>
    /// <returns></returns>
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

    [TestMethod]
    public async Task GetPointInTimeValue()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetPointInTimeValuesFromMachine(machines.First()?.MachineNumber ?? "123", "Test", DateTime.Now);

        // Assert
        result.Should().NotBeNull();

    }

    [TestMethod]
    public async Task GetHistoricalValue()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        var result = await client.GetTimeSeriesFromMachine(machines.First()?.MachineNumber ?? "123", "Test", DateTime.Now.AddDays(-5), DateTime.Now);

        // Assert
        result.Should().NotBeNull();

    }

    [TestMethod]
    public async Task GetHistoricalValueFail()
    {
        // Arrange
        var client = GetMmrMobileClient();

        // Act
        var machines = await client.GetMachines();
        try
        {
            await client.GetTimeSeriesFromMachine(machines.First()?.MachineNumber ?? "123", "Test", DateTime.Now.AddDays(-5), DateTime.Now, 1001);
            Assert.Fail();
        }
        catch (Exception)
        {
            // expected exception to be ignored
        }

    }
    protected virtual MmrMobileClient GetMmrMobileClient()
    {
        var (baseUrl, username, token) = ReadProps("MmrMobile");

        if (TestContext != null && TestContext.Properties.Contains("BaseUrl"))
        {
            baseUrl = TestContext.Properties["BaseUrl"]?.ToString() ;
        }

        if (TestContext != null && TestContext.Properties.Contains("Token"))
        {
            token = TestContext.Properties["Token"]?.ToString();
        }

        if (TestContext != null && TestContext.Properties.Contains("Username"))
        {
            username = TestContext.Properties["Username"]?.ToString();
        }

        baseUrl.Should().NotBeNullOrEmpty();
        username.Should().NotBeNullOrEmpty();
        token.Should().NotBeNullOrEmpty();

        baseUrl ??= "dummy"; // sonar
        username ??= "dummy"; // sonar
        token ??= "dummy"; // sonar
        var client = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(username, token));
        return new MmrMobileClient(client);
    }
}