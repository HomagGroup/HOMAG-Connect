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
    edgebandEntities = await materialAssist.GetEdgebandEntities(take, skip).ToListAsync();

} while (edgebandEntities.Count == take);

foreach (var edgebandEntity in allEdgebandEntities)
{
    Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
}
```

```csharp
// GetById

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);
        
var edgebandEntity = await client.GetEdgebandEntityById("42");
Console.WriteLine(edgebandEntity);
       
List<string> ids = ["42", "50", "23"];
var edgebandEntities = await client.GetEdgebandEntitiesByIds(ids);
foreach (var edgebandEntity in edgebandEntities)
{
    Console.WriteLine(edgebandEntity);
}
```

```csharp
// GetByEdgebandCode

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

var edgebandEntity = await client.GetEdgebandEntitiesByEdgebandCode("RAUKANTEX COLOR 22/1,3");
Console.WriteLine(edgebandEntity);
        
List<string> allEdgebandCodes = ["RAUKANTEX COLOR 22/1,3", "RAUKANTEX dekor pro 23 x 1,4", "ABS_Schwarz_2_23_HM"];
var allEdgebandEntities = await client.GetEdgebandEntitiesByEdgebandCodes(allEdgebandCodes);
foreach (var edgebandEntity in allEdgebandEntities)
{
    Console.WriteLine(edgebandEntity);
}
```

```csharp
// GetStorageLocations

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

var allStorageLocations = await client.GetStorageLocations();
var storageLocation = await client.GetStorageLocations("Compartment 02");

Console.WriteLine(allStorageLocations);
Console.WriteLine(storageLocation);
```

```csharp
// GetWorkstations

var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);
        
var workstations = await client.GetWorkstations();
Console.WriteLine(workstations);
```