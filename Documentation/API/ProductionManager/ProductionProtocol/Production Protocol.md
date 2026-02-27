# HOMAG Connect productionManager - Production Protocol

## Endpoint

```http
GET https://connect.homag.cloud/api/productionManager/workstations/{workstationId}/productionprotocol
```

## Description

Retrieves production protocol entries for a specific workstation.
This variant matches the default Bruno request using `daysBack`, `take`, and `skip`.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `workstationId` | string | Workstation identifier. URL-encoded in the client via `Uri.EscapeDataString(workstationId)`. |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `daysBack` | integer | no | Number of days to include in the history window. |
| `take` | integer | no | Maximum number of items to return. |
| `skip` | integer | no | Number of items to skip for paging. Default: `0`. |

## Example Request

```http
GET https://connect.homag.cloud/api/productionManager/workstations/8f9d9f1a-5a01-4d43-98a8-2e9e88f4e1a7/productionprotocol?daysBack=90&take=1000&skip=0
```

## Response

### Success Response (HTTP 200)

```json
[
  {
    "type": "ProcessedPartDividing",
    "workstationId": "8f9d9f1a-5a01-4d43-98a8-2e9e88f4e1a7",
    "from": "productionManager",
    "timestamp": "2026-02-26T14:30:00+00:00",
    "itemType": "Part"
  }
]
```

### Response Properties (base `ProcessedItem`)

| Property | Type | Description |
|----------|------|-------------|
| `type` | string | Discriminator used to deserialize to the concrete processed-item subtype. |
| `workstationId` | GUID | Workstation that produced the protocol entry. |
| `from` | string | Source of the processing information. |
| `timestamp` | datetimeoffset | Processing timestamp. |
| `itemType` | string | Production item type. |
| `subscriptionId` | GUID | Subscription identifier. |
| `additionalProperties` | object | Optional extension fields. |

## Referenced Types

- [`ProcessedItem`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/ProductionProtocol/ProcessedItem.cs)
- [`ProcessedItemType`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/ProductionProtocol/ProcessedItemType.cs)
- [`IProductionManagerClient`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Contracts/IProductionManagerClient.cs)
- [`ProductionManagerClient`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Applications/ProductionManager/Client/ProductionManagerClient.cs)
- [`Workstation`](https://github.com/HomagGroup/HOMAG-Connect/blob/main/Base/HomagConnect.Base.Contracts/Workstation.cs)
