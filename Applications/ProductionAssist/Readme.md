<strong>[This is preliminary documentation and is subject to change.]</strong>

# HOMAG productionAssistFedback Client

The HOMAG productionAssistFedback Client streamlines the process of accessing the productionAssistFedback REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.ProductionAssist.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.
## Getting started

### Authentication / Authorization

To begin using the HOMAG ProductionAssistFeedback Client, you must first ensure that you have a valid HOMAG Connect ProductionAssistFedback License.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the ProductionAssistFeedback client:
            
var client = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);
``` 
