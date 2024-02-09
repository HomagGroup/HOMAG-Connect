namespace HomagConnect.IntelliDivide.Tests.Base
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
        public TemporaryDisabledOnServerAttribute(int year, int month, int day)
        {
            var disabledUntil = new DateTime(year, month, day);

            var list = new List<string>();

            if (disabledUntil > DateTime.Now)
            {
                list.Add("TemporaryDisabled");
            }

            TestCategories = list;
        }

        /// <summary>
        /// Gets the test categories that have been applied to the test.
        /// </summary>
        public override IList<string> TestCategories { get; }
    }
}