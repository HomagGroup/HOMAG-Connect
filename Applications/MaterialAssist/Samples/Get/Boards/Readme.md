# Get board entities
With the HOMAG Connect materialAssist board client, you can retrieve a list of all board entities.

<strong>Example:</strong>

```csharp
// Create a new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Define the number of board entities to retrieve in each call:
int take = 100;
int skip = 0;

// Create a list to hold all board entities
var allBoardEntities = new List<BoardEntity>();

// Initialize the boardEntities variable
List<BoardEntity> boardEntities;

do
{
    // Retrieve the next set of board entities
    boardEntities = await client.GetBoardEntities(take, skip);

    // Add the retrieved entities to the list
    allBoardEntities.AddRange(boardEntities);

    // Increment the skip counter to get the next batch
    skip += take;

    // Continue the loop while the number of retrieved entities equals 'take'
} while (boardEntities.Count == take);

// Use the retrieved board entities for further processing
foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine($"Board entity ID: {boardEntity.Id}");
}
```