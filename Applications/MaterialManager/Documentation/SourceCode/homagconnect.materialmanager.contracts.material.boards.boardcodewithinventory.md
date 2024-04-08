<span style="color:red">[This is preliminary documentation and is subject to change.] </span>
# BoardCodeWithInventory

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

A board code with inventory.

```csharp
public class BoardCodeWithInventory : IBoardCodeWithInventory, IExtensibleDataObject
```

Inheritance: 

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) -> [BoardCodeWithInventory](./homagconnect.materialmanager.contracts.material.boards.boardcodewithinventory.md)
Implements [IExtensibleDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject), [IBoardCodeWithInventory](./homagconnect.materialmanager.contracts.material.boards.interfaces.iboardcodewithinventory)

## Properties

### **BoardCode**

Gets or sets the board code.

```csharp
[JsonProperty(Order = 1)]
public string BoardCode { get; set; } = string.Empty;
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)

### **TotalQuantityInInventory**

Gets or sets the total quantity of the board in inventory.

```csharp
[JsonProperty(Order = 50)]
public int? TotalQuantityInInventory { get; set; }
```

#### Property Value

[Nullable Int32](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)

### **TotalQuantityAllocated**

Gets or sets the total quantity of the board that has been allocated.

```csharp
[JsonProperty(Order = 51)]
public int? TotalQuantityAllocated { get; set; }
```

#### Property Value

[Nullable Int32](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)

### **TotalQuantityAvailable**

Gets or sets the total quantity of the board that is available.

```csharp
[JsonProperty(Order = 52)]
public int? TotalQuantityAvailable { get; set; }
```

#### Property Value

[Nullable Int32](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)

### **TotalAreaInventory**

Gets the total area of the board in inventory.

```csharp
[JsonProperty(Order = 56)]
public double? TotalAreaInventory { get; }
```

#### Property Value

[Nullable Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)

### **TotalAreaAllocated**

Gets or sets the total area of the board that has been allocated.

```csharp
[JsonProperty(Order = 57)]
public double? TotalAreaAllocated { get; set; }
```

#### Property Value

[Nullable Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)

### **TotalAreaAvailable**

Gets or sets the total area of the board that is available.

```csharp
[JsonProperty(Order = 58)]
public double? TotalAreaAvailable { get; set; }
```

#### Property Value

[Nullable Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)

### **Inventory**

Gets or sets the collection of board type inventory.

```csharp
[JsonProperty(Order = 82)]
public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();
```

#### Property Value

[ICollection&lt;BoardTypeInventory&gt;](HomagConnect.MaterialManager.Contracts.Material.Boards.BoardTypeInventory)

## Constructors

### **BoardCodeWithInventory()**
Creates a new instance of the BoardCodeWithInventory class.

```csharp
public BoardCodeWithInventory()
```
