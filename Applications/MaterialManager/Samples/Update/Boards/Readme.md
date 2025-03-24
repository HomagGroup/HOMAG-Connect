# Update board type

With the HOMAG Connect materialManager boards client, existing board types can be updated. 

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeUpdate = new MaterialManagerUpdateBoardType
{
    Length = 2070.0,
    // Add other properties
};

var updatedBoardType = await client.UpdateBoardType("BT_White_19mm", boardTypeUpdate);

Console.WriteLine($"Updated Board Type: {updatedBoardType.Code}"); 
```

