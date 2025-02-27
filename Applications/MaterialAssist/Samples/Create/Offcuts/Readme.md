# Create offcut entity

With the HOMAG Connect materialAssist board client, offcut entities can be created. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the create offcut entity request:
var boardEntityRequest = new MaterialAssistRequestOffcutEntity()
{
    Id = "42",
    //The board code is the identifier of the board type
    BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
    Comments = "This is a comment",
    Length = 1000,
    Width = 50,
};

// Create the offcut entity
var newBoardEntity = await client.CreateOffcutEntity(boardEntityRequest);

// Use the created offcut entity for further processing
Console.WriteLine($"Created offcut entity: {newBoardEntity.Id}");
```
