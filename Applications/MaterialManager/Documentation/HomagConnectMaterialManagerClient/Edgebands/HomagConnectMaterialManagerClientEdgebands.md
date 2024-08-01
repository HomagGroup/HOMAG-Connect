<strong>[This is preliminary documentation and is subject to change.]</strong>

<h1 id="readEdgebandData"> Read edgeband data</h1>

With the Homag Connect materialManager edgebands client, the edgebands related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the edgebandCodes you want to retrieve data for:
var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ACR_Buche_mit_Silberstreifen_2.00_43.0_HM" };

// Get the data
var edgebandTypes = client.GetEdgebandsTypes(edgebandCodes);

// Use the retrieved data for further processing
var totalThickness = edgebandTypes.Sum(x => x.Thickness);
var totalLength = edgebandTypes.Sum(x => x.Length);

```

As this is only one sample on how to use the client you can find further methods of the interface in "to be added"



<h1 id="createEdgebandType"> Create edgeband type</h1>



<h1 id="updateEdgebandType"> Update edgeband type</h1>



<h1 id="edgebandInventoryHistory"> Edgeband inventory history</h1>

