# EdgeInventoryHistory

Namespace: HomagConnect.MaterialManager.Contracts.Statistics

A edge inventory for statistical use.

```csharp
public class EdgeInventoryHistory : System.Runtime.Serialization.IExtensibleDataObject, HomagConnect.Base.Contracts.Interfaces.IContainsUnitSystemDependentProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EdgeInventoryHistory](./homagconnect.materialmanager.contracts.statistics.edgeinventoryhistory.md)<br>
Implements IExtensibleDataObject, IContainsUnitSystemDependentProperties

## Properties

### **EdgebandCode**

Gets or sets the board code.

```csharp
public string EdgebandCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Costs**

Gets or sets the costs of the Edgeband. The unit depends on the settings of the subscription.

```csharp
public Nullable<double> Costs { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Height**

Gets or sets the length of the board. The unit depends on the settings of the subscription (metric: mm, imperial:
 inch).

```csharp
public Nullable<double> Height { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Timestamp**

Gets or sets the timestamp of the inventory.

```csharp
public DateTimeOffset Timestamp { get; set; }
```

#### Property Value

[DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>

### **TotalLengthInInventory**

Gets or sets the total quantity allocated

```csharp
public Nullable<double> TotalLengthInInventory { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

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

## Constructors

### **EdgeInventoryHistory()**

```csharp
public EdgeInventoryHistory()
```
