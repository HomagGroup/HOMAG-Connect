# MaterialManagerClientMaterial

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClientMaterial : HomagConnect.Base.Services.ServiceBase
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClientMaterial](./homagconnect.materialmanager.client.materialmanagerclientmaterial.md)

## Properties

### **Edgebands**

```csharp
public MaterialManagerClientMaterialEdgebands Edgebands { get; set; }
```

#### Property Value

[MaterialManagerClientMaterialEdgebands](./homagconnect.materialmanager.client.materialmanagerclientmaterialedgebands.md)<br>

### **Boards**

```csharp
public MaterialManagerClientMaterialBoards Boards { get; set; }
```

#### Property Value

[MaterialManagerClientMaterialBoards](./homagconnect.materialmanager.client.materialmanagerclientmaterialboards.md)<br>

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

### **MaterialManagerClientMaterial(HttpClient)**

```csharp
public MaterialManagerClientMaterial(HttpClient client)
```

#### Parameters

`client` HttpClient<br>
