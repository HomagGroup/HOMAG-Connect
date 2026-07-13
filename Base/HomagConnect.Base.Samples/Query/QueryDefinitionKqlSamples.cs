using HomagConnect.Base.Contracts.Query.Extensions;
using HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;

namespace HomagConnect.Base.Samples.Query;

/// <summary>
/// Usage samples demonstrating how to convert QueryDefinition to KQL (Kusto Query Language)
/// </summary>
public class QueryDefinitionKqlSamples
{
    /// <summary>
    /// Example 1: Basic query with date range and limit
    /// </summary>
    public static void BasicDateRangeQuery()
    {
        var query = new QueryDefinition
        {
            FromDate = DateTime.Parse("2023-01-01"),
            ToDate = DateTime.Parse("2023-12-31"),
            Take = 100
        };

        string kql = query.ToKql("ProductionData");
        Console.WriteLine("Basic Date Range Query:");
        Console.WriteLine(kql);
        // Output: ProductionData | where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.0000000Z) | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Example 2: Query with simple filter
    /// </summary>
    public static void SimpleFilterQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Status")
                {
                    Operator = FilterOperator.Equal,
                    Value = "Active",
                    DataType = FieldDataType.String
                }
            },
            Take = 50
        };

        string kql = query.ToKql("Orders");
        Console.WriteLine("Simple Filter Query:");
        Console.WriteLine(kql);
        // Output: Orders | where Status == "Active" | take 50
        Console.WriteLine();
    }

    /// <summary>
    /// Example 3: Query with multiple conditions (AND logic)
    /// </summary>
    public static void ComplexAndFilterQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Logic = LogicalOperator.And,
                Conditions = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Rule = new FilterRule("Status")
                        {
                            Operator = FilterOperator.Equal,
                            Value = "Active",
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("Quantity")
                        {
                            Operator = FilterOperator.GreaterThan,
                            Value = 10,
                            DataType = FieldDataType.Integer
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("Priority")
                        {
                            Operator = FilterOperator.In,
                            Value = new[] { "High", "Critical" },
                            DataType = FieldDataType.String
                        }
                    }
                }
            },
            Take = 100
        };

        string kql = query.ToKql("WorkOrders");
        Console.WriteLine("Complex AND Filter Query:");
        Console.WriteLine(kql);
        // Output: WorkOrders | where (Status == "Active" and Quantity > 10 and Priority in ("High", "Critical")) | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Example 4: Query with OR logic
    /// </summary>
    public static void OrFilterQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Logic = LogicalOperator.Or,
                Conditions = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Rule = new FilterRule("Status")
                        {
                            Operator = FilterOperator.Equal,
                            Value = "Pending",
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("Status")
                        {
                            Operator = FilterOperator.Equal,
                            Value = "InProgress",
                            DataType = FieldDataType.String
                        }
                    }
                }
            },
            Take = 100
        };

        string kql = query.ToKql("Tasks");
        Console.WriteLine("OR Filter Query:");
        Console.WriteLine(kql);
        // Output: Tasks | where (Status == "Pending" or Status == "InProgress") | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Example 5: Query with nested filter groups
    /// </summary>
    public static void NestedFilterQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Logic = LogicalOperator.And,
                Conditions = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Rule = new FilterRule("Category")
                        {
                            Operator = FilterOperator.Equal,
                            Value = "Manufacturing",
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Logic = LogicalOperator.Or,
                        Conditions = new List<FilterCondition>
                        {
                            new FilterCondition
                            {
                                Rule = new FilterRule("Machine")
                                {
                                    Operator = FilterOperator.StartsWith,
                                    Value = "CNC",
                                    DataType = FieldDataType.String
                                }
                            },
                            new FilterCondition
                            {
                                Rule = new FilterRule("Machine")
                                {
                                    Operator = FilterOperator.StartsWith,
                                    Value = "LASER",
                                    DataType = FieldDataType.String
                                }
                            }
                        }
                    }
                }
            },
            Take = 100
        };

        string kql = query.ToKql("MachineEvents");
        Console.WriteLine("Nested Filter Query:");
        Console.WriteLine(kql);
        // Output: MachineEvents | where (Category == "Manufacturing" and (Machine startswith "CNC" or Machine startswith "LASER")) | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Example 6: Query with sorting
    /// </summary>
    public static void SortingQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Status")
                {
                    Operator = FilterOperator.NotEqual,
                    Value = "Deleted",
                    DataType = FieldDataType.String
                }
            },
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("Priority") { Direction = SortDirection.Descending, Priority = 0 },
                new SortCriteria("CreatedAt") { Direction = SortDirection.Ascending, Priority = 1 }
            },
            Take = 50
        };

        string kql = query.ToKql("Issues");
        Console.WriteLine("Sorting Query:");
        Console.WriteLine(kql);
        // Output: Issues | where Status != "Deleted" | order by Priority desc, CreatedAt asc | take 50
        Console.WriteLine();
    }

    /// <summary>
    /// Example 7: Full-featured query with date range, filters, and sorting
    /// </summary>
    public static void ComprehensiveQuery()
    {
        var query = new QueryDefinition
        {
            FromDate = DateTime.Parse("2023-01-01"),
            ToDate = DateTime.Parse("2023-12-31"),
            Filter = new FilterCondition
            {
                Logic = LogicalOperator.And,
                Conditions = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Rule = new FilterRule("WorkstationType")
                        {
                            Operator = FilterOperator.In,
                            Value = new[] { "CNC", "EdgeBanding", "Saw" },
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("ItemsProcessed")
                        {
                            Operator = FilterOperator.GreaterThanOrEqual,
                            Value = 100,
                            DataType = FieldDataType.Integer
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("ErrorRate")
                        {
                            Operator = FilterOperator.LessThan,
                            Value = 0.05,
                            DataType = FieldDataType.Decimal
                        }
                    }
                }
            },
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("ItemsProcessed") { Direction = SortDirection.Descending, Priority = 0 }
            },
            Take = 100
        };

        string kql = query.ToKql("ProductionMetrics");
        Console.WriteLine("Comprehensive Query:");
        Console.WriteLine(kql);
        // Output: ProductionMetrics | where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.0000000Z) | where (WorkstationType in ("CNC", "EdgeBanding", "Saw") and ItemsProcessed >= 100 and ErrorRate < 0.05) | order by ItemsProcessed desc | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Example 8: Query with pagination (skip and take)
    /// </summary>
    public static void PaginationQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Status")
                {
                    Operator = FilterOperator.Equal,
                    Value = "Completed",
                    DataType = FieldDataType.String
                }
            },
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("CompletedAt") { Direction = SortDirection.Descending }
            },
            Skip = 100,
            Take = 50
        };

        string kql = query.ToKqlWithPagination("Orders");
        Console.WriteLine("Pagination Query:");
        Console.WriteLine(kql);
        // Output: Orders | where Status == "Completed" | order by CompletedAt desc | serialize rowNum = row_number() | where rowNum > 100 | take 50
        Console.WriteLine();
    }

    /// <summary>
    /// Example 9: Query with string operations
    /// </summary>
    public static void StringOperationsQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Logic = LogicalOperator.And,
                Conditions = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Rule = new FilterRule("CustomerName")
                        {
                            Operator = FilterOperator.Contains,
                            Value = "GmbH",
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("Email")
                        {
                            Operator = FilterOperator.EndsWith,
                            Value = "@example.com",
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("OrderNumber")
                        {
                            Operator = FilterOperator.StartsWith,
                            Value = "ORD",
                            DataType = FieldDataType.String
                        }
                    }
                }
            },
            Take = 100
        };

        string kql = query.ToKql("Customers");
        Console.WriteLine("String Operations Query:");
        Console.WriteLine(kql);
        // Output: Customers | where (CustomerName contains "GmbH" and Email endswith "@example.com" and OrderNumber startswith "ORD") | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Example 10: Query with null checks
    /// </summary>
    public static void NullCheckQuery()
    {
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Logic = LogicalOperator.And,
                Conditions = new List<FilterCondition>
                {
                    new FilterCondition
                    {
                        Rule = new FilterRule("CompletedAt")
                        {
                            Operator = FilterOperator.IsNotNull,
                            DataType = FieldDataType.DateTime
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("DeletedAt")
                        {
                            Operator = FilterOperator.IsNull,
                            DataType = FieldDataType.DateTime
                        }
                    }
                }
            },
            Take = 100
        };

        string kql = query.ToKql("ProcessedOrders");
        Console.WriteLine("Null Check Query:");
        Console.WriteLine(kql);
        // Output: ProcessedOrders | where (isnotnull(CompletedAt) and isnull(DeletedAt)) | take 100
        Console.WriteLine();
    }

    /// <summary>
    /// Run all samples
    /// </summary>
    public static void RunAllSamples()
    {
        Console.WriteLine("=== QueryDefinition to KQL Conversion Samples ===");
        Console.WriteLine();

        BasicDateRangeQuery();
        SimpleFilterQuery();
        ComplexAndFilterQuery();
        OrFilterQuery();
        NestedFilterQuery();
        SortingQuery();
        ComprehensiveQuery();
        PaginationQuery();
        StringOperationsQuery();
        NullCheckQuery();

        Console.WriteLine("=== End of Samples ===");
    }
}
