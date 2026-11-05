# Order actions

With the HOMAG Connect Client, orders can be retrieved, released, or have their release reset for further programmatic evaluation.

<strong>Example GetOrders:</strong>

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

// Get the data
var orders = await client.GetOrders(5).ToListAsync();

// Use the retrieved data
orders.Trace();

var orderNames = orders.Select(x => x.OrderName).ToList();
orderNames.Trace(nameof(orderNames));
```

The response is a list of [OrderOverview](../../../../Contracts/Orders/OrderOverview.cs) which exposes basic information about the order.

`GetOrders` also accepts an [OrderState](../../../../Contracts/Orders/OrderState.cs) (or array of states), an array of order numbers, or can be filtered by `changedSince`/`releasedSince` using `GetOrdersChangedSince`/`GetOrdersReleasedSince`.

```c#
var newOrders = await client.GetOrders(OrderState.New, 5);
var newOrInProgress = await client.GetOrders([OrderState.New, OrderState.InProgress], 5);
var byNumbers = await client.GetOrders(["10000001"]);
var changedSince = await client.GetOrdersChangedSince(OrderState.ReadyForProduction, DateTime.UtcNow.AddMinutes(-5), 5);
var releasedSince = await client.GetOrdersReleasedSince(OrderState.ReadyForProduction, DateTime.UtcNow.AddMinutes(-5), 5);
```

The sample code can be found at [OrderManager - Get Orders sample](GetOrderSamples.cs).

<strong>Example GetOrder:</strong>

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

// Get the data by order id
var orderId = Guid.NewGuid(); // replace with existing order id
var order = await client.GetOrder(orderId);

// Or get the data by order number
var order2 = await client.GetOrder("10000001");

// Use the retrieved data
order.Trace();
```

The response is [OrderDetails](../../../../Contracts/Orders/OrderDetails.cs) which exposes detailed information about the order.

The sample code can be found at [OrderManager - Get Orders sample](GetOrderSamples.cs).

<strong>Example ReleaseOrder:</strong>

```c#
 // Create new instance of the orderManager client:
 var client = new OrderManagerClient(subscriptionId, authorizationKey);

 Guid? orderId = null; // set existing order id
 if (orderId == null)
 {
     throw new ArgumentNullException("No order id set");
 }

 await client.ReleaseOrder(orderId.Value);
```

<strong>Example ResetReleaseOrder:</strong>

```c#
 // Create new instance of the orderManager client:
 var client = new OrderManagerClient(subscriptionId, authorizationKey);

 Guid? orderId = null; // set existing order id
 if (orderId == null)
 {
     throw new ArgumentNullException("No order id set");
 }

 The sample code can be found at [OrderManager - Release Orders sample](ReleaseOrderSamples.cs).

 <strong>Example DeleteOrdersByOrderId:</strong>

 ```c#
 // Create new instance of the orderManager client:
 var client = new OrderManagerClient(subscriptionId, authorizationKey);

 var orderId = Guid.Parse("PUT_ORDER_ID_HERE");

 await client.DeleteOrdersByOrderId(orderId);
 ```

 <strong>Example DeleteOrdersByOrderIds:</strong>

 ```c#
 // Create new instance of the orderManager client:
 var client = new OrderManagerClient(subscriptionId, authorizationKey);

 var orderIds = new[]
 {
     Guid.Parse("PUT_ORDER_ID1_HERE"),
     Guid.Parse("PUT_ORDER_ID2_HERE")
 };

 await client.DeleteOrdersByOrderIds(orderIds);
 ```

 <strong>Example DeleteOrdersByOrderNumber / DeleteOrdersByOrderNumbers:</strong>

 ```c#
 // Create new instance of the orderManager client:
 var client = new OrderManagerClient(subscriptionId, authorizationKey);

 await client.DeleteOrdersByOrderNumber("10000001");

 await client.DeleteOrdersByOrderNumbers(["10000001", "10000002"]);
 ```

 The sample code can be found at [OrderManager - Delete Orders sample](DeleteOrderSamples.cs).
