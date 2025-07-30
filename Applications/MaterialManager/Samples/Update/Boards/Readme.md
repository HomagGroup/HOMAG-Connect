# Update board type

With the HOMAG Connect materialManager boards client, existing board types can be updated. 

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

var boardTypeUpdate = new MaterialManagerUpdateBoardType
{
    Length = 500.0,
    // Add other properties
};

var updatedBoardType = await client.UpdateBoardType("HPL_F274_9_12.0_4100.0_650.0", boardTypeUpdate);

Console.WriteLine($"Updated Board Type: {updatedBoardType.Code}"); 
```

