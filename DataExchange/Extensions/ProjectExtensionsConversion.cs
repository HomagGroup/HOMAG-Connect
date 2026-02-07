using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Reflection;

using HomagConnect.Base.Contracts;
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
    /// Converts the project to groups including the additional data.
    /// </summary>
    public static IEnumerable<(Group group, FileReference[] images)> ConvertToGroups(this Project project, FileReference[] fileReferences)
    {
        var groups = new Collection<(Group, FileReference[])>();

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

                var fileReferencesReferenced = GetReferencedFileReferences(group, fileReferences);

                groups.Add(new(group, fileReferencesReferenced));
            }
        }

        return groups;
    }

    /// <summary>
    /// Converts the project to order.
    /// </summary>
    public static IEnumerable<(OrderDetails, FileReference[])> ConvertToOrders(this Project project, FileReference[] fileReferences)
    {
        var orders = new Collection<(OrderDetails, FileReference[])>();

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

                var fileReferencesReferenced = GetReferencedFileReferences(order, fileReferences);

                orders.Add(new(order, fileReferencesReferenced));
            }
        }

        return orders;
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

    /// <summary>
    /// Removes all barcodes from the project.
    /// </summary>
    public static void SetBarcodesToNull(this Project project)
    {
        var projectWrapper = new ProjectWrapper(project);

        foreach (var wrapperOrder in projectWrapper.Orders)
        {
            RemoveBarcodes(wrapperOrder.Entities);
        }
    }

    /// <summary>
    /// Sets the delivery date planned for all orders in the project.
    /// </summary>
    public static void SetDeliveryDatePlanned(this Project project, DateTime deliveryDatePlanned)
    {
        var projectWrapper = new ProjectWrapper(project);

        foreach (var wrapperOrder in projectWrapper.Orders)
        {
            wrapperOrder.DeliveryDatePlanned = deliveryDatePlanned;
        }
    }

    /// <summary>
    /// Sets the order date for all orders in the project.
    /// </summary>
    public static void SetOrderDate(this Project project, DateTime orderDate)
    {
        var projectWrapper = new ProjectWrapper(project);

        foreach (var wrapperOrder in projectWrapper.Orders)
        {
            wrapperOrder.OrderDate = orderDate;
        }
    }

    /// <summary>
    /// Sets the source for the project.
    /// </summary>
    public static void SetSource(this Project project, string source)
    {
        var projectWrapper = new ProjectWrapper(project);

        projectWrapper.Source = source;

        foreach (var projectWrapperOrder in projectWrapper.Orders)
        {
            projectWrapperOrder.Source = source;
        }
    }

    private static OrderManager.Contracts.OrderItems.Base CreateInstance(IEnumerable<Param> properties)
    {
        if (properties == null)
        {
            throw new ArgumentNullException(nameof(properties));
        }

        var propertiesAsArray = properties.ToArray();

        var entityEntityProperty = propertiesAsArray.FirstOrDefault(t => string.Equals(t.Name, "Type", StringComparison.InvariantCultureIgnoreCase) ||
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

    private static IEnumerable<AdditionalDataEntity> GetAdditionalDataEntities(OrderDetails orderDetails)
    {
        if (orderDetails.AdditionalData != null)
        {
            foreach (var additionalDataEntity in orderDetails.AdditionalData)
            {
                yield return additionalDataEntity;
            }
        }

        if (orderDetails.Items != null)
        {
            foreach (var orderItem in orderDetails.Items)
            {
                foreach (var additionalDataEntityReference in GetAllAdditionalDataEntities(orderItem))
                {
                    yield return additionalDataEntityReference;
                }
            }
        }
    }

    private static IEnumerable<AdditionalDataEntity> GetAdditionalDataEntities(Group group)
    {
        if (group.AdditionalData != null)
        {
            foreach (var additionalDataEntity in group.AdditionalData)
            {
                yield return additionalDataEntity;
            }
        }

        if (group.Items != null)
        {
            foreach (var orderItem in group.Items)
            {
                foreach (var additionalDataEntityReference in GetAllAdditionalDataEntities(orderItem))
                {
                    yield return additionalDataEntityReference;
                }
            }
        }
    }

    private static IEnumerable<AdditionalDataEntity> GetAllAdditionalDataEntities(OrderManager.Contracts.OrderItems.Base orderItem)
    {
        if (orderItem.AdditionalData != null)
        {
            foreach (var additionalDataEntity in orderItem.AdditionalData)
            {
                yield return additionalDataEntity;
            }
        }

        if (orderItem.Items != null)
        {
            foreach (OrderManager.Contracts.OrderItems.Base orderItemItem in orderItem.Items)
            {
                foreach (var additionalDataEntityReference in GetAllAdditionalDataEntities(orderItemItem))
                {
                    yield return additionalDataEntityReference;
                }
            }
        }
    }

    private static FileReference[] GetReferencedFileReferences(OrderDetails order, FileReference[] fileReferencesAvailable)
    {
        var additionalDataEntities = GetAdditionalDataEntities(order).ToList();

        var fileReferencesReferenced = new List<FileReference>();
        var fileReferencesNotReferenced = new List<FileReference>();

        foreach (var fileReference in fileReferencesAvailable)
        {
            if (additionalDataEntities.Any(additionalDataEntity => additionalDataEntity.ReferenceEquals(fileReference)))
            {
                fileReferencesReferenced.Add(fileReference);
            }
            else
            {
                fileReferencesNotReferenced.Add(fileReference);
            }
        }

        return Replace3dsReferencesWithZipPackages(fileReferencesReferenced, additionalDataEntities, fileReferencesNotReferenced).ToArray();
    }

    private static FileReference[] GetReferencedFileReferences(Group group, FileReference[] fileReferencesAvailable)
    {
        var additionalDataEntities = GetAdditionalDataEntities(group).ToList();

        var fileReferencesReferenced = new List<FileReference>();
        var fileReferencesNotReferenced = new List<FileReference>();

        foreach (var fileReference in fileReferencesAvailable)
        {
            if (additionalDataEntities.Any(additionalDataEntity => additionalDataEntity.ReferenceEquals(fileReference)))
            {
                fileReferencesReferenced.Add(fileReference);
            }
            else
            {
                fileReferencesNotReferenced.Add(fileReference);
            }
        }

        return Replace3dsReferencesWithZipPackages(fileReferencesReferenced, additionalDataEntities, fileReferencesNotReferenced).ToArray();
    }

    private static List<FileReference> Replace3dsReferencesWithZipPackages(List<FileReference> fileReferencesReferenced, List<AdditionalDataEntity> additionalDataEntities, List<FileReference> fileReferencesNotReferenced)
    {
        const string zipExtension = ".zip";
        const string threeDs3dsExtension = ".3ds";

        foreach (var fileReference in fileReferencesReferenced.Where(f => string.Equals(Path.GetExtension(f.FileInfo.FullName), threeDs3dsExtension, StringComparison.OrdinalIgnoreCase)))
        {
            var zipFileInfo = new FileInfo(fileReference.FileInfo.FullName + zipExtension);

            using (var fileStream = new FileStream(zipFileInfo.FullName, FileMode.Create))
            {
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(fileReference.FileInfo.FullName, fileReference.FileInfo.Name, CompressionLevel.Optimal);

                    foreach (var fileReferenceNotReferenced in fileReferencesNotReferenced)
                    {
                        if (zipFileInfo.Directory != null && fileReferenceNotReferenced.FileInfo.FullName.StartsWith(zipFileInfo.Directory.FullName))
                        {
                            archive.CreateEntryFromFile(fileReferenceNotReferenced.FileInfo.FullName, fileReferenceNotReferenced.FileInfo.Name, CompressionLevel.Optimal); // TODO: Implement subfolder handling
                        }
                    }
                }
            }

            foreach (var fileReferenceUriAdditionalDataEntity in additionalDataEntities.Where(a => a.ReferenceEquals(fileReference)))
            {
                fileReferenceUriAdditionalDataEntity.DownloadFileName += zipExtension;
                fileReferenceUriAdditionalDataEntity.DownloadUri = new Uri(fileReference.Reference + zipExtension, UriKind.RelativeOrAbsolute);
            }

            fileReference.Reference += zipExtension;
            fileReference.FileInfo = zipFileInfo;
        }

        return fileReferencesReferenced;
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

                    target.AdditionalProperties.TryGetValue(property.Name, out var additionalProperty);

                    if (additionalProperty != null)
                    {
                        target.AdditionalProperties[property.Name] = property.Value;
                    }
                    else
                    {
                        target.AdditionalProperties.Add(property.Name, property.Value);
                    }
                }
            }
        }

        MapImages(entity, target);
    }

    private static void MapImages(Entity entity, ISupportsAdditionalData target)
    {
        if (entity.Images.Count > 0)
        {
            target.AdditionalData = [];

            foreach (var projectOrderImage in entity.Images)
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
                else if (imageWrapper.OriginalFileName != null)
                {
                    downloadUri = new Uri(imageWrapper.OriginalFileName, UriKind.Relative);
                }

                var additionalDataEntity = AdditionalDataEntity.CreateInstance(imageWrapper.Extension);

                additionalDataEntity.Category = imageWrapper.Category;
                additionalDataEntity.Name = imageWrapper.Description;
                additionalDataEntity.DownloadFileName = imageWrapper.OriginalFileName;
                additionalDataEntity.DownloadUri = downloadUri;

                if (additionalDataEntity.DownloadUri != null || additionalDataEntity.DownloadFileName != null)
                {
                    // Ignore invalid additional data entities.

                    target.AdditionalData.Add(additionalDataEntity);
                }
            }
        }
    }

    private static void MapOrder(Project project, Order order, OrderDetails orderDetails)
    {
        MapEntity(order, orderDetails);

        var orderWrapper = new OrderWrapper(order);

        if (!string.IsNullOrEmpty(orderWrapper.OrderNumber))
        {
            orderDetails.OrderNumber = null;
            orderDetails.OrderNumberExternal = orderWrapper.OrderNumber;
        }

        if (orderDetails.Addresses == null || orderDetails.Addresses.Count == 0)
        {
            var address = new Address();

            address.Type = [AddressType.Delivery];
            address.Name = orderWrapper.CustomerName;
            address.Street = orderWrapper.Street;
            address.HouseNumber = orderWrapper.HouseNumber;
            address.PostalCode = orderWrapper.PostalCode;
            address.City = orderWrapper.City;
            address.Country = orderWrapper.Country;

            orderDetails.Addresses =
            [
                address
            ];
        }

        if (order.Entities.Count > 0)
        {
            orderDetails.Items = new Collection<OrderManager.Contracts.OrderItems.Base>();

            var group = new Group();

            Map(project, order, group);

            orderDetails.Items.Add(group);
        }
    }

    private static void MapOrderItem(Entity entity, OrderManager.Contracts.OrderItems.Base orderItem)
    {
        if (orderItem is Position position)
        {
            MapPosition(entity, position);
        }
        else
        {
            MapEntity(entity, orderItem);
        }

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

    private static void MapPosition(Entity entity, Position position)
    {
        var orderItemWrapper = new OrderItemWrapper(entity);

        position.Id = orderItemWrapper.OrderItemNumber;
        position.Name = orderItemWrapper.OrderItemNumber;
        position.ArticleNumber = orderItemWrapper.ArticleNumber;
        position.ArticleName = orderItemWrapper.Description;
        position.Description = orderItemWrapper.Description;
        position.Height = orderItemWrapper.Length;
        position.Width = orderItemWrapper.Width;
        position.Depth = orderItemWrapper.Thickness;
        position.Quantity = orderItemWrapper.Quantity ?? 1;
        position.Catalog = orderItemWrapper.Catalog;
        position.ProcurementType = orderItemWrapper.ProcurementType;

        var propertiesToIgnore = new[]
        {
            "Type",
            "QuantityUnit",
            "Barcode",

            // TODO: Validate if these properties are really not needed

            "ArticleGroup",
            "StartDatePlanned",
            "Grain"
        };

        var wrapperPropertyNames = orderItemWrapper.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name);

        foreach (var property in entity.Properties
                     .Where(p => !propertiesToIgnore.Any(i => string.Equals(i, p.Name, StringComparison.OrdinalIgnoreCase)))
                     .Where(p => !wrapperPropertyNames.Any(w => string.Equals(w, p.Name, StringComparison.OrdinalIgnoreCase))))
        {
            if (property is { Name: not null, Value: not null })
            {
                position.AdditionalProperties ??= new Dictionary<string, object>();

                if (!string.IsNullOrWhiteSpace(property.Value)) // Ignore empty values
                {
                    position.AdditionalProperties.Add(property.Name, property.Value);
                }
            }
        }

        MapImages(entity, position);
    }

    private static void RemoveBarcodes(this IList<EntityWrapper>? entities)
    {
        if (entities != null)
        {
            foreach (var entity in entities)
            {
                if (!string.IsNullOrWhiteSpace(entity.Barcode))
                {
                    entity.Barcode = null;
                }

                RemoveBarcodes(entity.Entities);
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