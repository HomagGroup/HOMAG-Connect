# BoardTypeAllocation

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

A board type allocation.

```csharp
public class BoardTypeAllocation : System.Runtime.Serialization.IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BoardTypeAllocation](./homagconnect.materialmanager.contracts.material.boards.boardtypeallocation.md)<br>
Implements IExtensibleDataObject

## Properties

### **AllocationComments**

Gets or sets the allocation comments.

```csharp
public string AllocationComments { get; set; }
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

### **Quantity**

Gets or sets the quantity.

```csharp
public Nullable<int> Quantity { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Type**

Gets or sets the type

```csharp
public string Type { get; set; }
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

### **ExtensionData**

```csharp
public ExtensionDataObject ExtensionData { get; set; }
```

#### Property Value

ExtensionDataObject<br>

## Constructors

### **BoardTypeAllocation()**

```csharp
public BoardTypeAllocation()
```
