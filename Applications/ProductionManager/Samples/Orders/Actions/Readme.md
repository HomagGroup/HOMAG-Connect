# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the orders can be retrieved from productionManager for further programmatic evaluation.

<strong>Example:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var orders =  await client.GetOrdersAsync();

// Use the retrieved data
  orders.Trace();

  Assert.IsTrue(orders.Any());
  foreach (var order in orders)
  {
      Assert.IsFalse(string.IsNullOrEmpty(order.Name));
      Assert.AreNotEqual(Guid.Empty, order.Id);
  }

  var orderNames = orders.Select(x => x.Name).ToList();
  orderNames.Trace(nameof(orderNames));
``` 

The response is a list of [Order](../../../Contracts/Import/Order.cs) which exposes basic information about the order

The sample code can be found at [ProductionManager - Get Orders sample ](GetOrdersSamples.cs).
