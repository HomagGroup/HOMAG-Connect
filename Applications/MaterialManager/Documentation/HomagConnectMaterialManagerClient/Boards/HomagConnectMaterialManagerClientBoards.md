<strong>[This is preliminary documentation and is subject to change.]</strong>

<h1 id="readBoardData"> Read board data</h1>

With the Homag Connect materialManager boards client, the board related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the boardCodes you want to retrieve data for:
var boardCodes = new List<string> { "P2_Gold Craft Oak_19_2800_2070", "P2_Weiss_19_2800_2070" };

// Get the data
var boardTypes = client.GetBoardTypes(boardCodes);

// Use the retrieved data for further processing
var totalThickness = boardTypes.Sum(x => x.Thickness);
var totalWidth = boardTypes.Sum(x => x.Width);
```

As this is only one sample on how to use the client you can find further methods of the interface in [Client documentation](../../SourceCode/homagconnect.materialmanager.client.materialmanagerclientmaterialboards)



<h1 id="createBoardType"> Create board type</h1>

With the Homag Connect materialManager boards client, new board types can be created.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the new board type request:
var boardTypeRequest = new MaterialManagerRequestBoardType
{
    Code = "BT_White_19mm",
    Description = "White Board 19mm",
    Thickness = 19.0,
    Width = 2800.0,
    Length = 2070.0,
    // Add other required properties
};

// Create the new board type
var newBoardType = await client.CreateBoardType(boardTypeRequest);

// Use the created board type for further processing
Console.WriteLine($"Created Board Type: {newBoardType.Code}, {newBoardType.Description}");
```



<h1 id="UpdateBoardType">Update board type</h1>

With the Homag Connect materialManager boards client, existing board types can be updated. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the board type update request:
var boardTypeUpdate = new MaterialManagerUpdateBoardType
{
    Description = "Updated White Board 19mm",
    Thickness = 19.5,
    Width = 2800.0,
    Length = 2070.0,
    // Add other required properties
};

// Update the board type
var updatedBoardType = await client.UpdateBoardType("BT_White_19mm", boardTypeUpdate);

// Use the updated board type for further processing
Console.WriteLine($"Updated Board Type: {updatedBoardType.Code}, {updatedBoardType.Description}"); 
```



<h1 id="BoardInventoryHistory"> Board inventory history</h1>

The Homag Connect materialManager boards client provides a method to view the the history of the board inventory. 

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

