# Type Property Localisations API

This document describes the generic `/api/localizations/types/{typeName}/properties/{culture}` endpoint for retrieving culture-specific display names of object properties.
All types decorated with `[Display]` attributes on their properties are automatically available through this endpoint.

---

## Endpoint

```http
GET /api/localizations/types/{typeName}/properties/{culture}
```

### Parameters

| Name       | Type   | Required | Description |
|------------|--------|----------|-------------|
| `typeName` | string | yes      | Name of the type/class (case-insensitive). See supported types below. |
| `culture`  | string | yes      | Two-letter ISO 639-1 language code (`de`, `en`, `fr`, etc.). |

### How it works

Uses reflection to:
1. Locate the type by name
2. Inspect all public properties decorated with `[Display]` attributes
3. Extract localized display names from resource files
4. Return all properties with their localized labels

**Example (C#):**

```csharp
var culture = CultureInfo.GetCultureInfo("de");
var url = $"/api/localizations/types/{nameof(BoardType)}/properties/{culture.TwoLetterISOLanguageName}";
var response = await httpClient.GetAsync(url);
```

---

## Responses

### 200 OK

```json
{
  "Guid": "ID",
  "Name": "Name",
  "Code": "Code",
  "Type": "Type",
  "MaterialCategory": "Material Category",
  "Thickness": "Thickness (mm)",
  "Width": "Width (mm)",
  "Length": "Length (mm)"
}
```

Response is a key-value dictionary:
- **key** – property name (C# identifier)
- **value** – localized display name from resource or `Display` attribute

### 400 Bad Request

Invalid parameters or missing type/culture.

### 404 Not Found

Type or culture not supported.

---

## Supported types

The following types have localized properties available:

| Type                      | Description                                   | Source |
|---------------------------|-----------------------------------------------|--------|
| `BoardEntity`             | Board entity with inventory and location data | `MaterialManager.Contracts.Material.Boards` |
| `BoardType`               | Board type with core properties               | `MaterialManager.Contracts.Material.Boards` |
| `BoardTypeAllocation`     | Board allocation/stock information            | `MaterialManager.Contracts.Material.Boards` |
| `BoardTypeDetails`        | Extended board details and specifications     | `MaterialManager.Contracts.Material.Boards` |
| `BoardTypeInventory`      | Board inventory summary and statistics        | `MaterialManager.Contracts.Material.Boards` |
| `Material`                | Generic material record                       | `MaterialManager.Contracts.Material.Base` |
| `PartHistory`             | Historical record of part production          | `MaterialManager.Contracts.Statistics` |
| `OptimizationRequestPart` | Optimization request with part details        | `IntelliDivide.Contracts.Request` |
| `Rework`                  | Rework order with metadata                    | `ProductionManager.Contracts.Rework` |
| `ReworkCreationDetails`   | Details when creating a rework order          | `ProductionManager.Contracts.Rework` |
| `ReworkRejectionDetails`  | Details when rejecting a rework order         | `ProductionManager.Contracts.Rework` |
| `ProductionItemFeedback`  | Production item feedback information          | `ProductionManager.Contracts.ProductionItems` |

---

## Implementation

### For consumers

Get localized property labels for UI generation:

```csharp
public async Task<Dictionary<string, string>> GetLocalizedProperties(
    string typeName,
    CultureInfo culture)
{
    var url = $"/api/localizations/types/{typeName}/properties/{culture.TwoLetterISOLanguageName}";
    var response = await _httpClient.GetAsync(url);

    if (!response.IsSuccessStatusCode)
        throw new HttpRequestException($"Failed to fetch {typeName} properties");

    var properties = await response.Content.ReadAsAsync<Dictionary<string, string>>();
    return properties;
}

// Usage
var props = await GetLocalizedProperties(nameof(BoardType), CultureInfo.GetCultureInfo("de"));
foreach (var prop in props)
{
    Console.WriteLine($"{prop.Key}: {prop.Value}");
    // Output: Guid: ID, Name: Name, Type: Type, ...
}
```

### For contributors

To add localized properties to an existing type:

1. **Decorate properties** with `[Display]`:
   ```csharp
   [Display(ResourceType = typeof(BoardTypePropertyDisplayNames), Name = nameof(Id))]
   public Guid Id { get; set; }
   ```

2. **Create resource file** `BoardTypePropertyDisplayNames.resx`:
   ```xml
   <data name="Id"><value>ID</value></data>
   <data name="Name"><value>Name</value></data>
   ```

3. **The endpoint auto-discovers** the type and serves it.

### Localized serialization

Types implementing `ISupportsLocalizedSerialization` can be serialized with localized property names:

```csharp
public class BoardType : ISupportsLocalizedSerialization
{
    [Display(ResourceType = typeof(BoardTypePropertyDisplayNames), Name = nameof(Id))]
    public Guid Id { get; set; }
}

// Serialize with localized names
var culture = CultureInfo.GetCultureInfo("de");
var json = boardType.SerializeLocalized(culture);
// Output: {"ID": "abc-123", "Name": "MyBoard", ...}
```

---

## Notes

- **Single endpoint** serves all types with display-decorated properties.
- **Auto-discovered**: no manual registration needed.
- **Resource-based**: uses standard .NET `Display` and `.resx` files.
- **Reflected per request**: properties resolved dynamically.
- **Empty response**: returned when type or culture is unsupported.
