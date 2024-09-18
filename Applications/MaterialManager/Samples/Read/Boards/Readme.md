<h1 id="readBoardData"> Read board data</h1>

With the HOMAG Connect materialManager boards client, the board related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the boardCodes you want to retrieve data for:
var boardCodes = new List<string> { "P2_Gold Craft Oak_19_2800_2070", "P2_Weiss_19_2800_2070" };

// Get the data
var boardTypes = client.GetBoardTypes(boardCodes);

// Use the retrieved data for further processing
```


