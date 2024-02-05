# SolutionDetails

Namespace: HomagConnect.IntelliDivide.Contracts.Result

```csharp
public class SolutionDetails : Solution, System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Solution](./homagconnect.intellidivide.contracts.result.solution.md) → [SolutionDetails](./homagconnect.intellidivide.contracts.result.solutiondetails.md)<br>
Implements IExtensibleDataObject

## Properties

### **Exports**

```csharp
public IReadOnlyCollection<SolutionExportType> Exports { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionExportType&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **Material**

```csharp
public SolutionMaterial Material { get; set; }
```

#### Property Value

[SolutionMaterial](./homagconnect.intellidivide.contracts.result.solutionmaterial.md)<br>

### **Parts**

```csharp
public IReadOnlyCollection<SolutionPart> Parts { get; set; }
```

#### Property Value

[IReadOnlyCollection&lt;SolutionPart&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)<br>

### **Id**

```csharp
public Guid Id { get; set; }
```

#### Property Value

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **Name**

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OptimizationId**

```csharp
public Guid OptimizationId { get; set; }
```

#### Property Value

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **Overview**

```csharp
public SolutionOverview Overview { get; set; }
```

#### Property Value

[SolutionOverview](./homagconnect.intellidivide.contracts.result.solutionoverview.md)<br>

### **TotalScore**

```csharp
public double TotalScore { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **SolutionDetails()**

```csharp
public SolutionDetails()
```
