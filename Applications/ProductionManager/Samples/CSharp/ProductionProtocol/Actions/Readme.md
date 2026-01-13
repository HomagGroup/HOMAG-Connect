# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the orders can be retrieved from productionManager for further programmatic evaluation.

<strong>Example GetProductionProtocol:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the workstationId.
// Get a list of all workstations in productionAssist via the ProductionAssistClient - See HomagConnect.ProductionAssist.Samples for more information on how to get the workstations
// Choose one workstation and use its ID here.
var workstationId = "your-workstation-id"; // 

// Get the data
var productionProtocol = await productionManager.GetProductionProtocol(workstationId).ToListAsync();

// Use the retrieved data
productionProtocol.Trace();

Assert.IsTrue(productionProtocol.Any());
var protocol = productionProtocol.Select(x => x.Id).ToList();
reworkIds.Trace(nameof(protocol));
``` 

GetWorkstations information and code samples can be found at [Documentation for productionAssist Client](./../../../../../ProductionAssist/Samples/CSharp/Workstations/Readme.md).
