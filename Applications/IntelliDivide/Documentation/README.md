# HOMAG IntelliDivide Client

The [HOMAG IntelliDivide Client](./homagconnect.intellidivide.client.intellidivideclient.md) enables access to the intelliDivide application for integration into your own applications. The functions offered on the website are available for a programmatic integration.

The HOMAG IntelliDivide Client is written in C# ([Source Code](./../Client/IntelliDivideClient.cs)). It only facilitates access to the intelliDivide REST API. The source code can be used to convert the relevant functions for integration into another programming language.

The most important functions are described below. More can be found in the [Documentation](./homagconnect.intellidivide.client.intellidivideclient.md) and in the [Source Code](./../Client/IntelliDivideClient.cs) on GitHub.

## Request a optimization

There are various methods available for requesting an optimization.

Method| sdf|Sample
------|------|------
Object Model|The data required (parts, machine, parameter, boards (optional)) for optimization is gathered in a [OptimizationRequest](homagconnect.intellidivide.contracts.request.optimizationrequest.md) object and transferred to intelliDivide.|[CuttingOptimizationUsingObjectModel.cs](./../Samples/Requests/Cutting/CuttingOptimizationUsingObjectModel.cs)
Import template + file (Excel, CSV, PNX)|A structured file (e.g. [Kitchen.xlsx](./../Samples/Requests/Cutting/Kitchen.xlsx)) is sent to intelliDivide. The data is converted into the intelliDivide object model using an import template previously created in the web application. See [docs.homag.cloud](https://docs.homag.cloud/en/intellidivide/tutorial/importing-data) for details.| [CuttingOptimizationUsingExcel.cs](./../Samples/Requests/Cutting/CuttingOptimizationUsingExcel.cs)
Project.zip|A structured zip file, whose format corresponds to the [ImportSpecification](https://dev.azure.com/homag-group/FOSSProjects/_git/homag-api-gateway-client?path=/Documentation/ImportSpecification.md&_a=preview) and contains all data, is sent to intelliDivide. |[CuttingOptimizationUsingProjectZip.cs](./../Samples/Requests/Cutting/CuttingOptimizationUsingProjectZip.cs)

Depending on the [OptimizationRequestAction](./homagconnect.intellidivide.contracts.request.optimizationrequestaction.md) set the [OptimizationRequest](./homagconnect.intellidivide.contracts.request.optimizationrequest.md) is just imported, the optimization is started or if supported by the machine the balanced solution is automatically send.