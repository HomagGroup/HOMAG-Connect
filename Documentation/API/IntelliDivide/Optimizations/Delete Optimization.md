# HOMAG Connect intelliDivide - Delete Optimization

## Endpoint

```http
DELETE https://connect.homag.cloud/api/intelliDivide/optimizations/{optimizationId}
```

## Description

Deletes an existing optimization by its unique ID.
After successful deletion, the optimization is no longer available in optimization listings and cannot be retrieved by ID.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `optimizationId` | GUID | Unique identifier of the optimization to delete. Must be a valid non-empty GUID. |

## Example Request

```http
DELETE https://connect.homag.cloud/api/intelliDivide/optimizations/550e8400-e29b-41d4-a716-446655440000
```

## Response

### Success Response (HTTP 204 No Content)

The optimization was deleted successfully.

_No response body is returned._

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
Task DeleteOptimization(Guid optimizationId);
```

### Usage Example

```csharp
var client = new IntelliDivideClient(baseUri, httpClient);

var optimizationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
await client.DeleteOptimization(optimizationId);

Console.WriteLine("Optimization deleted.");
```

## Usage Notes

- Deleting an optimization is irreversible.
- If needed, archive the optimization first using `POST /api/intelliDivide/optimizations/{optimizationId}/archive`.
- Use `GET /api/intelliDivide/optimizations` to verify it no longer appears in the result set (see [`Optimization`](../../../../Applications/IntelliDivide/Contracts/Optimization.cs) and [`OptimizationStatus`](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)).

## Referenced Types

- [`Optimization`](../../../../Applications/IntelliDivide/Contracts/Optimization.cs)
- [`OptimizationStatus`](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)
- [`IIntelliDivideClient`](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs)
- [`IntelliDivideClient`](../../../../Applications/IntelliDivide/Client/IntelliDivideClient.cs)

## Related Endpoints

- `GET /api/intelliDivide/optimizations` - Get a paginated list of optimizations
- `GET /api/intelliDivide/optimizations/{optimizationId}` - Get a specific optimization by ID
- `POST /api/intelliDivide/optimizations/{optimizationId}/archive` - Archive an optimization
- `POST /api/intelliDivide/optimizations/{optimizationId}/optimize` - Start an optimization

## See Also

- [Get Optimizations](Get%20Optimizations.md)
- [Get Optimization](Get%20Optimization.md)
- [IntelliDivide Documentation](../../../../Applications/IntelliDivide/Readme.md)
- [Client Interface Reference](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs)
