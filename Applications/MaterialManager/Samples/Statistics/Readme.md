<h1 id="BoardInventoryHistory"> Board inventory history</h1>

The HOMAG Connect materialManager boards client provides a method to view the the history of the board inventory. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the date range for the inventory history
DateTime from = DateTime.Now.AddMonths(-1);
DateTime to = DateTime.Now;

// Get the inventory history
var inventoryHistory = await client.GetBoardTypeInventoryHistoryAsync(from, to);

// Use the inventory history for further processing
foreach (var history in inventoryHistory)
{
    Console.WriteLine($"Date: {history.Date}, Quantity: {history.Quantity}");
}
```

<h1 id="edgebandInventoryHistory"> Edgeband inventory history</h1>

The HOMAG Connect materialManager edgebands client provides a method to view the the history of the edgeband inventory. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the date range for the inventory history
DateTime from = DateTime.Now.AddMonths(-1);
DateTime to = DateTime.Now;

// Get the inventory history
var inventoryHistory = await client.GetEdgebandTypeInventoryHistoryAsync(from, to);

// Use the inventory history for further processing
foreach (var history in inventoryHistory)
{
    Console.WriteLine($"Date: {history.Date}, Quantity: {history.Quantity}");
}
```
