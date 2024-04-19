# HOMAG IntelliDivide Client

The HOMAG IntelliDivide Client streamlines the process of accessing the intelliDivide REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.IntelliDivide.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.

## Getting started

### Authentication / Authoriziation

To begin using the HOMAG IntelliDivide Client, you must first ensure that you have a valid HOMAG Connect IntelliDivide License. This license is included in the IntelliDivide Advanced and Premium licenses.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the intelliDivide client:
            
var client = new IntelliDivideClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](Samples/Authentication) page for detailed instructions.

### Perform an optimization

#### Prepare the optimization request

There are several ways to request an optimization:

1. Using the Object Model ([Cutting](Samples/Requests/ObjectModel/Cutting/Readme.md) / [Nesting]())
2. [Importing a structured file (Excel, CSV, PNX, ...)]()
3. [Importing a standardized ZIP file]()

Please refer to the linked pages for detailed samples and explanations.

For all optimization requests, you can specify the optimization name, the machine, and the parameters to be used.

```c#
request.Name = "Sample";
request.Machine = "productionAssist Cutting";
request.Parameters = "Default";
```
If you don't provide this information, the optimization name will be automatically generated and the first machine and default parameters will be used.

The type of machine determines whether a cutting or nesting algorithm is used to perform the optimization.

#### Specify the boards taken into account

You can specify the boards to be considered for the optimization, although it is not required. 

```c#
 request.Boards.Add(
    new OptimizationRequestBoard
    {
        MaterialCode = "MDF_19.0",
        BoardCode = "MDF",
        Length = 2800,
        Width = 2070,
        Thickness = 19.0,
        Costs = 10,
        Grain = Grain.None,
        Quantity = 70,
    });
``` 
If no boards are defined, the data will be automatically obtained from materialManager.

#### Define the steps to be performed

By default, the optimization request is only created. 

However, it is also possible to [specify](./Contracts/Request/OptimizationRequestAction.cs) that the optimization should be automatically started and, if successful, sent to the workstation.

```c#
request.Action = OptimizationRequestAction.Optimize;
``` 

#### Send the request and wait for the result

The prepared request needs to be sent to intelliDivide.

```c#
var response = await intelliDivide.RequestOptimizationAsync(request);

if (response.ValidationErrors.Any())
{
    // Request contains errors which need to get corrected before the optimization can get executed.

    throw new ValidationException(response.ValidationErrors[0].ToString());
}
else
{
    var optimizationId = response.OptimizationId;
    var optimizationStatus = response.OptimizationStatus;
    var linkToOptimizationInApp = response.Link;
}
``` 

If there are any validation errors in the response, the optimization cannot be executed until they have been addressed.

If the action was set to <i> OptimizationRequestAction.Optimize</i> or <i>OptimizationRequestAction.Send</i> the optimization gets executed automatically. 

Otherwise the optimization needs to get started explicitly.

```c#
await intelliDivide.StartOptimizationAsync(optimizationId); 
``` 

IntelliDivide executes the optimization, and its completion can be awaited.

```c#
var optimization = await intelliDivide.WaitForOptimizationStatusAsync(optimizationId, OptimizationStatus.Optimized, TimeSpan.FromMinutes(5));
``` 

#### Download solution exports

After the optimization is done the solutions can get retrieved. The first one is the recommended solution.

```c#
var solutions = await intelliDivide.GetSolutionsAsync(optimization.Id);

var recommendedSolution = solutions.First();
var targetDirectory = new DirectoryInfo(".");

await intelliDivide.DownloadSolutionExportAsync(recommendedSolution, SolutionExportType.Saw, targetDirectory);
``` 

The downloaded saw file can get copied to the machine network share.

## Detailed documentation

- [Authentication / Authorization](Samples/Authentication/Readme.md)
- [Material Statistics](Samples/Statistics/Material/Readme.md)