using System.Collections.ObjectModel;
using System.Reflection;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.DataExchange.Extensions.Wrapper;
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
            if (projectOrder != null)
            {
                var order = new OrderDetails();

                // Map properties

                foreach (var property in projectOrder.Properties)
                {
                    if (property.Name != null && property.Value != null && !TrySetProperty(order, property))
                    {
                        // TODO: Handle renamed properties (maybe on deserialization)
                        // If there is a property that is not a property of the order, add it to the additional properties.

                        order.AdditionalProperties ??= new Dictionary<string, object>();
                        order.AdditionalProperties.Add(property.Name, property.Value);
                    }
                }

                // Map images

                if (projectOrder.Images.Count > 0)
                {
                    order.AdditionalData = new Collection<AdditionalDataEntity>();

                    foreach (var projectOrderImage in projectOrder.Images)
                    {
                        if (projectOrderImage != null)
                        {
                            var imageWrapper = new ImageWrapper(projectOrderImage);

                            Uri? downloadUri = null;

                            if (imageWrapper.ImageLinkPicture != null && !string.IsNullOrWhiteSpace(imageWrapper.ImageLinkPicture))
                            {
                                downloadUri = new Uri(imageWrapper.ImageLinkPicture, UriKind.RelativeOrAbsolute);
                            }

                            var additionalDataEntity = AdditionalDataEntity.CreateInstance(imageWrapper.Extension);

                            additionalDataEntity.Category = imageWrapper.Category;
                            additionalDataEntity.Name = imageWrapper.Description;
                            additionalDataEntity.DownloadFileName = imageWrapper.OriginalFileName;
                            additionalDataEntity.DownloadUri = downloadUri;

                            order.AdditionalData.Add(additionalDataEntity);
                        }
                    }
                }

                yield return order;
            }
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