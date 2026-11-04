using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HomagConnect.Base.Contracts.Extensions;

/// <summary>
/// Resolves property validation rules declared on classes and their implemented interfaces.
/// </summary>
public static class PropertyValidation
{
    /// <summary>
    /// Gets the validation attributes for a property, including interface declarations.
    /// </summary>
    public static IEnumerable<ValidationAttribute> GetValidationAttributes(Type type, PropertyInfo property)
    {
        return property.GetCustomAttributes<ValidationAttribute>(true)
            .Concat(GetInterfaceValidationAttributes(type, property.Name)).Distinct();
    }

    /// <summary>
    /// Validates only the supplied property value, including rules on implemented interfaces.
    /// Other properties and nested objects are not validated.
    /// </summary>
    public static bool TryValidateProperty(object? value, ValidationContext context, ICollection<ValidationResult> results)
    {
        var isValid = Validator.TryValidateProperty(value, context, results);
        return TryValidateInterfaceProperty(value, context, results) && isValid;
    }

    /// <summary>
    /// Validates an object's annotations, including interface properties, without traversing nested objects.
    /// </summary>
    public static bool TryValidateObject(object instance, ValidationContext context, ICollection<ValidationResult> results)
    {
        var isValid = Validator.TryValidateObject(instance, context, results, true);
        foreach (var property in instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .Where(property => property.CanRead && property.GetIndexParameters().Length == 0 &&
                                        GetInterfaceValidationAttributes(instance.GetType(), property.Name).Any()))
        {
            if (!TryValidateInterfaceProperty(property.GetValue(instance),
                    new ValidationContext(instance, null, context.Items) { MemberName = property.Name }, results))
            {
                isValid = false;
            }
        }

        return isValid;
    }

    private static bool TryValidateInterfaceProperty(object? value, ValidationContext context, ICollection<ValidationResult> results)
    {
        var isValid = true;
        foreach (var attribute in GetInterfaceValidationAttributes(context.ObjectType, context.MemberName!).Distinct())
        {
            var result = attribute.GetValidationResult(value, context);
            if (result != ValidationResult.Success)
            {
                results.Add(result!);
                isValid = false;
            }
        }

        return isValid;
    }

    private static IEnumerable<ValidationAttribute> GetInterfaceValidationAttributes(Type type, string propertyName)
    {
        return type.GetInterfaces()
            .Select(interfaceType => interfaceType.GetProperty(propertyName))
            .Where(property => property != null)
            .SelectMany(property => property!.GetCustomAttributes<ValidationAttribute>(true));
    }
}
