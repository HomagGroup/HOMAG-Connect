using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

using HomagConnect.Base.Contracts.Extensions;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Provides a builder for creating partial update (patch) objects for a specified type, allowing selective property
/// assignment and validation.
/// </summary>
/// <remarks>Use this class to construct a dictionary representing only the properties you wish to update on an
/// object of type T. Property values are validated according to data annotation attributes defined on T and its interfaces. The resulting
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
        var path = GetNestedPath(propertyExpression);
        if (path.Count > 1)
        {
            var leafExpression = propertyExpression.Body;
            while (leafExpression is UnaryExpression unary)
            {
                leafExpression = unary.Operand;
            }

            var leafMember = (MemberExpression)leafExpression;
            var leafProperty = (PropertyInfo)leafMember.Member;
            var ownerType = leafMember.Expression!.Type;
            ValidateProperty(leafProperty, ownerType, value);

            var target = _values;
            for (var i = 0; i < path.Count - 1; i++)
            {
                var key = ToCamelCase(path[i]);
                if (!target.TryGetValue(key, out var child))
                {
                    child = new Dictionary<string, object?>();
                    target[key] = child;
                }

                if (!(child is Dictionary<string, object?> childDictionary))
                {
                    throw new InvalidOperationException($"Cannot set a nested property because '{key}' already has an assigned value.");
                }

                target = childDictionary;
            }

            target[ToCamelCase(path[path.Count - 1])] = value;
            return this;
        }

        var property = GetPropertyInfo(propertyExpression);

        ValidateProperty(property, typeof(T), value);

        var jsonName = ToCamelCase(property.Name);
        _values[jsonName] = value;

        return this;
    }

    public IReadOnlyDictionary<string, object?> Build() => _values;

    private static List<string> GetNestedPath<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        var names = new List<string>();
        var current = expression.Body;
        while (true)
        {
            if (current is UnaryExpression unary)
            {
                current = unary.Operand;
            }
            else if (current is MemberExpression member && member.Member is PropertyInfo)
            {
                names.Insert(0, member.Member.Name);
                current = member.Expression!;
            }
            else
            {
                if (current != expression.Parameters[0])
                {
                    throw new ArgumentException("Expression must point to a property rooted in the lambda parameter.", nameof(expression));
                }

                break;
            }
        }

        return names;
    }

    private static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member && member.Member is PropertyInfo property)
        {
            return property;

        }

        if (expression.Body is UnaryExpression unary &&
            unary.Operand is MemberExpression unaryMember &&
            unaryMember.Member is PropertyInfo unaryProperty)
        {
            return unaryProperty;
        }

        throw new ArgumentException("Expression must point to a property.");
    }

    private static (PropertyInfo Property, Type OwnerType) GetNestedProperty(IReadOnlyList<string> path)
    {
        var ownerType = typeof(T);
        PropertyInfo? property = null;
        for (var i = 0; i < path.Count; i++)
        {
            property = ownerType.GetProperty(path[i]);
            if (property == null)
            {
                throw new ArgumentException("Expression must point to a property.");
            }

            if (i < path.Count - 1)
            {
                ownerType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            }
        }

        return (property!, ownerType);
    }

    private static void ValidateProperty<TProperty>(PropertyInfo property, Type ownerType, TProperty value)
    {
        if (!PropertyValidation.GetValidationAttributes(ownerType, property).Any())
        {
            return;
        }

        var instance = ownerType == typeof(T)
            ? new T()
            : Activator.CreateInstance(ownerType) ?? throw new InvalidOperationException(
                $"Cannot validate nested property '{property.Name}' because '{ownerType}' cannot be instantiated.");

        var context = new ValidationContext(instance)
        {
            MemberName = property.Name
        };

        var results = new List<ValidationResult>();

        bool isValid = PropertyValidation.TryValidateProperty(value, context, results);

        if (!isValid)
        {
            var message = string.Join("; ", results.Select(r => r.ErrorMessage));
            throw new ValidationException($"Invalid value for '{property.Name}': {message}");
        }
    }

    private static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
        {
            return name;
        }

        // Avoid using range operator to support older frameworks
        return char.ToLowerInvariant(name[0]) + name.Substring(1);
    }
}