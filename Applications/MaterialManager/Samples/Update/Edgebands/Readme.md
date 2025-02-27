# Update edgeband type

With the HOMAG Connect materialManager edgebands client, existing edgeband types can be updated. 

<strong>Example:</strong>

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the edgeband type update request:
var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
{
    Thickness = 1.1,
    // Add other properties
};

// Update the edgeband type
var updatedEdgebandType = await client.UpdateEdgebandType("EB_White_1mm", edgebandTypeUpdate);

// Use the updated edgeband type for further processing
Console.WriteLine($"Updated Edgeband Type: {updatedEdgebandType.Code}");
```
