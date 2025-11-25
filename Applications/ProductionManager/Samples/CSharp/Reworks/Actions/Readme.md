# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the orders can be retrieved from productionManager for further programmatic evaluation.

<strong>Example GetCompletedReworks:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var completedReworks = await productionManager.GetCompletedReworks().ToListAsync();

// Use the retrieved data
completedReworks.Trace();

Assert.IsTrue(completedReworks.Any());
var reworkIds = completedReworks.Select(x => x.Id).ToList();
reworkIds.Trace(nameof(reworkIds));
``` 