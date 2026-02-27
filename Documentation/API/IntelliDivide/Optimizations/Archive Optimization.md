# HOMAG Connect intelliDivide - Archive Optimization

## Endpoint

```http
POST https://connect.homag.cloud/api/intelliDivide/optimizations/{optimizationId}/archiv
```

## Description

Archives an existing optimization by its unique ID.
Archived optimizations remain available for retrieval but are marked with status `Archived`.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `optimizationId` | GUID | Unique identifier of the optimization to archive. Must be a valid non-empty GUID. |

## Example Request

```http
POST https://connect.homag.cloud/api/intelliDivide/optimizations/550e8400-e29b-41d4-a716-446655440000/archiv
```

## Response

### Success Response (HTTP 204 No Content)

The optimization was archived successfully.

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
Task ArchiveOptimization(Guid optimizationId);
```

### Usage Example

```csharp
var client = new IntelliDivideClient(baseUri, httpClient);

var optimizationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
await client.ArchiveOptimization(optimizationId);

Console.WriteLine("Optimization archived.");
```

## Usage Notes

- Archiving keeps the optimization data but changes the lifecycle state.
- Archived optimizations can still be listed or fetched by ID.
- Use `DELETE /api/intelliDivide/optimizations/{optimizationId}` for permanent removal.

## Related Endpoints

- `GET /api/intelliDivide/optimizations` - Get a paginated list of optimizations
- `GET /api/intelliDivide/optimizations/{optimizationId}` - Get a specific optimization by ID
- `POST /api/intelliDivide/optimizations/{optimizationId}/optimize` - Start an optimization
- `DELETE /api/intelliDivide/optimizations/{optimizationId}` - Delete an optimization