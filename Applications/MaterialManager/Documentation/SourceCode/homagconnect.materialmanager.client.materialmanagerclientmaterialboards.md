# MaterialManagerClientMaterialBoards

Namespace: HomagConnect.MaterialManager.Client

```csharp
public class MaterialManagerClientMaterialBoards : HomagConnect.Base.Services.ServiceBase, HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces.IMaterialManagerClientMaterialBoards
```

Inheritance  [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → ServiceBase → [MaterialManagerClientMaterialBoards](./homagconnect.materialmanager.client.materialboards.md)<br>
Implements [IMaterialManagerClientMaterialBoards](./homagconnect.materialmanager.contracts.material.boards.interfaces.imaterialmanagerclientmaterialboards.md)

## Constants

### **_BaseRoute**

```csharp
private const string _BaseRoute = "api/materialManager/materials/boards";
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **_InventoryRoute**

```csharp
private const string _InventoryRoute = "/inventory";
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **_MaterialCodesRoute**

```csharp
private const string _MaterialCodesRoute = "/materialCodes";
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **_MaterialCode**

```csharp
private const string _MaterialCode = "materialCode";
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **_BoardCode**

```csharp
private const string _BoardCode = "boardCode";
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **_IncludingDetails**

```csharp
private const string _IncludingDetails = "includingDetails";
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Constructors

### **MaterialManagerClientMaterialBoards(HttpClient)**

```csharp
public MaterialManagerClientMaterialBoards(HttpClient client)
```

#### Parameters

`client` HttpClient<br>

## Methods

### **GetBoardTypeAsync(String)**

```csharp
public Task<BoardType> GetBoardType(string boardCode)
```

#### Parameters

`boardCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;BoardType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypeInventoryAsync(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardCodeWithInventory>> GetBoardTypeInventory(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` IEnumerable&lt;String&gt;<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardCodeWithInventory&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesAsync(Int32, Int32)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip = 0)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesAsync(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypes(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` IEnumerable&lt;String&gt;<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodeAsync(String)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodeIncludingDetailsAsync(String)**

```csharp
public Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodesAsync(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` IEnumerable&lt;String&gt;<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByMaterialCodesIncludingDetailsAsync(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` IEnumerable&lt;String&gt;<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesIncludingDetailsAsync(IEnumerable&lt;String&gt;)**

```csharp
public Task<IEnumerable<BoardTypeDetails>> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` IEnumerable&lt;String&gt;<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMaterialCodesAsync(String, Int32, Int32)**

```csharp
public Task<IEnumerable<MaterialCodeWithThumbnail>> GetMaterialCodes(string search, int take, int skip = 0)
```

#### Parameters

`search` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;MaterialCodeWithThumbnail&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetMaterialCodesAsync(Int32, Int32)**

```csharp
public Task<IEnumerable<MaterialCodeWithThumbnail>> GetMaterialCodes(int take, int skip = 0)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;MaterialCodeWithThumbnail&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **CreateUrls(IEnumerable&lt;String&gt;, String, String, Boolean)**

```csharp
private static List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "",
        bool includingDetails = false)
```

#### Parameters

`codes` IEnumerable&lt;String&gt;<br>

`searchCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`route` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`includingDetails` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[List&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>