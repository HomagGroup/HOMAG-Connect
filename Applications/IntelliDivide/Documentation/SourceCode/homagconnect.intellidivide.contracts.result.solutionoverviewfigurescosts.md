# SolutionOverviewFiguresCosts

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Provides the overview figures for costs.

```csharp
public class SolutionOverviewFiguresCosts : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SolutionOverviewFiguresCosts](./homagconnect.intellidivide.contracts.result.solutionoverviewfigurescosts.md)<br>
Implements IExtensibleDataObject

## Properties

### **CostsOfBoardsPlusOffcuts**

Gets the costs of boards and offcuts in the currency of the subscription.

```csharp
public Nullable<double> CostsOfBoardsPlusOffcuts { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **CostsOfEdgebands**

Gets the costs of edgebands in the currency of the subscription.

```csharp
public Nullable<double> CostsOfEdgebands { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MaterialCosts**

Gets the total material costs in the currency of the subscription.

```csharp
public Nullable<double> MaterialCosts { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MaterialCostsPerPart**

Gets the average material costs per part in the currency of the subscription.

```csharp
public Nullable<double> MaterialCostsPerPart { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **SolutionOverviewFiguresCosts()**

```csharp
public SolutionOverviewFiguresCosts()
```
