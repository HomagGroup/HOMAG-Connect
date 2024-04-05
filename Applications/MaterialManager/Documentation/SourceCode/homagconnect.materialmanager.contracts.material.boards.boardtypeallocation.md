# BoardTypeAllocation

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

Represents the allocation of a specific type of board material.

```csharp
public class BoardTypeAllocation : IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BoardTypeAllocation](./homagconnect.materialmanager.contracts.material.boards.boardtypeallocation.md)
Implements [IExtensibleDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)

## Properties

### **AllocationComments**

Gets or sets any allocation comments for the board material.

```csharp
public string? AllocationComments { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **CreationDate**

Gets or sets the creation date of the board material allocation.

```csharp
public DateTimeOffset? CreationDate { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **Quantity**

Gets or sets the quantity of the board material allocation.

```csharp
public int? Quantity { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)?<br>

### **Type**

Gets or sets the type of the board material.

```csharp
public string? Type { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Workstation**

Gets or sets the workstation that the board material is allocated to.

```csharp
public string? Workstation { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **IExtensibleDataObject Members**

#### **ExtensionData**

Gets or sets the extension data for the board material allocation.

```csharp
public ExtensionDataObject? ExtensionData { get; set; }
```

#### Property Value

[ExtensionDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.extensiondataobject)<br>

## See Also

* [IExtensibleDataObject Interface](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)
