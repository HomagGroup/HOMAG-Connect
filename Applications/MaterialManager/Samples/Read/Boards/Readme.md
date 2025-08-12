# Read board data

With the HOMAG Connect materialManager boards client, the board related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);
var taken = 10; // Number of board types to retrieve
var skip = 0; // Number of board types to skip

var boardTypeAllocations = client.GetBoardTypeAllocations(taken, skip);
```


