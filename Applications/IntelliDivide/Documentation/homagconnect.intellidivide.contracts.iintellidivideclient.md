# IIntelliDivideClient

Namespace: HomagConnect.IntelliDivide.Contracts

IntelliDivide client interface.

```csharp
public interface IIntelliDivideClient
```

## Methods

### **ArchiveOptimizationAsync(Guid)**

Archives the optimization having the specified id.

```csharp
Task ArchiveOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **DeleteOptimizationAsync(Guid)**

Deletes the optimization having the specified id.

```csharp
Task DeleteOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **DownloadSolutionExport(Guid, Guid, SolutionExportType, FileInfo)**

Downloads the specified [SolutionExportType](./homagconnect.intellidivide.contracts.result.solutionexporttype.md) into the specified file.

```csharp
Task DownloadSolutionExport(Guid optimizationId, Guid solutionId, SolutionExportType exportTye, FileInfo fileInfo)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`solutionId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`exportTye` [SolutionExportType](./homagconnect.intellidivide.contracts.result.solutionexporttype.md)<br>

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[FileNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/system.io.filenotfoundexception)<br>
Thrown, when the specified file is not available.

### **GetImportTemplatesAsync(OptimizationType, String, String)**

Gets the import templates which have been created for the [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md). See
  for details.

```csharp
Task<IEnumerable<OptimizationImportTemplate>> GetImportTemplatesAsync(OptimizationType optimizationType, string fileExtension, string name)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>

`fileExtension` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;OptimizationImportTemplate&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMachineAsync(String)**

Gets the machine having the name.

```csharp
Task<OptimizationMachine> GetMachineAsync(string machineName)
```

#### Parameters

`machineName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;OptimizationMachine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The machine if exists, otherwise null.

### **GetMachinesAsync()**

Gets the list of machines.

```csharp
Task<IEnumerable<OptimizationMachine>> GetMachinesAsync()
```

#### Returns

