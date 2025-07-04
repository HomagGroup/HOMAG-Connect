# Delete board entity

With the HOMAG Connect materialAssist board client, board and offcut entities can be deleted. 
It is possible to delete one item or a list of items.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

await client.DeleteBoardEntity("id");
```

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
List<string> ids = ["id", "id", "id"];

await client.DeleteBoardEntities(ids);
```