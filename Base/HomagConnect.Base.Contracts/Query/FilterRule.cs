using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

/// <summary>
/// Represents a single filter rule with field, operator, and value(s)
/// </summary>
public class FilterRule
{
    public FilterRule(string columnName)
    {
        ColumnName = columnName;
    }

    /// <summary>
    /// Column name to filter on
    /// </summary>
    [Required]
    public string ColumnName { get; set; }

    /// <summary>
    /// Comparison operator
    /// </summary>
    [Required]
    public FilterOperator Operator { get; set; }

    /// <summary>
    /// Value to compare against (for Equal, NotEqual, GreaterThan, etc.)
    /// Can be string, number, date, or enum value
    /// </summary>
    public object Value { get; set; }

    /// <summary>
    /// Data type of the field (helps with SQL generation and validation)
    /// </summary>
    public FieldDataType DataType { get; set; }
}