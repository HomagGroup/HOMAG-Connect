# BoardTypeInventory

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

A board type inventory.

```csharp
public class BoardTypeInventory : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BoardTypeInventory](./homagconnect.materialmanager.contracts.material.boards.boardtypeinventory.md)<br>
Implements IExtensibleDataObject

## Properties

### **OrderNumber**

Gets or sets the order number.

```csharp
public string OrderNumber { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Code**

Gets or sets the board code.

```csharp
public string Code { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Location**

Gets or sets the location.

```csharp
public string Location { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Workstation**

Gets or sets the workstation.

```csharp
public string Workstation { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Quantity**

Gets or sets the quantity.

```csharp
public int Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AdditionalCommentsBoards**

Gets or sets the addtional comments for the board.

```csharp
public string AdditionalCommentsBoards { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CreationDate**

Gets or sets the creation date of the instance data.

```csharp
public Nullable<DateTimeOffset> CreationDate { get; set; }
```

#### Property Value

[Nullable&lt;DateTimeOffset&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **BoardTypeInventory()**

```csharp
public BoardTypeInventory()
```
