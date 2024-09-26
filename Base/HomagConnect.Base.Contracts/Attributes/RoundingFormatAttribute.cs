namespace HomagConnect.Base.Contracts.Attributes
{
    /// <summary>
    /// Defines the rounding format of a property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class RoundingFormatAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="RoundingFormatAttribute" /> class.
        /// </summary>
        public RoundingFormatAttribute(int decimalsMetricUnitSystem, int decimalsImperialUnitSystem)
        {
            DecimalsMetricUnitSystem = decimalsMetricUnitSystem;
            DecimalsImperialUnitSystem = decimalsImperialUnitSystem;
        }

        /// <summary>
        /// Decimals for the metric unit system.
        /// </summary>
        public int DecimalsImperialUnitSystem { get; }

        /// <summary>
        /// Decimals for the imperial unit system.
        /// </summary>
        public int DecimalsMetricUnitSystem { get; }
    }
}