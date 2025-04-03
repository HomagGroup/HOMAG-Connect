using System.Collections.ObjectModel;
using System.Reflection;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Wrapper;
using HomagConnect.OrderManager.Contracts.OrderItems;
using HomagConnect.OrderManager.Contracts.Orders;

namespace HomagConnect.DataExchange.Extensions;

/// <summary>
/// Param base extensions.
/// </summary>
public static class ProjectExtensionsConversion
{
    /// <summary>
    /// Converts the project to order.
    /// </summary>
    public static IEnumerable<Group> ConvertToGroups(this Project project)
    {
        var groups = new Collection<Group>();

        if (project == null)
        {
            throw new ArgumentNullException(nameof(project));
        }

        if (project.Orders is { Count: > 0 })
        {
            foreach (var projectOrder in project.Orders)
            {
                var group = new Group();

                Map(project, projectOrder, group);

                groups.Add(group);
            }
        }

        return groups;
    }

    /// <summary>
    /// Converts the project to order.
    /// </summary>
    public static IEnumerable<OrderDetails> ConvertToOrders(this Project project)
    {
        var orders = new Collection<OrderDetails>();

        if (project == null)
        {
            throw new ArgumentNullException(nameof(project));
        }

        if (project.Orders is { Count: > 0 })
        {
            foreach (var projectOrder in project.Orders)
            {
                var order = new OrderDetails();

                MapOrder(project, projectOrder, order);

                orders.Add(order);
            }
        }

        return orders;
    }

    private static void MapOrder(Project project, Order order, OrderDetails orderDetails)
    {
        MapEntity(order, orderDetails);

        if (order.Entities.Count > 0)
        {
            orderDetails.Items = new Collection<OrderManager.Contracts.OrderItems.Base>();

            var group = new Group();

            Map(project, order, group);

            orderDetails.Items.Add(group);
        }
    }

    private static void Map(Project project, Order order, Group group)
    {
        var projectWrapper = new ProjectWrapper(project);
        var orderWrapper = new OrderWrapper(order);

        group.Name = orderWrapper.OrderName;
        group.Quantity = orderWrapper.Quantity ?? 1;

        if (!string.IsNullOrWhiteSpace(orderWrapper.Source))
        {
            group.Source = orderWrapper.Source;
        }
        else if (!string.IsNullOrWhiteSpace(projectWrapper.Source))
        {
            group.Source = projectWrapper.Source;
        }

        MapEntity(order, group, true);

        if (order.Entities.Count > 0)
        {
            group.Items = new Collection<OrderManager.Contracts.OrderItems.Base>();

            foreach (var entityEntity in order.Entities)
            {
                var orderItem = CreateInstance(entityEntity.Properties);

                MapOrderItem(entityEntity, orderItem);

                group.Items.Add(orderItem);
            }
        }
    }

    private static void MapOrderItem(Entity entity, OrderManager.Contracts.OrderItems.Base orderItem)
    {
        MapEntity(entity, orderItem);

        if (entity.Entities.Count > 0)
        {
            orderItem.Items = new Collection<OrderManager.Contracts.OrderItems.Base>();

            foreach (var entityEntity in entity.Entities)
            {
                var orderItemItem = CreateInstance(entityEntity.Properties);

                MapOrderItem(entityEntity, orderItemItem);

                orderItem.Items.Add(orderItemItem);
            }
        }
    }

    private static OrderManager.Contracts.OrderItems.Base CreateInstance(IEnumerable<Param> properties)
    {
        var entityEntityProperty = properties.FirstOrDefault(t => string.Equals(t.Name, "Type", StringComparison.InvariantCultureIgnoreCase) ||
                                                                  string.Equals(t.Name, "Typ", StringComparison.InvariantCultureIgnoreCase));

        if (entityEntityProperty?.Value == null)
        {
            throw new NotSupportedException("Type property not found");
        }

        var type = entityEntityProperty.Value;

        if (string.Equals(type, "OrderItem", StringComparison.CurrentCultureIgnoreCase))
        {
            return new Position();
        }

        if (string.Equals(type, "Component", StringComparison.CurrentCultureIgnoreCase))
        {
            return new Component();
        }

        if (string.Equals(type, "ProductionOrder", StringComparison.CurrentCultureIgnoreCase))
        {
            return new Part();
        }

        if (string.Equals(type, "Resource", StringComparison.CurrentCultureIgnoreCase))
        {
            return new Resource();
        }

        throw new NotSupportedException($"Entity of type '{entityEntityProperty.Value}' not supported.");
    }

    private static void MapEntity(Entity entity, ISupportsAdditionalData target, bool ignoreAdditionalProperties = false)
    {
        // Map properties

        foreach (var property in entity.Properties)
        {
            if (property.Name != null && property.Value != null && !TrySetProperty(target, property))
            {
                // TODO: Handle renamed properties (maybe on deserialization)
                // If there is a property that is not a property of the order, add it to the additional properties.

                if (!ignoreAdditionalProperties)
                {
                    target.AdditionalProperties ??= new Dictionary<string, object>();
                    target.AdditionalProperties.Add(property.Name, property.Value);
                }
            }
        }

        // Map images

        if (entity.Images.Count > 0)
        {
            target.AdditionalData = new Collection<AdditionalDataEntity>();

            foreach (var projectOrderImage in entity.Images)
            {
                if (projectOrderImage != null)
                {
                    var imageWrapper = new ImageWrapper(projectOrderImage);

                    Uri? downloadUri = null;

                    if (imageWrapper.ImageLinkPicture != null)
                    {
                        downloadUri = imageWrapper.ImageLinkPicture;
                    }
                    else if (imageWrapper.ImageLinkBinary != null)
                    {
                        downloadUri = imageWrapper.ImageLinkBinary;
                    }

                    var additionalDataEntity = AdditionalDataEntity.CreateInstance(imageWrapper.Extension);

                    additionalDataEntity.Category = imageWrapper.Category;
                    additionalDataEntity.Name = imageWrapper.Description;
                    additionalDataEntity.DownloadFileName = imageWrapper.OriginalFileName;
                    additionalDataEntity.DownloadUri = downloadUri;

                    target.AdditionalData.Add(additionalDataEntity);
                }
            }
        }
    }

    private static bool TrySetProperty(object o, Param property)
    {
        var propertyInfos = o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

        if (property.Value != null)
        {
            var propertyInfo = Array.Find(propertyInfos, (x) => x.Name == property.Name);

            if (propertyInfo == null)
            {
                return false;
            }

            if (!propertyInfo.CanWrite)
            {
                return true; // Ignore read-only properties
            }

            var propertyValue = property.Value.FromXmlValue(propertyInfo.PropertyType);

            if (propertyValue != null)
            {
                propertyInfo.SetValue(o, propertyValue);
            }
        }

        return true;
    }
}