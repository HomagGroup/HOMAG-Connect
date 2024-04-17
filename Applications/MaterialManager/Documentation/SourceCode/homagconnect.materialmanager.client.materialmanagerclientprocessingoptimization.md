# MaterialManagerClientProcessingOptimization

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClientProcessingOptimization : HomagConnect.Base.Services.ServiceBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClientProcessingOptimization](./homagconnect.materialmanager.client.materialmanagerclientprocessingoptimization.md)

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

### **MaterialManagerClientProcessingOptimization(HttpClient)**

```csharp
public MaterialManagerClientProcessingOptimization(HttpClient client)
```

#### Parameters

`client` HttpClient<br>

## Methods

### **GetOffcutParameterSetAsync(String)**

```csharp
public Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;OffcutParameterSet&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOffcutParameterSetsAsync(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<OffcutParameterSet>> GetOffcutParameterSetsAsync(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;OffcutParameterSet&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
