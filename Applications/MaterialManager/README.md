# HOMAG materialManager Client

The HOMAG materialManager Client enables access to the materialManager application for integration into your own applications. The functions offered on the website are available for a programmatic integration.
The client is also available as a NuGet package on nuget.org and can be smoothly integrated into .NET applications.

The HOMAG materialManager Client is written in C# ([Source Code](./Client/MaterialManagerClient.cs)). It facilitates access to the materialManager REST API. The source code can be used to convert the relevant functions for integration into another programming language.

The most important functions are described in the Documentation below, more can be found [here](./homagconnect.materialmanager.client.materialmanagerclient.md) and in the [Source Code](./Client/MaterialManagerClient.cs) on GitHub.

## Getting started

### Authentication / Authoriziation

To begin using the HOMAG Connect materialManager Client, you must first ensure that you have a valid HOMAG Connect materialManager License. The license can be purchased as an Add-on to materialManager in the tapio Marketplace.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the materialManager client:
            
var client = new materialManagerClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](../../Documentation/Authentication/Readme.md) page for detailed instructions.

## Further details and explanations

1. [Samples](Samples/Readme.md)
2. [Excel (analog to MmrMobile)](../MmrMobile/Documentation/Excel/README.md)
3. [powerBI (analog to MmrMobile)](../MmrMobile/Documentation/powerBi/README.md)

