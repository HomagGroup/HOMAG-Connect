# IMaterialManagerClientProcessingOptimization

Namespace: HomagConnect.MaterialManager.Contracts.Processing.Interfaces

Interface for materialManager Client for processing optimization

```csharp
public interface IMaterialManagerClientProcessingOptimization
```

## Methods

### **GetOffcutParameterSetAsync(String)**

Get offcut parameter set for the given material code

```csharp
Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode)
```

#### Parameters

`materialCode` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Task&lt;OffcutParameterSet&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **GetOffcutParameterSetsAsync(ICollection&lt;String&gt;)**

Get offcut parameter sets for the given material codes

```csharp
Task<IEnumerable<OffcutParameterSet>> GetOffcutParameterSetsAsync(ICollection<string> materialCodes)
```

#### Parameters

`materialCodes` [ICollection&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

#### Returns

[Task&lt;IEnumerable&lt;OffcutParameterSet&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
