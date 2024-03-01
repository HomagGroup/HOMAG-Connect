# MaterialEfficiency

Namespace: HomagConnect.IntelliDivide.Contracts.Statistics

Provides the material efficiency data for a material within an optimization.

```csharp
public class MaterialEfficiency
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MaterialEfficiency](./homagconnect.intellidivide.contracts.statistics.materialefficiency.md)

## Properties

### **MachineName**

Gets or sets the name of the machine.

```csharp
public string MachineName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MaterialCode**

Gets or sets the material code.

```csharp
public string MaterialCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OptimizationId**

Gets or sets the id of the optimization.

```csharp
public string OptimizationId { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OptimizationName**

Gets or sets the name of the optimization.

```csharp
public string OptimizationName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TransferredAt**

Gets or sets the datetime when the optimization was transferred.

```csharp
public DateTimeOffset TransferredAt { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **BoardsUsed**

Gets or sets the area of boards used in m² (or ft² in subscriptions using the imperial unit system).

```csharp
public double BoardsUsed { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **BoardsUsedPercentage**

Gets or sets the area of boards used in %.

```csharp
public double BoardsUsedPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OffcutsUsed**

Gets or sets the area of offcuts used in m² (or ft² in subscriptions using the imperial unit system).

```csharp
public double OffcutsUsed { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OffcutsUsedPercentage**

Gets or sets the area of offcuts used in %.

```csharp
public double OffcutsUsedPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Parts**

Gets or sets the area of parts produced in m² (or ft² in subscriptions using the imperial unit system).

```csharp
public double Parts { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **PartsPercentage**

Gets or sets the area of parts produced in %.

```csharp
public double PartsPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OffcutsProduced**

Gets or sets the area of offcuts produced in m² (or ft² in subscriptions using the imperial unit system).

```csharp
public double OffcutsProduced { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OffcutsProducedPercentage**

Gets or sets the area of offcuts produced in %.

```csharp
public double OffcutsProducedPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OffcutsGrowth**

Gets or sets the area of offcuts produced - offcuts used used in m² (or ft² in subscriptions using the imperial unit
 system).

```csharp
public double OffcutsGrowth { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **OffcutsGrowthPercentage**

Gets or sets the area of offcuts produced - offcuts used used in %.

```csharp
public double OffcutsGrowthPercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Waste**

Gets or sets the area of waste produced in m² (or ft² in subscriptions using the imperial unit system).

```csharp
public double Waste { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **WastePercentage**

Gets or sets the area of waste produced in %.

```csharp
public double WastePercentage { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### **MaterialEfficiency()**

```csharp
public MaterialEfficiency()
```
