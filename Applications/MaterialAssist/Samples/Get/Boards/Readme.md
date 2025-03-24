# Get board entities
With the HOMAG Connect materialAssist board client, you can retrieve a list of all board entities.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

int take = 100;
int skip = 0;

var allBoardEntities = new List<BoardEntity>();

List<BoardEntity> boardEntities;

do
{
    boardEntities = await client.GetBoardEntities(take, skip);

    allBoardEntities.AddRange(boardEntities);

    skip += take;

} while (boardEntities.Count == take);

foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine($"Board entity ID: {boardEntity.Id}");
}
```