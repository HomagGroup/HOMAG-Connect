#Class: BoardTypeDetails

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

The board type details.

```csharp
public class BoardTypeDetails : BoardType, IBoardCodeWithInventory
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) -> [BoardType](./homagconnect.materialmanager.contracts.material.boards.boardtype.md) -> [BoardTypeDetails](./homagconnect.materialmanager.contracts.material.boards.boardtypedetails.md) -> [IBoardCodeWithInventory](./homagconnect.materialmanager.contracts.material.boards.interfaces.boardtypedetails.md)
Implements [IExtensibleDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)


## Properties

### **Allocations**

Gets or sets the board type allocations.

```csharp
[JsonProperty(Order = 83)]
public ICollection<BoardTypeAllocation> Allocations { get; set; } = new List<BoardTypeAllocation>();
```

#### Property Value

[ICollection&lt;BoardTypeAllocation&gt;](./homagconnect.materialmanager.contracts.material.boards.boardtypeallocation.md)

### **Images**

Gets or sets the list of additional images.

```csharp
[JsonProperty(Order = 81)]
public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();
```

#### Property Value

[ICollection&lt;ImageInformation&gt;](./homagconnect.materialmanager.contracts.material.base.imageinformation.md)

### **Inventory**

Gets or sets the board type inventory.

```csharp
[JsonProperty(Order = 82)]
public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();
```

#### Property Value

[ICollection&lt;BoardTypeInventory&gt;](./homagconnect.materialmanager.contracts.material.boards.boardtypeinventory.md)

## Constructors
### **BoardTypeDetails()**
Creates a new instance of the BoardTypeDetails class.

```csharp
public BoardTypeDetails()
```