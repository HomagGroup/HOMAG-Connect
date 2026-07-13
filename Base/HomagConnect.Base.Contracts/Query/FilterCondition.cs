namespace HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

/// <summary>
/// Base class for filter conditions - can be a single condition or a group
/// </summary>
public class FilterCondition
{
    /// <summary>
    /// Logical operator to combine conditions (AND/OR)
    /// Only used when Conditions list is populated
    /// </summary>
    public LogicalOperator Logic { get; set; } = LogicalOperator.And;

    /// <summary>
    /// List of child filter conditions (for complex nested queries)
    /// </summary>
    public List<FilterCondition> Conditions { get; set; } = new List<FilterCondition>();

    /// <summary>
    /// Single filter rule (mutually exclusive with Conditions)
    /// </summary>
    public FilterRule? Rule { get; set; }

    /// <summary>
    /// Indicates if this is a group of conditions or a single rule
    /// </summary>
    public bool IsGroup => Conditions.Count > 0;
}