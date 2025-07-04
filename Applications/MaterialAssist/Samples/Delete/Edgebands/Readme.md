# Delete edgeband entity

With the HOMAG Connect materialAssist edgeband client, edgeband entities can be deleted. 
It is possible to delete one item or a list of items.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

await client.DeleteEdgebandEntity("id");
```

```csharp
var client = new MaterialAssistClientEdgebands(subscriptionId, authorizationKey);

List<string> ids = ["id", "id", "id"];

await client.DeleteEdgebandEntity(ids);
```

