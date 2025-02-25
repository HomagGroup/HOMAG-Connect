# Delete board entity

With the HOMAG Connect materialAssist board client, board and offcut entities can be deleted.

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Delete the board entity
await client.DeleteBoardEntity("42");
```