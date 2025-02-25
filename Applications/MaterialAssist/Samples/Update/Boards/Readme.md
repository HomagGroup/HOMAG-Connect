# Update board entity

With the HOMAG Connect materialAssist board client, board entities can be updated. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the update board entity request:
var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
{
    Id = "42",
    Length = 100,
    Comments = "This is a comment",
    Quantity = 1,
    //add other properties
};

// Update the board entity
var updateBoardEntity = await client.UpdateBoardEntity("42", boardEntityUpdate);


// Use the updated board entity for further processing
Console.WriteLine($"Updated board entity: {updateBoardEntity.Id}");
```