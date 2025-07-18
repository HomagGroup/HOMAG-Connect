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

IList<BoardEntity> boardEntities;

do
{
    boardEntities = await materialAssist.GetBoardEntities(take, skip).ToListAsync();

} while (boardEntities.Count == take);

foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine($"Board entity ID: {boardEntity.Id}");
}
```

```csharp
// GetById

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
var boardEntity = await client.GetBoardEntityById("42");
Console.WriteLine(boardEntity);
        
List<string> ids = ["42", "50", "23"];
var boardEntities = await client.GetBoardEntitiesByIds(ids);
foreach (var boardEntity in boardEntities)
{
    Console.WriteLine(boardEntity);
}
```

```csharp
// GetByBoardCode

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var boardEntity = await client.GetBoardEntitiesByBoardCode("MDF_H3171_12_11.6_2800.0_1310.0");
Console.WriteLine(boardEntity);
       
List<string> allBoardCodes = ["MDF_H3171_12_11.6_2800.0_1310.0", "RP_EG_H3303_ST10_19", "XEG_U156_ST02_08_2070_444.2"];
var allBoardEntities = await client.GetBoardEntitiesByBoardCodes(allBoardCodes);
foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine(boardEntity);
}
```

```csharp
// GetByMaterialCode

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
  
var boardEntity = await materialAssist.GetBoardEntitiesByMaterialCode("EG_H3303_ST10_19");
Console.WriteLine(boardEntity);
        
List<string> allMaterialCodes = ["EG_H3303_ST10_19", "EG_U156_ST02_08", "EG_U702_PM_19"];
var allBoardEntities = await client.GetBoardEntitiesByMaterialCodes(allMaterialCodes);
foreach (var boardEntity in allBoardEntities)
{
    Console.WriteLine(boardEntity);
}
```

```csharp
// GetStorageLocations

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
var allStorageLocations = await client.GetStorageLocations();
var storageLocation = await client.GetStorageLocations("Compartment 02");

Console.WriteLine(allStorageLocations);
Console.WriteLine(storageLocation);
```

```csharp
// GetWorkstations

var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
var workstations = await client.GetWorkstations();
Console.WriteLine(workstations);

```