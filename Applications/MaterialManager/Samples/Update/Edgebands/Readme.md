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
