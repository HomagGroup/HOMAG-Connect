# Access data using the HOMAG Connect Client

### 1. GetWorkstations
With the HOMAG Connect Client, the workstations can be retrieved from productionAssist for further programmatic evaluation.

<strong>Example:</strong>

```c#
// Create new instance of the productionAssist client:
  var client = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);

// Get the data
  var workstations = await client.GetWorkstationsAsync();

// Use the retrieved data
  workstations.Trace();

  Assert.IsTrue(workstations.Any());

  var workstationsNames = workstations.Select(x => x.DisplayName).ToList();
  workstationsNames.Trace(nameof(workstationsNames));
``` 

The response is a list of [FeedbackWorkstation](../../Contracts/Feedback/FeedbackWorkstation.cs) which exposes basic information about the workstation


### 2. ReportAsFinished
With the HOMAG Connect Client, a production entity can be reported as finished. For this, you have to specify three parameters: workstationId, productionEntityId, quantity.

<strong>Example:</strong>

```c#
// Create new instance of the productionAssist client:
  var client = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);

// Create the request
  await client.ReportAsFinishedAsync(Guid.NewGuid(), Guid.NewGuid().ToString(), 1);
```

The sample code can be found at [ProductionAssist - Feedback ](ProductionAssistFeedbackSamples.cs).
