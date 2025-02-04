# Request an optimization

There are various methods available for requesting an optimization.

Method| sdf|Sample
------|------|------
Object Model|The data required (parts, machine, parameter, boards (optional)) for optimization is gathered in a [OptimizationRequest](../../Contracts/Request/OptimizationRequest.cs) object and transferred to intelliDivide.|[CuttingOptimizationUsingObjectModel.cs](./../../Samples/CSharp/Requests/Cutting/CuttingOptimizationCommonSamples.cs)
Import file (Excel, CSV, PNX) using a template|A structured file (e.g. [Kitchen.xlsx](./../../Samples/CSharp/Data/Cutting/Kitchen.xlsx)) is sent to intelliDivide. The data is converted into the intelliDivide object model using an import template previously created in the web application. See [docs.homag.cloud](https://docs.homag.cloud/en/intellidivide/tutorial/importing-data) for details.| [CuttingRequestUsingTemplateSamples.cs](./../../Samples/CSharp/Requests/Cutting/Template/CuttingRequestUsingTemplateSamples.cs)
Project.zip|A structured zip file, whose format corresponds to the [ImportSpecification](https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md&_a=preview) and contains all data, is sent to intelliDivide. |[CuttingOptimizationUsingProjectZip.cs](./../../Samples/CSharp/Requests/Cutting/Project/CuttingOptimizationUsingProjectZip.cs)

Depending on the set [OptimizationRequestAction](./../../Contracts/Request/OptimizationRequestAction.cs), the [OptimizationRequest](./../../Contracts/Request/OptimizationRequest.cs) is imported, the optimization is started or, if supported by the machine, the balanced solution is sent automatically.