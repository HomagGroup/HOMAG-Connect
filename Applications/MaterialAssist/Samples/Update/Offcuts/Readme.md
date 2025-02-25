<h1 id="updateOffcutEntity"> Update offcut entity</h1>

With the HOMAG Connect materialAssist board client, offcut entities can be updated. Offcut entities are updated in the same way as board entities.

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the update offcut entity request:
var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
{
    Id = "42",
    Length = 100,
    Comments = "This is a comment",
    Quantity = 1,
    //add other properties
};

// Update the offcut entity
var updateBoardEntity = await client.UpdateBoardEntity("42", boardEntityUpdate);


// Use the updated offcut entity for further processing
Console.WriteLine($"Updated offcut entity: {updateBoardEntity.Id}");
```