# Update board entity

With the HOMAG Connect materialAssist board client, board entities can be updated. 
It is also possible to remove board entities from workplaces in different ways.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
{
    Id = "42",
    Length = 100,
    Comments = "This is a comment",
    Quantity = 1,
};

var updateBoardEntity = await client.UpdateBoardEntity("42", boardEntityUpdate);

Console.WriteLine($"Updated board entity: {updateBoardEntity.Id}");
```

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

var storageLocation = new StorageLocation()
{
    Barcode = "string",
    LocationId = "string",
    Name = "string",
};

//string id, int length, int width, StorageLocation storageLocation
await client.StoreBoardEntity("42", 10, 10, storageLocation);
```

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// string id, bool deleteBoardFromInventory = false
await client.RemoveAllBoardEntitiesFromWorkplace("id");
       
//string id, int quantity, bool deleteBoardFromInventory = false
await client.RemoveSubsetBoardEntitiesFromWorkplace("id", 10);
        
//string id, int quantity, bool deleteBoardFromInventory = false
await client.RemoveSingleBoardEntitiesFromWorkplace("id", 10);
```