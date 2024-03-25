# OptimizationBasePart

Namespace: HomagConnect.IntelliDivide.Contracts.Common

Describes a part in context of an intelliDivide optimization.

```csharp
public class OptimizationBasePart : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationBasePart](./homagconnect.intellidivide.contracts.common.optimizationbasepart.md)<br>
Implements IExtensibleDataObject

## Properties

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

### **Description**

Gets or sets a description for the part.

```csharp
public string Description { get; set; }
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

### **Grain**

Gets or sets the [Grain](./homagconnect.intellidivide.contracts.common.grain.md) of the part.

```csharp
public Grain Grain { get; set; }
```

#### Property Value

[Grain](./homagconnect.intellidivide.contracts.common.grain.md)<br>

### **Length**

Gets or sets the length of the part.

```csharp
public double Length { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Width**

Gets or sets the width of the part.

```csharp
public double Width { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **EdgeFront**

Gets or sets the edgeband code of the edgeband type which should get applied on the front.

```csharp
public string EdgeFront { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeBack**

Gets or sets the edgeband code of the edgeband type which should get applied on the back.

```csharp
public string EdgeBack { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeLeft**

Gets or sets the edgeband code of the edgeband type which should get applied on the left.

```csharp
public string EdgeLeft { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeRight**

Gets or sets the edgeband code of the edgeband type which should get applied on the right.

```csharp
public string EdgeRight { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **EdgeDiagram**

Gets or sets how the edgebands should get applied.

```csharp
public string EdgeDiagram { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CncProgramName1**

Gets or sets the program name of the CNC program to execute.

```csharp
public string CncProgramName1 { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CncProgramName2**

Gets or sets the program name of the CNC program to execute.

```csharp
public string CncProgramName2 { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LaminateTop**

Gets or sets the material code of the laminate type which should get applied on the top.

```csharp
public string LaminateTop { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LaminateBottom**

Gets or sets the material code of the laminate type which should get applied on the bottom.

```csharp
public string LaminateBottom { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CustomerName**

Gets or sets the name of the customer who has ordered the part.

```csharp
public string CustomerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **OrderName**

Gets or sets the name of the order.

```csharp
public string OrderName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FinishLength**

Gets or sets the finish length.

```csharp
public Nullable<double> FinishLength { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **FinishWidth**

Gets or sets the finish length.

```csharp
public Nullable<double> FinishWidth { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

## Constructors

### **OptimizationBasePart()**

```csharp
public OptimizationBasePart()
```
