<h1 id="createBoardEntity"> Create board entity</h1>

With the HOMAG Connect materialAssist board client, board entities can be created. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the create board entity request:
var boardEntityRequest = new MaterialAssistRequestBoardEntity()
{
    Id = "42",
    BoardCode = "White Board 19mm",
    ManagementType = ManagementType.Single,
    Comments = "This is a comment",
    Quantity = 1
};

// Create the board entity
var newBoardEntity = await client.CreateBoardEntity(boardEntityRequest);

// Use the created board entity for further processing
Console.WriteLine($"Created board entity: {newBoardEntity.Id}");
```
