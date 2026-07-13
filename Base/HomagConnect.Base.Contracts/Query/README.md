# QueryDefinition KQL Resolver

This feature provides extension methods to convert `QueryDefinition` objects into valid KQL (Kusto Query Language) strings for Azure Data Explorer and Azure Monitor Logs queries.

## Overview

The `QueryDefinitionKqlExtensions` class provides two extension methods:
- `ToKql(string tableName)` - Converts a QueryDefinition to KQL without advanced pagination
- `ToKqlWithPagination(string tableName)` - Converts a QueryDefinition to KQL with proper skip/take pagination using row_number()

## Features

### Supported Capabilities

- **Date Range Filtering**: Automatically converts `FromDate` and `ToDate` to KQL timestamp filters
- **Complex Filters**: Supports nested AND/OR logic with multiple conditions
- **Filter Operators**:
  - Comparison: `Equal`, `NotEqual`, `GreaterThan`, `GreaterThanOrEqual`, `LessThan`, `LessThanOrEqual`
  - String: `Contains`, `StartsWith`, `EndsWith`
  - Collection: `In`, `NotIn`
  - Range: `Between`
  - Null checks: `IsNull`, `IsNotNull`
- **Sorting**: Multi-column sorting with priority
- **Pagination**: Skip and Take with proper row numbering
- **Data Type Support**: String, Integer, Decimal, DateTime, Boolean, Enum

## Usage Examples

### Basic Query

```csharp
var query = new QueryDefinition
{
	FromDate = DateTime.Parse("2023-01-01"),
	ToDate = DateTime.Parse("2023-12-31"),
	Take = 100
};

string kql = query.ToKql("ProductionData");
// Output: ProductionData | where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.0000000Z) | take 100
```

### Filter with AND Logic

```csharp
var query = new QueryDefinition
{
	Filter = new FilterCondition
	{
		Logic = LogicalOperator.And,
		Conditions = new List<FilterCondition>
		{
			new FilterCondition
			{
				Rule = new FilterRule("Status")
				{
					Operator = FilterOperator.Equal,
					Value = "Active",
					DataType = FieldDataType.String
				}
			},
			new FilterCondition
			{
				Rule = new FilterRule("Quantity")
				{
					Operator = FilterOperator.GreaterThan,
					Value = 10,
					DataType = FieldDataType.Integer
				}
			}
		}
	},
	Take = 100
};

string kql = query.ToKql("Orders");
// Output: Orders | where (Status == "Active" and Quantity > 10) | take 100
```

### Query with Sorting

```csharp
var query = new QueryDefinition
{
	SortBy = new List<SortCriteria>
	{
		new SortCriteria("Priority") { Direction = SortDirection.Descending, Priority = 0 },
		new SortCriteria("CreatedAt") { Direction = SortDirection.Ascending, Priority = 1 }
	},
	Take = 50
};

string kql = query.ToKql("Issues");
// Output: Issues | order by Priority desc, CreatedAt asc | take 50
```

### Pagination

```csharp
var query = new QueryDefinition
{
	Skip = 100,
	Take = 50
};

string kql = query.ToKqlWithPagination("Orders");
// Output: Orders | serialize rowNum = row_number() | where rowNum > 100 | take 50
```

### Complex Nested Query

```csharp
var query = new QueryDefinition
{
	FromDate = DateTime.Parse("2023-01-01"),
	ToDate = DateTime.Parse("2023-12-31"),
	Filter = new FilterCondition
	{
		Logic = LogicalOperator.And,
		Conditions = new List<FilterCondition>
		{
			new FilterCondition
			{
				Rule = new FilterRule("Category")
				{
					Operator = FilterOperator.Equal,
					Value = "Manufacturing",
					DataType = FieldDataType.String
				}
			},
			new FilterCondition
			{
				Logic = LogicalOperator.Or,
				Conditions = new List<FilterCondition>
				{
					new FilterCondition
					{
						Rule = new FilterRule("Machine")
						{
							Operator = FilterOperator.StartsWith,
							Value = "CNC",
							DataType = FieldDataType.String
						}
					},
					new FilterCondition
					{
						Rule = new FilterRule("Machine")
						{
							Operator = FilterOperator.StartsWith,
							Value = "LASER",
							DataType = FieldDataType.String
						}
					}
				}
			}
		}
	},
	SortBy = new List<SortCriteria>
	{
		new SortCriteria("Timestamp") { Direction = SortDirection.Descending }
	},
	Take = 100
};

string kql = query.ToKql("MachineEvents");
// Output: MachineEvents | where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.0000000Z) | where (Category == "Manufacturing" and (Machine startswith "CNC" or Machine startswith "LASER")) | order by Timestamp desc | take 100
```

## Important Notes

1. **Date Handling**: All dates are converted to UTC and formatted in ISO 8601 format
2. **String Escaping**: Special characters in strings are automatically escaped (`"`, `\`, `\n`, `\r`, `\t`)
3. **Pagination**: The `ToKql` method does not support `Skip` for pagination; use `ToKqlWithPagination` for proper skip/take support
4. **Field Names**: Column names are used as-is; ensure they match your KQL table schema
5. **Timestamp Column**: Date range queries assume a `Timestamp` column exists; customize if your table uses a different column name

## Testing

Comprehensive unit tests are available in `HomagConnect.Base.Tests\Query\Extensions\QueryDefinitionKqlExtensionsTests.cs` covering:
- All filter operators
- Nested filter groups
- Sorting scenarios
- Pagination
- Date range filtering
- Special character escaping
- Error handling

## Samples

Additional usage samples are available in `HomagConnect.Base.Samples\Query\QueryDefinitionKqlSamples.cs` demonstrating:
- Basic queries
- Complex filters
- String operations
- Null checks
- Comprehensive multi-feature queries

## Files

- **Extension**: `Base\HomagConnect.Base.Contracts\Query\Extensions\QueryDefinitionKqlExtensions.cs`
- **Tests**: `Base\HomagConnect.Base.Tests\Query\Extensions\QueryDefinitionKqlExtensionsTests.cs`
- **Samples**: `Base\HomagConnect.Base.Samples\Query\QueryDefinitionKqlSamples.cs`
- **Contract**: `Base\HomagConnect.Base.Contracts\Query\QueryDefinition.cs`
