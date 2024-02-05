# Solution

Namespace: HomagConnect.IntelliDivide.Contracts.Result

```csharp
public class Solution : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Solution](./homagconnect.intellidivide.contracts.result.solution.md)<br>
Implements IExtensibleDataObject

## Properties

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

### **Solution()**

```csharp
public Solution()
```
