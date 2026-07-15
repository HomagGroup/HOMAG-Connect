namespace HomagConnect.Base.Contracts.QueryFilter;

/// <summary>
/// An ordered list of sort fields.
/// The client serializes this into a $orderby OData query parameter.
/// </summary>
public class OrderByRequest
{
    public IList<OrderByField> Fields { get; set; } = new List<OrderByField>();
}