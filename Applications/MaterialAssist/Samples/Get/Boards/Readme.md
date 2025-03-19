# Get board entities
With the HOMAG Connect materialAssist board client, you can retrieve a list of all board entities.

<strong>Example:</strong>

```csharp
// Create a new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the number of board entities to retrieve:
int take = 50;
// Define the number of board entities to skip:
int skip = 0;

// Retrieve the list of board entities
var boardEntities = await client.GetBoardEntities(take, skip);

// Use the retrieved board entities for further processing
foreach (var boardEntity in boardEntities)
{
    Console.WriteLine($"Board entity ID: {boardEntity.Id}");
}
```