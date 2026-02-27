# Enum Localisations API

This document describes the generic `/api/localizations/enums/{enumTypeName}/{culture}` endpoint for retrieving culture-specific display names for localized enums.

---

## Endpoint

```http
GET /api/localizations/enums/{enumTypeName}/{culture}
```

### Parameters

| Name           | Type   | Required | Description |
|----------------|--------|----------|-------------|
| `enumTypeName` | string | yes      | Name of the enum type (case-insensitive). |
| `culture`      | string | yes      | Two-letter ISO 639-1 language code (`de`, `en`, `fr`, etc.). |

### How it works

The endpoint uses reflection to:
1. Locate the enum by name
2. Validate it is configured for localization via `[ResourceManager]`
3. Resolve the matching `<EnumName>DisplayNames.resx` resource file
4. Return enum member names and localized display names

**Example (C#):**

```csharp
string url = $"/api/localizations/enums/{nameof(SomeEnum)}/{cultureInfo.TwoLetterISOLanguageName}";
```

---

## Responses

### 200 OK

```json
[
  { "name": "Board", "displayName": "Platte" },
  { "name": "Offcut", "displayName": "Rest" },
  { "name": "Template", "displayName": "Schablone" }
]
```

Response fields:
- **name** – enum member identifier
- **displayName** – localized value from resource file

### 404 Not Found

Returned when enum type or culture is not supported.
The response can include a list of supported enum types.

---

## Supported enum types

The following enums can be queried via this endpoint:

| Enum type | Description | Source |
|-----------|-------------|--------|
| [`BoardTypeType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Base/HomagConnect.Base.Contracts/Enumerations/BoardTypeType.cs) | The type of the board type | `Base.Contracts.Enumerations` |
| [`RotationAngle`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Contracts/Common/RotationAngle.cs) | Provides fixed rotation angles | `IntelliDivide.Contracts.Common` |
| [`ReworkCategory`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/ReworkCategory.cs) | Rework category | `ProductionManager.Contracts.Rework` |
| [`ReworkReason`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/ReworkReason.cs) | Rework reason | `ProductionManager.Contracts.Rework` |
| [`ReworkState`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/ReworkState.cs) | Rework state | `ProductionManager.Contracts.Rework` |
| [`ProductionItemFeedbackAction`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/ProductionItems/ProductionItemFeedbackAction%20.cs) | Various production-item feedback actions | `ProductionManager.Contracts.ProductionItems` |
| [`GroupType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/OrderManager/Contracts/OrderItems/GroupType.cs) | Type of group | `OrderManager.Contracts.OrderItems` |
| [`BookHeightMode`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Processing/Optimization/BookheightMode.cs) | Book height mode | `MaterialManager.Contracts.Processing.Optimization` |
| [`TensionTrimType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Processing/Optimization/TensionTrimType.cs) | Tension trim type | `MaterialManager.Contracts.Processing.Optimization` |
| [`ImageSize`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Base/ImageSize.cs) | Image size enumeration | `MaterialManager.Contracts.Material.Base` |
| [`ImageType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Base/ImageType.cs) | Image type enumeration | `MaterialManager.Contracts.Material.Base` |
| [`BoardMaterialCategory`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Boards/Enumerations/BoardMaterialCategory.cs) | Board material category | `MaterialManager.Contracts.Material.Boards.Enumerations` |
| [`EdgebandingProcess`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Edgebands/Enumerations/EdgebandingProcess.cs) | Edgebanding process | `MaterialManager.Contracts.Material.Edgebands.Enumerations` |
| [`EdgebandMaterialCategory`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Edgebands/Enumerations/EdgebandMaterialCategory.cs) | Edgeband material category | `MaterialManager.Contracts.Material.Edgebands.Enumerations` |
| [`ManagementType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Base/ManagementType.cs) | Management type | `MaterialManager.Contracts.Material.Base` |
| [`CoatingCategory`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Boards/Enumerations/CoatingCategory.cs) | Coating category | `MaterialManager.Contracts.Material.Boards.Enumerations` |
| [`InventoryType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Boards/Enumerations/InventoryType.cs) | Inventory type | `MaterialManager.Contracts.Material.Boards.Enumerations` |
| [`StandardQuality`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Boards/Enumerations/StandardQuality.cs) | Standard quality classification | `MaterialManager.Contracts.Material.Boards.Enumerations` |
| [`ImportMode`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/MaterialManager/Contracts/Material/Boards/Enumerations/ImportMode.cs) | Import mode | `MaterialManager.Contracts.Material.Boards.Enumerations` |
| [`OptimizationStatus`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Contracts/OptimizationStatus.cs) | Optimization status | `IntelliDivide.Contracts` |
| [`ProgramTransferMode`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Contracts/Common/ProgramTransferMode.cs) | Program transfer mode | `IntelliDivide.Contracts.Common` |
| [`SolutionCharacteristic`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/IntelliDivide/Contracts/Result/SolutionCharacteristic.cs) | Solution characteristic | `IntelliDivide.Contracts.Result` |

New enums annotated with `[ResourceManager]` become automatically available via this route.

---

## Usage notes

1. Use this endpoint for localized dropdowns, labels, and enum display values.
2. To add a new enum, decorate it and provide a matching `<EnumName>DisplayNames.resx` file.
3. The response shape is consistent across all supported enums.
