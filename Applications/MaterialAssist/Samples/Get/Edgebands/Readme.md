# Get edgeband entities
With the HOMAG Connect materialAssist edgebands client, you can retrieve a list of all edgeband entities.

<strong>Example:</strong>

```csharp
// Create a new instance of the materialAssist client:
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

// Define the number of edgeband entities to retrieve:
int take = 50;
// Define the number of edgeband entities to skip:
int skip = 0;

// Retrieve the list of edgeband entities
var edgebandEntities = await client.GetEdgebandEntities(take, skip);

// Use the retrieved edgeband entities for further processing
foreach (var edgebandEntity in edgebandEntities)
{
    Console.WriteLine($"Edgeband entity ID: {edgebandEntity.Id}");
}
```