using HomagConnect.Base.Extensions;
using HomagConnect.ProductionManager.Samples.ProductionProtocol.Actions;

namespace HomagConnect.ProductionManager.Tests;

/// <summary />
[TestClass]
[TestCategory("ProductionManager")]
public class ProductionManagerTest : ProductionManagerTestBase
{
    /// <summary />
    [TestMethod]
    public async Task ProductionManager_GetProductionProtocol_NoException()
    {
        var exceptionThrown = false;
        var client = GetProductionManagerClient();

        try
        {
           await GetProductionProtocolSamples.GetProductionProtocol(client);
        }
        catch (Exception e)
        {
            e.Trace();
            exceptionThrown = true;
        }

        Assert.IsFalse(exceptionThrown);
    }
}
