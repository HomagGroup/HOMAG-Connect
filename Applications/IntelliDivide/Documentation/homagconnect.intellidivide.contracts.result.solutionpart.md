# SolutionPart

Namespace: HomagConnect.IntelliDivide.Contracts.Result

Provides access to part properties.

```csharp
public class SolutionPart : HomagConnect.IntelliDivide.Contracts.Common.OptimizationBasePart, System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationBasePart](./homagconnect.intellidivide.contracts.common.optimizationbasepart.md) → [SolutionPart](./homagconnect.intellidivide.contracts.result.solutionpart.md)<br>
Implements IExtensibleDataObject

## Properties

### **Preview**

Gets a link to a preview image of the part.

```csharp
public Uri Preview { get; set; }
```

#### Property Value

Uri<br>

### **Quantity**

Gets the quantity of parts.

```csharp
public int Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **QuantityPlus**

Gets the quantity of plus parts.

```csharp
public int QuantityPlus { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **QuantityTotal**

Gets the total quantity of parts.

```csharp
public int QuantityTotal { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **CncProgramName1**

```csharp
public string CncProgramName1 { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CncProgramName2**

```csharp
public string CncProgramName2 { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CustomerName**

```csharp
public string CustomerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Description**

```csharp
public string Description { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeBack**

```csharp
public string EdgeBack { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeDiagram**

```csharp
public string EdgeDiagram { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeFront**

```csharp
public string EdgeFront { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeLeft**

```csharp
public string EdgeLeft { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeRight**

```csharp
public string EdgeRight { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FinishLength**

```csharp
public Nullable<double> FinishLength { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **FinishWidth**

```csharp
public Nullable<double> FinishWidth { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Grain**

```csharp
public Grain Grain { get; set; }
```

#### Property Value

[Grain](./homagconnect.intellidivide.contracts.common.grain.md)<br>

### **LaminateBottom**

```csharp
public string LaminateBottom { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LaminateTop**

```csharp
public string LaminateTop { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Length**

```csharp
public double Length { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **MaterialCode**

```csharp
public string MaterialCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OrderName**

```csharp
public string OrderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Width**

```csharp
public double Width { get; set; }
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

### **SolutionPart()**

```csharp
public SolutionPart()
```
