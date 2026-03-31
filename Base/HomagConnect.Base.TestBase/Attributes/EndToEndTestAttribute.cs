using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.Base.TestBase.Attributes;

/// <summary>
/// Applies end-to-end-related categories to a test, including area-based categories and an optional <see cref="TestPriority" />.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class EndToEndTestAttribute : TestCategoryBaseAttribute
{
    private const string RootCategory = "EndToEndTests";

    /// <summary>
    /// Initializes a new instance of the <see cref="EndToEndTestAttribute" /> class.
    /// </summary>
    /// <param name="area">The area path used to generate hierarchical end-to-end test categories.</param>
    /// <param name="priority">The optional priority category to apply to the test.</param>
    public EndToEndTestAttribute(string area, TestPriority priority = TestPriority.Undefined)
    {
        TestCategories = BuildTestCategories(area, priority);
    }

    /// <inheritdoc />
    public override IList<string> TestCategories { get; }

    private static List<string> BuildTestCategories(string area, TestPriority priority)
    {
        var categories = new List<string> { RootCategory };

        if (!string.IsNullOrWhiteSpace(area))
        {
            var areas = area.Split('.');

            for (var i = 0; i < areas.Length; i++)
            {
                categories.Add($"{RootCategory}.{string.Join('.', areas, 0, i + 1)}");
            }
        }

        if (priority != TestPriority.Undefined)
        {
            categories.Add($"{RootCategory}.Priority.{priority}");
        }

        return categories;
    }
}