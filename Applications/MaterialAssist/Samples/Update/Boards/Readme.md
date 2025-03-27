# Update board entity

With the HOMAG Connect materialAssist board client, board entities can be updated. 

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
{
    Id = "42",
    Length = 100,
    Comments = "This is a comment",
    Quantity = 1,
};

var updateBoardEntity = await client.UpdateBoardEntity("42", boardEntityUpdate);


Console.WriteLine($"Updated board entity: {updateBoardEntity.Id}");
```