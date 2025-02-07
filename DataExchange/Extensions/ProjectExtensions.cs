using System.Reflection;

using HomagConnect.DataExchange.Contracts;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.DataExchange.Extensions;

/// <summary>
/// Param base extensions.
/// </summary>
public static class ProjectExtensions
{
    /// <summary>
    /// Converts the project to order.
    /// </summary>
    public static IEnumerable<OrderDetails> ConvertToOrders(this Project project)
    {
        if (project == null)
        {
            throw new ArgumentNullException(nameof(project));
        }

        if (project.Orders == null || project.Orders.Count == 0)
        {
            yield break;
        }

        foreach (var projectOrder in project.Orders)
        {
            var order = new OrderDetails();

            foreach (var property in projectOrder.Properties)
            {
                if (property.Name != null && property.Value != null)
                {
                    if (!TrySetProperty(order, property))
                    {
                        if (order.AdditionalProperties == null)
                        {
                            order.AdditionalProperties = new Dictionary<string, object>();
                        }

                        order.AdditionalProperties.Add(property.Name, property.Value);
                    }
                }
            }

            yield return order;
        }
    }

    private static bool TrySetProperty(object o, Param property)
    {
        var propertyInfos = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        try
        {
            if (property.Value != null)
            {
                var propertyInfo = propertyInfos.FirstOrDefault(x => x.Name == property.Name);

                if (propertyInfo == null)
                {
                    return false;
                }

                var propertyValue = property.Value.FromXmlValue(propertyInfo.PropertyType);

                if (propertyValue != null)
                {
                    propertyInfo.SetValue(o, propertyValue);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }
}