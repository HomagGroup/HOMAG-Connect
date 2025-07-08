# Update edgeband entity

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be updated. 
It is also possible to remove edgeband entities from workplaces in different ways.

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

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var edgebandEntityStore = new MaterialAssistStoreEdgebandEntity()
{
    Id = "42",
    Length = 100,
    StorageLocation = new StorageLocation()
    {
        Name = "Compartment 02",
    },
};

await client.StoreEdgebandEntity(edgebandEntityStore);
```

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// string id, bool deleteBoardFromInventory = false
await client.RemoveAllEdgebandEntitiesFromWorkplace("42");
       
//string id, int quantity, bool deleteBoardFromInventory = false
await client.RemoveSubsetEdgebandEntitiesFromWorkplace("42", 2);
        
//string id, int quantity, bool deleteBoardFromInventory = false
await client.RemoveSingleEdgebandEntitiesFromWorkplace("42", 1);
```
