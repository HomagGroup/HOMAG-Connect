using System.Globalization;

namespace HomagConnect.Base.Contracts.QueryFilter;

public static class ODataQueryBuilder
{
    /// <summary>
    /// Converts a FilterRequest into the value of the $filter query parameter.
    /// Returns null when no conditions are present.
    /// </summary>
    public static string? BuildFilter(FilterRequest? request)
    {
        if (request == null || request.Conditions.Count == 0)
            return null;

        var parts = request.Conditions.Select(BuildCondition);
        return string.Join(" and ", parts);
    }

    /// <summary>
    /// Converts a SortRequest into the value of the $orderby query parameter.
    /// Returns null when no fields are present.
    /// </summary>
    public static string? BuildOrderBy(OrderByRequest? request)
    {
        if (request == null || request.Fields.Count == 0)
            return null;

        return string.Join(",", request.Fields.Select(f =>
            $"{f.Column} {(f.Direction == OrderByDirection.Descending ? "desc" : "asc")}"));
    }

    private static string BuildCondition(FilterCondition c)
    {
        return c.Operator switch
        {
            FilterOperator.Eq => BuildEqCondition(c),
            FilterOperator.Contains => BuildContainsCondition(c),
            FilterOperator.Ge => $"{c.Column} ge {FormatValue(c.Value)}",
            FilterOperator.Le => $"{c.Column} le {FormatValue(c.Value)}",
            _ => throw new NotSupportedException($"Operator {c.Operator} is not supported.")
        };
    }

    private static string BuildEqCondition(FilterCondition c)
    {
        // String array: one eq clause per value, joined with 'or' in brackets
        if (c.Value is string[] stringArray)
        {
            var clauses = stringArray.Select(v => $"{c.Column} eq '{Escape(v)}'");
            return $"({string.Join(" or ", clauses)})";
        }

        // String: exact equality match
        if (c.Value is string s)
            return $"{c.Column} eq '{Escape(s)}'";

        // Enum list: one eq clause per value, joined with 'or'
        if (c.Value is IEnumerable<object> list)
        {
            var clauses = list.Select(v => $"{c.Column} eq '{v}'");
            return $"({string.Join(" or ", clauses)})";
        }

        // Number or DateTimeOffset: exact equality
        return $"{c.Column} eq {FormatValue(c.Value)}";
    }

    private static string BuildContainsCondition(FilterCondition c)
    {
        // String array: multiple contains clauses joined with 'or' in brackets
        if (c.Value is string[] stringArray)
        {
            var clauses = stringArray.Select(v => $"contains({c.Column}, '{Escape(v)}')");
            return $"({string.Join(" or ", clauses)})";
        }

        // Single string: contains
        if (c.Value is string s)
            return $"contains({c.Column}, '{Escape(s)}')";

        throw new NotSupportedException($"Contains operator only supports string or string[] values. Got: {c.Value?.GetType().Name}");
    }

    private static string FormatValue(object? value) => value switch
    {
        DateTime dt => dt.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
        DateTimeOffset dto => dto.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
        int or double or float or decimal => Convert.ToString(value, CultureInfo.InvariantCulture)!,
        _ => $"'{Escape(value?.ToString() ?? "")}'",
    };

    private static string Escape(string s) => s.Replace("'", "''");
}