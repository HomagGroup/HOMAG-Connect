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

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Autorization](Documentation/Autorization/Autorization.md) page for detailed instructions.

### Create an Optimization Request

Lorem impsum


### Create an Optimization Request

Lorem impsum





## Further details

1. [Authorization](Authorization/Authorization.md)
2. [Request an optimization](OptimizationRequest/OptimizationRequest.md)
3. [Anaylze Data](Statistics/Material/MaterialStatistics.md)