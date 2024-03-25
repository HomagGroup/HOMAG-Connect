# OptimizationRequestBoard

Namespace: HomagConnect.IntelliDivide.Contracts.Request

Represents a board to use in an optimization.

```csharp
public class OptimizationRequestBoard : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [OptimizationRequestBoard](./homagconnect.intellidivide.contracts.request.optimizationrequestboard.md)<br>
Implements IExtensibleDataObject

## Properties

### **BoardCode**

Gets or sets the board code.

```csharp
public string BoardCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Costs**

Gets or sets the costs of the board per m² or ft².

```csharp
public double Costs { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Grain**

Gets or sets the grain of the board.

```csharp
public Grain Grain { get; set; }
```

#### Property Value

[Grain](./homagconnect.intellidivide.contracts.common.grain.md)<br>

### **Length**

Gets or sets the length of the board.

```csharp
public double Length { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **MaterialCode**

Gets or sets the material code.

```csharp
public string MaterialCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Quantity**

Gets or sets the name of the board.

```csharp
public int Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Thickness**

Gets or sets the thickness of the board.

```csharp
public double Thickness { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Type**

Gets or sets the type of the board.

```csharp
public OptimizationBoardType Type { get; set; }
```

#### Property Value

[OptimizationBoardType](./homagconnect.intellidivide.contracts.common.optimizationboardtype.md)<br>

### **Width**

Gets or sets the length of the board.

```csharp
public double Width { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **ExtensionData**

members.

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **OptimizationRequestBoard()**

```csharp
public OptimizationRequestBoard()
```
