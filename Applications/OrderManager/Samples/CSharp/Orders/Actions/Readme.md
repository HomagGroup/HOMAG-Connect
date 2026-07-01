# Order actions

With the HOMAG Connect Client, an order can be released or its release can be reset for further programmatic evaluation.

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

 await client.ResetReleaseOrder(orderId.Value);
```

The sample code can be found at [OrderManager - Release Orders sample](ReleaseOrderSamples.cs).
