# FilterRequest Usage Examples

This document demonstrates how to use the `FilterRequest` tool to build OData filter queries with **type-safe property selectors** for compile-time safety and refactoring support.

## Overview

The `FilterRequest` class provides a fluent API for building OData `$filter` query parameters using lambda expressions. It supports:

- ✅ **Compile-time safety**: Property name validation at compile time
- ✅ **IntelliSense support**: Full IDE support for property selection
- ✅ **Refactoring-friendly**: Properties auto-update when model changes
- ✅ **Type-safe operations**: Equality (`eq`), contains, and range comparisons (`ge`, `le`)
- ✅ **Multiple data types**: int, float, DateTimeOffset, string, and string arrays

## Getting Started

### Define Your Model

First, define the model that represents your data structure:

```csharp
public class OrderModel
{
	public string Status { get; set; }
	public string Description { get; set; }
	public string CustomerName { get; set; }
	public int Quantity { get; set; }
	public float Price { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
}
```

## Basic Filtering

### Integer Equality

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.Quantity, 42);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Quantity eq 42"
```

### Float Equality

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.Price, 19.99f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Price eq 19.99"
```

### DateTimeOffset Equality

```csharp
var date = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.CreatedDate, date);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "CreatedDate eq 2024-01-15T10:30:00Z"
```

## String Operations

### String Equality (Exact Match)

The `AddEquals` method performs **exact matching** for strings:

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.CustomerName, "John");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "CustomerName eq 'John'"
```

### String Contains (Substring Matching)

The `AddContains` method performs **substring matching**:

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddContains(x => x.Description, "keyword");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "contains(Description, 'keyword')"
```

## Array Filtering with OR Conditions

### String Array with Equals

When you provide an array of strings with `AddEquals`, it creates OR conditions within brackets:

```csharp
var statuses = new[] { "Active", "Pending", "Completed" };
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.Status, statuses);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(Status eq 'Active' or Status eq 'Pending' or Status eq 'Completed')"
```

### String Array with Contains

When you provide an array of strings with `AddContains`, it creates multiple contains clauses with OR:

```csharp
var keywords = new[] { "urgent", "high priority", "critical" };
FilterRequest filter = FilterRequest<OrderModel>
	.AddContains(x => x.Description, keywords);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(contains(Description, 'urgent') or contains(Description, 'high priority') or contains(Description, 'critical'))"
```

## Range Comparisons

### Greater Than or Equal

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddGreaterThanOrEqual(x => x.Quantity, 100);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Quantity ge 100"
```

### Less Than or Equal

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddLessThanOrEqual(x => x.Price, 50.5f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Price le 50.5"
```

### Date Range

```csharp
var startDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
FilterRequest filter = FilterRequest<OrderModel>
	.AddGreaterThanOrEqual(x => x.CreatedDate, startDate);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "CreatedDate ge 2024-01-01T00:00:00Z"
```

## Complex Queries

### Multiple Conditions (AND)

All conditions are automatically joined with `and`:

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.Status, "Active")
	.AddContains(x => x.CustomerName, "test")
	.AddGreaterThanOrEqual(x => x.Quantity, 18)
	.AddLessThanOrEqual(x => x.Price, 100.0f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Status eq 'Active' and contains(CustomerName, 'test') and Quantity ge 18 and Price le 100"
```

### Mixed Array and Single Conditions

```csharp
var statuses = new[] { "Active", "Pending" };
var keywords = new[] { "urgent", "critical" };

FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.Status, statuses)
	.AddContains(x => x.Description, keywords)
	.AddGreaterThanOrEqual(x => x.Quantity, 5);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(Status eq 'Active' or Status eq 'Pending') and (contains(Description, 'urgent') or contains(Description, 'critical')) and Quantity ge 5"
```

### Real-World Example

```csharp
var statuses = new[] { "Active", "Pending", "InProgress" };
var keywords = new[] { "urgent", "critical" };
var startDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.Status, statuses)
	.AddContains(x => x.Description, keywords)
	.AddGreaterThanOrEqual(x => x.CreatedDate, startDate)
	.AddLessThanOrEqual(x => x.Price, 500.0f);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(Status eq 'Active' or Status eq 'Pending' or Status eq 'InProgress') 
//          and (contains(Description, 'urgent') or contains(Description, 'critical')) 
//          and CreatedDate ge 2024-01-01T00:00:00Z and Price le 500"
```

## Special Characters

The tool automatically escapes apostrophes in string values:

```csharp
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.CustomerName, "O'Brien");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "CustomerName eq 'O''Brien'"
```

With arrays:

```csharp
var names = new[] { "O'Brien", "D'Angelo" };
FilterRequest filter = FilterRequest<OrderModel>
	.AddEquals(x => x.CustomerName, names);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "(CustomerName eq 'O''Brien' or CustomerName eq 'D''Angelo')"
```

## Alternative Syntax Options

### Using New FilterRequest() with Type Parameter

If you prefer explicit instantiation:

```csharp
var filter = new FilterRequest()
	.AddEquals<OrderModel>(x => x.Status, "Active")
	.AddContains<OrderModel>(x => x.Description, "urgent")
	.AddGreaterThanOrEqual<OrderModel>(x => x.Quantity, 10);

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Status eq 'Active' and contains(Description, 'urgent') and Quantity ge 10"
```

### Using Static Factory Method

Use `FilterRequest.CreateEquals<T>()` for single-condition initialization:

```csharp
var filter = FilterRequest.CreateEquals<OrderModel>(x => x.Status, "Active");

var odataFilter = ODataQueryBuilder.BuildFilter(filter);
// Result: "Status eq 'Active'"
```

## Property Selector Rules

✅ **Valid:**
```csharp
.AddEquals(x => x.Status, "Active")          // Direct property access
.AddContains(x => x.Description, "test")     // Any property
.AddGreaterThanOrEqual(x => x.Quantity, 10)  // Value types
```

❌ **Invalid:**
```csharp
.AddEquals(x => x.GetStatus(), "Active")           // Method calls not allowed
.AddEquals(x => x.Status.ToUpper(), "ACTIVE")      // Transformations not allowed
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

1. ✅ **Compile-time safety**: Property names validated at compile time
2. ✅ **IntelliSense support**: Full IDE autocompletion
3. ✅ **Refactoring-friendly**: Auto-updates when models change
4. ✅ **Type safety**: Supports int, float, DateTimeOffset, string, and string[]
5. ✅ **Clear intent**: Separate methods for equality vs. contains
6. ✅ **Automatic formatting**: Handles OData syntax, escaping, and brackets
7. ✅ **Chainable**: All methods return `this` for method chaining
8. ✅ **Well-tested**: Comprehensive unit tests ensure correctness

**Recommended Pattern:** Use `FilterRequest<T>` with property selectors (lambda expressions) for the best development experience.

Use `AddEquals` when you need **exact matching** and `AddContains` when you need **substring searching**.
