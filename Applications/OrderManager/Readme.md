<strong>[This is preliminary documentation and is subject to change.]</strong>

# HOMAG orderManager Client

The HOMAG orderManager Client streamlines the process of accessing the orderManager REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.OrderManager.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.
## Getting started

### Authentication / Authorization

To begin using the HOMAG orderManager Feedback Client, you must first ensure that you have a valid HOMAG Connect orderManager Fedback License.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the ProductionAssist Feedback client:
            
var client = new OrderManagerClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](Samples/Authentication) page for detailed instructions.
