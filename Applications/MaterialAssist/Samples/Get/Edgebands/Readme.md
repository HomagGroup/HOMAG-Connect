# Get edgeband entities
With the HOMAG Connect materialAssist edgebands client, you can retrieve a list of all edgeband entities.

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