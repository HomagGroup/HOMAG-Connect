<strong>[This is preliminary documentation and is subject to change.]</strong>

# HOMAG productionManager Client

The HOMAG productionManager Client streamlines the process of accessing the productionManager REST API. It's available as a NuGet package on [nuget.org](https://www.nuget.org/packages/HomagGroup.HomagConnect.ProductionManager.Client) and can be smoothly integrated into .NET applications. 

Moreover, developers can easily access the source code on [GitHub](https://github.com/HomagGroup/HOMAG-Connect) to modify functions or convert them for usage in other programming languages.
## Getting started

### Authentication / Authorization

To begin using the HOMAG ProductionManager Client, you must first ensure that you have a valid HOMAG Connect ProductionManager License.

Once you have confirmed your license, you can create an instance of the client by providing your Subscription Id and Authorization Key. 
```c#
// Create new instance of the ProductionManager client:
            
var client = new ProductionManagerClient(subscriptionId, authorizationKey);
``` 

For further information on how to obtain your Authorization Key and Subscription Id, please visit the [Authentication](Samples/CSharp/Authentication) page for detailed instructions.

### Import Order

#### Prepare the import request

There are several ways to request an optimization:
1. [Importing a standardized ZIP file](Samples/CSharp/Orders/Import/Readme.md)

Please refer to the linked pages for detailed samples and explanations.

For import request you have to specify two parameters, the FileInfo(the zip file which will be imported) and the action which will take place during import.

```c#
  var projectFile = new FileInfo(@"Orders\Project.zip");
  
  var request = new ImportOrderRequest
  {
      Action = ImportOrderRequestAction.ImportOnly
  };
```

#### Define the steps to be performed

By default, the import request is only created. 

However, it is also possible to [specify](./Contracts/Import/ImportOrderRequestAction.cs) that the import should only be started.

#### Send the request and wait for the result

The prepared request needs to be sent to productionManager.

```c#
var response = await productionManager.ImportOrderAsync(request, projectFile);

```
### Utilize the outcome of the import

The outcome of the import would be a 'correlationId' which can be used to find additional data about the import which is taking place

```c#
var response = await productionManager.ImportOrderAsync(request, projectFile);
var correlationId = response.CorrelationId;
```

#### Get the import state

After the import job is started, the response will contain a 'correlationId' which can be used to retrieve the status of the import job.

```c#
var importState = await productionManager.GetImportOrderStateAsync(correlationId);
```

The result is a [ImportOrderStateResponse](./Contracts/Import/ImportOrderStateResponse.cs) which based on the progress of the import job can retrieve a link to the newly created order and also the ID of the order.
In case of error during import, 'ErrorDetails' should contain some basic information why the import failed

## Further details and explanations

For a detailed examples and explanations, please refer to [HOMAG Connect ProductionManager Samples](Samples/Readme.md).
