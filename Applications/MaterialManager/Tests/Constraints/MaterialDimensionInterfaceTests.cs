using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialDimensionInterfaceTests
{
    [TestMethod]
    [DynamicData(nameof(DimensionProperties))]
    public void Dimensions_UseInterfaceMetadataWithoutDuplicatingAttributes(Type type, string member)
    {
        typeof(IDimensionPropertiesLengthWidth).IsAssignableFrom(type).ShouldBeTrue();
        var property = type.GetProperty(member)!;
        property.GetCustomAttribute<RangeAttribute>().ShouldBeNull();
        property.GetCustomAttribute<DisplayAttribute>().ShouldBeNull();
        property.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>().ShouldBeNull();

        var range = PropertyValidation.GetValidationAttributes(type, property).OfType<RangeAttribute>().Single();
        Convert.ToDouble(range.Minimum).ShouldBe(0.1);
        Convert.ToDouble(range.Maximum).ShouldBe(member == nameof(IDimensionProperties.Thickness) ? 999.9 : 19999.9);
        type.GetPropertyDisplayName(member, CultureInfo.GetCultureInfo("de-DE"))
            .ShouldBe(typeof(IDimensionProperties).GetPropertyDisplayName(member, CultureInfo.GetCultureInfo("de-DE")));
    }

    [TestMethod]
    [DataRow(typeof(BoardType))]
    [DataRow(typeof(BoardTypeInventoryHistory))]
    [DataRow(typeof(PartHistoryPart))]
    public void DimensionHeaders_IncludeInterfaceUnits(Type type)
    {
        var item = (IContainsUnitSystemDependentProperties)Activator.CreateInstance(type)!;
        foreach (var system in new[] { UnitSystem.Metric, UnitSystem.Imperial })
        {
            item.UnitSystem = system;
            var headers = item.GetPropertyDisplayNames(CultureInfo.GetCultureInfo("en-US"), includingUnitSystem: true);
            headers[nameof(IDimensionProperties.Length)].ShouldEndWith(system == UnitSystem.Metric ? " (mm)" : " (in)");
            headers[nameof(IDimensionProperties.Width)].ShouldEndWith(system == UnitSystem.Metric ? " (mm)" : " (in)");
        }
    }

    public static IEnumerable<object[]> DimensionProperties()
    {
        foreach (var type in new[]
                 {
                     typeof(BoardType), typeof(MaterialManagerUpdateBoardType), typeof(CatalogBoardType),
                     typeof(PartHistoryPart), typeof(BoardTypeInventoryHistory)
                 })
        {
            yield return [type, nameof(IDimensionProperties.Length)];
            yield return [type, nameof(IDimensionProperties.Width)];
            if (typeof(IDimensionProperties).IsAssignableFrom(type))
            {
                yield return [type, nameof(IDimensionProperties.Thickness)];
            }
        }
    }
}
