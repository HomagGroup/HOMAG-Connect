﻿# Request a nesting optimization using a structured file (Excel, CSV, PNX, ...), the referenced MPRs and a template

With the HOMAG Connect intelliDivide Client, you can easily import parts lists from structured files, such as Excel, CSV, or PNX together with the referenced MPRs. 

The import is done by referring to a template that indicates how to map columns to the parts list. This feature comes in handy when you have created the parts list in an external system and need to import it into the app.

Before using a template for optimization in the client, it's crucial to create one. You can find detailed instructions on how to do so in the `Import of parts lists for intelliDivide Nesting` section in the [documentation](https://docs.homag.cloud/en/intellidivide/tutorial/importing-data).

As for all optimization requests, you can specify the optimization name, the machine, and the parameters to be used. 

Also, it is necessary to assign the property `ImportTemplate` with the name of the import template to be used.

```c#
var request = new OptimizationRequestUsingTemplate
{
    Name = "HOMAG Connect - Template_CSV-MPR_ImportOnly",
    Machine = "productionAssist Nesting",
    Parameters = "Default",
    ImportTemplate = "CSV-MPR template" // Name of the template as shown in the app  
};
```

The structured file is passed with the referenced MPRs as a `ZIP` package to intelliDivide as an `ImportFile` object. The file can be imported from a local path or a stream.

```c#
var importFile = await ImportFile.CreateAsync(@"Data\Nesting\Kitchen.zip");

var response = await intelliDivide.RequestOptimization(request, importFile);
```

The response contains the `optimizationId`, which can be used to start the optimization or to retrieve the optimization result.

>For a detailed example, please refer to `NestingRequest_Template_CSV_MPR_ImportOnly` in the file [NestingRequestUsingTemplateSamples.cs](NestingRequestUsingTemplateSamples.cs).