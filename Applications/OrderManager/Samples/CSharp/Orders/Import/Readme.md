# Import an order

With the HOMAG Connect Client, orders can be imported into orderManager either as a structured `.zip` file or as an [OrderDetails](../../../../Contracts/Orders/OrderDetails.cs) object, optionally together with referenced files (for example images).

#### Prepare the import request

<strong>Using a project `.zip` file:</strong>

```c#
var projectFile = new FileInfo(@"Orders\Samples\project-01.zip");
```

<strong>Using an `OrderDetails` object:</strong>

```c#
var order = new OrderDetails
{
	OrderNumber = "736362",
	State = OrderState.New,
	OrderName = "Bedroom & bathroom 01",
	CustomerNumber = "462642",
	OrderDate = DateTime.Today,
	DeliveryDatePlanned = DateTime.Today.AddDays(14)
};
```

#### Send the request

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

// Import using a project zip file:
var response = await client.ImportOrderRequest(projectFile);

// Or import using an OrderDetails object:
var response2 = await client.ImportOrderRequest(order);

// Or import using an OrderDetails object together with referenced files:
var fileReferences = new[]
{
	new FileReference("testImage.png", new FileInfo(@"Orders\Samples\testImage.png"))
};
var response3 = await client.ImportOrderRequest(order, fileReferences);
```

The outcome of the import is a `correlationId`, which can be used to find additional data about the import that is taking place.

```c#
var correlationId = response.CorrelationId;
```

The sample code can be found at [OrderManager - Import Order sample](ImportOrderSamples.cs).

#### Utilize the outcome of the import

##### Get the import state

After the import job is started, the response contains a `correlationId`, which can be used to retrieve the status of the import job.

```c#
var importState = await client.GetImportOrderState(correlationId);
```

The result is an [ImportOrderStateResponse](../../../../Contracts/Import/ImportOrderStateResponse.cs), which based on the progress of the import job can retrieve a link to the newly created order and also the id of the order. In case of error during import, `ErrorDetails` contains basic information why the import failed.

Also the current state of the import is retrieved. The possible states are defined in [ImportState](../../../../Contracts/Import/ImportState.cs).

##### Wait for the import to be completed

Instead of polling `GetImportOrderState` manually, `WaitForImportOrderCompletion` can be used to await the import result directly, returning the imported [OrderDetails](../../../../Contracts/Orders/OrderDetails.cs).

```c#
var orderDetails = await client.WaitForImportOrderCompletion(correlationId, TimeSpan.FromMinutes(1));
```

The sample code can be found at [OrderManager - Import Order sample](ImportOrderSamples.cs).

# Add or update a group within an existing order

With the HOMAG Connect Client, a new [Group](../../../../Contracts/OrderItems/Group.cs) can be added, or an existing one updated, within an already imported order, either using file references or a project `.zip` file.

#### Example AddOrUpdateGroup using file references

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

var orderNumber = "736362"; // set existing order number

var group = new Group
{
	Name = "Bedroom & bathroom 02",
	Source = "orderConfigurator"
};

var fileReferences = new[]
{
	new FileReference("testImage.png", new FileInfo(@"Orders\Samples\testImage.png"))
};

var response = await client.AddOrUpdateGroup(orderNumber, group, fileReferences);
```

#### Example AddOrUpdateGroup using a project `.zip` file

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

var orderNumber = "736362"; // set existing order number
var projectFile = new FileInfo(@"Orders\Samples\project-01.zip");

var response = await client.AddOrUpdateGroup(orderNumber, projectFile);
```

The sample code can be found at [OrderManager - Import Order sample](ImportOrderSamples.cs).
