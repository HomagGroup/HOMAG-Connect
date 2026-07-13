using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

/// <summary>
/// Defines sorting criteria for a column
/// </summary>
public class SortCriteria
{
    public SortCriteria(string columnName)
    {
        ColumnName = columnName;
    }

    /// <summary>
    /// Column name to sort by
    /// </summary>
    [Required]
    public string ColumnName { get; set; }

    /// <summary>
    /// Sort direction
    /// </summary>
    public SortDirection Direction { get; set; } = SortDirection.Ascending;

    /// <summary>
    /// Order in which to apply this sort (for multi-column sorting)
    /// </summary>
    public int Priority { get; set; } = 0;
}