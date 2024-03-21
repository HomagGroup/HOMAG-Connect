using System;
using System.Linq;
using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.Base.Extensions;

public static class ContainsUnitSystemDependentPropertiesExtensions
{
    public static T SwitchUnitSystem<T>(this T o, UnitSystem unitSystem) where T : IContainsUnitSystemDependentProperties, new()
    {
        if (o == null)
        {
            throw new ArgumentNullException(nameof(o));
        }

        var serializedObject = JsonConvert.SerializeObject(o);
        var clone = JsonConvert.DeserializeObject<T>(serializedObject);

        if (clone == null)
        {
            throw new InvalidOperationException("Failed to clone object.");
        }

        if (o.UnitSystem == unitSystem)
        {
            return clone;
        }

        clone.UnitSystem = unitSystem;

        var propertyInfos = clone.GetType().GetProperties();

        foreach (var propertyInfo in propertyInfos)
        {
            var valueDependsOnUnitSystemAttribute = propertyInfo.GetCustomAttributes().OfType<ValueDependsOnUnitSystemAttribute>().FirstOrDefault();

            if (valueDependsOnUnitSystemAttribute != null)
            {
                if (valueDependsOnUnitSystemAttribute.BaseUnit == BaseUnit.Millimeter)
                {
                    var value = propertyInfo.GetValue(clone);

                    if (value != null)
                    {
                        if (clone.UnitSystem == UnitSystem.Imperial)
                        {
                            propertyInfo.SetValue(clone,ConvertMillimeterToInch(value));
                        }
                        else if (clone.UnitSystem == UnitSystem.Metric)
                        {
                            propertyInfo.SetValue(clone, ConvertInchToMillimeter(value));
                        }
                        else
                        {
                            throw new InvalidOperationException("Invalid unit system.");
                        }
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        return clone;
    }

    private static double? ConvertInchToMillimeter(object value)
    {
        return (double?)value * 25.4;
    }

    private static double? ConvertMillimeterToInch(object value)
    {
        return (double?)value / 25.4;
    }
}