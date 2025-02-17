<h1 id="deleteOffcutEntity"> Delete offcut entity</h1>

With the HOMAG Connect materialAssist board client, offcut entities can be deleted. Offcut entities are deleted in the same way as board entities.

<strong>Example:</strong>

```csharp
// Create new instance of the materialAssist client:
var client = new MaterialAssistClientBoards(subscriptionId, authorizationKey);

// Delete the offcut entity
await client.DeleteBoardEntity("42");
```