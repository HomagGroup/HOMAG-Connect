# HOMAG Connect intelliDivide - Get Optimization

## Endpoint

```http
GET https://connect.homag.cloud/api/intelliDivide/optimizations/{optimizationId}
```

## Description

Retrieves a single optimization by its unique ID.
Returns the same `Optimization` model used in optimization list responses, including status, machine, timing, and validation details.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `optimizationId` | GUID | Unique identifier of the optimization to retrieve. Must be a valid non-empty GUID. |

## Example Request

```http
GET https://connect.homag.cloud/api/intelliDivide/optimizations/550e8400-e29b-41d4-a716-446655440000
```

## Response

### Success Response (HTTP 200)

```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "name": "JobOrder_2026_001",
  "optimizationType": "Cutting",
  "status": "Optimized",
  "machine": "SawMachine01",
  "parameterName": "Standard_Parameters",
  "quantityOfParts": 45,
  "waste": 12.5,
  "productionTime": "00:15:30",
  "lastModifiedDate": "2026-02-26T14:30:00Z",
  "link": "https://connect.homag.cloud/intelliDivide/optimization/550e8400-e29b-41d4-a716-446655440000",
  "selectedSolutionId": "660f8400-e29b-41d4-a716-446655440001",
  "validationResults": []
}
```

### Response Properties

| Property | Type | Description |
|----------|------|-------------|
| `id` | GUID | Unique identifier for the optimization. |
| `name` | string | Human-readable name of the optimization. |
| `optimizationType` | string | Type of optimization: `Cutting` or `Nesting`. |
| `status` | string | Current status of the optimization (e.g., `New`, `Started`, `Optimized`, `Archived`). |
| `machine` | string | Name of the machine the optimization is configured for. |
| `parameterName` | string | Name of the parameter set used for this optimization. |
| `quantityOfParts` | integer | Total number of parts in the optimization. |
| `waste` | number | Waste percentage as a decimal (e.g., 12.5 = 12.5%). Nullable. |
| `productionTime` | timespan | Estimated time required for production. Nullable. |
| `lastModifiedDate` | datetime | ISO 8601 timestamp of the last modification. |
| `link` | URI | Direct link to view the optimization in the IntelliDivide application. |
| `selectedSolutionId` | GUID | ID of the selected solution, if transferred to production. Nullable. |
| `validationResults` | array | Array of validation messages, if any issues exist. |

## Error Responses

### Bad Request (HTTP 400)

Returned when `optimizationId` is invalid (for example, empty or malformed).

```json
{
  "error": "The optimization id must not be empty"
}
```

### Unauthorized (HTTP 401)

Returned when authentication is missing or invalid.

```json
{
  "error": "Unauthorized"
}
```

### Not Found (HTTP 404)

Returned when no optimization exists for the specified ID.

```json
{
  "error": "Optimization not found"
}
```

### Internal Server Error (HTTP 500)

Returned when the server encounters an unexpected error.

```json
{
  "error": "An unexpected error occurred"
}
```

## Implementation Details

### Client Method

The .NET client provides a strongly-typed method for this endpoint (see [`IIntelliDivideClient`](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs) and [`IntelliDivideClient`](../../../../Applications/IntelliDivide/Client/IntelliDivideClient.cs)):

```csharp
Task<Optimization?> GetOptimization(Guid optimizationId);
```

### Usage Example

```csharp
var client = new IntelliDivideClient(baseUri, httpClient);

var optimizationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
var optimization = await client.GetOptimization(optimizationId);

if (optimization is null)
{
    Console.WriteLine("Optimization not found.");
    return;
}

Console.WriteLine($"{optimization.Name} - {optimization.Status}");
```

## Referenced Types

- [`Optimization`](../../../../Applications/IntelliDivide/Contracts/Optimization.cs)
- [`OptimizationType`](../../../../Applications/IntelliDivide/Contracts/Common/OptimizationType.cs)
- [`OptimizationStatus`](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)
- [`IIntelliDivideClient`](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs)
- [`IntelliDivideClient`](../../../../Applications/IntelliDivide/Client/IntelliDivideClient.cs)

## Related Endpoints

- `GET /api/intelliDivide/optimizations` - Get a paginated list of optimizations
- `GET /api/intelliDivide/optimizations/{optimizationId}/state` - Get optimization status only
- `GET /api/intelliDivide/optimizations/{optimizationId}/solutions` - Get solutions for an optimization
- `DELETE /api/intelliDivide/optimizations/{optimizationId}` - Delete an optimization

## See Also

- [Get Optimizations](Get%20Optimizations.md)
- [IntelliDivide Documentation](../../../../Applications/IntelliDivide/Readme.md)
- [OptimizationType Reference](../../../../Applications/IntelliDivide/Contracts/Common/OptimizationType.cs)
- [OptimizationStatus Reference](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)
- [Optimization Model Reference](../../../../Applications/IntelliDivide/Contracts/Optimization.cs)