[Task&lt;IEnumerable&lt;OptimizationMachine&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMachinesAsync(OptimizationType)**

Gets the list of machines of the specified [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md).

```csharp
Task<IEnumerable<OptimizationMachine>> GetMachinesAsync(OptimizationType optimizationType)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>

#### Returns

[Task&lt;IEnumerable&lt;OptimizationMachine&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMaterialStatisticsAsync(DateTime, DateTime, UInt32, UInt32)**

Gets the material statistics.

```csharp
Task<IEnumerable<MaterialEfficiency>> GetMaterialStatisticsAsync(DateTime from, DateTime to, uint take, uint skip)
```

#### Parameters

`from` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`to` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;MaterialEfficiency&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationAsync(Guid)**

Gets the optimization having the specified optimization id.

```csharp
Task<Optimization> GetOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The id of of the optimization to get.

#### Returns

[Task&lt;Optimization&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(OptimizationType, UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, uint take, uint skip)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>
Request only optimizations having a specific [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to return max.

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to skip.

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown, when more then 100 optimizations are requested.

### **GetOptimizationsAsync(OptimizationType, String, UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, string orderBy, uint take, uint skip)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>
Request only optimizations having a specific [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)

`orderBy` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Optimization property name to order by [Optimization](./homagconnect.intellidivide.contracts.optimization.md)

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to return max.

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to skip.

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown, when more then 100 optimizations are requested.

### **GetOptimizationsAsync(OptimizationType, OptimizationStatus, UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, uint take, uint skip)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>
Request only optimizations having a specific [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)

`optimizationStatus` [OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)<br>
Request only optimizations having a specific [OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to return max.

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to skip.

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown, when more then 100 optimizations are requested.

### **GetOptimizationsAsync(OptimizationType, OptimizationStatus, String, UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, string orderBy, uint take, uint skip)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>
Request only optimizations having a specific [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)

`optimizationStatus` [OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)<br>
Request only optimizations having a specific [OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md)

`orderBy` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Optimization property name to order by [Optimization](./homagconnect.intellidivide.contracts.optimization.md)

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to return max.

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to skip.

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown, when more then 100 optimizations are requested.

### **GetOptimizationsAsync(UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
Task<IEnumerable<Optimization>> GetOptimizationsAsync(uint take, uint skip)
```

#### Parameters

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to return max.

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to skip.

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown, when more then 100 optimizations are requested.

### **GetOptimizationsAsync(String, UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
Task<IEnumerable<Optimization>> GetOptimizationsAsync(string orderBy, uint take, uint skip)
```

#### Parameters

`orderBy` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Optimization property name to order by [Optimization](./homagconnect.intellidivide.contracts.optimization.md)

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to return max.

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
Quantity of optimizations to skip.

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception)<br>
Thrown, when more then 100 optimizations are requested.

### **GetOptimizationStatusAsync(Guid)**

Gets the [OptimizationStatus](./homagconnect.intellidivide.contracts.optimizationstatus.md) of the optimization having the provided optimization id.

```csharp
Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The id of of the optimization.

#### Returns

[Task&lt;OptimizationStatus&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetParametersAsync(OptimizationType)**

Gets the list of parameter sets for the specified [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md).

```csharp
Task<IEnumerable<OptimizationParameter>> GetParametersAsync(OptimizationType optimizationType)
```

#### Parameters

`optimizationType` [OptimizationType](./homagconnect.intellidivide.contracts.common.optimizationtype.md)<br>

#### Returns

[Task&lt;IEnumerable&lt;OptimizationParameter&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetSolutionDetailsAsync(Guid, Guid)**

Gets the solution details.

```csharp
Task<SolutionDetails> GetSolutionDetailsAsync(Guid optimizationId, Guid solutionId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`solutionId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;SolutionDetails&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetSolutionsAsync(Guid)**

Gets the [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) which have been calculated for an optimization request.

```csharp
Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The id of of the optimization.

#### Returns

[Task&lt;IEnumerable&lt;Solution&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
Solutions if the optimization has been optimized successfully, otherwise an empty list.

### **RequestOptimizationAsync(OptimizationRequest, ImportFile[])**

Request an optimization based on a structured [OptimizationRequest](./homagconnect.intellidivide.contracts.request.optimizationrequest.md).

```csharp
Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, ImportFile[] files)
```

#### Parameters

`optimizationRequest` [OptimizationRequest](./homagconnect.intellidivide.contracts.request.optimizationrequest.md)<br>

`files` [ImportFile[]](./homagconnect.intellidivide.contracts.common.importfile.md)<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RequestOptimizationAsync(OptimizationRequestUsingTemplate, ImportFile[])**

Request an optimization based on a structured [OptimizationRequest](./homagconnect.intellidivide.contracts.request.optimizationrequest.md).

```csharp
Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingTemplate optimizationRequest, ImportFile[] files)
```

#### Parameters

`optimizationRequest` [OptimizationRequestUsingTemplate](./homagconnect.intellidivide.contracts.request.optimizationrequestusingtemplate.md)<br>

`files` [ImportFile[]](./homagconnect.intellidivide.contracts.common.importfile.md)<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RequestOptimizationAsync(OptimizationRequestUsingProject, FileInfo)**

Request an optimization using a structured zip file.

```csharp
Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingProject optimizationRequest, FileInfo projectFile)
```

#### Parameters

`optimizationRequest` [OptimizationRequestUsingProject](./homagconnect.intellidivide.contracts.request.optimizationrequestusingproject.md)<br>
Optimization request based on a structured [OptimizationRequest](./homagconnect.intellidivide.contracts.request.optimizationrequest.md).

`projectFile` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>
Structured zip file, whose format corresponds to the ImportSpecification (
 
 format.

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **SendSolutionAsync(Guid, Guid)**

Sends the solution to the machine for which the optimization was requested for.

```csharp
Task SendSolutionAsync(Guid optimizationId, Guid solutionId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`solutionId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

#### Exceptions

[NotSupportedException](https://docs.microsoft.com/en-us/dotnet/api/system.notsupportedexception)<br>
Thrown, if the selected machine is not able send.

### **StartOptimizationAsync(Guid)**

Starts the optimization having the specified id.

```csharp
Task StartOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **WaitForCompletion(Guid, TimeSpan)**

Waits until the optimization has reached the state [OptimizationStatus.Optimized](./homagconnect.intellidivide.contracts.optimizationstatus.md#optimized) or has reached a state
 from which the state [OptimizationStatus.Optimized](./homagconnect.intellidivide.contracts.optimizationstatus.md#optimized) can't get reached any more.

```csharp
Task<Optimization> WaitForCompletion(Guid optimizationId, TimeSpan maxDuration)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`maxDuration` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

#### Returns

[Task&lt;Optimization&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[TimeoutException](https://docs.microsoft.com/en-us/dotnet/api/system.timeoutexception)<br>
Raised, when the specified maxDuration has been exceeded.
