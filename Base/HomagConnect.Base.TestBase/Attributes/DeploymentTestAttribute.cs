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
    private const string RootCategory = "DeploymentTests";

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
        var categories = new List<string> { RootCategory };
        var areas = GetAreas(area);

        for (var i = 0; i < areas.Length; i++)
        {
            categories.Add($"{RootCategory}.{string.Join('.', areas, 0, i + 1)}");
        }

        if (priority != TestPriority.Undefined)
        {
            categories.Add($"{RootCategory}.Priority.{priority}");

            if (areas.Length > 0)
            {
                categories.Add($"{RootCategory}.Priority.{priority}.{areas[0]}");
            }
        }

        return categories;
    }

    private static string[] GetAreas(string area)
    {
        return string.IsNullOrWhiteSpace(area)
            ? []
            : area.Split('.');
    }
}