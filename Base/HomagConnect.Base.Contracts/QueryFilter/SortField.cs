namespace HomagConnect.Base.Contracts.QueryFilter;

public class SortField
{
    public string Column { get; set; } = string.Empty;
    public SortDirection Direction { get; set; } = SortDirection.Ascending;
}