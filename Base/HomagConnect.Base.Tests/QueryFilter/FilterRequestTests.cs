using HomagConnect.Base.Contracts.QueryFilter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace HomagConnect.Base.Tests.QueryFilter
{
    [TestClass]
    public class FilterRequestTests
    {
        [TestMethod]
        public void AddEquals_WithInt_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = request.AddEquals("Age", 42);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Age");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Eq);
            request.Conditions[0].Value.ShouldBe(42);
        }

        [TestMethod]
        public void AddEquals_WithFloat_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = request.AddEquals("Price", 19.99f);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Price");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Eq);
            request.Conditions[0].Value.ShouldBe(19.99f);
        }

        [TestMethod]
        public void AddEquals_WithDateTimeOffset_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();
            var date = new DateTimeOffset(2024, 1, 15, 10, 30, 0, TimeSpan.Zero);

            // Act
            var result = request.AddEquals("CreatedDate", date);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("CreatedDate");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Eq);
            request.Conditions[0].Value.ShouldBe(date);
        }

        [TestMethod]
        public void AddEquals_WithString_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = request.AddEquals("Name", "John");

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Name");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Eq);
            request.Conditions[0].Value.ShouldBe("John");
        }

        [TestMethod]
        public void AddEquals_WithStringArray_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();
            var values = new[] { "Active", "Pending", "Completed" };

            // Act
            var result = request.AddEquals("Status", values);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Status");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Eq);
            request.Conditions[0].Value.ShouldBe(values);
        }

        [TestMethod]
        public void AddContains_WithString_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = request.AddContains("Description", "keyword");

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Description");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Contains);
            request.Conditions[0].Value.ShouldBe("keyword");
        }

        [TestMethod]
        public void AddContains_WithStringArray_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();
            var values = new[] { "urgent", "high priority", "critical" };

            // Act
            var result = request.AddContains("Tags", values);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Tags");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Contains);
            request.Conditions[0].Value.ShouldBe(values);
        }

        [TestMethod]
        public void AddGreaterThanOrEqual_WithInt_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = request.AddGreaterThanOrEqual("Quantity", 100);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Quantity");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Ge);
            request.Conditions[0].Value.ShouldBe(100);
        }

        [TestMethod]
        public void AddLessThanOrEqual_WithFloat_ShouldAddCondition()
        {
            // Arrange
            var request = new FilterRequest();

            // Act
            var result = request.AddLessThanOrEqual("Weight", 50.5f);

            // Assert
            result.ShouldBeSameAs(request);
            request.Conditions.Count.ShouldBe(1);
            request.Conditions[0].Column.ShouldBe("Weight");
            request.Conditions[0].Operator.ShouldBe(FilterOperator.Le);
            request.Conditions[0].Value.ShouldBe(50.5f);
        }

        [TestMethod]
        public void FluentInterface_ShouldChainMultipleConditions()
        {
            // Arrange & Act
            var request = new FilterRequest()
                .AddEquals("Status", "Active")
                .AddContains("Name", "test")
                .AddGreaterThanOrEqual("Age", 18)
                .AddLessThanOrEqual("Price", 100.0f);

            // Assert
            request.Conditions.Count.ShouldBe(4);
            request.Conditions[0].Column.ShouldBe("Status");
            request.Conditions[1].Column.ShouldBe("Name");
            request.Conditions[2].Column.ShouldBe("Age");
            request.Conditions[3].Column.ShouldBe("Price");
        }
    }
}
