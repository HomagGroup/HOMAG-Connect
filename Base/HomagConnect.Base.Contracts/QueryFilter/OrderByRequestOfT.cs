using System;
using System.Linq.Expressions;

namespace HomagConnect.Base.Contracts.QueryFilter;

/// <summary>
/// Strongly typed order-by request wrapper for a specific model type.
/// </summary>
/// <typeparam name="T">The model type used for property selector expressions.</typeparam>
public sealed class OrderByRequest<T>
{
    private readonly OrderByRequest _innerRequest;

    private OrderByRequest(OrderByRequest innerRequest)
    {
        _innerRequest = innerRequest ?? throw new ArgumentNullException(nameof(innerRequest));
    }

    public static OrderByRequest<T> OrderBy<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        => new OrderByRequest<T>(new OrderByRequest()).ThenBy(propertySelector);

    public static OrderByRequest<T> OrderByDescending<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        => new OrderByRequest<T>(new OrderByRequest()).ThenByDescending(propertySelector);

    public OrderByRequest<T> ThenBy<TProperty>(Expression<Func<T, TProperty>> propertySelector)
    {
        _innerRequest.ThenBy(ConvertSelector(propertySelector));
        return this;
    }

    public OrderByRequest<T> ThenByDescending<TProperty>(Expression<Func<T, TProperty>> propertySelector)
    {
        _innerRequest.ThenByDescending(ConvertSelector(propertySelector));
        return this;
    }

    public static implicit operator OrderByRequest(OrderByRequest<T> typedOrderByRequest)
        => typedOrderByRequest._innerRequest;

    private static Expression<Func<T, object?>> ConvertSelector<TProperty>(Expression<Func<T, TProperty>> propertySelector)
    {
        var parameter = propertySelector.Parameters[0];
        var convertedBody = Expression.Convert(propertySelector.Body, typeof(object));
        return Expression.Lambda<Func<T, object?>>(convertedBody, parameter);
    }
}
