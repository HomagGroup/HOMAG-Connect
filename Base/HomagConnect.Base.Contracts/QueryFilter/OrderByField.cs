namespace HomagConnect.Base.Contracts.QueryFilter;

public class OrderByField
{
    public string Column { get; set; } = string.Empty;
    public OrderByDirection Direction { get; set; } = OrderByDirection.Ascending;
}