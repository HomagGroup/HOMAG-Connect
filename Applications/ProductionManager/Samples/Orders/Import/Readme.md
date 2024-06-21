# Import order request using .zip file

#### Prepare the import request

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

However, it is also possible to [specify](../../../Contracts/Import/ImportOrderRequestAction.cs) that the import should only be started.

#### Send the request and wait for the result

The prepared request needs to be sent to productionManager.

```c#
var response = await productionManager.ImportOrderAsync(request, projectFile);

```
> For a detailed example on how start an import job, please refer to <i>ImportOrderUsingProjectZipAsync</i> in the file [ImportOrdersSamples.cs](ImportOrderSamples.cs).
##### Utilize the outcome of the import

The outcome of the import would be a 'correlationId' which can be used to find additional data about the import which is taking place

```c#
var response = await productionManager.ImportOrderAsync(request, projectFile);
var correlationId = response.CorrelationId;
```

> For a detailed example on how to use the outcome of import job, please refer to <i>GetImportOrderStateAsync</i> in the file [ImportOrdersSamples.cs](ImportOrderSamples.cs).
