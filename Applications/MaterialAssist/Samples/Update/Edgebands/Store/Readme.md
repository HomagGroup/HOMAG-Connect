# Store Edgeband Entity

With the HOMAG Connect materialAssist edgeband client, you can store edgeband entities.
To store an edgeband entity, you need to specify the id, the legth, the workstation, and the storage location where the entity will be stored.
The length is only required when storing a Single edgeband entity.

## Scenarios

- **Store Edgeband Entity of type Single**: Stores a Single coil to the desired location. The stored length can be specified.
- **Store Edgeband Entity of type Stack**: Stores a Stack to the desired location. The stored length cannot be specified.
- **Store Edgeband Entity of type GoodsInStock**: Stores a GoodsInStock to the desired location. The stored length cannot be specified.

**Example:**

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

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

// Create the store edgeband entity
var storeEdgebandEntity = new MaterialAssistStoreEdgebandEntity
{
    Id = Guid.NewGuid().ToString(),
    Length = 10, // Example length, adjust as needed
    Workstation = firstWorkstation,
    StorageLocation = firstStorageLocation
};

await client.StoreEdgebandEntity(storeEdgebandEntity).ConfigureAwait(false);

Console.WriteLine($"Stored edgeband entity: {storeEdgebandEntity.Id}");
```