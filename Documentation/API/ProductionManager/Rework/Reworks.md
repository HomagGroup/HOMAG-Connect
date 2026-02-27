# HOMAG Connect productionManager - Get Reworks

## Endpoint

```http
GET https://connect.homag.cloud/api/productionManager/orders/reworks
```

## Description

Retrieves rework entries with optional filtering by state, capture date range, and paging.
The endpoint can also return localized output when `format=localized` is used.

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `state` | string[] | no | Rework state filter. Repeat parameter for multiple values (for example `state=Rejected&state=Transferred`). |
| `capturedAtFrom` | datetimeoffset | no | Start of capture timestamp range (inclusive). |
| `capturedAtTo` | datetimeoffset | no | End of capture timestamp range (inclusive). |
| `take` | integer | no | Maximum number of records to return. |
| `skip` | integer | no | Number of records to skip for paging. |
| `format` | string | no | Optional output mode. See **Format parameter values** below. |
| `culture` | string | no | Culture for localized output values (for example `de`, `en`, `de-DE`). |

### Format parameter values

| Value | Behavior |
|-------|----------|
| _(empty)_ | Uses default serialization. |
| `localized` | Uses localized serialization (`SerializeLocalized(culture)`). Comparison is case-insensitive. |
| any other value | Uses default serialization. |

Notes:
- If `culture` is not provided, server falls back to `CultureInfo.CurrentCulture`.
- `culture` is relevant when `format=localized`.
- `format=localized` is useful for direct usage in Excel/Power BI because column names and enum values are rendered in the selected culture.

## Example Requests

### Default request

```http
GET https://connect.homag.cloud/api/productionManager/orders/reworks
```

### Filter by states and date range

```http
GET https://connect.homag.cloud/api/productionManager/orders/reworks?state=Rejected&state=Transferred&capturedAtFrom=2026-01-01T00:00:00Z&capturedAtTo=2026-01-31T23:59:59Z&take=100&skip=0
```

### Localized output (`de`)

```http
GET https://connect.homag.cloud/api/productionManager/orders/reworks?state=Rejected&format=localized&culture=de&take=100
```

## Response

### Success Response (HTTP 200)

```json
[
  {
    "id": "16332994",
    "reworkId": "RW-2026-001",
    "description": "Damaged edge",
    "state": "Rejected",
    "category": "Edgebanding",
    "reason": "Damages",
    "capturedAt": "2026-01-15T08:30:00+00:00",
    "quantityOfReworks": 1,
    "material": "MDF_Australia_19.0"
  }
]
```

### Response Properties (base `Rework`)

| Property | Type | Description |
|----------|------|-------------|
| `id` | string | Identifier of the part for which rework was created. |
| `reworkId` | string | Identifier of the rework record. |
| `description` | string | Rework description. |
| `state` | string | Rework lifecycle state (`Pending`, `Approved`, `Rejected`, `Transferred`, `Unknown`). |
| `category` | string | Rework category (`Dividing`, `Edgebanding`, `CNC`, `Surface`, `Other`). |
| `reason` | string | Rework reason enum value. |
| `capturedAt` | datetimeoffset | Timestamp when rework was captured. |
| `quantityOfReworks` | integer | Quantity of items to rework. |
| `material` | string | Material designation. |
| `captureDetails` | object | Optional creation details. |
| `rejectionDetails` | object | Optional rejection details. |
| `additionalProperties` | object | Optional extension fields. |

## Error Responses

### Unauthorized (HTTP 401)

Returned when authentication is missing or invalid.

### Not Found (HTTP 404)

Returned when no matching route or resource is found.

## Implementation Details

### Client Method

The .NET client provides:

```csharp
Task<IEnumerable<Rework>?> GetReworks(
    ReworkState[]? states,
    DateTimeOffset? capturedAtFrom = null,
    DateTimeOffset? capturedAtTo = null,
    int take = int.MaxValue,
    int skip = 0);

Task<IEnumerable<Rework>?> GetCompletedReworks();
```

Note: the typed client currently exposes state/date/paging filters directly; `format` and `culture` are gateway query parameters.

## Referenced Types

- [`Rework`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/Rework.cs)
- [`ReworkState`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/ReworkState.cs)
- [`ReworkCategory`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/ReworkCategory.cs)
- [`ReworkReason`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/Rework/ReworkReason.cs)
- [`IProductionManagerClient`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/IProductionManagerClient.cs)
- [`ProductionManagerClient`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Client/ProductionManagerClient.cs)
