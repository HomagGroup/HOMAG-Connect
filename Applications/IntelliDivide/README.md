# HOMAG IntelliDivide Client

The [HOMAG IntelliDivide Client](./homagconnect.intellidivide.client.intellidivideclient.md) enables access to the intelliDivide application for integration into your own applications. The functions offered on the website are available for a programmatic integration.

The HOMAG IntelliDivide Client streamlines the process of accessing the intelliDivide REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.IntelliDivide.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.

## Getting started

### Authoriziation

To begin using the HOMAG IntelliDivide Client, you must first ensure that you have a valid HOMAG Connect IntelliDivide License. This license is included in the IntelliDivide Advanced and Premium licenses.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the intelliDivide client:
            
var client = new IntelliDivideClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authorization](Documentation/Authorization/Authorization.md) page for detailed instructions.

### Perform an optimization

#### Prepare the optimization request

There are several ways to request an optimization, each with its advantages and disadvantages:

1. [Using the Object Model]()   
2. [Importing a structured file (Excel, CSV, PNX, ...)]()
3. [Importing a standardized ZIP file]()

Please refer to the linked pages for detailed samples ane explanations.

For all optimization requests, you can specify the optimization name, the machine, and the parameters to be used.

```c#
request.Name = "Sample";
request.Machine = "productionAssist Cutting";
request.Parameters = "Default";
```
If you don't provide this information, the optimization name will be automatically generated and the first machine and default parameters will be used.

#### Specify the boards the boards taken into account

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

By default, the optimization request is only created. However, it is also possible to [specify](./Contracts/Request/OptimizationRequestAction.cs) that the optimization should be automatically started and, if successful, sent to the machine.

```c#
request.Action = OptimizationRequestAction.Optimize;
``` 



#### Send and wait for the result

Lorem impsum


### Create an Optimization Request

Lorem impsum





## Further details

1. [Authorization](Authorization/Authorization.md)
2. [Request an optimization](OptimizationRequest/OptimizationRequest.md)
3. [Anaylze Data](Statistics/Material/MaterialStatistics.md)