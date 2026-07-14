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
    }
}
