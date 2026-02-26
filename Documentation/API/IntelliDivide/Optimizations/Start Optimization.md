# HOMAG Connect intelliDivide - Start Optimization

## Endpoint

```http
POST https://connect.homag.cloud/api/intelliDivide/optimizations/{optimizationId}/optimize
```

## Description

Starts execution of an existing optimization by its unique ID.
The optimization transitions from a non-running state (for example `New`) to processing states such as `Queued` or `Started`.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `optimizationId` | GUID | Unique identifier of the optimization to start. Must be a valid non-empty GUID. |

## Example Request

```http
POST https://connect.homag.cloud/api/intelliDivide/optimizations/550e8400-e29b-41d4-a716-446655440000/optimize
```

## Response

### Success Response (HTTP 204 No Content)

The optimization was started successfully.

_No response body is returned._

## Error Responses

### Bad Request (HTTP 400)

Returned when `optimizationId` is invalid or the optimization cannot be started in its current state.

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
Task StartOptimization(Guid optimizationId);
```

### Usage Example

```csharp
var client = new IntelliDivideClient(baseUri, httpClient);

var optimizationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
await client.StartOptimization(optimizationId);

Console.WriteLine("Optimization started.");
```

## Usage Notes

- Starting triggers optimization execution on the server.
- Poll `GET /api/intelliDivide/optimizations/{optimizationId}/state` to track progress.
- After completion, use `GET /api/intelliDivide/optimizations/{optimizationId}/solutions` to retrieve available solutions.

## Referenced Types

- [`Optimization`](../../../../Applications/IntelliDivide/Contracts/Optimization.cs)
- [`OptimizationStatus`](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)
- [`IIntelliDivideClient`](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs)
- [`IntelliDivideClient`](../../../../Applications/IntelliDivide/Client/IntelliDivideClient.cs)

## Related Endpoints

- `GET /api/intelliDivide/optimizations/{optimizationId}` - Get a specific optimization by ID
- `GET /api/intelliDivide/optimizations/{optimizationId}/state` - Get optimization status only
- `GET /api/intelliDivide/optimizations/{optimizationId}/solutions` - Get solutions for an optimization
- `POST /api/intelliDivide/optimizations/{optimizationId}/archive` - Archive an optimization

## See Also

- [Get Optimizations](Get%20Optimizations.md)
- [Get Optimization](Get%20Optimization.md)
- [Archive Optimization](Archive%20Optimization.md)
- [Delete Optimization](Delete%20Optimization.md)
- [IntelliDivide Documentation](../../../../Applications/IntelliDivide/Readme.md)
- [Client Interface Reference](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs)
