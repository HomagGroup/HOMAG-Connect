using System;
using System.Linq.Expressions;

namespace HomagConnect.Base.Contracts.QueryFilter
{
    /// <summary>
    /// Strongly typed filter request wrapper for a specific model type.
    /// </summary>
    /// <typeparam name="T">The model type used for property selector expressions.</typeparam>
    public sealed class FilterRequest<T>
    {
        private readonly FilterRequest _innerRequest;

        private FilterRequest(FilterRequest innerRequest)
        {
            _innerRequest = innerRequest ?? throw new ArgumentNullException(nameof(innerRequest));
        }

        /// <summary>
        /// Starts a typed filter request with one equality condition.
        /// </summary>
        /// <typeparam name="TProperty">The selected property type.</typeparam>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to compare.</param>
        /// <returns>A typed filter request.</returns>
        public static FilterRequest<T> AddEquals<TProperty>(Expression<Func<T, TProperty>> propertySelector, TProperty value)
            => new FilterRequest<T>(new FilterRequest()).AddEquals(ConvertSelector(propertySelector), value);

        /// <summary>
        /// Adds an equality condition for the same model type.
        /// </summary>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to compare.</param>
        /// <returns>The same typed filter request for chaining.</returns>
        public FilterRequest<T> AddEquals(Expression<Func<T, object?>> propertySelector, object? value)
        {
            _innerRequest.AddEquals(propertySelector, value);
            return this;
        }

        /// <summary>
        /// Converts the typed wrapper to a plain <see cref="FilterRequest" />.
        /// </summary>
        /// <param name="typedFilterRequest">The typed filter request.</param>
        public static implicit operator FilterRequest(FilterRequest<T> typedFilterRequest)
            => typedFilterRequest._innerRequest;

        private static Expression<Func<T, object?>> ConvertSelector<TProperty>(Expression<Func<T, TProperty>> propertySelector)
        {
            var parameter = propertySelector.Parameters[0];
            var convertedBody = Expression.Convert(propertySelector.Body, typeof(object));
            return Expression.Lambda<Func<T, object?>>(convertedBody, parameter);
        }
    }
}
