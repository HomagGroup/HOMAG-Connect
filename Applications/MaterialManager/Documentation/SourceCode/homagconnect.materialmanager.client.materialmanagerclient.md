# MaterialManagerClient

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClient : HomagConnect.Base.Services.ServiceBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClient](./homagconnect.materialmanager.client.materialmanagerclient.md)

## Properties

### **Material**

```csharp
public MaterialManagerClientMaterial Material { get; set; }
```

#### Property Value

[MaterialManagerClientMaterial](./homagconnect.materialmanager.client.materialmanagerclientmaterial.md)<br>

### **Processing**

```csharp
public MaterialManagerClientProcessing Processing { get; }
```

#### Property Value

[MaterialManagerClientProcessing](./homagconnect.materialmanager.client.materialmanagerclientprocessing.md)<br>

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

### **MaterialManagerClient(HttpClient)**

```csharp
public MaterialManagerClient(HttpClient client)
```

#### Parameters

`client` HttpClient<br>