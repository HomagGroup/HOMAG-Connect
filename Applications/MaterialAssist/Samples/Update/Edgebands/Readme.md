# Update edgeband entity

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be updated. 

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

var edgebandEntityUpdate = new MaterialAssistUpdateEdgebandEntity()
{
    Id = "42",
    Length = 100,
    Comments = "This is a comment",
    Quantity = 1,
};

var updateEdgebandEntity = await client.UpdateEdgebandEntity("42", edgebandEntityUpdate);

Console.WriteLine($"Updated edgeband entity: {updateEdgebandEntity.Id}");
```
