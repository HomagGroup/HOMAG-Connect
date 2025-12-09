# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the lot can be created/removed from productionManager for further programmatic evaluation.

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

<strong>Example DeleteOrDecomposeLotByLotId:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

 var lotId = Guid.Parse("PUT_LOT_ID_HERE");

 await productionManager.DeleteOrDecomposeLotByLotId(lotId);
``` 

The sample code can be found at [ProductionManager - Delete or Decompose Lot Samples ](DeleteOrDecomposeLotSamples.cs).

<strong>Example DeleteOrDecomposeLotsByLotIds:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

 var lotId = new[] 
{
    Guid.Parse("PUT_LOT_ID1_HERE"),
    Guid.Parse("PUT_LOT_ID2_HERE")
};

await productionManager.DeleteOrDecomposeLotsByLotIds(lotId);
``` 

The sample code can be found at [ProductionManager - Delete or Decompose Lot Samples ](DeleteOrDecomposeLotSamples.cs).

<strong>Example DeleteOrDecomposeLotByLotName:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

var lotName = "PUT_LOT1_Name_HERE";

await productionManager.DeleteOrDecomposeLotByLotName(lotName);
``` 

The sample code can be found at [ProductionManager - Delete or Decompose Lot Samples ](DeleteOrDecomposeLotSamples.cs).

<strong>Example DeleteOrDecomposeLotsByLotNames:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

var lotNames = new[]
{
    "PUT_LOT1_Name_HERE",
    "PUT_LOT2_Name_HERE"
};

await productionManager.DeleteOrDecomposeLotsByLotNames(lotNames);
``` 

The sample code can be found at [ProductionManager - Delete or Decompose Lot Samples ](DeleteOrDecomposeLotSamples.cs).
