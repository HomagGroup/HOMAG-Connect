<strong>[This is preliminary documentation and is subject to change.]</strong>

# HOMAG productionAssist Client

The HOMAG productionAssist Client streamlines the process of accessing the productionAssist REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.ProductionAssist.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.
## Getting started

### Authentication / Authorization

To begin using the HOMAG ProductionAssist Client, you must first ensure that you have a valid HOMAG Connect ProductionAssist License.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the ProductionAssist client:
            
var client = new ProductionAssistClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](Samples/CSharp/Authentication) page for detailed instructions.

### Rate limiting

All endpoints of HOMAG Connect are rate limited. This applies to the productionAssist Client as well as to the productionAssist Feedback Client. If your application sends too many requests within a short time, HOMAG Connect rejects them with **HTTP 429 (Too Many Requests)** and the client throws an `HttpRequestException`. Wait before you retry and reduce the number of requests where possible (for example by using paging and caching). To let the client repeat rejected requests automatically, set `RetryOnRateLimit = true` (see [retry mode](https://github.com/HomagGroup/HOMAG-Connect#retry-mode)). Further recommendations and a retry example can be found in the [rate limiting documentation](https://github.com/HomagGroup/HOMAG-Connect#rate-limiting).

## Further details and explanations
1. For detailed examples and explanations, please refer to [Samples](Samples/Readme.md)

# HOMAG productionAssist Feedback Client

The HOMAG productionAssist Feedback Client streamlines the process of accessing the productionAssist Feedback REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.ProductionAssist.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.
## Getting started

### Authentication / Authorization

To begin using the HOMAG ProductionAssist Feedback Client, you must first ensure that you have a valid HOMAG Connect ProductionAssist Fedback License.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the ProductionAssist Feedback client:
            
var client = new ProductionAssistFeedbackClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](Samples/CSharp/Authentication) page for detailed instructions.




