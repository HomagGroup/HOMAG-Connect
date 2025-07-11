# Delete board entity

With the HOMAG Connect materialAssist board client, board and offcut entities can be deleted. 
It is possible to delete one item or a list of items.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

await client.DeleteBoardEntity("42");
```

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);
        
List<string> ids = ["42", "50", "23"];

await client.DeleteBoardEntities(ids);
```