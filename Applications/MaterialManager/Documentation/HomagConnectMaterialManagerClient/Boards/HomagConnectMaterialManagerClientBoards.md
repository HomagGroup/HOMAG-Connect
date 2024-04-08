<strong>[This is preliminary documentation and is subject to change.]</strong>

# Read board data

With the Homag Connect materialManager boards client, the board related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialBoards(subscriptionId, authorizationKey);

// Define the boardCodes you want to retrieve data for:
var boardCodes = new List<string> { "P2_Gold Craft Oak_19_2800_2070", "P2_Weiss_19_2800_2070" };

// Get the data
var boardTypes = client.GetBoardTypes(boardCodes);

// Use the retrieved data for further processing
var totalThickness = boardTypes.Sum(x => x.Thickness);
var totalWidth = boardTypes.Sum(x => x.Width);

```

As this is only one sample on how to use the client you can find further methods of the interface in [Client documentation](../../SourceCode/homagconnect.materialmanager.client.materialmanagerclientmaterialboards)
