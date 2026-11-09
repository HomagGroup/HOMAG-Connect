using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

[TestClass]
public class MaterialUnitMetadataTests
{
    [TestMethod]
    [DataRow(typeof(MaterialAssistUpdateBoardEntity))]
    [DataRow(typeof(MaterialAssistStoreBoardEntity))]
    public void NullableBoardDimensions_DeclareSharedMetadataOnlyOnTheInterface(Type type)
    {
        typeof(IDimensionPropertiesLengthWidth).IsAssignableFrom(type).ShouldBeTrue();
        foreach (var member in new[] { "Length", "Width" })
        {
            var property = type.GetProperty(member)!;
            property.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>().ShouldBeNull();
            property.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>().ShouldBeNull();
            property.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>().ShouldBeNull();
        }
    }

    [TestMethod]
    public void BoardDimensions_IncludingInheritedTemplate_HaveMillimetreMetadata()
    {
        foreach (var type in new[]
                 {
                     typeof(MaterialAssistRequestOffcutEntity), typeof(MaterialAssistRequestTemplateEntity),
                     typeof(MaterialAssistUpdateBoardEntity), typeof(MaterialAssistStoreBoardEntity)
                 })
        foreach (var name in new[] { "Length", "Width" })
        {
            var property = type.GetProperty(name)!;
            var metadata = property.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>() ??
                type.GetInterfaces().Select(interfaceType => interfaceType.GetProperty(name)
                    ?.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>()).FirstOrDefault(attribute => attribute != null);
            metadata.ShouldNotBeNull($"{type.Name}.{name}");
            metadata.BaseUnit.ShouldBe(BaseUnit.Millimeter);
        }
    }

    [TestMethod]
    [DataRow(typeof(MaterialAssistRequestEdgebandEntity), "Length", BaseUnit.Meter, 2)]
    [DataRow(typeof(MaterialAssistUpdateEdgebandEntity), "Length", BaseUnit.Meter, 2)]
    [DataRow(typeof(MaterialAssistStoreEdgebandEntity), "Length", BaseUnit.Meter, 2)]
    [DataRow(typeof(MaterialAssistRequestEdgebandEntity), "CurrentThickness", BaseUnit.Millimeter, 2)]
    [DataRow(typeof(MaterialAssistUpdateEdgebandEntity), "CurrentThickness", BaseUnit.Millimeter, 2)]
    public void EdgebandMeasurements_PreserveTheCorrectUnits(Type type, string name, BaseUnit expected, int decimals)
    {
        var metadata = type.GetProperty(name)!.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>() ??
            type.GetInterfaces().Select(interfaceType => interfaceType.GetProperty(name)
                ?.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>()).FirstOrDefault(attribute => attribute != null);
        metadata.ShouldNotBeNull();
        metadata.BaseUnit.ShouldBe(expected);
        metadata.DecimalsMetric.ShouldBe(decimals);
    }

    [TestMethod]
    public void Quantity_HasNoConversionMetadata()
    {
        typeof(MaterialAssistRequestBase).GetProperty("Quantity")!
            .GetCustomAttribute<ValueDependsOnUnitSystemAttribute>().ShouldBeNull();
    }
}