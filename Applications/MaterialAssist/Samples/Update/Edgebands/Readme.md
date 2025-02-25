<h1 id="updateEdgebandEntity"> Update edgeband entity</h1>

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be updated. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

// Define the update edgeband entity request:
var edgebandEntityUpdate = new MaterialAssistUpdateEdgebandEntity()
{
    Id = "42",
    Length = 100,
    Comments = "This is a comment",
    Quantity = 1,
    //add other properties
};

// Update the edgeband entity
var updateEdgebandEntity = await client.UpdateEdgebandEntity("42", edgebandEntityUpdate);

// Use the updated edgeband entity for further processing
Console.WriteLine($"Updated edgeband entity: {updateEdgebandEntity.Id}");
```
