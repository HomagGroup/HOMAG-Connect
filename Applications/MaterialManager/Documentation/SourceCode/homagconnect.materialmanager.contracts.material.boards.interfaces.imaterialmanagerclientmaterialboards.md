<span style="color:red">[This is preliminary documentation and is subject to change.] </span>
# IMaterialManagerClientMaterialBoards

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces

materialMaterial client interface for boards.

```csharp
public interface IMaterialManagerClientMaterialBoards
```

## Methods

### **GetBoardType(string)**

Gets the board type by board code.

```csharp
Task<BoardType> GetBoardType(string boardCode)
```

#### Parameters

`boardCode` [string](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;BoardType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypeInventory(IEnumerable&lt;string&gt;)**

Gets the board type inventory by board codes.

```csharp
Task<IEnumerable<BoardCodeWithInventory>> GetBoardTypeInventory(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardCodeWithInventory&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardCodeWithInventory.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardcodewithinventory.cs#properties).

### **GetBoardTypes(int, int)**

Gets the board types paginated

```csharp
Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip = 0)
```

#### Parameters

`take` [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown, if take is greater than 1000.

### **GetBoardTypes(IEnumerable&lt;string&gt;)**

Gets the board types by board codes.

```csharp
Task<IEnumerable<BoardType>> GetBoardTypes(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties).

### **GetBoardTypesByMaterialCode(string)**

Gets the board types by material code.

```csharp
Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode)
```

#### Parameters

`materialCode` [string](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties).

### **GetBoardTypesByMaterialCodeIncludingDetails(string)**

Gets the board types by material code including details (inventory, allocation, images).

```csharp
Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
```

#### Parameters

`materialCode` [string](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties).

### **GetBoardTypesByMaterialCodes(IEnumerable&lt;string&gt;)**

Gets the board types by material codes.

```csharp
Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties).

### **GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable&lt;string&gt;)**

Gets the board types by material codes including details (inventory, allocation, images).

```csharp
Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties).

### **GetBoardTypesIncludingDetails(IEnumerable&lt;string&gt;)**

Gets the board types  by board codes including details (inventory, allocation, images).

```csharp
Task<IEnumerable<BoardTypeDetails>> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;string&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardequivalent.csproj/boardtype.cs#properties).

### **GetMaterialCodes(string, int, int)**

Gets material codes including thumbnail paginated.

```csharp
Task<IEnumerable<MaterialCodeWithThumbnail>> GetMaterialCodes(string search, int take, int skip = 0)
```

#### Parameters

`search` [string](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Part of the material code

`take` [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
How many items to take.

`skip` [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
How many items to skip.

#### Returns

[Task&lt;IEnumerable&lt;MaterialCodeWithThumbnail&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
List of [MaterialCodeWithThumbnail](./homagconnect.materialmanager.contracts.material.boardequivalent.csproj/materialcodewiththumbnail.cs#properties) sorted by [MaterialCodeWithThumbnail.MaterialCode](./homagconnect.materialmanager.contracts.material.boardequivalent.csproj/materialcodewiththumbnail.cs#properties)

### **GetMaterialCodes(int, int)**

Gets material codes including thumbnail paginated.

```csharp
Task<IEnumerable<MaterialCodeWithThumbnail>> GetMaterialCodes(int take, int skip = 0)
```

#### Parameters

`take` [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
How many items to take.

`skip` [int](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
How many items to skip.

#### Returns

[Task&lt;IEnumerable&lt;MaterialCodeWithThumbnail&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
List of [MaterialCodeWithThumbnail](./homagconnect.materialmanager.contracts.material.boardequivalent.csproj/materialcodewiththumbnail.cs#properties) sorted by [MaterialCodeWithThumbnail.MaterialCode](./homagconnect.materialmanager.contracts.material.boardequivalent.csproj/materialcodewiththumbnail.cs#properties)