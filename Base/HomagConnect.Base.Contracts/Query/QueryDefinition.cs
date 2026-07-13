namespace HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

/// <summary>
/// Request model for querying with dynamic filtering and sorting.
/// Use the extension methods <c>ToKql(tableName)</c> or <c>ToKqlWithPagination(tableName)</c> 
/// to convert this query definition into a valid KQL (Kusto Query Language) string.
/// </summary>
/// <remarks>
/// Extension methods are available in <see cref="HomagConnect.Base.Contracts.Query.Extensions.QueryDefinitionKqlExtensions"/>.
/// </remarks>
/// <example>
/// {
///   "FromDate": "2023-01-01T00:00:00",
///   "ToDate": "2023-12-31T23:59:59",
///   "Skip": 0,
///   "Take": 100,
///   "SortBy": [
///     { "ColumnName": "Name", "Direction": "Ascending" }
///   ],
///   "Filter": {
///     "LogicalOperator": "And",
///     "Conditions": [
///       { "ColumnName": "Status", "Operator": "Equal", "Value": "Active" }
///     ]
///   }
/// </example>
public class QueryDefinition
{
    /// <summary>
    /// Start date for the query range
    /// </summary>
    public DateTime? FromDate { get; set; }

    /// <summary>
    /// End date for the query range
    /// </summary>
    public DateTime? ToDate { get; set; }

    /// <summary>
    /// Number of records to skip (pagination)
    /// </summary>
    public int Skip { get; set; } = 0;

    /// <summary>
    /// Number of records to take (pagination)
    /// </summary>
    public int Take { get; set; } = 100;

    /// <summary>
    /// List of sorting criteria to apply
    /// </summary>
    public List<SortCriteria> SortBy { get; set; } = new List<SortCriteria>();

    /// <summary>
    /// Root filter condition (can be a group or single condition)
    /// </summary>
    public FilterCondition? Filter { get; set; }
}