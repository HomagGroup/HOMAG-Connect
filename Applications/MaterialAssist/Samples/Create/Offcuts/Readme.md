<h1 id="createOffcutEntity"> Create offcut entity</h1>

With the HOMAG Connect materialAssist board client, offcut entities can be created. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the create offcut entity request:
var boardEntityRequest = new MaterialAssistRequestOffcutEntity()
{
    Id = "42",
    BoardCode = "White Board 19mm",
    Comments = "This is a comment",
    Length = 1000,
    Width = 50,
};

// Create the offcut entity
var newBoardEntity = await client.CreateOffcutEntity(boardEntityRequest);

// Use the created offcut entity for further processing
Console.WriteLine($"Created offcut entity: {newBoardEntity.Id}");
```
