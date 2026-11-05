using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using HomagConnect.Base.Contracts.Extensions;

namespace HomagConnect.Base.Extensions;

#pragma warning disable S3956 // "Generic.List" instances should not be part of public APIs
#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high
#pragma warning disable S134 // Control flow statements "if", "switch", "for", "foreach", "while", "do"  and "try" should not be nested too deeply

/// <summary>
/// See https://github.com/reustmd/DataAnnotationsValidatorRecursive for details.
/// </summary>
public static class DataAnnotationsValidator
{
    public static bool TryValidateObjectRecursive<T>(T obj, out List<ValidationResult> results, IDictionary<object, object> validationContextItems = null)
    {
        results = [];

        return TryValidateObjectRecursive(obj, results, new HashSet<object>(), validationContextItems);
    }

    /// <summary>
    /// Validates a graph with an explicit traversal policy while preserving the default traversal for existing callers.
    /// </summary>
    public static bool TryValidateObjectRecursive<T>(T obj, out List<ValidationResult> results,
        IDictionary<object, object> validationContextItems, Func<PropertyInfo, bool> shouldTraverseProperty)
    {
        results = [];
        return TryValidateObjectRecursive(obj, results, new HashSet<object>(), validationContextItems, shouldTraverseProperty);
    }

    private static bool TryValidateObject(object obj, ICollection<ValidationResult> results, IDictionary<object, object> validationContextItems = null)
    {
        return Validator.TryValidateObject(obj, new ValidationContext(obj, null, validationContextItems), results, true);
    }

    private static bool TryValidateObjectRecursive<T>(T obj, List<ValidationResult> results, ISet<object> validatedObjects,
        IDictionary<object, object> validationContextItems = null, Func<PropertyInfo, bool>? shouldTraverseProperty = null)
    {
        if (validatedObjects.Contains(obj))
        {
            return true;
        }

        validatedObjects.Add(obj);
        var result = TryValidateObject(obj, results, validationContextItems);

        var type = obj.GetType();
        var properties = type.GetProperties().Where(prop => prop.CanRead && prop.GetIndexParameters().Length == 0).ToList();

        foreach (var property in properties)
        {
            var value = obj.GetPropertyValue(property.Name);

            if (value == null)
            {
                continue;
            }

            foreach (var attr in PropertyValidation.GetValidationAttributes(type, property))
            {
                var validationResult = attr.GetValidationResult(value, new ValidationContext(obj) { MemberName = property.Name });
                if (validationResult != ValidationResult.Success)
                {
                    results.Add(new ValidationResult(attr.FormatErrorMessage(property.Name), [property.Name]));
                    result = false;
                }
            }

            if (shouldTraverseProperty != null && !shouldTraverseProperty(property))
            {
                continue;
            }

            if (value is IEnumerable asEnumerable && !(value is string))
            {
                var index = 0;
                foreach (var enumObj in asEnumerable)
                {
                    if (enumObj != null)
                    {
                        var nestedResults = new List<ValidationResult>();
                        if (!TryValidateObjectRecursive(enumObj, nestedResults, validatedObjects, validationContextItems, shouldTraverseProperty))
                        {
                            result = false;
                            foreach (var validationResult in nestedResults)
                            {
                                results.Add(new ValidationResult(validationResult.ErrorMessage, validationResult.MemberNames.Select(x => property.Name + $"[{index}]." + x).ToArray()));
                            }
                        }
                    }

                    index++;
                }
            }
            else if (value is not string && !property.PropertyType.IsValueType)
            {
                var nestedResults = new List<ValidationResult>();
                if (!TryValidateObjectRecursive(value, nestedResults, validatedObjects, validationContextItems, shouldTraverseProperty))
                {
                    result = false;
                    foreach (var validationResult in nestedResults)
                    {
                        results.Add(new ValidationResult(validationResult.ErrorMessage, validationResult.MemberNames.Select(x => property.Name + '.' + x).ToArray()));
                    }
                }
            }
        }

        return result;
    }
}

#pragma warning restore S3956 // "Generic.List" instances should not be part of public APIs
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high
#pragma warning restore S134 // Control flow statements "if", "switch", "for", "foreach", "while", "do"  and "try" should not be nested too deeply
