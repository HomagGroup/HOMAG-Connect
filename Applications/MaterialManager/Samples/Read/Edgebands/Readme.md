# Read edgeband data

With the HOMAG Connect materialManager edgebands client, the edgebands related data can be retrieved from materialManager for further programmatic evaluation.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the edgebandCodes you want to retrieve data for:
var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ACR_Buche_mit_Silberstreifen_2.00_43.0_HM" };

// Get the data
var edgebandTypes = client.GetEdgebandsTypes(edgebandCodes);

// Use the retrieved data for further processing

```