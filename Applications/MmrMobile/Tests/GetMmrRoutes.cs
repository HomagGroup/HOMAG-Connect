using HomagConnect.Base.TestBase.Attributes;

namespace HomagConnect.MmrMobile.Tests;

[TestClass]
[TestCategory("MmrMobile")]
public class GetMmrRoutes : MmrTestBase
{
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 4, 15, "DF-Insights")]
    public async Task GetCounterTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var counters = await mmrMobileClient.GetCounterData();

        Assert.IsNotNull(counters);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2025, 4, 15, "DF-Insights")]
    public async Task GetStatesTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var states = await mmrMobileClient.GetStateData();

        Assert.IsNotNull(states);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2025, 4, 15, "DF-Insights")]
    public async Task GetMmrMachinesTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var machines = await mmrMobileClient.GetMmrMachines();

        Assert.IsNotNull(machines);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2025, 4, 15, "DF-Insights")]
    public async Task GetMmrMachineTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var machine = await mmrMobileClient.GetMmrMachine("t-201-97-1701");

        Assert.IsNotNull(machine);
    }
}