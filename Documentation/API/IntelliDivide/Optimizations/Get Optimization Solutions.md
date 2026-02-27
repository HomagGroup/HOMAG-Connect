# HOMAG Connect intelliDivide - Get Optimization Solutions

## Endpoint

```http
GET https://connect.homag.cloud/api/intelliDivide/optimizations/{optimizationId}/solutions
```

## Description

Retrieves all available solutions for a specific optimization.
If the optimization is not finished successfully, the endpoint can return an empty list.

## Path Parameters

| Parameter | Type | Description |
|-----------|------|-------------|
| `optimizationId` | GUID | Unique identifier of the optimization. Must be a valid non-empty GUID. |

## Example Request

```http
GET https://connect.homag.cloud/api/intelliDivide/optimizations/550e8400-e29b-41d4-a716-446655440000/solutions
```

## Response

### Success Response (HTTP 200)

```json
[
  {
    "id": "660f8400-e29b-41d4-a716-446655440001",
    "optimizationId": "550e8400-e29b-41d4-a716-446655440000",
    "name": "Balanced Solution",
    "characteristic": "BalancedSolution",
    "characteristicsInAddition": ["MinimumWaste"],
    "description": "Recommended default solution",
    "overview": {
      "figures": {},
      "pattern": []
    },
    "unitSystem": "Metric"
  }
]
```

### Response Properties

| Property | Type | Description |
|----------|------|-------------|
| `id` | GUID | Unique identifier of the solution. |
| `optimizationId` | GUID | ID of the parent optimization. |
| `name` | string | Human-readable solution name. |
| `characteristic` | string | Primary solution characteristic (for example `BalancedSolution`, `MinimumWaste`). |
| `characteristicsInAddition` | array | Additional characteristics assigned to the solution. |
| `description` | string | Optional solution description. |
| `overview` | object | Aggregated overview data, including figures and patterns. |
| `unitSystem` | string | Unit system used by solution values (for example `Metric`). |
| `additionalProperties` | object | Optional application-specific extension properties. |

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
Task<IEnumerable<Solution>?> GetSolutions(Guid optimizationId);
```

### Usage Example

```csharp
var client = new IntelliDivideClient(baseUri, httpClient);

var optimizationId = Guid.Parse("550e8400-e29b-41d4-a716-446655440000");
var solutions = await client.GetSolutions(optimizationId);

if (solutions is null || !solutions.Any())
{
    Console.WriteLine("No solutions available.");
    return;
}

foreach (var solution in solutions)
{
    Console.WriteLine($"{solution.Name} ({solution.Characteristic})");
}
```

## Usage Notes

- Solutions are typically available after optimization status is `Optimized` (see [`OptimizationStatus`](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)).
- Use `GET /api/intelliDivide/optimizations/{optimizationId}/state` to check progress.
- Use `GET /api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}` for full solution details.

## Referenced Types

- [`Optimization`](../../../../Applications/IntelliDivide/Contracts/Optimization.cs)
- [`OptimizationStatus`](../../../../Applications/IntelliDivide/Contracts/OptimizationStatus.cs)
- [`Solution`](../../../../Applications/IntelliDivide/Contracts/Result/Solution.cs)
- [`SolutionCharacteristic`](../../../../Applications/IntelliDivide/Contracts/Result/SolutionCharacteristic.cs)
- [`SolutionOverview`](../../../../Applications/IntelliDivide/Contracts/Result/SolutionOverview.cs)
- [`IIntelliDivideClient`](../../../../Applications/IntelliDivide/Contracts/IIntelliDivideClient.cs)
- [`IntelliDivideClient`](../../../../Applications/IntelliDivide/Client/IntelliDivideClient.cs)

## Related Endpoints

- `GET /api/intelliDivide/optimizations/{optimizationId}` - Get a specific optimization by ID
- `GET /api/intelliDivide/optimizations/{optimizationId}/state` - Get optimization status only
- `GET /api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}` - Get details for one solution
- `POST /api/intelliDivide/optimizations/{optimizationId}/solutions/{solutionId}/send` - Send a solution to machine
