using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.Base.TestBase.Attributes;

/// <summary>
/// Applies deployment-related categories to a test, including area-based categories and an optional <see cref="TestPriority" />.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class DeploymentTestAttribute : TestCategoryBaseAttribute
{
    private const string _RootCategory = "DeploymentTests";

    /// <summary>
    /// Initializes a new instance of the <see cref="DeploymentTestAttribute" /> class.
    /// </summary>
    /// <param name="area">The area path used to generate hierarchical deployment test categories.</param>
    /// <param name="priority">The optional priority category to apply to the test.</param>
    public DeploymentTestAttribute(string area, TestPriority priority = TestPriority.Undefined)
    {
        TestCategories = BuildTestCategories(area, priority);
    }

    /// <inheritdoc />
    public override IList<string> TestCategories { get; }

    private static List<string> BuildTestCategories(string area, TestPriority priority)
    {
        var categories = new List<string> { _RootCategory };

        if (!string.IsNullOrWhiteSpace(area))
        {
            var areas = area.Split('.');

            categories.AddRange(areas.Select((t, i) => $"{_RootCategory}.{string.Join('.', areas, 0, i + 1)}"));
        }
        
        categories.Add($"{_RootCategory}.Priority.{priority}");

        return categories;
    }
}