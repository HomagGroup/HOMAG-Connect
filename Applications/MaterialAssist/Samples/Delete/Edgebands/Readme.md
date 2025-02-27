# Delete edgeband entity

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be deleted. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

// Delete the edgeband entity
await client.DeleteEdgebandEntity("42");
```

