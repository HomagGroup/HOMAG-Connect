namespace HomagConnect.Base.Contracts.QueryFilter;

/// <summary>
/// A single filter condition: one column, one operator, one or two values.
/// </summary>
public class FilterCondition
{
    /// <summary>Column name, case-sensitive (e.g. "Name", "Status", "CreatedDate")</summary>
    public string Column { get; set; } = string.Empty;

    /// <summary>The operator to apply.</summary>
    public FilterOperator Operator { get; set; }

    /// The value to compare against.
    /// Supported types: string, int, double, DateTime, DateTimeOffset, Enum, IEnumerable of Enum.
    public object? Value { get; set; }
}