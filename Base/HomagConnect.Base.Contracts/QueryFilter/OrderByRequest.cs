using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HomagConnect.Base.Contracts.QueryFilter;

/// <summary>
/// An ordered list of sort fields.
/// The client serializes this into a $orderby OData query parameter.
/// </summary>
public class OrderByRequest
{
    public IList<OrderByField> Fields { get; set; } = new List<OrderByField>();

    public OrderByRequest OrderBy(string column)
        => AddField(column, OrderByDirection.Ascending);

    public OrderByRequest OrderByDescending(string column)
        => AddField(column, OrderByDirection.Descending);

    public OrderByRequest ThenBy(string column)
        => AddField(column, OrderByDirection.Ascending);

    public OrderByRequest ThenByDescending(string column)
        => AddField(column, OrderByDirection.Descending);

    public OrderByRequest OrderBy<T>(Expression<Func<T, object?>> propertySelector)
        => OrderBy(GetPropertyName(propertySelector));

    public OrderByRequest OrderByDescending<T>(Expression<Func<T, object?>> propertySelector)
        => OrderByDescending(GetPropertyName(propertySelector));

    public OrderByRequest ThenBy<T>(Expression<Func<T, object?>> propertySelector)
        => ThenBy(GetPropertyName(propertySelector));

    public OrderByRequest ThenByDescending<T>(Expression<Func<T, object?>> propertySelector)
        => ThenByDescending(GetPropertyName(propertySelector));

    private OrderByRequest AddField(string column, OrderByDirection direction)
    {
        Fields.Add(new OrderByField
        {
            Column = column,
            Direction = direction
        });

        return this;
    }

    private static string GetPropertyName<T>(Expression<Func<T, object?>> propertySelector)
    {
        if (propertySelector is null)
        {
            throw new ArgumentNullException(nameof(propertySelector));
        }

        var memberExpression = propertySelector.Body as MemberExpression;
        if (memberExpression is null && propertySelector.Body is UnaryExpression unaryExpression)
        {
            memberExpression = unaryExpression.Operand as MemberExpression;
        }

        if (memberExpression?.Member is PropertyInfo propertyInfo)
        {
            return propertyInfo.Name;
        }

        throw new ArgumentException("Expression must select a property.", nameof(propertySelector));
    }
}
