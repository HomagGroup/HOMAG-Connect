<span style="color:red">[This is preliminary documentation and is subject to change.] </span>
# BoardTypeInventory

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

Represents the inventory of a specific type of board material.

```csharp
public class BoardTypeInventory : IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BoardTypeInventory](./homagconnect.materialmanager.contracts.material.boards.boardtypeinventory.md)
Implements [IExtensibleDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)

## Properties

### **OrderNumber**

Gets or sets the order number that the board material belongs to.

```csharp
public string? OrderNumber { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Code**

Gets or sets the code of the board material.

```csharp
public string? Code { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Location**

Gets or sets the location of the board material in the inventory.

```csharp
public string? Location { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Workstation**

Gets or sets the workstation that the board material is assigned to.

```csharp
public string? Workstation { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Quantity**

Gets or sets the quantity of the board material in the inventory.

```csharp
public int Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AdditionalCommentsBoards**

Gets or sets any additional comments for the board material.

```csharp
public string? AdditionalCommentsBoards { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CreationDate**

Gets or sets the creation date of the board material.

```csharp
public DateTimeOffset? CreationDate { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **IExtensibleDataObject Members**

#### **ExtensionData**

Gets or sets the extension data for the board material.

```csharp
public ExtensionDataObject? ExtensionData { get; set; }
```

#### Property Value

[ExtensionDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.extensiondataobject)<br>

## Constructors

### **BoardTypeInventory()**

```csharp
public BoardTypeInventory()
```

## See Also

* [IExtensibleDataObject Interface](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)

## Inheritance

`BoardTypeInventory` implements `IExtensibleDataObject`.