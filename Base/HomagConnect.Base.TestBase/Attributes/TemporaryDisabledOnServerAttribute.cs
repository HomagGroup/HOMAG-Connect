using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.Base.TestBase.Attributes
{
    /// <summary>
    /// Test Attribute to comment to link to an Area
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public sealed class TemporaryDisabledOnServerAttribute : TestCategoryBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute" />
        /// class
        /// by applying the supplied category to the test.
        /// </summary>
        public TemporaryDisabledOnServerAttribute(int year, int month, int day, string owner)
        {
            var disabledUntil = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);

            var list = new List<string>();

            if (disabledUntil > DateTime.Now)
            {
                list.Add("TemporaryDisabled");
            }

            list.Add($"TemporaryDisabled.{owner}");

            TestCategories = list;
        }

        /// <summary>
        /// Gets the test categories that have been applied to the test.
        /// </summary>
        public override IList<string> TestCategories { get; }
    }
}