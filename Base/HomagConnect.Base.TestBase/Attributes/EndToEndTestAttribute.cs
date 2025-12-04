using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.Base.TestBase.Attributes;

/// <summary>
/// Test Attribute to comment to link to an Area
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class EndToEndTestAttribute : TestCategoryBaseAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute" />
    /// class
    /// by applying the supplied category to the test.
    /// </summary>
    /// <param name="area">The area to be applied.</param>
    public EndToEndTestAttribute(string area)
    {
        var list = new List<string> { "EndToEndTests" };

        if (!string.IsNullOrWhiteSpace(area))
        {
            var areas = area.Split('.');

            for (int i = 0; i < areas.Length; i++)
            {
                var areaName = new StringBuilder();

                for (int j = 0; j <= i; j++)
                {
                    areaName.Append(areas[j] + ".");
                }

                list.Add("EndToEndTests." + areaName.ToString().TrimEnd('.'));
            }
        }

        TestCategories = list;
    }

    /// <summary>
    /// Gets the test categories that have been applied to the test.
    /// </summary>
    public override IList<string> TestCategories { get; }
}