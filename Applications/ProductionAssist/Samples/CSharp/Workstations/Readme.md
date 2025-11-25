# Access data using the HOMAG Connect Client

### 1. Get Workstations
With the HOMAG Connect Client, the workstations can be retrieved from productionAssist for further programmatic evaluation.

<strong>Example:</strong>

```c#
// Create new instance of the productionAssist client:
  var client = new ProductionAssistClient(subscriptionId, authorizationKey);

// Get the data
  var workstations = await client.GetWorkstations();

// Use the retrieved data
  workstations.Trace();

  Assert.IsTrue(workstations.Any());

  var workstationsNames = workstations.Select(x => x.DisplayName).ToList();
  workstationsNames.Trace(nameof(workstationsNames));
``` 

The response is a list of [Workstation](../../../Contracts/Workstation.cs) which exposes basic information about the workstation
The sample code can be found at [ProductionAssist Samples ](../../../Samples/CSharp/ProductionAssistSamples.cs).
