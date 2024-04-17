# MaterialManagerClientMaterialBoards

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClientMaterialBoards : HomagConnect.Base.Services.ServiceBase, HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces.IMaterialManagerClientMaterialBoards
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClientMaterialBoards](./homagconnect.materialmanager.client.materialmanagerclientmaterialboards.md)<br>
Implements IMaterialManagerClientMaterialBoards

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

### **MaterialManagerClientMaterialBoards(HttpClient)**

```csharp
public MaterialManagerClientMaterialBoards(HttpClient client)
```

#### Parameters

`client` HttpClient<br>

## Methods

### **GetBoardTypes(Int32, Int32)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypeByBoardCode(String)**

```csharp
public Task<BoardType> GetBoardTypeByBoardCode(string boardCode)
```

#### Parameters

`boardCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;BoardType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypeByBoardCodeIncludingDetails(String)**

```csharp
public Task<BoardType> GetBoardTypeByBoardCodeIncludingDetails(string boardCode)
```

#### Parameters

`boardCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;BoardType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByBoardCodes(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypesByBoardCodes(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByBoardCodesIncludingDetails(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCode(String)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodeIncludingDetails(String)**

```csharp
public Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodes(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
