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

With the Homag Connect materialManager edgebands client, new edgeband types can be created.

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the new edgeband type request:
var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
{
    Code = "EB_White_1mm",
    Description = "White Edgeband 1mm",
    Thickness = 1.0,
    Width = 23.0,
    // Add other required properties
};

// Create the new edgeband type
var newEdgebandType = await client.CreateEdgebandType(edgebandTypeRequest);

// Use the created edgeband type for further processing
Console.WriteLine($"Created Edgeband Type: {newEdgebandType.Code}, {newEdgebandType.Description}");
```



<h1 id="updateEdgebandType"> Update edgeband type</h1>

With the Homag Connect materialManager edgebands client, existing edgeband types can be updated. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the edgeband type update request:
var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
{
    Description = "Updated White Edgeband 1mm",
    Thickness = 1.1,
    Width = 24.0,
    // Add other required properties
};

// Update the edgeband type
var updatedEdgebandType = await client.UpdateEdgebandType("EB_White_1mm", edgebandTypeUpdate);

// Use the updated edgeband type for further processing
Console.WriteLine($"Updated Edgeband Type: {updatedEdgebandType.Code}, {updatedEdgebandType.Description}");
```



<h1 id="edgebandInventoryHistory"> Edgeband inventory history</h1>

The Homag Connect materialManager edgebands client provides a method to view the the history of the edgeband inventory. 



<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the date range for the inventory history
DateTime from = DateTime.Now.AddMonths(-1);
DateTime to = DateTime.Now;

// Get the inventory history
var inventoryHistory = await client.GetEdgebandTypeInventoryHistoryAsync(from, to);

// Use the inventory history for further processing
foreach (var history in inventoryHistory)
{
    Console.WriteLine($"Date: {history.Date}, Quantity: {history.Quantity}");
}
```

