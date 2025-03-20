# Get edgeband entities
With the HOMAG Connect materialAssist edgebands client, you can retrieve a list of all edgeband entities.

<strong>Example:</strong>

```csharp
// Create a new instance of the materialAssist client:
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

// Define the number of edgeband entities to retrieve in each call:
int take = 100;
int skip = 0;

// Create a list to hold all edgeband entities
var allEdgebandEntities = new List<EdgebandEntity>();

// Initialize the edgebandEntities variable
List<EdgebandEntity> edgebandEntities;

do
{
    // Retrieve the next set of edgeband entities
    edgebandEntities = await client.GetEdgebandEntities(take, skip);

    // Add the retrieved entities to the list
    allEdgebandEntities.AddRange(edgebandEntities);

    // Increment the skip counter to get the next batch
    skip += take;

    // Continue the loop while the number of retrieved entities equals 'take'
} while (edgebandEntities.Count == take);

// Use the retrieved edgeband entities for further processing
foreach (var edgebandEntity in allEdgebandEntities)
{
    Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
}
```