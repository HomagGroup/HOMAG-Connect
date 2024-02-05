# SolutionOverviewFiguresProduction

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Provides the overview figures for production.

```csharp
public class SolutionOverviewFiguresProduction : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [SolutionOverviewFiguresProduction](./homagconnect.intellidivide.contracts.result.solutionoverviewfiguresproduction.md)<br>
Implements IExtensibleDataObject

## Properties

### **AverageBookHeight**

Gets the average book height in mm or inch.

```csharp
public double AverageBookHeight { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Cycles**

Gets the quantity of cutting cycles.

```csharp
public int Cycles { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ProductionTime**

Gets the estimated production time.

```csharp
public TimeSpan ProductionTime { get; set; }
```

#### Property Value

[TimeSpan](https://docs.microsoft.com/en-us/dotnet/api/system.timespan)<br>

### **ProductionTimePerPart**

Gets the average production time per part in seconds.

```csharp
public double ProductionTimePerPart { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **QuantityOfParts**

Gets the quantity of parts.

```csharp
public int QuantityOfParts { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **QuantityOfPlusParts**

Gets the quantity of plus parts (optional parts).

```csharp
public int QuantityOfPlusParts { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ExtensionData**



```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **SolutionOverviewFiguresProduction()**

```csharp
public SolutionOverviewFiguresProduction()
```
