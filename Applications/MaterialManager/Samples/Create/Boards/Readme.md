<h1 id="createBoardType"> Create board type</h1>

With the HOMAG Connect materialManager boards client, new board types can be created.

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
