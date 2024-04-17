# MaterialManagerClientMaterialEdgebands

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClientMaterialEdgebands : HomagConnect.Base.Services.ServiceBase, HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces.IMaterialManagerClientMaterialEdgebands
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClientMaterialEdgebands](./homagconnect.materialmanager.client.materialmanagerclientmaterialedgebands.md)<br>
Implements IMaterialManagerClientMaterialEdgebands

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

### **MaterialManagerClientMaterialEdgebands(HttpClient)**

```csharp
public MaterialManagerClientMaterialEdgebands(HttpClient client)
```

#### Parameters

`client` HttpClient<br>

## Methods

### **GetEdgebandTypes(Int32, Int32)**

```csharp
public Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypeByEdgebandCode(String)**

```csharp
public Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode)
```

#### Parameters

`edgebandCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;EdgebandType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypeByEdgebandCodeIncludingDetails(String)**

```csharp
public Task<EdgebandType> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode)
```

#### Parameters

`edgebandCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;EdgebandType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypesByMaterialCodes(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<EdgebandType>> GetEdgebandTypesByMaterialCodes(IEnumerable<string> edgebandCodes)
```

#### Parameters

`edgebandCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypesByMaterialCodesIncludingDetails(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByMaterialCodesIncludingDetails(IEnumerable<string> edgebandCodes)
```

#### Parameters

`edgebandCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
