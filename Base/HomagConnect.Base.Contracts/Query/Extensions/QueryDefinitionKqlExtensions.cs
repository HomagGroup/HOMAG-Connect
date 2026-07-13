using System.Globalization;
using System.Text;
using HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

namespace HomagConnect.Base.Contracts.Query.Extensions;

/// <summary>
/// Extension methods for converting QueryDefinition to KQL (Kusto Query Language)
/// </summary>
public static class QueryDefinitionKqlExtensions
{
    /// <summary>
    /// Converts a QueryDefinition to a valid KQL (Kusto Query Language) query string.
    /// </summary>
    /// <param name="queryDefinition">The query definition to convert</param>
    /// <param name="tableName">The name of the table to query</param>
    /// <returns>A valid KQL query string</returns>
    /// <example>
    /// <code>
    /// var query = new QueryDefinition
    /// {
    ///     FromDate = DateTime.Parse("2023-01-01"),
    ///     ToDate = DateTime.Parse("2023-12-31"),
    ///     Filter = new FilterCondition
    ///     {
    ///         Rule = new FilterRule("Status") { Operator = FilterOperator.Equal, Value = "Active" }
    ///     },
    ///     SortBy = new List&lt;SortCriteria&gt; { new SortCriteria("Name") }
    /// };
    /// string kql = query.ToKql("MyTable");
    /// // Result: MyTable | where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.9999999Z) | where Status == "Active" | order by Name asc | take 100
    /// </code>
    /// </example>
    public static string ToKql(this QueryDefinition queryDefinition, string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
        {
            throw new ArgumentException("Table name cannot be null or empty", nameof(tableName));
        }

        var kql = new StringBuilder();
        kql.Append(tableName);

        // Add time range filter if FromDate or ToDate is specified
        if (queryDefinition.FromDate.HasValue || queryDefinition.ToDate.HasValue)
        {
            kql.Append(" | where ");
            var timeFilters = new List<string>();

            if (queryDefinition.FromDate.HasValue)
            {
                timeFilters.Add($"Timestamp >= datetime({FormatDateTime(queryDefinition.FromDate.Value)})");
            }

            if (queryDefinition.ToDate.HasValue)
            {
                timeFilters.Add($"Timestamp <= datetime({FormatDateTime(queryDefinition.ToDate.Value)})");
            }

            kql.Append(string.Join(" and ", timeFilters));
        }

        // Add filter conditions
        if (queryDefinition.Filter != null)
        {
            var filterKql = BuildFilterKql(queryDefinition.Filter);
            if (!string.IsNullOrEmpty(filterKql))
            {
                kql.Append(" | where ");
                kql.Append(filterKql);
            }
        }

        // Add sorting
        if (queryDefinition.SortBy != null && queryDefinition.SortBy.Count > 0)
        {
            var sortClauses = queryDefinition.SortBy
                .OrderBy(s => s.Priority)
                .Select(s => $"{s.ColumnName} {(s.Direction == SortDirection.Ascending ? "asc" : "desc")}");

            kql.Append(" | order by ");
            kql.Append(string.Join(", ", sortClauses));
        }

        // Add pagination - skip is handled by Kusto's syntax
        if (queryDefinition.Skip > 0)
        {
            // Kusto doesn't have a direct "skip" - we need to use a different approach
            // One common pattern is to use the "serialize" operator with row_number()
            // For simplicity, we'll document this limitation
            // Advanced pagination requires adding: | serialize rowNum = row_number() | where rowNum > skip
        }

        // Add limit (take)
        if (queryDefinition.Take > 0)
        {
            kql.Append($" | take {queryDefinition.Take}");
        }

        return kql.ToString();
    }

    /// <summary>
    /// Converts a QueryDefinition to a valid KQL query with pagination support using serialize.
    /// This method properly handles the Skip property for pagination.
    /// </summary>
    /// <param name="queryDefinition">The query definition to convert</param>
    /// <param name="tableName">The name of the table to query</param>
    /// <returns>A valid KQL query string with pagination</returns>
    public static string ToKqlWithPagination(this QueryDefinition queryDefinition, string tableName)
    {
        if (string.IsNullOrWhiteSpace(tableName))
        {
            throw new ArgumentException("Table name cannot be null or empty", nameof(tableName));
        }

        var kql = new StringBuilder();
        kql.Append(tableName);

        // Add time range filter if FromDate or ToDate is specified
        if (queryDefinition.FromDate.HasValue || queryDefinition.ToDate.HasValue)
        {
            kql.Append(" | where ");
            var timeFilters = new List<string>();

            if (queryDefinition.FromDate.HasValue)
            {
                timeFilters.Add($"Timestamp >= datetime({FormatDateTime(queryDefinition.FromDate.Value)})");
            }

            if (queryDefinition.ToDate.HasValue)
            {
                timeFilters.Add($"Timestamp <= datetime({FormatDateTime(queryDefinition.ToDate.Value)})");
            }

            kql.Append(string.Join(" and ", timeFilters));
        }

        // Add filter conditions
        if (queryDefinition.Filter != null)
        {
            var filterKql = BuildFilterKql(queryDefinition.Filter);
            if (!string.IsNullOrEmpty(filterKql))
            {
                kql.Append(" | where ");
                kql.Append(filterKql);
            }
        }

        // Add sorting
        if (queryDefinition.SortBy != null && queryDefinition.SortBy.Count > 0)
        {
            var sortClauses = queryDefinition.SortBy
                .OrderBy(s => s.Priority)
                .Select(s => $"{s.ColumnName} {(s.Direction == SortDirection.Ascending ? "asc" : "desc")}");

            kql.Append(" | order by ");
            kql.Append(string.Join(", ", sortClauses));
        }

        // Add pagination with serialize for proper skip support
        if (queryDefinition.Skip > 0 || queryDefinition.Take > 0)
        {
            kql.Append(" | serialize rowNum = row_number()");

            if (queryDefinition.Skip > 0)
            {
                kql.Append($" | where rowNum > {queryDefinition.Skip}");
            }

            if (queryDefinition.Take > 0)
            {
                kql.Append($" | take {queryDefinition.Take}");
            }
        }
        else if (queryDefinition.Take > 0)
        {
            kql.Append($" | take {queryDefinition.Take}");
        }

        return kql.ToString();
    }

