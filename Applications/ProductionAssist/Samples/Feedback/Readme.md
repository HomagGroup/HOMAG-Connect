# Access data using the HOMAG Connect Client

### 1. GetWorkstations
With the HOMAG Connect Client, the workstations can be retrieved from productionAssist for further programmatic evaluation.

<strong>Example:</strong>

```c#
// Create new instance of the productionAssist client:
  var client = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);

// Get the data
  var workstations = await client.GetWorkstations();

// Use the retrieved data
  workstations.Trace();

  Assert.IsTrue(workstations.Any());

  var workstationsNames = workstations.Select(x => x.DisplayName).ToList();
  workstationsNames.Trace(nameof(workstationsNames));
``` 

The response is a list of [FeedbackWorkstation](../../Contracts/Feedback/FeedbackWorkstation.cs) which exposes basic information about the workstation


### 2. ReportAsFinished
With the HOMAG Connect Client, a production entity can be reported as finished. For this, you have to specify the following parameters:
- workstationId
-  productionEntityId (can be identitified by it)
-  publicId (can be identitified by it)
-  barcode (can be identitified by it)
-  quantity

<strong>Example:</strong>

```c#
// Create new instance of the productionAssist client:
  var client = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);

// Create the request
  var workstationId = Guid.NewGuid(); // should be replaced with an existing workstationId
  var productionEntityId = Guid.NewGuid().ToString(); // should be replaced with an existing productionEntityId, can be identitified by it
  var publicId = "123456"; // should be replaced with an existing public id, can be identitified by it
  var barcode = Guid.NewGuid().ToString(); // should be replaced with an existing barcode, can be identitified by it

  var quantity = 1;

  await client.ReportAsFinished(workstationId, productionEntityId, quantity, publicId, barcode);
```

The sample code can be found at [ProductionAssist - Feedback ](ProductionAssistFeedbackSamples.cs).
