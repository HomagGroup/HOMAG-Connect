# SolutionOverview

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Provides the overview data.

```csharp
public class SolutionOverview : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SolutionOverview](./homagconnect.intellidivide.contracts.result.solutionoverview.md)<br>
Implements IExtensibleDataObject

## Properties

### **Figures**

Provides the overview figures.

```csharp
public SolutionOverviewFigures Figures { get; set; }
```

#### Property Value

[SolutionOverviewFigures](./homagconnect.intellidivide.contracts.result.solutionoverviewfigures.md)<br>

### **Pattern**

Gets the list of patterns.

```csharp
public IReadOnlyCollection<SolutionPattern> Pattern { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionPattern&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **ExtensionData**



```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **SolutionOverview()**

```csharp
public SolutionOverview()
```
