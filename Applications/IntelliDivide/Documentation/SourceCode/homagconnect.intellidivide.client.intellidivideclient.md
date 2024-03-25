# IntelliDivideClient

Namespace: HomagConnect.IntelliDivide.Client

```csharp
public class IntelliDivideClient : HomagConnect.Base.Services.ServiceBase, HomagConnect.IntelliDivide.Contracts.IIntelliDivideClient
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [IntelliDivideClient](./homagconnect.intellidivide.client.intellidivideclient.md)<br>
Implements IIntelliDivideClient

## Properties

### **ApiVersion**

```csharp
public string ApiVersion { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Client**

```csharp
public HttpClient Client { get; }
```

#### Property Value

HttpClient<br>

### **HeaderKey**

```csharp
public string HeaderKey { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OnDeprecatedAction**

```csharp
public Action<HttpRequestMessage, HttpResponseMessage> OnDeprecatedAction { get; set; }
```

#### Property Value

[Action&lt;HttpRequestMessage, HttpResponseMessage&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.action-2)<br>

### **ThrowExceptionOnDeprecatedCalls**

```csharp
public bool ThrowExceptionOnDeprecatedCalls { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **IntelliDivideClient(HttpClient)**

```csharp
public IntelliDivideClient(HttpClient client)
```

#### Parameters

`client` HttpClient<br>

## Methods

### **GetMaterialStatisticsAsync(DateTime, DateTime, UInt32, UInt32)**

Gets the statistics for the material efficiency.

```csharp
public Task<IEnumerable<MaterialEfficiency>> GetMaterialStatisticsAsync(DateTime from, DateTime to, uint take, uint skip)
```

#### Parameters

`from` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`to` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;MaterialEfficiency&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetImportTemplatesAsync(OptimizationType, String, String)**

```csharp
public Task<IEnumerable<OptimizationImportTemplate>> GetImportTemplatesAsync(OptimizationType optimizationType, string fileExtension, string name)
```

#### Parameters

`optimizationType` OptimizationType<br>

`fileExtension` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;OptimizationImportTemplate&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMachineAsync(String)**

```csharp
public Task<OptimizationMachine> GetMachineAsync(string machineName)
```

#### Parameters

`machineName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;OptimizationMachine&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMachinesAsync()**

```csharp
public Task<IEnumerable<OptimizationMachine>> GetMachinesAsync()
```

#### Returns

[Task&lt;IEnumerable&lt;OptimizationMachine&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMachinesAsync(OptimizationType)**

```csharp
public Task<IEnumerable<OptimizationMachine>> GetMachinesAsync(OptimizationType optimizationType)
```

#### Parameters

`optimizationType` OptimizationType<br>

#### Returns

[Task&lt;IEnumerable&lt;OptimizationMachine&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetParametersAsync(OptimizationType)**

```csharp
public Task<IEnumerable<OptimizationParameter>> GetParametersAsync(OptimizationType optimizationType)
```

#### Parameters

`optimizationType` OptimizationType<br>

#### Returns

[Task&lt;IEnumerable&lt;OptimizationParameter&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RequestOptimizationAsync(OptimizationRequest, ImportFile[])**

```csharp
public Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, ImportFile[] files)
```

#### Parameters

`optimizationRequest` OptimizationRequest<br>

`files` ImportFile[]<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RequestOptimizationAsync(OptimizationRequestUsingTemplate, ImportFile[])**

```csharp
public Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingTemplate optimizationRequest, ImportFile[] files)
```

#### Parameters

`optimizationRequest` OptimizationRequestUsingTemplate<br>

`files` ImportFile[]<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RequestOptimizationAsync(OptimizationRequestUsingProject, FileInfo)**

```csharp
public Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequestUsingProject optimizationRequest, FileInfo projectFile)
```

#### Parameters

`optimizationRequest` OptimizationRequestUsingProject<br>

`projectFile` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **WaitForCompletion(Guid, TimeSpan)**

```csharp
public Task<Optimization> WaitForCompletion(Guid optimizationId, TimeSpan maxDuration)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`maxDuration` [TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

#### Returns

[Task&lt;Optimization&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationAsync(Guid)**

```csharp
public Task<Optimization> GetOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;Optimization&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(OptimizationType, UInt32, UInt32)**

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, uint take, uint skip)
```

#### Parameters

`optimizationType` OptimizationType<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(OptimizationType, String, UInt32, UInt32)**

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, string orderBy, uint take, uint skip)
```

#### Parameters

`optimizationType` OptimizationType<br>

`orderBy` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(OptimizationType, OptimizationStatus, UInt32, UInt32)**

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, uint take, uint skip)
```

#### Parameters

`optimizationType` OptimizationType<br>

`optimizationStatus` OptimizationStatus<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(OptimizationType, OptimizationStatus, String, UInt32, UInt32)**

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(OptimizationType optimizationType, OptimizationStatus optimizationStatus, string orderBy, uint take, uint skip)
```

#### Parameters

`optimizationType` OptimizationType<br>

`optimizationStatus` OptimizationStatus<br>

`orderBy` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(UInt32, UInt32)**

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(uint take, uint skip)
```

#### Parameters

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(String, UInt32, UInt32)**

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(string orderBy, uint take, uint skip)
```

#### Parameters

`orderBy` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`take` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`skip` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Task&lt;IEnumerable&lt;Optimization&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationStatusAsync(Guid)**

```csharp
public Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;OptimizationStatus&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **ArchiveOptimizationAsync(Guid)**

```csharp
public Task ArchiveOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **DeleteOptimizationAsync(Guid)**

```csharp
public Task DeleteOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **StartOptimizationAsync(Guid)**

```csharp
public Task StartOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **GetSolutionDetailsAsync(Guid, Guid)**

```csharp
public Task<SolutionDetails> GetSolutionDetailsAsync(Guid optimizationId, Guid solutionId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`solutionId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;SolutionDetails&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetSolutionsAsync(Guid)**

```csharp
public Task<IEnumerable<Solution>> GetSolutionsAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;IEnumerable&lt;Solution&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **SendSolutionAsync(Guid, Guid)**

```csharp
public Task SendSolutionAsync(Guid optimizationId, Guid solutionId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`solutionId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **DownloadSolutionExport(Guid, Guid, SolutionExportType, FileInfo)**

```csharp
public Task DownloadSolutionExport(Guid optimizationId, Guid solutionId, SolutionExportType exportTye, FileInfo fileInfo)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`solutionId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`exportTye` SolutionExportType<br>

`fileInfo` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
