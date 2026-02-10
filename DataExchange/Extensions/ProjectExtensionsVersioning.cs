using HomagConnect.Base.Contracts;
using HomagConnect.DataExchange.Contracts;
using HomagConnect.OrderManager.Contracts.OrderItems;

using Type = System.Type;

namespace HomagConnect.DataExchange.Extensions;

/// <summary>
/// Provides extension methods to migrates the project to the latest version.
/// </summary>
public static class ProjectExtensionsVersioning
{
    private static List<(Type Level, string OldName, string NewName)> RenamedProperties { get; } =
    [
        (typeof(Order), "PersonInCharge", "ProjectManager"),
        (typeof(Order), "DeliveryDatePlanned", "DeliveryDate"),
        (typeof(Order), "Notes", "AdditionalComments"),

        (typeof(Order), "AddressField1", nameof(Address.Street)),
        (typeof(Order), "AddressField2", nameof(Address.HouseNumber)),
        (typeof(Order), "AddressField3", nameof(Address.City)),
        (typeof(Order), "AddressField4", nameof(Address.PostalCode)),
        (typeof(Order), "AddressField5", nameof(Address.Country)),

        (typeof(Entity), "Typ", nameof(Part.Type)),
        (typeof(Entity), "EdgeDiagramm", nameof(Part.EdgeDiagram)),
        (typeof(Entity), "AdditionalComments", nameof(Part.Notes)),
        (typeof(Entity), "CuttingLength", nameof(Part.SecondCutLength)),
        (typeof(Entity), "CuttingWidth", nameof(Part.SecondCutWidth)),
        (typeof(Entity), "FrontLaminate", nameof(Part.LaminateTop)),
        (typeof(Entity), "BackLaminate", nameof(Part.LaminateBottom)),
        (typeof(Entity), "CncProgramName", nameof(Part.CncProgramName1)),

        // Validation needed
        (typeof(Entity), "ArticleDescription", nameof(Part.Description)),
        (typeof(Entity), "Grouping", "OrderPosition"),
    ];

    /// <summary>
    /// Migrates the project to the latest version.
    /// </summary>
    public static Project MigrateToLatestVersion(this Project project)
    {
        if (project == null)
        {
            throw new ArgumentNullException(nameof(project));
        }

        if (project.Orders is { Count: > 0 })
        {
            foreach (var order in project.Orders)
            {
                RenamePropertiesIfNecessary(order);
            }
        }

        return project;
    }

    private static void RenamePropertiesIfNecessary(Entity entity)
    {
        RenamePropertiesIfNecessary(entity.GetType(), entity.Properties);

        foreach (var entityEntity in entity.Entities)
        {
            RenamePropertiesIfNecessary(entityEntity);
        }
    }

    private static void RenamePropertiesIfNecessary(Type level, List<Param>? properties)
    {
        if (properties is { Count: > 0 })
        {
            var renamedProperties = RenamedProperties;
            var renamedPropertiesForType = renamedProperties.Where(r => r.Level == level).ToList();

            if (renamedProperties.Any())
            {
                foreach (var property in properties)
                {
                    var renamedProperty = renamedPropertiesForType.FirstOrDefault(r => string.Equals(r.OldName, property.Name, StringComparison.OrdinalIgnoreCase));

                    if (renamedProperty != default)
                    {
                        property.Name = renamedProperty.NewName;
                    }
                }
            }
        }
    }
}