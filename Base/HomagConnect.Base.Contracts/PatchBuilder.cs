using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Provides a builder for creating partial update (patch) objects for a specified type, allowing selective property
/// assignment and validation.
/// </summary>
/// <remarks>Use this class to construct a dictionary representing only the properties you wish to update on an
/// object of type T. Property values are validated according to data annotation attributes defined on T. The resulting
/// patch can be used for scenarios such as HTTP PATCH requests or partial updates in APIs.</remarks>
/// <typeparam name="T">The type of object for which the patch is being built. Must be a reference type with a parameterless constructor.</typeparam>
public class PatchBuilder<T> where T : class, new()
{
    private readonly Dictionary<string, object?> _values = new();

    public static PatchBuilder<T> For() => new();

    public PatchBuilder<T> Set<TProperty>(
        Expression<Func<T, TProperty>> propertyExpression,
        TProperty value)
    {
        var property = GetPropertyInfo(propertyExpression);

        ValidateProperty(property, value);

        var jsonName = ToCamelCase(property.Name);
        _values[jsonName] = value;

        return this;
    }

    public IReadOnlyDictionary<string, object?> Build() => _values;

    private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member && member.Member is PropertyInfo property)
            return property;

        if (expression.Body is UnaryExpression unary &&
            unary.Operand is MemberExpression unaryMember &&
            unaryMember.Member is PropertyInfo unaryProperty)
            return unaryProperty;

        throw new ArgumentException("Expression must point to a property.");
    }

    private static void ValidateProperty<TProperty>(PropertyInfo property, TProperty value)
    {
        var instance = new T();

        var context = new ValidationContext(instance)
        {
            MemberName = property.Name
        };

        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateProperty(value, context, results);

        if (!isValid)
        {
            var message = string.Join("; ", results.Select(r => r.ErrorMessage));
            throw new ValidationException($"Invalid value for '{property.Name}': {message}");
        }
    }

    private static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
            return name;

        // Avoid using range operator to support older frameworks
        return char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}