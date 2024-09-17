<h1 id="createBoardType"> Create board type</h1>

With the HOMAG Connect materialManager boards client, new board types can be created.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the new board type request:
var boardTypeRequest = new MaterialManagerRequestBoardType
{
    MaterialCode = "BT_White_19mm_100_100",
    BoardCode = "White Board 19mm",
    Length = 2070.0,
    Width = 2800.0,
    Thickness = 19.0,
    Type = BoardTypeType.Board,
    MaterialCategory = BoardMaterialCategory.Undefined,
    CoatingCategory CoatingCategory = CoatingCategory.Undefined,
    Grain = Grain.None,
    // Add other properties
};

// Create the new board type
var newBoardType = await client.CreateBoardType(boardTypeRequest);

// Use the created board type for further processing
Console.WriteLine($"Created Board Type: {newBoardType.Code}, {newBoardType.Description}");
```
