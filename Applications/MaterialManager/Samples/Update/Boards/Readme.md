<h1 id="UpdateBoardType">Update board type</h1>

With the HOMAG Connect materialManager boards client, existing board types can be updated. 

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

