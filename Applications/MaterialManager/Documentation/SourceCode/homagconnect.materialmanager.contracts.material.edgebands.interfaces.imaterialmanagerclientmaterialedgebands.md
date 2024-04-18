# IMaterialManagerClientMaterialEdgebands

Namespace: HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces

```csharp
public interface IMaterialManagerClientMaterialEdgebands
```

## Methods

### **GetEdgebandTypes(Int32, Int32)**

Gets all edgebands

```csharp
Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip)
```

#### Parameters

`take` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`skip` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypeByEdgebandCode(String)**

Gets an edgeband by edgeband code

```csharp
Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode)
```

#### Parameters

`edgebandCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;EdgebandType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypeByEdgebandCodeIncludingDetails(String)**

Gets an edgeband by edgeband code including details

```csharp
Task<EdgebandType> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode)
```

#### Parameters

`edgebandCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;EdgebandType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetEdgebandTypesByMaterialCodes(IEnumerable&lt;String&gt;)**

Gets edgebands by edgeband codes

```csharp
Task<IEnumerable<EdgebandType>> GetEdgebandTypesByMaterialCodes(IEnumerable<string> edgebandCodes)
```

#### Parameters

`edgebandCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandType&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The edgeband types sorted by [EdgebandType.EdgebandCode](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#edgebandcode).

### **GetEdgebandTypesByMaterialCodesIncludingDetails(IEnumerable&lt;String&gt;)**

Gets edgebands by edgeband codes including details

```csharp
Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByMaterialCodesIncludingDetails(IEnumerable<string> edgebandCodes)
```

#### Parameters

`edgebandCodes` [IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;EdgebandTypeDetails&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The edgeband types sorted by [EdgebandType.EdgebandCode](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#edgebandcode).
