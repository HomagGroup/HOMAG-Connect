# Solution

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Represents a solution of a cutting or nesting optimization.

```csharp
public class Solution : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Solution](./homagconnect.intellidivide.contracts.result.solution.md)<br>
Implements IExtensibleDataObject

## Properties

### **Id**

Gets or sets the unique identifier of the solution.

```csharp
public Guid Id { get; set; }
```

#### Property Value

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **Name**

Gets or sets the name of the solution. See [SolutionName](./homagconnect.intellidivide.contracts.constants.solutionname.md) for more details.

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OptimizationId**

Gets or sets the optimization id.

```csharp
public Guid OptimizationId { get; set; }
```

#### Property Value

[Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

### **Overview**

Gets or sets the [SolutionOverview](./homagconnect.intellidivide.contracts.result.solutionoverview.md).

```csharp
public SolutionOverview Overview { get; set; }
```

#### Property Value

[SolutionOverview](./homagconnect.intellidivide.contracts.result.solutionoverview.md)<br>

### **TotalScore**

Gets or sets the total score of the solution. The [SolutionName.BalancedSolution](./homagconnect.intellidivide.contracts.constants.solutionname.md#balancedsolution) has typically the
 highest score. The solutions are listed in the app sorted by the score (highest first).

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
