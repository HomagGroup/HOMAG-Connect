# Import order request using .zip file

#### Prepare the import request

For import request you have to specify two parameters, the FileInfo (the [zip file](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Samples/CSharp/Orders/Project.zip) which will be imported) and the action which will take place during import.

```c#
  var projectFile = new FileInfo(@"Orders\Project.zip");
  
  var request = new ImportOrderRequest
  {
      Action = ImportOrderRequestAction.ImportOnly
  };
```

#### Define the steps to be performed

By default, the import request is only created. 

However, it is also possible to [specify](../../../../Contracts/Import/ImportOrderRequestAction.cs) that the import should only be started.

#### Send the request and wait for the result

The prepared request needs to be sent to productionManager.

```c#
var response = await productionManager.ImportOrderAsync(request, projectFile);

```

The outcome of the import would be a 'correlationId' which can be used to find additional data about the import which is taking place

```c#
var correlationId = response.CorrelationId;
```
> For a detailed example on how start an import job, please refer to <i>ImportOrderUsingProjectZipAsync</i> in the file [ImportOrdersSamples.cs](ImportOrderSamples.cs).

# Utilize the outcome of the import
### Get the import state

After the import job is started, the response will contain a 'correlationId' which can be used to retrieve the status of the import job.

```c#
var importState = await productionManager.GetImportOrderStateAsync(correlationId);
```

The result is a [ImportOrderStateResponse](../../../../Contracts/Import/ImportOrderStateResponse.cs) which based on the progress of the import job can retrieve a link to the newly created order and also the ID of the order.
In case of error during import, 'ErrorDetails' should contain some basic information why the import failed.

Also the current state of the import is retrieved. The possible states are defined in [ImportState](../../../../Contracts/Import/ImportState.cs).

> For a detailed example on how to use the outcome of import job, please refer to <i>GetImportOrderStateAsync</i> in the file [ImportOrdersSamples.cs](ImportOrderSamples.cs).
