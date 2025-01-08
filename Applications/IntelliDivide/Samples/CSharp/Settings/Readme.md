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

> For samples, please refer to 
 [ImportTemplatesSamples.cs](ImportTemplatesSamples.cs), [MachineSamples.cs](MachineSamples.cs) and [ParameterSamples.cs](ParameterSamples.cs).
