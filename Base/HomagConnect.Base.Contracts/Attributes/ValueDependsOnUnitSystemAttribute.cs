using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ValueDependsOnUnitSystemAttribute : Attribute
{
    /// <summary>
    /// Attribute to define the unit system of a property.
    /// Please pass a <see cref="metricDecimalPrecision"/> or <see cref="imperialDecimalPrecision"/> value if you want to override the default decimals.
    /// </summary>
    /// <param name="baseUnit">The unit system of that property.</param>
    /// <param name="metricDecimalPrecision">Defines how many decimals after comma are shown in metric system. Default is 1.</param>
    /// <param name="imperialDecimalPrecision">Defines how many decimals after comma are shown in imperial system. Default is 3.</param>
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit, int metricDecimalPrecision = DecimalPrecision.OneDecimalPlace, int imperialDecimalPrecision = DecimalPrecision.ThreeDecimalPlaces)
    {
        BaseUnit = baseUnit;
        MetricDecimalPrecision = metricDecimalPrecision;
        ImperialDecimalPrecision = imperialDecimalPrecision;
    }

    public BaseUnit BaseUnit { get; }

    public int MetricDecimalPrecision { get; }

    public int ImperialDecimalPrecision { get; }
}