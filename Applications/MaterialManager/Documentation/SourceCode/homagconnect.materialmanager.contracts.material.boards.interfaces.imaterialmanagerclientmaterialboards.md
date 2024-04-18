# IMaterialManagerClientMaterialBoards

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces

materialMaterial Client for boards

```csharp
public interface IMaterialManagerClientMaterialBoards
```

## Methods

### **GetBoardTypes(Int32, Int32)**

Gets the board types paginated

```csharp
Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown, if take is greater than 1000.

### **GetBoardTypeByBoardCode(String)**

Gets the board type by board code.

```csharp
Task<BoardType> GetBoardTypeByBoardCode(string boardCode)
```

#### Parameters

`boardCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;BoardType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypeByBoardCodeIncludingDetails(String)**

Gets the board type by board code including details (inventory, allocation, images).

```csharp
Task<BoardType> GetBoardTypeByBoardCodeIncludingDetails(string boardCode)
```

#### Parameters

`boardCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;BoardType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetBoardTypesByBoardCodes(IEnumerable&lt;String&gt;)**

Gets the board types by board codes.

```csharp
Task<IEnumerable<BoardType>> GetBoardTypesByBoardCodes(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#materialcode) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#boardcode).

### **GetBoardTypesByMaterialCode(String)**

Gets the board types by material code.

```csharp
Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#boardcode).

### **GetBoardTypesByMaterialCodeIncludingDetails(String)**

Gets the board types by material code including details (inventory, allocation, images).

```csharp
Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#boardcode).

### **GetBoardTypesByMaterialCodes(IEnumerable&lt;String&gt;)**

Gets the board types by material codes.

```csharp
Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#materialcode) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#boardcode).

### **GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable&lt;String&gt;)**

Gets the board types by material codes including details (inventory, allocation, images).

```csharp
Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
```

#### Parameters

`materialCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#materialcode) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#boardcode).

### **GetBoardTypesByBoardCodesIncludingDetails(IEnumerable&lt;String&gt;)**

Gets the board types by board codes including details (inventory, allocation, images).

```csharp
Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes)
```

#### Parameters

`boardCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;BoardTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The board types sorted by [BoardType.MaterialCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#materialcode) and [BoardType.BoardCode](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#boardcode).
