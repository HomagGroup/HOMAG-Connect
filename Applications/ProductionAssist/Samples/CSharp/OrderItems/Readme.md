# Access data using the HOMAG Connect Client

### 1. Get Workstations
With the HOMAG Connect Client, the orderItems can be retrieved from productionAssist for further programmatic evaluation.

<strong>Example:</strong>

```c#
// Create new instance of the productionAssist client:
  var client = new ProductionAssistClient(subscriptionId, authorizationKey);
  var identifiers =new string[]
  {
      "orderItem1Identifier",
      "orderItem2Identifier"
  };

  //Get the data  
  var response = await client.GetOrderItems(identifiers);

  //use the retrieved data
  response.Trace();
``` 

The response is a list of [ProductionItemBase](../../Contracts/ProductionItemBase.cs) which exposes basic information about the workstation
The sample code can be found at [ProductionAssist Order Item Samples ](ProductionAssistOrderItemSamples.cs).
