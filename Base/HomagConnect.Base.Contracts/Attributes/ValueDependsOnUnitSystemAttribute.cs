using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ValueDependsOnUnitSystemAttribute : Attribute
{
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit, int metricDecimals, int imperialDecimals)
    {
        BaseUnit = baseUnit;
        MetricDecimals = metricDecimals;
        ImperialDecimals = imperialDecimals;
    }

    public BaseUnit BaseUnit { get; }

    public int MetricDecimals { get; }

    public int ImperialDecimals { get; }
}