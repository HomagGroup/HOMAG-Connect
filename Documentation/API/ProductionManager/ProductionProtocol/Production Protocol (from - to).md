# HOMAG Connect productionManager - Production Protocol (from / to)

## Endpoint

```http
GET https://connect.homag.cloud/api/productionManager/workstations/{workstationId}/productionprotocol
```

## Description

Retrieves production protocol entries for a specific explicit date range.
This variant matches the Bruno request using `from` and `to` filters.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `workstationId` | string | Workstation identifier. URL-encoded in the client via `Uri.EscapeDataString(workstationId)`. |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `from` | datetime | yes | Start of time range (inclusive). |
| `to` | datetime | yes | End of time range (inclusive). |
| `take` | integer | no | Maximum number of items to return. |
| `skip` | integer | no | Number of items to skip for paging. Default: `0`. |

## Example Request

```http
GET https://connect.homag.cloud/api/productionManager/workstations/8f9d9f1a-5a01-4d43-98a8-2e9e88f4e1a7/productionprotocol?from=2025-12-01&to=2026-01-31&take=1000&skip=0
```

## Success Response (HTTP 200)

```json
[
  {
    "type": "ProcessedPartDividing",
    "workstationId": "8f9d9f1a-5a01-4d43-98a8-2e9e88f4e1a7",
    "from": "productionManager",
    "timestamp": "2026-01-15T10:45:00+00:00",
    "itemType": "Part"
  }
]
```

## Related Variants

- [Production Protocol](Production%20Protocol.md)
- [Production Protocol (localized)](Production%20Protocol%20(localized).md)
