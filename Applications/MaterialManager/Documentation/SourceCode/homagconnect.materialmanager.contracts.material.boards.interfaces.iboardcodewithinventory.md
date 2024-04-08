<span style="color:red">[This is preliminary documentation and is subject to change.] </span>
# IBoardCodeWithInventory

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces

Interface for board code with inventory.

```csharp
public interface IBoardCodeWithInventory
```

## Properties

### **BoardCode**

Gets or sets the board code.

```csharp
string BoardCode { get; set; }
```

### **TotalQuantityInInventory**

Gets or sets the total quantity of boards of this type in the inventory.

```csharp
int? TotalQuantityInInventory { get; set; }
```

### **TotalQuantityAllocated**

Gets or sets the total quantity of boards of this type which have been allocated to a production order.

```csharp
int? TotalQuantityAllocated { get; set; }
```

### **TotalQuantityAvailable**

Gets the total quantity of boards of this type which are available in the inventory.

```csharp
int? TotalQuantityAvailable { get; }
```

### **TotalAreaInventory**

Gets or sets the total area of boards of this type in the inventory. The unit depends on the settings of the subscription (metric: m², imperial: ft²).

```csharp
double? TotalAreaInventory { get; }
```

### **TotalAreaAllocated**

Gets or sets the total area of boards of this type which have been allocated to a production order. The unit depends on the settings of the subscription (metric: m², imperial: ft²).

```csharp
double? TotalAreaAllocated { get; }
```

### **TotalAreaAvailable**

Gets or sets the total area of boards of this type which are available in the inventory. The unit depends on the settings of the subscription (metric: m², imperial: ft²).

```csharp
double? TotalAreaAvailable { get; }
```

### **Inventory**

Gets or sets the board type inventory.

```csharp
ICollection<BoardTypeInventory> Inventory { get; set; }
```