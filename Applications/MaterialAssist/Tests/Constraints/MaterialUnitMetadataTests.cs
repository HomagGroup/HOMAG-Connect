using System.Reflection;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

[TestClass]
public class MaterialUnitMetadataTests
{
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
            var metadata = type.GetProperty(name)!.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>();
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
        var metadata = type.GetProperty(name)!.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>();
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