using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Extensions;

public static class BaseUnitExtensions
{
    public static int GetDecimals(this BaseUnit baseUnit, UnitSystem unitSystem)
    {
        return baseUnit switch
        {
            BaseUnit.Millimeter => unitSystem == UnitSystem.Metric ? 1 : 3,
            BaseUnit.Meter => 2,
            BaseUnit.Bar => unitSystem == UnitSystem.Metric ? 2 : 1,
            BaseUnit.SquareMeter => 2,
            BaseUnit.MeterPerSecond => 1,
            BaseUnit.KilogramPerCubicMeter => 1,
            BaseUnit.Kilogram => 1,
            _ => throw new NotImplementedException($"Decimals for {baseUnit} are not defined.")
        };
    }
}