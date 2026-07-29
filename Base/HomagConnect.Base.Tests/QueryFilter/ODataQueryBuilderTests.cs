using HomagConnect.Base.Contracts.QueryFilter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace HomagConnect.Base.Tests.QueryFilter
{
    [TestClass]
    public class ODataQueryBuilderTests
    {
        #region BuildFilter - Equals Tests

        [TestMethod]
        public void BuildFilter_WithIntEquals_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var request = new FilterRequest().AddEquals("Age", 42);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Age eq 42");
        }

        [TestMethod]
        public void BuildFilter_WithFloatEquals_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var request = new FilterRequest().AddEquals("Price", 19.99f);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Price eq 19.99");
        }

        [TestMethod]
        public void BuildFilter_WithDateTimeOffsetEquals_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var date = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);
            var request = new FilterRequest().AddEquals("CreatedDate", date);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("CreatedDate eq 2024-01-15T10:30:00Z");
        }

        [TestMethod]
        public void BuildFilter_WithStringEquals_ShouldGenerateExactMatch()
        {
            // Arrange
            var request = new FilterRequest().AddEquals("Name", "John");

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Name eq 'John'");
        }

        [TestMethod]
        public void BuildFilter_WithStringEqualsContainingApostrophe_ShouldEscapeCorrectly()
        {
            // Arrange
            var request = new FilterRequest().AddEquals("Name", "O'Brien");

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Name eq 'O''Brien'");
        }

        [TestMethod]
        public void BuildFilter_WithStringArrayEquals_ShouldGenerateOrConditionsWithBrackets()
        {
            // Arrange
            var values = new[] { "Active", "Pending", "Completed" };
            var request = new FilterRequest().AddEquals("Status", values);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("(Status eq 'Active' or Status eq 'Pending' or Status eq 'Completed')");
        }

        [TestMethod]
        public void BuildFilter_WithStringArrayEqualsContainingApostrophes_ShouldEscapeCorrectly()
        {
            // Arrange
            var values = new[] { "O'Brien", "D'Angelo" };
            var request = new FilterRequest().AddEquals("Name", values);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("(Name eq 'O''Brien' or Name eq 'D''Angelo')");
        }

        #endregion

        #region BuildFilter - Contains Tests

        [TestMethod]
        public void BuildFilter_WithStringContains_ShouldGenerateCaseInsensitiveContains()
        {
            // Arrange
            var request = new FilterRequest().AddContains("Description", "keyword");

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("contains(Description, 'keyword')");
        }

        [TestMethod]
        public void BuildFilter_WithStringContainsWithApostrophe_ShouldEscapeCorrectly()
        {
            // Arrange
            var request = new FilterRequest().AddContains("Description", "it's");

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("contains(Description, 'it''s')");
        }

        [TestMethod]
        public void BuildFilter_WithStringArrayContains_ShouldGenerateOrConditionsWithBrackets()
        {
            // Arrange
            var values = new[] { "urgent", "high priority", "critical" };
            var request = new FilterRequest().AddContains("Tags", values);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("(contains(Tags, 'urgent') or contains(Tags, 'high priority') or contains(Tags, 'critical'))");
        }

        [TestMethod]
        public void BuildFilter_WithStringArrayContainsWithApostrophes_ShouldEscapeCorrectly()
        {
            // Arrange
            var values = new[] { "it's", "we're" };
            var request = new FilterRequest().AddContains("Tags", values);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("(contains(Tags, 'it''s') or contains(Tags, 'we''re'))");
        }

        #endregion

        #region BuildFilter - Greater/Less Than Tests

        [TestMethod]
        public void BuildFilter_WithGreaterThanOrEqual_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var request = new FilterRequest().AddGreaterThanOrEqual("Quantity", 100);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Quantity ge 100");
        }

        [TestMethod]
        public void BuildFilter_WithLessThanOrEqual_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var request = new FilterRequest().AddLessThanOrEqual("Weight", 50.5f);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Weight le 50.5");
        }

        [TestMethod]
        public void BuildFilter_WithDateTimeOffsetGreaterThan_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var date = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var request = new FilterRequest().AddGreaterThanOrEqual("CreatedDate", date);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("CreatedDate ge 2024-01-01T00:00:00Z");
        }

        #endregion

        #region BuildFilter - Multiple Conditions Tests

        [TestMethod]
        public void BuildFilter_WithMultipleConditions_ShouldJoinWithAnd()
        {
            // Arrange
            var request = new FilterRequest()
                .AddEquals("Status", "Active")
                .AddGreaterThanOrEqual("Age", 18)
                .AddLessThanOrEqual("Price", 100.0f);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Status eq 'Active' and Age ge 18 and Price le 100");
        }

        [TestMethod]
        public void BuildFilter_WithMixedEqualsAndContains_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var request = new FilterRequest()
                .AddEquals("Status", "Active")
                .AddContains("Name", "test");

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("Status eq 'Active' and contains(Name, 'test')");
        }

        [TestMethod]
        public void BuildFilter_WithStringArrayAndOtherConditions_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var statuses = new[] { "Active", "Pending" };
            var request = new FilterRequest()
                .AddEquals("Status", statuses)
                .AddGreaterThanOrEqual("Age", 18);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("(Status eq 'Active' or Status eq 'Pending') and Age ge 18");
        }

        [TestMethod]
        public void BuildFilter_WithComplexMixedConditions_ShouldGenerateCorrectFilter()
        {
            // Arrange
            var tags = new[] { "urgent", "critical" };
            var request = new FilterRequest()
                .AddContains("Tags", tags)
                .AddEquals("Status", "Open")
                .AddGreaterThanOrEqual("Priority", 5);

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBe("(contains(Tags, 'urgent') or contains(Tags, 'critical')) and Status eq 'Open' and Priority ge 5");
        }

        #endregion

        #region BuildFilter - Edge Cases

        [TestMethod]
        public void BuildFilter_WithNullRequest_ShouldReturnNull()
        {
            // Act
            var result = ODataQueryBuilder.BuildFilter(null);

            // Assert
            result.ShouldBeNull();
        }

        [TestMethod]
        public void BuildFilter_WithEmptyConditions_ShouldReturnNull()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = ODataQueryBuilder.BuildFilter(request);

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        #region BuildOrderBy Tests

        [TestMethod]
        public void BuildOrderBy_WithSingleAscendingField_ShouldGenerateCorrectOrderBy()
        {
            // Arrange
            var request = new OrderByRequest();
            request.Fields.Add(new OrderByField { Column = "Name", Direction = OrderByDirection.Ascending });

            // Act
            var result = ODataQueryBuilder.BuildOrderBy(request);

            // Assert
            result.ShouldBe("Name asc");
        }

        [TestMethod]
        public void BuildOrderBy_WithSingleDescendingField_ShouldGenerateCorrectOrderBy()
        {
            // Arrange
            var request = new OrderByRequest();
            request.Fields.Add(new OrderByField { Column = "CreatedDate", Direction = OrderByDirection.Descending });

            // Act
            var result = ODataQueryBuilder.BuildOrderBy(request);

            // Assert
            result.ShouldBe("CreatedDate desc");
        }

        [TestMethod]
        public void BuildOrderBy_WithMultipleFields_ShouldGenerateCorrectOrderBy()
        {
            // Arrange
            var request = new OrderByRequest();
            request.Fields.Add(new OrderByField { Column = "Status", Direction = OrderByDirection.Ascending });
            request.Fields.Add(new OrderByField { Column = "Priority", Direction = OrderByDirection.Descending });
            request.Fields.Add(new OrderByField { Column = "Name", Direction = OrderByDirection.Ascending });

            // Act
            var result = ODataQueryBuilder.BuildOrderBy(request);

            // Assert
            result.ShouldBe("Status asc,Priority desc,Name asc");
        }

        [TestMethod]
        public void BuildOrderBy_WithTypedOrderByAndThenBy_ShouldGenerateCorrectOrderBy()
        {
            // Arrange
            OrderByRequest request = OrderByRequest<SampleOrderByModel>
                .OrderBy(x => x.Name)
                .ThenByDescending(x => x.CreatedDate)
                .ThenBy(x => x.Priority);

            // Act
            var result = ODataQueryBuilder.BuildOrderBy(request);

            // Assert
            result.ShouldBe("Name asc,CreatedDate desc,Priority asc");
        }

        [TestMethod]
        public void BuildOrderBy_WithTypedOrderByUsingMethodSelector_ShouldThrowArgumentException()
        {
            // Act
            var exception = Should.Throw<ArgumentException>(() => OrderByRequest<SampleOrderByModel>.OrderBy(x => x.GetDisplayName()));

            // Assert
            exception.ParamName.ShouldBe("propertySelector");
            exception.Message.ShouldContain("Expression must select a property.");
        }

        [TestMethod]
        public void BuildOrderBy_WithNullRequest_ShouldReturnNull()
        {
            // Act
            var result = ODataQueryBuilder.BuildOrderBy(null);

            // Assert
            result.ShouldBeNull();
        }

        [TestMethod]
        public void BuildOrderBy_WithEmptyFields_ShouldReturnNull()
        {
            // Arrange
            var request = new OrderByRequest();

            // Act
            var result = ODataQueryBuilder.BuildOrderBy(request);

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        private sealed class SampleOrderByModel
        {
            public string Name { get; set; } = string.Empty;

            public DateTimeOffset CreatedDate { get; set; }

            public int Priority { get; set; }

            public string GetDisplayName()
            {
                return Name;
            }
        }
    }
}
