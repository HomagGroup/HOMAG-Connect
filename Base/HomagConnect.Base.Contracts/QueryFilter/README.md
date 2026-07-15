# FilterRequest Usage Examples

This document demonstrates how to use the `FilterRequest` tool to build OData filter queries with various data types and conditions.

## Overview

The `FilterRequest` class provides a fluent API for building OData `$filter` query parameters. It supports:

- **Equality comparisons** (`eq`) for int, float, DateTimeOffset, string, and string arrays
- **Contains comparisons** (case-insensitive substring matching) for strings and string arrays
- **Range comparisons** (`ge` and `le`) for numbers and dates

## Basic Usage

### Integer Equality

```csharp
var filter = new FilterRequest()
	.AddEquals("Age", 42);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Age eq 42"
```

### Float Equality

```csharp
var filter = new FilterRequest()
	.AddEquals("Price", 19.99f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Price eq 19.99"
```

### DateTimeOffset Equality

```csharp
var date = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);
var filter = new FilterRequest()
	.AddEquals("CreatedDate", date);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "CreatedDate eq 2024-01-15T10:30:00Z"
```

## String Operations

### String Equality (Exact Match)

The `AddEquals` method performs **exact matching** for strings:

```csharp
var filter = new FilterRequest()
	.AddEquals("Name", "John");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Name eq 'John'"
```

### String Contains (Substring Matching)

The `AddContains` method performs **substring matching**:

```csharp
var filter = new FilterRequest()
	.AddContains("Description", "keyword");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "contains(Description, 'keyword')"
```

## Array of Strings

### String Array with Equals (OR Conditions)

When you provide an array of strings with `AddEquals`, it creates OR conditions within brackets:

```csharp
var statuses = new[] { "Active", "Pending", "Completed" };
var filter = new FilterRequest()
	.AddEquals("Status", statuses);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(Status eq 'Active' or Status eq 'Pending' or Status eq 'Completed')"
```

### String Array with Contains (OR Conditions)

When you provide an array of strings with `AddContains`, it creates multiple contains clauses with OR:

```csharp
var tags = new[] { "urgent", "high priority", "critical" };
var filter = new FilterRequest()
	.AddContains("Tags", tags);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(contains(Tags, 'urgent') or contains(Tags, 'high priority') or contains(Tags, 'critical'))"
```

## Range Comparisons

### Greater Than or Equal

```csharp
var filter = new FilterRequest()
	.AddGreaterThanOrEqual("Quantity", 100);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Quantity ge 100"
```

### Less Than or Equal

```csharp
var filter = new FilterRequest()
	.AddLessThanOrEqual("Weight", 50.5f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Weight le 50.5"
```

### Date Range

```csharp
var startDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
var filter = new FilterRequest()
	.AddGreaterThanOrEqual("CreatedDate", startDate);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "CreatedDate ge 2024-01-01T00:00:00Z"
```

## Complex Queries with Multiple Conditions

All conditions are automatically joined with `and`:

```csharp
var filter = new FilterRequest()
	.AddEquals("Status", "Active")
	.AddContains("Name", "test")
	.AddGreaterThanOrEqual("Age", 18)
	.AddLessThanOrEqual("Price", 100.0f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Status eq 'Active' and contains(Name, 'test') and Age ge 18 and Price le 100"
```

## Mixed Array and Single Conditions

```csharp
var statuses = new[] { "Active", "Pending" };
var tags = new[] { "urgent", "critical" };

var filter = new FilterRequest()
	.AddEquals("Status", statuses)
	.AddContains("Tags", tags)
	.AddGreaterThanOrEqual("Priority", 5);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(Status eq 'Active' or Status eq 'Pending') and (contains(Tags, 'urgent') or contains(Tags, 'critical')) and Priority ge 5"
```

## Special Characters

The tool automatically escapes apostrophes in string values:

```csharp
var filter = new FilterRequest()
	.AddEquals("Name", "O'Brien");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Name eq 'O''Brien'"
```

```csharp
var names = new[] { "O'Brien", "D'Angelo" };
var filter = new FilterRequest()
	.AddEquals("Name", names);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(Name eq 'O''Brien' or Name eq 'D''Angelo')"
```

## Key Differences: Equals vs Contains

| Aspect | AddEquals | AddContains |
|--------|-----------|-------------|
| **String matching** | Exact match | Substring match |
| **OData output** | `Name eq 'John'` | `contains(Name, 'john')` |
| **Use case** | Exact status codes, IDs | Search descriptions, tags |
| **Array support** | Yes (OR conditions) | Yes (OR conditions) |
| **Number support** | Yes | No (throws exception) |
| **Date support** | Yes | No (throws exception) |

## Summary

The `FilterRequest` tool provides a clean, fluent API for building OData filters with:

1. **Type safety**: Supports int, float, DateTimeOffset, string, and string[]
2. **Clear intent**: Separate methods for equality vs. contains
3. **Automatic formatting**: Handles OData syntax, escaping, and brackets
4. **Chainable**: All methods return `this` for method chaining
5. **Well-tested**: Comprehensive unit tests ensure correctness

Use `AddEquals` when you need **exact matching** and `AddContains` when you need **substring searching**.
