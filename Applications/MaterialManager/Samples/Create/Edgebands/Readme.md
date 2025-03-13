# Create edgeband type

With the HOMAG Connect materialManager edgebands client, new edgeband types can be created.

## CreateEdgebandType

**Example:**

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the new edgeband type request:
var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
{
    EdgebandCode = "EB_White_1mm",
    Height = 20,
    Thickness = 1.0,
    DefaultLength = 23.0,
    MaterialCategory = EdgebandMaterialCategory.Veneer,
    Process = Process.Other,
    // Add other properties
};

// Create the new edgeband type
var newEdgebandType = await client.CreateEdgebandType(edgebandTypeRequest);

// Use the created edgeband type for further processing
Console.WriteLine($"Created Edgeband Type: {newEdgebandType.Code}");
```

## CreateEdgebandType with technology macro

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
{
    EdgebandCode = "EB_White_1mm",
    Height = 20,
    Thickness = 1.0,
    DefaultLength = 23.0,
    MaterialCategory = EdgebandMaterialCategory.Veneer,
    Process = Process.Other,    
    MachineTechnologyMacro = new Dictionary<string, string>
    {
        { "hg0000000000", "ABS_1.00_RM_HM"}
    },
    // other properties
};

var newEdgebandType = await client.CreateEdgebandType(edgebandTypeRequest);
```

Available technology macro names and machines can be requested within the same client.
