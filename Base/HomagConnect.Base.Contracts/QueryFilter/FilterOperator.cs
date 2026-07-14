namespace HomagConnect.Base.Contracts.QueryFilter;

public enum FilterOperator
{
    /// <summary>
    /// Equality comparison.
    /// For strings: case-sensitive exact match (eq).
    /// For enums / lists: equality (one eq per value, joined with 'or').
    /// For numbers: equality.
    /// </summary>
    Eq,

    /// <summary>
    /// Contains comparison (case-insensitive).
    /// For strings: case-insensitive substring match using contains().
    /// For string arrays: multiple contains clauses joined with 'or'.
    /// </summary>
    Contains,

    /// <summary>Greater than or equal. For numbers and dates.</summary>
    Ge,

    /// <summary>Less than or equal. For numbers and dates.</summary>
    Le
}