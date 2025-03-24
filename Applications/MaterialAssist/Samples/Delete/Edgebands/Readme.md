# Delete edgeband entity

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be deleted. 

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

await client.DeleteEdgebandEntity("42");
```

