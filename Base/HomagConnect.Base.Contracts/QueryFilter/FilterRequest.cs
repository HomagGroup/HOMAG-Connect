using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HomagConnect.Base.Contracts.QueryFilter
{
    /// <summary>
    /// A collection of filter conditions joined by AND.
    /// The client serializes this into a $filter OData query parameter.
    /// </summary>
    public class FilterRequest
    {
        public IList<FilterCondition> Conditions { get; set; } = new List<FilterCondition>();

        /// <summary>
        /// Adds an equality filter condition for the specified column and value.
        /// </summary>
        /// <param name="column">The column name to filter on.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset, string, string[].</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddEquals(string column, object? value)
        {
            Conditions.Add(new FilterCondition
            {
                Column = column,
                Operator = FilterOperator.Eq,
                Value = value
            });
            return this;
        }

        /// <summary>
        /// Creates a filter request with one equality condition for the specified property and value.
        /// </summary>
        /// <typeparam name="T">The type that contains the property.</typeparam>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset, string, string[].</param>
        /// <returns>A new <see cref="FilterRequest" /> with one equality condition.</returns>
        public static FilterRequest CreateEquals<T>(Expression<Func<T, object?>> propertySelector, object? value)
            => new FilterRequest().AddEquals(propertySelector, value);

        /// <summary>
        /// Adds an equality filter condition for the specified property and value.
        /// </summary>
        /// <typeparam name="T">The type that contains the property.</typeparam>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset, string, string[].</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddEquals<T>(Expression<Func<T, object?>> propertySelector, object? value)
            => AddEquals(GetPropertyName(propertySelector), value);

        /// <summary>
        /// Adds a contains filter condition for the specified column and value.
        /// Substring matching for strings.
        /// For string arrays, creates OR conditions with brackets.
        /// </summary>
        /// <param name="column">The column name to filter on.</param>
        /// <param name="value">The value to search for. Supported types: string, string[].</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddContains(string column, object? value)
        {
            Conditions.Add(new FilterCondition
            {
                Column = column,
                Operator = FilterOperator.Contains,
                Value = value
            });
            return this;
        }

        /// <summary>
        /// Adds a contains filter condition for the specified property and value.
        /// </summary>
        /// <typeparam name="T">The type that contains the property.</typeparam>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to search for. Supported types: string, string[].</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddContains<T>(Expression<Func<T, object?>> propertySelector, object? value)
            => AddContains(GetPropertyName(propertySelector), value);

        /// <summary>
        /// Adds a greater than or equal filter condition for the specified column and value.
        /// </summary>
        /// <param name="column">The column name to filter on.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset.</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddGreaterThanOrEqual(string column, object? value)
        {
            Conditions.Add(new FilterCondition
            {
                Column = column,
                Operator = FilterOperator.Ge,
                Value = value
            });
            return this;
        }

        /// <summary>
        /// Adds a greater than or equal filter condition for the specified property and value.
        /// </summary>
        /// <typeparam name="T">The type that contains the property.</typeparam>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset.</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddGreaterThanOrEqual<T>(Expression<Func<T, object?>> propertySelector, object? value)
            => AddGreaterThanOrEqual(GetPropertyName(propertySelector), value);

        /// <summary>
        /// Adds a less than or equal filter condition for the specified column and value.
        /// </summary>
        /// <param name="column">The column name to filter on.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset.</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddLessThanOrEqual(string column, object? value)
        {
            Conditions.Add(new FilterCondition
            {
                Column = column,
                Operator = FilterOperator.Le,
                Value = value
            });
            return this;
        }

        /// <summary>
        /// Adds a less than or equal filter condition for the specified property and value.
        /// </summary>
        /// <typeparam name="T">The type that contains the property.</typeparam>
        /// <param name="propertySelector">The property selector expression.</param>
        /// <param name="value">The value to compare. Supported types: int, float, DateTimeOffset.</param>
        /// <returns>The current FilterRequest instance for method chaining.</returns>
        public FilterRequest AddLessThanOrEqual<T>(Expression<Func<T, object?>> propertySelector, object? value)
            => AddLessThanOrEqual(GetPropertyName(propertySelector), value);

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
}
