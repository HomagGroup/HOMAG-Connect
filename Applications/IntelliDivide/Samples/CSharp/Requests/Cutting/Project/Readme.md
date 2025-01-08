# Request a cutting optimization using a structured zip file

With the HOMAG Connect intelliDivide Client, you can easily import orders using a zip file in a specific format. 

The specification of the structure of the file can be found in the [documentation](https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md&_a=preview).

As for all optimization requests, you can specify the optimization name, the machine, and the parameters to be used. 

```c#
var request = new OptimizationRequestUsingProject
{
    Name = "Sample",
    Machine = "productionAssist Cutting",
    Parameters = "Default",        
};
```

The structured file is passed to intelliDivide as an `ImportFile` object. The file can be imported from a local path or a stream.

```c#
var projectFile = new FileInfo(@"Requests\Cutting\Project\Project.zip");

var response = await intelliDivide.RequestOptimization(request, projectFile);
```

The response contains the `optimizationId`, which can be used to start the optimization or to retrieve the optimization result.

> For a detailed example, please refer to `CuttingRequest_ProjectZip_ImportOnly` in the file [CuttingOptimizationUsingProjectZip.cs](CuttingOptimizationUsingProjectZip.cs).