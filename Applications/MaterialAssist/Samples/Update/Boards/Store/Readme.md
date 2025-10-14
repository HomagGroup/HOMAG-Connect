# Store Board Entity

With the HOMAG Connect materialAssist board client, you can store board entities.
To store a board entity, you need to specify the type of entity (Single, Stack, or GoodsInStock), the workstation, and the storage location where the entity will be stored.

## Scenarios

- **Store Board Entity of type Single**: Stores a Single coil to the desired location. The stored length and width can be specified.
- **Store Board Entity of type Stack**: Stores a Stack to the desired location. The stored length and width cannot be specified.
- **Store Board Entity of type GoodsInStock**: Stores a GoodsInStock to the desired location. The stored length and width cannot be specified.

**Example:**

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Get the first workstation
var workstations = await client.GetWorkstations().ConfigureAwait(false);
var firstWorkstation = workstations.FirstOrDefault();
if (firstWorkstation == null)
{
    throw new InvalidOperationException("No workstations found.");
}

// Get the first storage location for the workstation
var storageLocations = await client.GetStorageLocations(firstWorkstation.Id.ToString()).ConfigureAwait(false);
var firstStorageLocation = storageLocations.FirstOrDefault();
if (firstStorageLocation == null)
{
    throw new InvalidOperationException("No storage locations found for the workstation.");
}

// Create the store board entity
var storeBoardEntity = new MaterialAssistStoreBoardEntity
{
    Id = Guid.NewGuid().ToString(),
    Length = 2800, // Example length, adjust as needed
    Width = 2070,  // Example width, adjust as needed
    Workstation = firstWorkstation,
    StorageLocation = firstStorageLocation
};

await client.StoreBoardEntity(storeBoardEntity).ConfigureAwait(false);

Console.WriteLine($"Stored board entity: {storeBoardEntity.Id}");
```