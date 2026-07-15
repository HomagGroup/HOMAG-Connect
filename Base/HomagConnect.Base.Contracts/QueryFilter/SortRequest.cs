namespace HomagConnect.Base.Contracts.QueryFilter;

/// <summary>
/// An ordered list of sort fields.
/// The client serializes this into a $orderby OData query parameter.
/// </summary>
public class SortRequest
{
    public IList<SortField> Fields { get; set; } = new List<SortField>();
}