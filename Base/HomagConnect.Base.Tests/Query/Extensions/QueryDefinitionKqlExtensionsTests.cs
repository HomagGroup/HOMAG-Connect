using HomagConnect.Base.Contracts.Query.Extensions;
using HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models;
using HomagConnect.Base.TestBase.Attributes;

using Shouldly;
using SortDirection = HomagConnect.Base.Contracts.Query.HomagGroup.IntelliInsights.Applications.API.Models.SortDirection;

namespace HomagConnect.Base.Tests.Query.Extensions;

/// <summary>
/// Tests for QueryDefinition to KQL conversion
/// </summary>
[TestClass]
[TemporaryDisabledOnServer(2026,8,1,"DF-Insights")]
public class QueryDefinitionKqlExtensionsTests
{
    [TestMethod]
    public void ToKql_WithTableNameOnly_ReturnsBasicQuery()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldBe("MyTable | take 100");
    }

    [TestMethod]
    public void ToKql_WithFromDate_AddsTimestampFilter()
    {
        // Arrange
        var query = new QueryDefinition
        {
            FromDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z)");
        kql.ShouldContain("| take 100");
    }

    [TestMethod]
    public void ToKql_WithToDate_AddsTimestampFilter()
    {
        // Arrange
        var query = new QueryDefinition
        {
            ToDate = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Timestamp <= datetime(2023-12-31T23:59:59.0000000Z)");
        kql.ShouldContain("| take 100");
    }

    [TestMethod]
    public void ToKql_WithFromDateAndToDate_AddsBothTimestampFilters()
    {
        // Arrange
        var query = new QueryDefinition
        {
            FromDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ToDate = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.0000000Z)");
        kql.ShouldContain("| take 100");
    }

    [TestMethod]
    public void ToKql_WithSimpleEqualFilter_AddsWhereClause()
    {
        // Arrange
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
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Status == \"Active\"");
        kql.ShouldContain("| take 100");
    }

    [TestMethod]
    public void ToKql_WithNotEqualFilter_AddsNotEqualOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Status")
                {
                    Operator = FilterOperator.NotEqual,
                    Value = "Inactive",
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Status != \"Inactive\"");
    }

    [TestMethod]
    public void ToKql_WithNumericGreaterThanFilter_AddsComparisonOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Quantity")
                {
                    Operator = FilterOperator.GreaterThan,
                    Value = 10,
                    DataType = FieldDataType.Integer
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Quantity > 10");
    }

    [TestMethod]
    public void ToKql_WithContainsFilter_AddsContainsOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Name")
                {
                    Operator = FilterOperator.Contains,
                    Value = "test",
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Name contains \"test\"");
    }

    [TestMethod]
    public void ToKql_WithStartsWithFilter_AddsStartsWithOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Code")
                {
                    Operator = FilterOperator.StartsWith,
                    Value = "PRE",
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Code startswith \"PRE\"");
    }

    [TestMethod]
    public void ToKql_WithEndsWithFilter_AddsEndsWithOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Email")
                {
                    Operator = FilterOperator.EndsWith,
                    Value = "@example.com",
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Email endswith \"@example.com\"");
    }

    [TestMethod]
    public void ToKql_WithIsNullFilter_AddsIsNullFunction()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("DeletedAt")
                {
                    Operator = FilterOperator.IsNull,
                    DataType = FieldDataType.DateTime
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where isnull(DeletedAt)");
    }

    [TestMethod]
    public void ToKql_WithIsNotNullFilter_AddsIsNotNullFunction()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("CompletedAt")
                {
                    Operator = FilterOperator.IsNotNull,
                    DataType = FieldDataType.DateTime
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where isnotnull(CompletedAt)");
    }

    [TestMethod]
    public void ToKql_WithInFilter_AddsInOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Status")
                {
                    Operator = FilterOperator.In,
                    Value = new[] { "Active", "Pending", "InProgress" },
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Status in (\"Active\", \"Pending\", \"InProgress\")");
    }

    [TestMethod]
    public void ToKql_WithNotInFilter_AddsNotInOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Status")
                {
                    Operator = FilterOperator.NotIn,
                    Value = new[] { "Cancelled", "Deleted" },
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Status !in (\"Cancelled\", \"Deleted\")");
    }

    [TestMethod]
    public void ToKql_WithBetweenFilter_AddsBetweenOperator()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Price")
                {
                    Operator = FilterOperator.Between,
                    Value = new object[] { 10, 100 },
                    DataType = FieldDataType.Decimal
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where Price between (10 .. 100)");
    }

    [TestMethod]
    public void ToKql_WithAndFilterGroup_CombinesWithAnd()
    {
        // Arrange
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
                            Value = 0,
                            DataType = FieldDataType.Integer
                        }
                    }
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where (Status == \"Active\" and Quantity > 0)");
    }

    [TestMethod]
    public void ToKql_WithOrFilterGroup_CombinesWithOr()
    {
        // Arrange
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
                            Value = "Active",
                            DataType = FieldDataType.String
                        }
                    },
                    new FilterCondition
                    {
                        Rule = new FilterRule("Status")
                        {
                            Operator = FilterOperator.Equal,
                            Value = "Pending",
                            DataType = FieldDataType.String
                        }
                    }
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where (Status == \"Active\" or Status == \"Pending\")");
    }

    [TestMethod]
    public void ToKql_WithNestedFilterGroups_GeneratesComplexFilter()
    {
        // Arrange
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
                            Value = "Electronics",
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
                                Rule = new FilterRule("Brand")
                                {
                                    Operator = FilterOperator.Equal,
                                    Value = "Samsung",
                                    DataType = FieldDataType.String
                                }
                            },
                            new FilterCondition
                            {
                                Rule = new FilterRule("Brand")
                                {
                                    Operator = FilterOperator.Equal,
                                    Value = "Apple",
                                    DataType = FieldDataType.String
                                }
                            }
                        }
                    }
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("where (Category == \"Electronics\" and (Brand == \"Samsung\" or Brand == \"Apple\"))");
    }

    [TestMethod]
    public void ToKql_WithSingleSortAscending_AddsOrderByClause()
    {
        // Arrange
        var query = new QueryDefinition
        {
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("Name") { Direction = SortDirection.Ascending }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("| order by Name asc");
    }

    [TestMethod]
    public void ToKql_WithSingleSortDescending_AddsOrderByClause()
    {
        // Arrange
        var query = new QueryDefinition
        {
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("CreatedAt") { Direction = SortDirection.Descending }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("| order by CreatedAt desc");
    }

    [TestMethod]
    public void ToKql_WithMultipleSorts_AddsAllSortCriteria()
    {
        // Arrange
        var query = new QueryDefinition
        {
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("Category") { Direction = SortDirection.Ascending, Priority = 0 },
                new SortCriteria("Name") { Direction = SortDirection.Ascending, Priority = 1 },
                new SortCriteria("Price") { Direction = SortDirection.Descending, Priority = 2 }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("| order by Category asc, Name asc, Price desc");
    }

    [TestMethod]
    public void ToKql_WithTakeOnly_AddsLimitClause()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Take = 50
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldBe("MyTable | take 50");
    }

    [TestMethod]
    public void ToKql_WithCompleteQuery_GeneratesFullKqlString()
    {
        // Arrange
        var query = new QueryDefinition
        {
            FromDate = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            ToDate = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
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
                            Value = 5,
                            DataType = FieldDataType.Integer
                        }
                    }
                }
            },
            SortBy = new List<SortCriteria>
            {
                new SortCriteria("Name") { Direction = SortDirection.Ascending }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("ProductionData");

        // Assert
        kql.ShouldContain("ProductionData");
        kql.ShouldContain("where Timestamp >= datetime(2023-01-01T00:00:00.0000000Z) and Timestamp <= datetime(2023-12-31T23:59:59.0000000Z)");
        kql.ShouldContain("where (Status == \"Active\" and Quantity > 5)");
        kql.ShouldContain("| order by Name asc");
        kql.ShouldContain("| take 100");
    }

    [TestMethod]
    public void ToKqlWithPagination_WithSkipAndTake_AddsSerializeAndFilters()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Skip = 50,
            Take = 25
        };

        // Act
        var kql = query.ToKqlWithPagination("MyTable");

        // Assert
        kql.ShouldContain("| serialize rowNum = row_number()");
        kql.ShouldContain("| where rowNum > 50");
        kql.ShouldContain("| take 25");
    }

    [TestMethod]
    public void ToKqlWithPagination_WithSkipOnly_AddsSerializeAndFilter()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Skip = 100,
            Take = 0
        };

        // Act
        var kql = query.ToKqlWithPagination("MyTable");

        // Assert
        kql.ShouldContain("| serialize rowNum = row_number()");
        kql.ShouldContain("| where rowNum > 100");
        kql.ShouldNotContain("| take");
    }

    [TestMethod]
    public void ToKql_WithSpecialCharactersInString_EscapesCorrectly()
    {
        // Arrange
        var query = new QueryDefinition
        {
            Filter = new FilterCondition
            {
                Rule = new FilterRule("Description")
                {
                    Operator = FilterOperator.Contains,
                    Value = "test\"value\nwith\\special",
                    DataType = FieldDataType.String
                }
            },
            Take = 100
        };

        // Act
        var kql = query.ToKql("MyTable");

        // Assert
        kql.ShouldContain("test\\\"value\\nwith\\\\special");
    }

    [TestMethod]
    public void ToKql_WithNullTableName_ThrowsArgumentException()
    {
        // Arrange
        var query = new QueryDefinition();

        // Act & Assert
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Should.Throw<ArgumentException>(() => query.ToKql(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [TestMethod]
    public void ToKql_WithEmptyTableName_ThrowsArgumentException()
    {
        // Arrange
        var query = new QueryDefinition();

        // Act & Assert
        Should.Throw<ArgumentException>(() => query.ToKql(string.Empty));
    }
}
