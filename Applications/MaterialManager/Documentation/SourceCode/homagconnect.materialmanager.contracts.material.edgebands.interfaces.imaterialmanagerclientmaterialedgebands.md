# IMaterialManagerClientMaterialEdgebands

Namespace: HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces

Interface for MaterialManager Edgebands Client.

```csharp
public interface IMaterialManagerClientMaterialEdgebands
```

## Methods

### **GetEdgebandTypes(Int32, Int32)**

Gets all edgebands.

```csharp
Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypeByEdgebandCode(String)**

Gets an edgeband by edgeband code.

```csharp
Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode)
```

#### Parameters

`edgebandCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;EdgebandType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypeByEdgebandCodeIncludingDetails(String)**

Gets an edgeband by edgeband code including details.

```csharp
Task<EdgebandTypeDetails> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode)
```

#### Parameters

`edgebandCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;EdgebandTypeDetails&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypesByEdgebandCodes(IEnumerable&lt;String&gt;)**

Gets edgebands by edgeband codes.

```csharp
Task<IEnumerable<EdgebandType>> GetEdgebandTypesByEdgebandCodes(IEnumerable<string> edgebandCodes)
```

#### Parameters

`edgebandCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The edgeband types sorted by [EdgebandType.EdgebandCode](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#edgebandcode).

### **GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable&lt;String&gt;)**

Gets edgebands by edgeband codes including details.

```csharp
Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes)
```

#### Parameters

`edgebandCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The edgeband types sorted by [EdgebandType.EdgebandCode](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#edgebandcode).

### **GetEdgebandTypeInventoryHistoryAsync(DateTime, DateTime)**

Get [EdgeInventoryHistory](./homagconnect.materialmanager.contracts.statistics.edgeinventoryhistory.md) inventory history for edgebands[EdgebandType](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md).

```csharp
Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(DateTime from, DateTime to)
```

#### Parameters

`from` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

`to` [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgeInventoryHistory&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
