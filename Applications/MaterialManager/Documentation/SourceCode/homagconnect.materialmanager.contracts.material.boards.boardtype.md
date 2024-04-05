# BoardType Class

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

Represents a type of board material.

```csharp
public class BoardType : IExtensibleDataObject, IContainsUnitSystemDependentProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BoardType](./homagconnect.materialmanager.contracts.material.boards.boardtype.md)
Implements [IExtensibleDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)


## Properties

### **LastUsed**

Gets or sets the timestamp when the board type has been used last.

```csharp
public DateTimeOffset? LastUsed { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)?<br>

### **UnitSystem**

Gets or sets the unit system of the board type.

```csharp
public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
```

#### Property Value

[UnitSystem](./homagconnect.base.contracts.enumerations.unitsystem.md)<br>

### **ExtensionData**

Gets or sets the extension data for the board type.

```csharp
public ExtensionDataObject? ExtensionData { get; set; }
```

#### Property Value

[ExtensionDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.extensiondataobject)?<br>

### **MaterialCode**

Gets or sets the material code of the board type.

```csharp
[Required]
[StringLength(50, MinimumLength = 1)]
[JsonProperty(Order = 10)]
public string MaterialCode { get; set; } = string.Empty;
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Thickness**

Gets or sets the thickness of the board type.

```csharp
[JsonProperty(Order = 24)]
[ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
public double? Thickness { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **MaterialCategory**

Gets or sets the material category of the board type.

```csharp
[JsonProperty(Order = 12)]
public MaterialCategory MaterialCategory { get; set; }
```

#### Property Value

[MaterialCategory](./homagconnect.materialmanager.contracts.material.boards.enumerations.materialcategory.md)<br>

### **CoatingCategory**

Gets or sets the coating category of the board type.

```csharp
[JsonProperty(Order = 13)]
public CoatingCategory CoatingCategory { get; set; }
```

#### Property Value

[CoatingCategory](./homagconnect.materialmanager.contracts.material.boards.enumerations.coatingcategory.md)<br>

### **StandardQuality**

Gets or sets the standard quality of the board type.

```csharp
[JsonProperty(Order = 14)]
public StandardQuality StandardQuality { get; set; }
```

#### Property Value

[StandardQuality](./homagconnect.materialmanager.contracts.material.boards.enumerations.standardquality.md)<br>

### **BoardCode**

Gets or sets the board code of the board type.

```csharp
[Key]
[Required]
[StringLength(50, MinimumLength = 1)]
[JsonProperty(Order = 1)]
public string BoardCode { get; set; } = string.Empty;
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Width**

Gets or sets the width of the board type.

```csharp
[Required]
[Range(0.1, 9999.9)]
[JsonProperty(Order = 22)]
[ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
public double? Width { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **Length**

Gets or sets the length of the board type.

```csharp
[Required]
[Range(0.1, 9999.9)]
[JsonProperty(Order = 23)]
[ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
public double? Length { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **Grain**

Gets or sets the grain of the board type.

```csharp
[JsonProperty(Order = 25)]
public Grain Grain { get; set; }
```

#### Property Value

[Grain](../../../IntelliDivide/Documentation/SourceCode/homagconnect.intellidivide.contracts.base.grain.md)<br>

### **Costs**

Gets or sets the costs of the board type.

```csharp
[JsonProperty(Order = 26)]
public double? Costs { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **BoardTypeType**

Gets or sets the type of the board type.

```csharp
[JsonProperty(Order = 27)]
public BoardTypeType BoardTypeType { get; set; }
```

#### Property Value

[BoardTypeType](./homagconnect.materialmanager.contracts.material.boards.enumerations.boardtypetype.md)<br>

### **ManufacturerName**

Gets or sets the name of the manufacturer of the board type.

```csharp
[JsonProperty(Order = 31)]
public string? ManufacturerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)?<br>

### **ProductName**

Gets or sets the name of the product of the board type.

```csharp
[JsonProperty(Order = 32)]
public string? ProductName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)?<br>

### **ArticleNumber**

Gets or sets the article number of the board type.

```csharp
[JsonProperty(Order = 33)]
public string? ArticleNumber { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)?<br>

### **DecorCode**

Gets or sets the decor code of the board type.

```csharp
[JsonProperty(Order = 34)]
public string? DecorCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)?<br>

### **DecorName**

Gets or sets the decor name of the board type.

```csharp
[JsonProperty(Order = 35)]
public string? DecorName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)?<br>

### **TotalQuantityAvailableWarningLimit**

Gets or sets the total quantity available warning limit of the board type.

```csharp
[JsonProperty(Order = 53)]
public int? TotalQuantityAvailableWarningLimit { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)?<br>

### **OptimizeAgainstInfinite**

Gets or sets a value indicating whether the board type should be optimized against infinite.

```csharp
[JsonProperty(Order = 92)]
public bool OptimizeAgainstInfinite { get; set; } = true;
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **LockedForOptimization**

Gets or sets a value indicating whether the board type is locked for optimization.

```csharp
[JsonProperty(Order = 93)]
public bool LockedForOptimization { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TotalQuantityInInventory**

Gets or sets the total quantity of boards of this type in the inventory.

```csharp
[JsonProperty(Order = 50)]
public int? TotalQuantityInInventory { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)?<br>

### **TotalQuantityAllocated**

Gets or sets the total quantity of boards of this type which have been allocated to a production order.

```csharp
[JsonProperty(Order = 51)]
public int? TotalQuantityAllocated { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)?<br>

### **TotalQuantityAvailable**

Gets or sets the total quantity of boards of this type which are available in the inventory.

```csharp
[JsonProperty(Order = 52)]
public int? TotalQuantityAvailable { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)?<br>

### **TotalAreaInventory**

Gets or sets the total area of boards of this type in the inventory.

```csharp
[JsonProperty(Order = 56)]
public double? TotalAreaInventory { get; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **TotalAreaAllocated**

Gets or sets the total area of boards of this type which have been allocated to a production order.

```csharp
[JsonProperty(Order = 57)]
public double? TotalAreaAllocated { get; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **TotalAreaAvailable**

Gets or sets the total area of boards of this type which are available in the inventory.

```csharp
[JsonProperty(Order = 58)]
public double? TotalAreaAvailable { get; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)?<br>

### **TotalQuantityAvailableWarningLimitReached**

Gets or sets an indication whether the `TotalQuantityAvailable` is below the defined limit `TotalQuantityAvailableWarningLimit`.

```csharp
[JsonProperty(Order = 54)]
public bool? TotalQuantityAvailableWarningLimitReached { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)?<br>

### **Comments**

Gets or sets any additional comments for the board type.

```csharp
[StringLength(300)]
[JsonProperty(Order = 80)]
public string Comments { get; set; } = string.Empty;
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Thumbnail**

Gets or sets the thumbnail URI.

```csharp
[JsonProperty(Order = 3)]
public Uri? Thumbnail { get; set; }
```

#### Property Value

[Uri](https://docs.microsoft.com/en-us/dotnet/api/system.uri)?<br>

## Interfaces

### IExtensibleDataObject

Represents a data contract extension object that is used to store data that is not recognized as belonging to the data contract.

```csharp
public ExtensionDataObject? ExtensionData { get; set; }
```

### IContainsUnitSystemDependentProperties

Provides methods and properties to get and set the unit system of dependant properties.

```csharp
public UnitSystem UnitSystem { get; set; }
```

