using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HomagConnect.Base.Extensions
{
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
            results = new List<ValidationResult>();

            return TryValidateObjectRecursive(obj, results, new HashSet<object>(), validationContextItems);
        }

        private static bool TryValidateObject(object obj, ICollection<ValidationResult> results, IDictionary<object, object> validationContextItems = null)
        {
            return Validator.TryValidateObject(obj, new ValidationContext(obj, null, validationContextItems), results, true);
        }

        private static bool TryValidateObjectRecursive<T>(T obj, List<ValidationResult> results, ISet<object> validatedObjects, IDictionary<object, object> validationContextItems = null)
        {
            if (validatedObjects.Contains(obj))
            {
                return true;
            }

            validatedObjects.Add(obj);
            var result = TryValidateObject(obj, results, validationContextItems);

            var properties = obj.GetType().GetProperties().Where(prop => prop.CanRead
                                                                         && prop.GetIndexParameters().Length == 0).ToList();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) || property.PropertyType.IsValueType)
                {
                    continue;
                }

                var value = obj.GetPropertyValue(property.Name);

                if (value == null)
                {
                    continue;
                }

                if (value is IEnumerable asEnumerable)
                {
                    var index = 0;

                    foreach (var enumObj in asEnumerable)
                    {
                        if (enumObj != null)
                        {
                            var nestedResults = new List<ValidationResult>();

                            if (!TryValidateObjectRecursive(enumObj, nestedResults, validatedObjects, validationContextItems))
                            {
                                result = false;

                                foreach (var validationResult in nestedResults)
                                {
                                    var property1 = property;
                                    var index1 = index;
                                    results.Add(new ValidationResult(validationResult.ErrorMessage, validationResult.MemberNames.Select(x => property1.Name + $"[{index1}]." + x)));
                                }
                            }
                        }

                        index++;
                    }
                }
                else
                {
                    var nestedResults = new List<ValidationResult>();

                    if (!TryValidateObjectRecursive(value, nestedResults, validatedObjects, validationContextItems))
                    {
                        result = false;

                        foreach (var validationResult in nestedResults)
                        {
                            var property1 = property;
                            results.Add(new ValidationResult(validationResult.ErrorMessage, validationResult.MemberNames.Select(x => property1.Name + '.' + x)));
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
}