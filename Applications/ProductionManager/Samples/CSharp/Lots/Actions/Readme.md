# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the lot can be created from productionManager for further programmatic evaluation.

<strong>Example CreateLotRequest:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

var request = new CreateLotRequest
{
    StartDatePlanned = DateTime.Now,
    CompletionDatePlanned = DateTime.Now.AddDays(10),
    Name = "Test Lot",
    PartIds = new List<string>()
    {
        // Add existing part ids from an order
        // This can be replaced with actual part ids you want to add in the lot
        "1043898",
        "1043899",
    }
};

var response = await productionManager.CreateLotRequest(request);

response.Trace();
``` 

The response is [CreateLotResponse](../../../../Contracts/Lots/CreateLotResponse.cs) object which exposes basic information about the request

The sample code can be found at [ProductionManager - Create Lot Samples ](CreateLotSamples.cs).
