# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the orders can be retrieved from productionManager for further programmatic evaluation.

<strong>Example GetOrderItems :</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

 //Get the data
 var orderItems = await client.GetOrderItems(identifiers);
 
 //Use the retrieved data
 orderItems.Trace();
``` 

The response is a list of [ProductionItemBase](../../../../Contracts/ProductionItems/ProductionItemBase.cs) which exposes basic information about the order item

The sample code can be found at [ProductionManager - Get OrderItems sample ](OrderItemsSamples.cs).
