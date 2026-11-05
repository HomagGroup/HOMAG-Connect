# Customer actions

With the HOMAG Connect Client, customers can be retrieved, created, or deleted for further programmatic evaluation.

<strong>Example GetCustomers (all):</strong>

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

var customers = await client.GetCustomers();

customers.Trace();
```

The response is a list of [Customer](../../../../Contracts/Customers/Customer.cs) which exposes basic and additional information about the customer.

<strong>Example GetCustomers by customer numbers / ids:</strong>

```c#
var byNumbers = await client.GetCustomers(["11111111111", "22222222222"]);

var byIds = await client.GetCustomers([Guid.Parse("PUT_CUSTOMER_ID1_HERE"), Guid.Parse("PUT_CUSTOMER_ID2_HERE")]);
```

<strong>Example GetCustomer (single, by number or id):</strong>

```c#
var customerByNumber = await client.GetCustomer("11111111111");

var customerById = await client.GetCustomer(Guid.Parse("PUT_CUSTOMER_ID_HERE"));
```

The sample code can be found at [OrderManager - Customer sample](CustomerSamples.cs).

<strong>Example CreateCustomer:</strong>

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

var request = new CreateCustomerRequest
{
	CustomerNumber = "956",
	CustomerName = "Test Customer",
	Email = "test@example.com",
	TelephoneNumber = "1234567890",
	Locale = Locale.deDE,
	ApiAccess = true
};

var customer = await client.CreateCustomer(request);
```

The request is a [CreateCustomerRequest](../../../../Contracts/Customers/CreateCustomerRequest.cs); if `CustomerNumber` is omitted, the backend generates a unique customer number. The response is the created [Customer](../../../../Contracts/Customers/Customer.cs).

The sample code can be found at [OrderManager - Customer sample](CustomerSamples.cs).

<strong>Example DeleteCustomersByCustomerIds / DeleteCustomersByCustomerNumbers:</strong>

```c#
// Create new instance of the orderManager client:
var client = new OrderManagerClient(subscriptionId, authorizationKey);

var ids = new List<Guid> { }; // add some ids
await client.DeleteCustomersByCustomerIds(ids);

var customerNumbers = new List<string> { }; // add some customer numbers
await client.DeleteCustomersByCustomerNumbers(customerNumbers);
```

The sample code can be found at [OrderManager - Customer sample](CustomerSamples.cs).
