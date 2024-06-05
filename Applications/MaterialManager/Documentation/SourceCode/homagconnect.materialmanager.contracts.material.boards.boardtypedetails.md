# BoardTypeDetails

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

The board type details.

```csharp
public class BoardTypeDetails : BoardType, System.Runtime.Serialization.IExtensibleDataObject, HomagConnect.Base.Contracts.Interfaces.IContainsUnitSystemDependentProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BoardType](./homagconnect.materialmanager.contracts.material.boards.boardtype.md) → [BoardTypeDetails](./homagconnect.materialmanager.contracts.material.boards.boardtypedetails.md)<br>
Implements IExtensibleDataObject, IContainsUnitSystemDependentProperties

## Properties

### **Allocations**

Gets or sets the board type allocations.

```csharp
public ICollection<BoardTypeAllocation> Allocations { get; set; }
```

#### Property Value

[ICollection&lt;BoardTypeAllocation&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

### **Images**

Gets or sets the list of additional images.

```csharp
public ICollection<ImageInformation> Images { get; set; }
```

#### Property Value

[ICollection&lt;ImageInformation&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

### **Inventory**

Gets or sets the board type inventory.

```csharp
public ICollection<BoardTypeInventory> Inventory { get; set; }
```

#### Property Value

[ICollection&lt;BoardTypeInventory&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

### **LastUsed**

Gets or sets the timestamp when board type has been used last.

```csharp
public Nullable<DateTimeOffset> LastUsed { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **UnitSystem**

```csharp
public UnitSystem UnitSystem { get; set; }
```

#### Property Value

UnitSystem<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

### **MaterialCode**

Gets or sets the material code.

```csharp
public string MaterialCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Thickness**

Gets or sets the thickness.

```csharp
public Nullable<double> Thickness { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MaterialCategory**

Gets or sets the material category name

```csharp
public BoardMaterialCategory MaterialCategory { get; set; }
```

#### Property Value

[BoardMaterialCategory](./homagconnect.materialmanager.contracts.material.boards.enumerations.boardmaterialcategory.md)<br>

### **CoatingCategory**

Gets or sets the coating category.

```csharp
public CoatingCategory CoatingCategory { get; set; }
```

#### Property Value

[CoatingCategory](./homagconnect.materialmanager.contracts.material.boards.enumerations.coatingcategory.md)<br>

### **StandardQuality**

Gets or set the standard quality.

```csharp
public StandardQuality StandardQuality { get; set; }
```

#### Property Value

[StandardQuality](./homagconnect.materialmanager.contracts.material.boards.enumerations.standardquality.md)<br>

### **BoardCode**

Gets or sets the board code.

```csharp
public string BoardCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Width**

Gets or sets the width of the board. The unit depends on the settings of the subscription (metric: mm, imperial: inch).

```csharp
public Nullable<double> Width { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Length**

Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
 inch).

```csharp
public Nullable<double> Length { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Grain**

Gets or set the grain of the board.

```csharp
public Grain Grain { get; set; }
```

#### Property Value

Grain<br>

### **Costs**

Gets or sets the costs of the board. The unit depends on the settings of the subscription.

```csharp
public Nullable<double> Costs { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **BoardTypeType**

Gets or sets the type of the board.

```csharp
public BoardTypeType BoardTypeType { get; set; }
```

#### Property Value

BoardTypeType<br>

### **ManufacturerName**

Gets or sets the name of the manufacturer.

```csharp
public string ManufacturerName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ProductName**

Gets or sets the name of the product.

```csharp
public string ProductName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ArticleNumber**

Gets or sets the article number.

```csharp
public string ArticleNumber { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DecorCode**

Gets or sets the decor code.

```csharp
public string DecorCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **DecorName**

Gets or sets the decor name.

```csharp
public string DecorName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TotalQuantityAvailableWarningLimit**

Gets or sets the total quantity available warning limit.

```csharp
public Nullable<int> TotalQuantityAvailableWarningLimit { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalAreaAvailableWarningLimit**

Gets or sets the total area available warning limit.

```csharp
public Nullable<double> TotalAreaAvailableWarningLimit { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **OptimizeAgainstInfinite**

Gets or sets whether the board type should be optimized against infinite.

```csharp
public bool OptimizeAgainstInfinite { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **LockedForOptimization**

Gets or sets whether the board type is locked for optimization.

```csharp
public bool LockedForOptimization { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TotalQuantityInInventory**

Gets or sets the total quantity of boards of this type in the inventory.

```csharp
public Nullable<int> TotalQuantityInInventory { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalQuantityAllocated**

Gets or sets the total quantity of boards of this type which have been allocated to a production order.

```csharp
public Nullable<int> TotalQuantityAllocated { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalQuantityAvailable**

Gets or sets the total quantity of boards of this type which are available in the inventory.

```csharp
public Nullable<int> TotalQuantityAvailable { get; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalAreaInInventory**

Gets or sets the total area of boards of this type in the inventory. The unit depends on the settings of the
 subscription (metric: m², imperial: ft²).

```csharp
public Nullable<double> TotalAreaInInventory { get; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalAreaAllocated**

Gets or sets the total area of boards of this type which have been allocated to a production order. The unit depends on
 the settings of the subscription (metric: m², imperial: ft²).

```csharp
public Nullable<double> TotalAreaAllocated { get; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalAreaAvailable**

Gets or sets the total area of boards of this type which are available in the inventory. The unit depends on the
 settings of the subscription (metric: m², imperial: ft²).

```csharp
public Nullable<double> TotalAreaAvailable { get; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **InsufficientInventory**

Gets or sets a indication whether the [BoardType.TotalQuantityAvailable](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#totalquantityavailable) is below the defined limit
 [BoardType.TotalQuantityAvailableWarningLimit](./homagconnect.materialmanager.contracts.material.boards.boardtype.md#totalquantityavailablewarninglimit).

```csharp
public Nullable<bool> InsufficientInventory { get; set; }
```

#### Property Value

[Nullable&lt;Boolean&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Comments**

Gets or sets the additional comments.

```csharp
public string Comments { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Thumbnail**

Gets or set the thumbnail uri.

```csharp
public Uri Thumbnail { get; set; }
```

#### Property Value

Uri<br>

## Constructors

### **BoardTypeDetails()**

```csharp
public BoardTypeDetails()
```
