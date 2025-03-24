# Get board entities
With the HOMAG Connect materialAssist board client, you can retrieve a list of all board entities.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

int take = 100000;
int skip = 0;

var allBoardEntities = new List<BoardEntity>();

List<BoardEntity>? boardEntities;

do
{
    boardEntities = await client.GetBoardEntities(take, skip) as List<BoardEntity>;

    if (boardEntities != null) allBoardEntities.AddRange(boardEntities);

    skip += take;

} while (boardEntities != null && boardEntities.Count == take);

foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine($@"Board entity ID: {boardEntity.Id}");
}
```