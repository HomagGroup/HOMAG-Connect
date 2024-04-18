# MaterialManagerClientProcessing

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClientProcessing : HomagConnect.Base.Services.ServiceBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClientProcessing](./homagconnect.materialmanager.client.materialmanagerclientprocessing.md)

## Properties

### **Optimization**

```csharp
public MaterialManagerClientProcessingOptimization Optimization { get; }
```

#### Property Value

[MaterialManagerClientProcessingOptimization](./homagconnect.materialmanager.client.materialmanagerclientprocessingoptimization.md)<br>

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

### **MaterialManagerClientProcessing(HttpClient)**

```csharp
public MaterialManagerClientProcessing(HttpClient client)
```

#### Parameters

`client` HttpClient<br>
