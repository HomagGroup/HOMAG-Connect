# Access data using the HOMAG Connect Client

With the HOMAG Connect Client, the orders can be retrieved from productionManager for further programmatic evaluation.

<strong>Example GetOrdersAsync:</strong>

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

The response is a list of [Order](../../../../Contracts/Orders/Order.cs) which exposes basic information about the order

The sample code can be found at [ProductionManager - Get Orders sample ](GetOrderSamples.cs).

<strong>Example GetOrder:</strong>

```c#
 // Create new instance of the productionManager client:
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

// Get the data
var orderId = Guid.NewGuid(); // replace with existing order id
var order =  await client.GetOrder(orderId);

// Use the retrieved data
  order.Trace();

  Assert.IsNotNull(order);
``` 

The response is [OrderDetails](../../../../Contracts/Orders/OrderDetails.cs) which exposes detailed information about the order

<strong>Example ReleaseOrder :</strong>

```c#
 // Create new instance of the productionManager client:
 var client = new ProductionManagerClient(subscriptionId, authorizationKey);

 Guid? orderId = null; // set existing order id
 if (orderId == null)
 {
     throw new ArgumentNullException("No order id set");
 }

 await productionManagerClient.ReleaseOrder(orderId.Value);
``` 

The sample code can be found at [ProductionManager - Release Orders sample ](ReleaseOrderSamples.cs).

<strong>Example ResetReleaseOrder :</strong>

```c#
 // Create new instance of the productionManager client:
 var client = new ProductionManagerClient(subscriptionId, authorizationKey);

 Guid? orderId = null; // set existing order id
 if (orderId == null)
 {
     throw new ArgumentNullException("No order id set");
 }

 await productionManagerClient.ResetReleaseOrder(orderId.Value);
``` 

The sample code can be found at [ProductionManager - Release Orders sample ](ReleaseOrderSamples.cs).

