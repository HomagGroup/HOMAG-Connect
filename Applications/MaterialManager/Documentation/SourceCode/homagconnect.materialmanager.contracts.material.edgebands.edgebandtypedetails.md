# EdgebandTypeDetails

Namespace: HomagConnect.MaterialManager.Contracts.Material.Edgebands

```csharp
public class EdgebandTypeDetails : EdgebandType, System.Runtime.Serialization.IExtensibleDataObject, HomagConnect.Base.Contracts.Interfaces.IContainsUnitSystemDependentProperties
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EdgebandType](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md) → [EdgebandTypeDetails](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtypedetails.md)<br>
Implements IExtensibleDataObject, IContainsUnitSystemDependentProperties

## Properties

### **Inventory**

Gets or sets the board type inventory.

```csharp
public ICollection<EdgebandTypeInventory> Inventory { get; set; }
```

#### Property Value

[ICollection&lt;EdgebandTypeInventory&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

### **Images**

Gets or sets the list of additional images.

```csharp
public ICollection<ImageInformation> Images { get; set; }
```

#### Property Value

[ICollection&lt;ImageInformation&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1)<br>

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

### **EdgebandCode**

Gets or sets the edgeband code

```csharp
public string EdgebandCode { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Height**

Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial: inch).

```csharp
public Nullable<double> Height { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Thickness**

Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial: inch).

```csharp
public Nullable<double> Thickness { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Length**

Gets or sets the length of the edgeband. The unit depends on the settings of the subscription (metric: m, imperial: ft).

```csharp
public Nullable<double> Length { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Costs**

Gets or sets the costs of the edgeband. The unit depends on the settings of the subscription.

```csharp
public Nullable<double> Costs { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MaterialCategory**

Gets or sets the material category.

```csharp
public Nullable<EdgebandMaterialCategory> MaterialCategory { get; set; }
```

#### Property Value

[Nullable&lt;EdgebandMaterialCategory&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **GluingCategory**

Gets or sets the gluing category.

```csharp
public Nullable<GluingCategory> GluingCategory { get; set; }
```

#### Property Value

[Nullable&lt;GluingCategory&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Lasertec**

Gets or sets the lasertec (J/cm^2).

```csharp
public Nullable<double> Lasertec { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Airtec**

Gets or sets the airtec. The unit depends on the settings of the subscription (metric: bar, imperial: psi).

```csharp
public Nullable<double> Airtec { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ProtectionFilmThickness**

Gets or sets the protection film thickness.

```csharp
public Nullable<double> ProtectionFilmThickness { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **FunctionLayerThickness**

Gets or sets the protection layer thickness.

```csharp
public Nullable<double> FunctionLayerThickness { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **MacroName**

Gets or sets the macro name.

```csharp
public string MacroName { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ManufacturerName**

Gets or sets the name of the manufacturer.

```csharp
public string ManufacturerName { get; set; }
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

### **ProductName**

Gets or sets the name of the product.

```csharp
public string ProductName { get; set; }
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

### **DecorEmbossingCode**

Gets or sets the decor embossing code.

```csharp
public string DecorEmbossingCode { get; set; }
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

### **TotalQuantityAvailableWarningLimit**

Gets or sets the total quantity available warning limit.

```csharp
public Nullable<int> TotalQuantityAvailableWarningLimit { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalLengthAvailableWarningLimit**

Gets or sets the total length available warning limit. The unit depends on the settings of the subscription (metric: m, imperial: ft).

```csharp
public Nullable<double> TotalLengthAvailableWarningLimit { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalQuantityAvailable**

Gets or sets the total quantity of boards of this type which are available in the inventory.

```csharp
public Nullable<int> TotalQuantityAvailable { get; set; }
```

#### Property Value

[Nullable&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TotalLengthAvailable**

Gets or sets the total length of boards of this type which are available in the inventory.

```csharp
public Nullable<double> TotalLengthAvailable { get; set; }
```

#### Property Value

[Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **InsufficientInventory**

Gets or sets a indication whether the [EdgebandType.TotalQuantityAvailable](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#totalquantityavailable) or [EdgebandType.TotalLengthAvailable](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#totallengthavailable) is
 below the defined limit
 [EdgebandType.TotalQuantityAvailableWarningLimit](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#totalquantityavailablewarninglimit).
 [EdgebandType.TotalLengthAvailableWarningLimit](./homagconnect.materialmanager.contracts.material.edgebands.edgebandtype.md#totallengthavailablewarninglimit).

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

### **EdgebandTypeDetails()**

```csharp
public EdgebandTypeDetails()
```
