# MaterialCodeWithThumbnail

Namespace: HomagConnect.MaterialManager.Contracts.Material.Boards

Represents a material code along with its thumbnail.

```csharp
public class MaterialCodeWithThumbnail: IExtensibleDataObject
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) -> [MaterialCodeWithThumbnail](./homagconnect.materialmanager.contracts.material.boards.materialcodewiththumbnail.md)
Implements [IExtensibleDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.iextensibledataobject)

## Properties

### **MaterialCode**

Gets or sets the material code.

```csharp
[Required]
[StringLength(50, MinimumLength = 1)]
[JsonProperty(Order = 1)]
public string MaterialCode { get; set; } = string.Empty;
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Thumbnail**

Gets or sets the URI for the material thumbnail image.

```csharp
[JsonProperty(Order = 2)]
public Uri? Thumbnail { get; set; }
```

#### Property Value

[Nullable Uri](https://docs.microsoft.com/en-us/dotnet/api/system.uri?view=net-5.0)

### **ExtensionData**

Gets or sets the extension data for the material code with thumbnail.

```csharp
public ExtensionDataObject? ExtensionData { get; set; }
```

#### Property Value

[ExtensionDataObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.serialization.extensiondataobject)? or null

## Implements

- IExtensibleDataObject

## Constructors

### **MaterialCodeWithThumbnail()**

Creates a new instance of the MaterialCodeWithThumbnail class.

```csharp
public MaterialCodeWithThumbnail()
```