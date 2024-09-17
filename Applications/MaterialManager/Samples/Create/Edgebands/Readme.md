<h1 id="createEdgebandType"> Create edgeband type</h1>

With the HOMAG Connect materialManager edgebands client, new edgeband types can be created.

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
