using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ValueDependsOnUnitSystemAttribute : Attribute
{
    public ValueDependsOnUnitSystemAttribute(BaseUnit baseUnit)
    {
        BaseUnit = baseUnit;
    }

    public BaseUnit BaseUnit { get; }
}