# Get edgeband entities
With the HOMAG Connect materialAssist edgebands client, you can retrieve edgeband entities in different ways.
It is also possible to get storage locations and workstations.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

int take = 100000;
int skip = 0;

var allEdgebandEntities = new List<EdgebandEntity>();

IList<EdgebandEntity> edgebandEntities;

do
{
    edgebandEntities = await client.GetEdgebandEntities(take, skip).ToListAsync();
    allEdgebandEntities.AddRange(edgebandEntities);
    skip += take;

} while (edgebandEntities.Count == take);

foreach (var edgebandEntity in allEdgebandEntities)
{
    Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
}
```

```csharp
// GetById

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);
        
var edgebandEntity = await client.GetEdgebandEntityById("id");
Console.WriteLine(edgebandEntity);
       
List<string> ids = ["id", "id", "id"];
var edgebandEntities = await client.GetEdgebandEntitiesByIds(ids);
foreach (var edgebandEntity in edgebandEntities)
{
    Console.WriteLine(edgebandEntity);
}
```

```csharp
// GetByEdgebandCode

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

var edgebandEntity = await client.GetEdgebandEntitiesByEdgebandCode("edgebandCode");
Console.WriteLine(edgebandEntity);
        
List<string> allEdgebandCodes = ["edgebandCode", "edgebandCode", "edgebandCode"];
var allEdgebandEntities = await client.GetEdgebandEntitiesByEdgebandCodes(allEdgebandCodes);
foreach (var edgebandEntity in allEdgebandEntities)
{
    Console.WriteLine(edgebandEntity);
}
```

```csharp
// GetStorageLocations

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);
var storageLocation1 = await client.GetStorageLocations();
var storageLocation2 = await client.GetStorageLocations("workstationId");

Console.WriteLine(storageLocation1);
Console.WriteLine(storageLocation2);
```

```csharp
// GetWorkstations

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);
        
var workstations = await client.GetWorkstations();
Console.WriteLine(workstations);
```