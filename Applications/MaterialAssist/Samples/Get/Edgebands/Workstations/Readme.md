﻿# Get Edgeband Workstations

With the HOMAG Connect materialAssist edgeband client, you can retrieve a list of all edgeband workstations.
The list contains the workstations that were correctly configured.

**Example:**

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

// Get all configured workstations
var workstations = await client.GetWorkstations().ConfigureAwait(false);

foreach (var workstation in workstations)
{
	Console.WriteLine($"Workstation ID: {workstation.Id}, Name: {workstation.Name}");
}
```
