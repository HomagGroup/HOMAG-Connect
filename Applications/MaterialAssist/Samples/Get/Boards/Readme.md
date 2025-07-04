# Get board entities
With the HOMAG Connect materialAssist board client, you can retrieve board entities in different ways.
It is also possible to get storage locations and workstations.

<strong>Example:</strong>

```csharp
//GetAll

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

int take = 100000;
int skip = 0;

var allBoardEntities = new List<BoardEntity>();

List<BoardEntity> boardEntities;

do
{
    boardEntities = await client.GetBoardEntities(take, skip).ToListAsync();
    allBoardEntities.AddRange(boardEntities);
    skip += take;

} while (boardEntities.Count == take);

foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine($"Board entity ID: {boardEntity.Id}");
}
```

```csharp
// GetById

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
var boardEntity = await client.GetBoardEntityById("id");
Console.WriteLine(boardEntity);
        
List<string> ids = ["id", "id", "id"];
var boardEntities = await client.GetBoardEntitiesByIds(ids);
foreach (var boardEntity in boardEntities)
{
    Console.WriteLine(boardEntity);
}
```

```csharp
// GetByBoardCode

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var boardEntity = await client.GetBoardEntitiesByBoardCode("boardCode");
Console.WriteLine(boardEntity);
       
List<string> allBoardCodes = ["boardCode", "boardCode", "boardCode"];
var allBoardEntities = await client.GetBoardEntitiesByBoardCodes(allBoardCodes);
foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine(boardEntity);
}
```

```csharp
// GetByMaterialCode

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
  
var boardEntity = await materialAssist.GetBoardEntitiesByMaterialCode("materialCode");
Console.WriteLine(boardEntity);
        
List<string> allMaterialCodes = ["materialCode", "materialCode", "materialCode"];
var allBoardEntities = await client.GetBoardEntitiesByMaterialCodes(allMaterialCodes);
foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine(boardEntity);
}
```

```csharp
// GetStorageLocations

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
var storageLocation1 = await client.GetStorageLocations();
var storageLocation2 = await client.GetStorageLocations("workstationId");

Console.WriteLine(storageLocation1);
Console.WriteLine(storageLocation2);
```

```csharp
// GetWorkstations

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
var workstations = await client.GetWorkstations();
Console.WriteLine(workstations);

```