    private static string BuildFilterKql(FilterCondition condition)
    {
        if (condition.IsGroup)
        {
            // Handle group of conditions
            var childFilters = condition.Conditions
                .Select(BuildFilterKql)
                .Where(f => !string.IsNullOrEmpty(f))
                .ToList();

            if (childFilters.Count == 0)
            {
                return string.Empty;
            }

            var logicOperator = condition.Logic == LogicalOperator.And ? " and " : " or ";
            return $"({string.Join(logicOperator, childFilters)})";
        }
        else if (condition.Rule != null)
        {
            // Handle single rule
            return BuildRuleKql(condition.Rule);
        }

        return string.Empty;
    }

    private static string BuildRuleKql(FilterRule rule)
    {
        var columnName = rule.ColumnName;
        var value = rule.Value;

        switch (rule.Operator)
        {
            case FilterOperator.Equal:
                return $"{columnName} == {FormatValue(value, rule.DataType)}";

            case FilterOperator.NotEqual:
                return $"{columnName} != {FormatValue(value, rule.DataType)}";

            case FilterOperator.GreaterThan:
                return $"{columnName} > {FormatValue(value, rule.DataType)}";

            case FilterOperator.GreaterThanOrEqual:
                return $"{columnName} >= {FormatValue(value, rule.DataType)}";

            case FilterOperator.LessThan:
                return $"{columnName} < {FormatValue(value, rule.DataType)}";

            case FilterOperator.LessThanOrEqual:
                return $"{columnName} <= {FormatValue(value, rule.DataType)}";

            case FilterOperator.Contains:
                return $"{columnName} contains {FormatValue(value, FieldDataType.String)}";

            case FilterOperator.StartsWith:
                return $"{columnName} startswith {FormatValue(value, FieldDataType.String)}";

            case FilterOperator.EndsWith:
                return $"{columnName} endswith {FormatValue(value, FieldDataType.String)}";

            case FilterOperator.IsNull:
                return $"isnull({columnName})";

            case FilterOperator.IsNotNull:
                return $"isnotnull({columnName})";

            case FilterOperator.In:
                if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    var values = enumerable.Cast<object>()
                        .Select(v => FormatValue(v, rule.DataType));
                    return $"{columnName} in ({string.Join(", ", values)})";
                }
                return $"{columnName} in ({FormatValue(value, rule.DataType)})";

            case FilterOperator.NotIn:
                if (value is System.Collections.IEnumerable enumerableNotIn && !(value is string))
                {
                    var values = enumerableNotIn.Cast<object>()
                        .Select(v => FormatValue(v, rule.DataType));
                    return $"{columnName} !in ({string.Join(", ", values)})";
                }
                return $"{columnName} !in ({FormatValue(value, rule.DataType)})";

            case FilterOperator.Between:
                // Expecting value to be an array or tuple with two elements
                if (value is object[] arr && arr.Length == 2)
                {
                    return $"{columnName} between ({FormatValue(arr[0], rule.DataType)} .. {FormatValue(arr[1], rule.DataType)})";
                }
                throw new InvalidOperationException("Between operator requires an array of exactly 2 values");

            default:
                throw new NotSupportedException($"Filter operator '{rule.Operator}' is not supported");
        }
    }

    private static string FormatValue(object value, FieldDataType dataType)
    {
        if (value == null)
        {
            return "null";
        }

        switch (dataType)
        {
            case FieldDataType.String:
            case FieldDataType.Enum:
                return $"\"{EscapeString(value.ToString())}\"";

            case FieldDataType.DateTime:
                if (value is DateTime dt)
                {
                    return $"datetime({FormatDateTime(dt)})";
                }
                if (value is DateTimeOffset dto)
                {
                    return $"datetime({FormatDateTime(dto.UtcDateTime)})";
                }
                return $"datetime({EscapeString(value.ToString())})";

            case FieldDataType.Boolean:
                if (value is bool b)
                {
                    return b ? "true" : "false";
                }
                return value.ToString().ToLowerInvariant();

            case FieldDataType.Integer:
            case FieldDataType.Decimal:
                return value.ToString();

            default:
                // Default to string representation
                return $"\"{EscapeString(value.ToString())}\"";
        }
    }

    private static string FormatDateTime(DateTime dateTime)
    {
        // KQL expects ISO 8601 format
        return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InvariantCulture);
    }

    private static string EscapeString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }

        // Escape special characters in KQL strings
        return value
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\n", "\\n")
            .Replace("\r", "\\r")
            .Replace("\t", "\\t");
    }
}
