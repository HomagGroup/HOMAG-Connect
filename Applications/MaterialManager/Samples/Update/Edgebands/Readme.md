# Update edgeband type

With the HOMAG Connect materialManager edgebands client, existing edgeband types can be updated.

## UpdateEdgebandType

**Example:**

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

## UpdateEdgebandType with technology macro

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeUpdate = new MaterialManagerUpdateEdgebandType
{
    // other properties
    MachineTechnologyMacro = new Dictionary<string, string>
    {
        { "hg0000000000", "ABS_1.00_RM_HM"}
    },
    // other properties
};

var updatedEdgebandType = await client.UpdateEdgebandType("ABS_White_1mm", edgebandTypeUpdate);
```

Available technology macro names and machines can be requested within the same client.
