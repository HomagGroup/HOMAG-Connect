# Create edgeband type

With the HOMAG Connect materialManager edgebands client, new edgeband types can be created.

## CreateEdgebandType

**Example:**

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
{
    EdgebandCode = "EB_White_1mm",
    Height = 20,
    Thickness = 1.0,
    DefaultLength = 23.0,
    MaterialCategory = EdgebandMaterialCategory.Veneer,
    Process = EdgebandingProcess.Other,
};

var newEdgebandType = await client.CreateEdgebandType(edgebandTypeRequest);

Console.WriteLine($"Created Edgeband Type: {newEdgebandType.Code}");
```

## CreateEdgebandType with technology macro

```csharp
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

var edgebandTypeUpdate = new MaterialManagerRequestEdgebandType
{
    EdgebandCode = "EB_White_1mm",
    Height = 20,
    Thickness = 1.0,
    DefaultLength = 23.0,
    MaterialCategory = EdgebandMaterialCategory.Veneer,
    Process = EdgebandingProcess.Other,    
    MachineTechnologyMacro = new Dictionary<string, string>
    {
        { "hg0000000000", "ABS_1.00_RM_HM"}
    },
    // other properties
};

var newEdgebandType = await client.CreateEdgebandType(edgebandTypeRequest);
```

Available technology macro names and machines can be requested within the same client.
