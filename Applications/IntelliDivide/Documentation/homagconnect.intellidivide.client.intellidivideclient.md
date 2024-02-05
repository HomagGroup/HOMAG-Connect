# IntelliDivideClient

Namespace: HomagConnect.IntelliDivide.Client

```csharp
public class IntelliDivideClient : HomagConnect.Base.Services.ServiceBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [IntelliDivideClient](./homagconnect.intellidivide.client.intellidivideclient.md)

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

### **RequestOptimizationAsync(FileInfo)**

```csharp
public Task<OptimizationRequestResponse> RequestOptimizationAsync(FileInfo projectFile)
```

#### Parameters

`projectFile` [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo)<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RequestOptimizationAsync(OptimizationRequest, ImportFile[])**

```csharp
public Task<OptimizationRequestResponse> RequestOptimizationAsync(OptimizationRequest optimizationRequest, ImportFile[] files)
```

#### Parameters

`optimizationRequest` OptimizationRequest<br>

`files` ImportFile[]<br>

#### Returns

[Task&lt;OptimizationRequestResponse&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationAsync(Guid)**

```csharp
public Task<Optimization> GetOptimizationAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;Optimization&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOptimizationsAsync(UInt32, UInt32)**

Gets a [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) of optimizations available.

```csharp
public Task<IEnumerable<Optimization>> GetOptimizationsAsync(uint take, uint skip)
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
Thrown, when more then  optimizations are requested.

### **GetOptimizationStatusAsync(Guid)**

```csharp
public Task<OptimizationStatus> GetOptimizationStatusAsync(Guid optimizationId)
```

#### Parameters

`optimizationId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

#### Returns

[Task&lt;OptimizationStatus&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

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
