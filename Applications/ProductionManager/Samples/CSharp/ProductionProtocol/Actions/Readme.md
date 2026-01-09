# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the orders can be retrieved from productionManager for further programmatic evaluation.

<strong>Example GetProductionProtocol:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var workstationId = "your-workstation-id"; // this is a GUID string
var productionProtocol = await productionManager.GetProductionProtocol(workstationId).ToListAsync();

// Use the retrieved data
productionProtocol.Trace();

Assert.IsTrue(productionProtocol.Any());
var protocol = productionProtocol.Select(x => x.Id).ToList();
reworkIds.Trace(nameof(protocol));
``` 