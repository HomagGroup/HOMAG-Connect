<strong>[This is preliminary documentation and is subject to change.]</strong>

# HOMAG orderManager Client

The HOMAG orderManager Client streamlines the process of accessing the orderManager REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.OrderManager.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.
## Getting started

### Authentication / Authorization

To begin using the HOMAG orderManager Client, you must first ensure that you have a valid HOMAG Connect orderManager License.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the orderManager client:

var client = new OrderManagerClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](Samples/CSharp/Authentication/Readme.md) page for detailed instructions.

### Import Order

#### Prepare the import request

There are several ways to request an import:
1. [Importing a structured ZIP file](Samples/CSharp/Orders/Import/Readme.md)
2. [Importing an OrderDetails object, optionally together with referenced files](Samples/CSharp/Orders/Import/Readme.md)

Please refer to the linked pages for detailed samples and explanations.

For an import request using a structured ZIP file, you have to specify the FileInfo of the zip file which will be imported.

```c#
  var projectFile = new FileInfo(@"Orders\Project.zip");
```

#### Send the request and wait for the result

The prepared request needs to be sent to orderManager.

```c#
var response = await orderManager.ImportOrderRequest(projectFile);
```

### Utilize the outcome of the import

The outcome of the import would be a 'correlationId' which can be used to find additional data about the import which is taking place

```c#
var response = await orderManager.ImportOrderRequest(projectFile);
var correlationId = response.CorrelationId;
```

#### Get the import state

After the import job is started, the response will contain a 'correlationId' which can be used to retrieve the status of the import job.

```c#
var importState = await orderManager.GetImportOrderState(correlationId);
```

The result is a [ImportOrderStateResponse](./Contracts/Import/ImportOrderStateResponse.cs) which based on the progress of the import job can retrieve a link to the newly created order and also the ID of the order.
In case of error during import, 'ErrorDetails' should contain some basic information why the import failed

Alternatively, [WaitForImportOrderCompletion](Samples/CSharp/Orders/Import/Readme.md#wait-for-the-import-to-be-completed) can be used to await the imported order directly instead of polling the import state.

## Further details and explanations

For a detailed examples and explanations, please refer to [HOMAG Connect OrderManager Samples](Samples/Readme.md).
