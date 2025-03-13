# Read edgeband data

With the HOMAG Connect materialManager edgebands client, the edgebands related data can be retrieved from materialManager for further programmatic evaluation.

## GetEdgebandTypes

**Example:**

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Define the edgebandCodes you want to retrieve data for:
var edgebandCodes = new List<string> { "ABS_Abruzzo_colore_1.00_100.0_HM", "ACR_Buche_mit_Silberstreifen_2.00_43.0_HM" };

// Get the data
var edgebandTypes = client.GetEdgebandTypes(edgebandCodes);

// Use the retrieved data for further processing

```

## GetLicensedMachines

Get the machine information of machines in tapio licensed for the materials api. This can be used to get and set the technology macros.

**Example:**

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Get the data
var tapioMachines = client.GetLicensedMachines();
```

**Returns:**

```csharp
public class TapioMachine
{
    /// <summary>
    /// Gets the name of the machine.
    /// </summary>
    public string Name { get; } = string.Empty;

    /// <summary>
    /// Gets the tapio machine id of the machine.
    /// </summary>
    public string TapioMachineId { get; } = string.Empty;
}
```

## GetTechnologyMacrosFromMachine

Technology macros can be set explicitly for a machine when [creating](../../Create/Edgebands/Readme.md) or [updating](../../Update/Edgebands/Readme.md) an `EdgebandType`. This is required when materialManager works together with the HOMAG woodCommander Software.

The `EdgebandType` and its update and reate objects contain these macro properties:

```csharp
    /// <summary>
    /// Gets or sets the technology macro name by tapio machine id as the key.
    /// </summary>
    public IDictionary<string, string>? MachineTechnologyMacro { get; set; }

    /// <summary>
    /// Gets or sets the technology macro name.
    /// </summary>
    [StringLength(50)]
    public string? TechnologyMacro { get; set; }
```

The `TechnologyMacro` sets a macro name for machines before woodCommander 5 or without capability of setting a macro specifically.
With `MachineTechnologyMacro` and woodCommander 5 you can set a macro specifically for one machine.

With `GetTechnologyMacrosFromMachine` the available technology macro names can be requested for a given tapio machine id.

**Example:**

```csharp
// Create new instance of the materialManager client:
var client = new MaterialManagerClientMaterialEdgebands(subscriptionId, authorizationKey);

// Get the data
var technologyMacros = client.GetTechnologyMacrosFromMachine(tapioMachineId);
```
