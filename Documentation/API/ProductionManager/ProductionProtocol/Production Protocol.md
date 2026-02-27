# HOMAG Connect productionManager - Get Production Protocol

## Endpoint

```http
GET https://connect.homag.cloud/api/productionManager/workstations/{workstationId}/productionprotocol?daysBack={daysBack}
```

## Description

Retrieves production protocol entries for a specific workstation.
The response contains processed production items (polymorphic `ProcessedItem` records) for the selected time window.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `workstationId` | string | Workstation identifier. URL-encoded in the client via `Uri.EscapeDataString(workstationId)`. |

## Query Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `daysBack` | integer | no | Number of days to include in the history window. Default in client: `7`. |

## Example Request

```http
GET https://connect.homag.cloud/api/productionManager/workstations/8f9d9f1a-5a01-4d43-98a8-2e9e88f4e1a7/productionprotocol?daysBack=7
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

## Error Responses

### Bad Request (HTTP 400)

Returned when parameters are invalid (for example unsupported `workstationId` format).

### Unauthorized (HTTP 401)

Returned when authentication is missing or invalid.

### Not Found (HTTP 404)

Returned when the specified workstation cannot be resolved.

### Internal Server Error (HTTP 500)

Returned when the server encounters an unexpected error.

## Implementation Details

### Client Method

The .NET client exposes this method (see [`IProductionManagerClient`](../../../Applications/ProductionManager/Contracts/IProductionManagerClient.cs) and [`ProductionManagerClient`](../../../Applications/ProductionManager/Client/ProductionManagerClient.cs)):

```csharp
Task<IEnumerable<ProcessedItem>?> GetProductionProtocol(string workstationId, int take = 100000, int skip = 0, int daysBack = 7);
```

### Usage Example

```csharp
var client = new ProductionManagerClient(subscriptionId, authorizationKey);

var workstations = await client.GetWorkstations();
var workstation = workstations?.FirstOrDefault();
if (workstation is null)
    return;

var protocol = await client.GetProductionProtocol(workstation.Id.ToString(), daysBack: 7);
```

## Referenced Types

- [`ProcessedItem`](../../../Applications/ProductionManager/Contracts/ProductionProtocol/ProcessedItem.cs)
- [`ProcessedItemType`](../../../Applications/ProductionManager/Contracts/ProductionProtocol/ProcessedItemType.cs)
- [`IProductionManagerClient`](../../../Applications/ProductionManager/Contracts/IProductionManagerClient.cs)
- [`ProductionManagerClient`](../../../Applications/ProductionManager/Client/ProductionManagerClient.cs)
- [`Workstation`](../../../Base/HomagConnect.Base.Contracts/Workstation.cs)
