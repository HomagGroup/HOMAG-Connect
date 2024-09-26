using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.Base.Contracts.Extensions;

public static class BaseUnitExtensions
{
    public static int GetDecimals(this BaseUnit baseUnit, UnitSystem unitSystem)
    {
        var fi = baseUnit.GetType().GetField(baseUnit.ToString());

        if (fi.GetCustomAttributes(typeof(RoundingFormatAttribute), false) is RoundingFormatAttribute[] attributes && attributes.Any())
        {
            return unitSystem == UnitSystem.Metric ? attributes.First().DecimalsMetricUnitSystem : attributes.First().DecimalsImperialUnitSystem;
        }

        return 0;
    }
}