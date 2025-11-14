# Read board data

With the HOMAG Connect materialManager boards client, the board related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>


```csharp
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the boardCodes you want to retrieve data for:
var boardCodes = new List<string> { "P2_Gold Craft Oak_19_2800_2070", "P2_Weiss_19_2800_2070" };

var boardTypes = client.GetBoardTypes(boardCodes);
```

