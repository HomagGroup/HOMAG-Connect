namespace HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

/// <summary>
/// Filter operators supporting different data types
/// </summary>
public enum FilterOperator
{
    // Common operators
    Equal,
    NotEqual,

    // Numeric and DateTime operators
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Between,

    // String operators
    Contains,
    StartsWith,
    EndsWith,

    // Null checks
    IsNull,
    IsNotNull,

    // Collection operators
    In,
    NotIn
}