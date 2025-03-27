# Delete board entity

With the HOMAG Connect materialAssist board client, board and offcut entities can be deleted.

<strong>Example:</strong>

```csharp
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

await client.DeleteBoardEntity("42");
```