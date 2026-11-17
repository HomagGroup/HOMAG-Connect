# Access the Settings

The HOMAG Connect IntelliDivide Client can be used to read the list of machines, parameters, and import templates for cutting and nesting optimizations.

```c#
// Get the list of all machines of type cutting
var machines = await intelliDivide.GetMachines(OptimizationType.Cutting)

// Get the list of all parameter sets for cutting
var parameters = await intelliDivide.GetParameters(OptimizationType.Cutting)

// Get the list of all import templates for cutting
var templates = await intelliDivide.GetImportTemplates(OptimizationType.Cutting)
``` 

## Read the machine parameters of a cutting machine

`GetMachineDetails` returns the machine configuration and, for cutting machines, the `CuttingParameters`. They are grouped like the sections of the intelliDivide "Machine parameters" dialog.

```c#
var machineDetails = await intelliDivide.GetMachineDetails("SAWTEQ S-300");
var parameters = machineDetails?.CuttingParameters;

var maxCuttingLength = parameters?.General?.MaxCuttingLength;
var bookHeight = parameters?.BookHeight?.MaximumBookHeight;
```

| Section | Property | Content | Availability |
|---|---|---|---|
| In general | `General` | Cutting lengths, program fence, optimization direction, strip width, saw blade thicknesses, manual cuttings, download option, production costs | Always; properties depend on the machine type |
| Offcuts | `Offcuts` | Small and large offcut sizes and ratings, materialManager offcuts | Always; `UseMaterialManagerOffcuts` requires a Cutting Advanced or Premium license |
| Trim cuts | `TrimCuts` | Front, back, left and right trims, recuts, reducible trims | Always; `UseMaterialManagerTrims` requires a Cutting Advanced or Premium license |
| Tension trims | `TensionTrims` | Use, type, width, distances, slot distance and middle bridge | Only if tension trims can be edited; the type requires machine support |
| Book height | `BookHeight` | Maximum book height | Not for circular saws, TKS or batch size one saws |
| Allocations | `Allocations` | Edgeband, board and offcut allocations | Only if materialManager supports the allocation types |

Rules:
- `CuttingParameters` is `null` for nesting machines.
- Sections and properties that aren't available for the machine or the license are `null` and are omitted from the JSON.
- Lengths are in millimeters and areas in square meters (`UnitSystem` = `Metric`). Percentages are whole numbers, for example `30` for 30%.
- When a `UseMaterialManager…` property is `true`, the values of the related section come from materialManager, and the section's size validations are skipped.
- Validation limits follow the dialog. For example, the tension trim width must be greater than 15 mm and less than 50 mm, and the large offcut sizes must be greater than the small offcut sizes.
- `Configuration` and the results of `GetMachine` and `GetMachines` are unchanged.

> For samples, please refer to 
 [ImportTemplatesSamples.cs](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Samples/CSharp/Settings/ImportTemplatesSamples.cs), [MachineSamples.cs](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Samples/CSharp/Settings/MachineSamples.cs) and [ParameterSamples.cs](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Samples/CSharp/Settings/ParameterSamples.cs).
