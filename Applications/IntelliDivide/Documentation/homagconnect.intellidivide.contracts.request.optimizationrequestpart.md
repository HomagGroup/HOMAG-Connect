# OptimizationRequestPart

Namespace: HomagConnect.IntelliDivide.Contracts.Request

Describes a part which should get optimized.

```csharp
public class OptimizationRequestPart : HomagConnect.IntelliDivide.Contracts.Common.OptimizationBasePart, System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationBasePart](./homagconnect.intellidivide.contracts.common.optimizationbasepart.md) → [OptimizationRequestPart](./homagconnect.intellidivide.contracts.request.optimizationrequestpart.md)<br>
Implements IExtensibleDataObject

## Properties

### **Template**

Gets or sets the name and position with in a grain matching template.

```csharp
public string Template { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Quantity**

Gets or sets the quantity how often the part is needed.

```csharp
public int Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **QuantityPlus**

Gets or sets the quantity how often the part can get produced optional.

```csharp
public int QuantityPlus { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MprFileName**

Gets or sets the name of the mpr file which describes the nesting contour.

```csharp
public string MprFileName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **MprProgramVariables**

Gets or sets the mpr program variable values.

```csharp
public List<MprProgramVariable> MprProgramVariables { get; set; }
```

#### Property Value

[List&lt;MprProgramVariable&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **AllowedRotationAngle**

Gets or sets the allowed rotation angle.

```csharp
public Nullable<RotationAngle> AllowedRotationAngle { get; set; }
```

#### Property Value

[Nullable&lt;RotationAngle&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

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

Gets or sets the [OptimizationBasePart.Grain](./homagconnect.intellidivide.contracts.common.optimizationbasepart.md#grain) of the part.

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

### **OptimizationRequestPart()**

```csharp
public OptimizationRequestPart()
```
