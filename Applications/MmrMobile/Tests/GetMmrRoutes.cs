﻿using HomagConnect.Base.TestBase.Attributes;

namespace HomagConnect.MmrMobile.Tests;

[TestClass]
[TestCategory("MmrMobile")]
public class GetMmrRoutes : MmrTestBase
{
    [TestMethod]
    public async Task GetCounterTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var counters = await mmrMobileClient.GetCounterData();

        Assert.IsNotNull(counters);
    }

    [TestMethod]
    public async Task GetStatesTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var states = await mmrMobileClient.GetStateData();

        Assert.IsNotNull(states);
    }

    [TestMethod]
    public async Task GetMmrMachinesTest()
    {
        var mmrMobileClient = GetMmrMobileClient();

        var machines = await mmrMobileClient.GetMmrMachines();

        Assert.IsNotNull(machines);
    }
}