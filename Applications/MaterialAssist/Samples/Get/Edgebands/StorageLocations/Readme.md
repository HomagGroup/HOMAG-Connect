# Get Edgeband Storage Locations

With the HOMAG Connect materialAssist edgeband client, you can retrieve a list of all edgeband storage locations.
The list contains all storage locations configured for the existing edgeband workstations.
A call can be made to retrieve the storage locations for a specific workstation.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

var storageLocations = await client.GetStorageLocations().ConfigureAwait(false);
foreach (var storageLocation in storageLocations)
{
	Console.WriteLine($"Storage Location ID: {storageLocation.LocationId}, Name: {storageLocation.Name}");
}

// To get storage locations for a specific workstation, use the workstation ID
// var workstationId = "your-workstation-id"; // Replace with your actual workstation ID
var workstations = await client.GetWorkstations().ConfigureAwait(false);
var firstWorkstation = workstations.FirstOrDefault();
if (firstWorkstation == null)
{
    Console.WriteLine("No workstations found.");
    return;
}

var workstationId = firstWorkstation.Id.ToString();

var workstationStorageLocations = await client.GetStorageLocations(workstationId).ConfigureAwait(false);
foreach (var storageLocation in workstationStorageLocations)
{
	Console.WriteLine($"Workstation Storage Location ID: {storageLocation.LocationId}, Name: {storageLocation.Name}");
}
```

