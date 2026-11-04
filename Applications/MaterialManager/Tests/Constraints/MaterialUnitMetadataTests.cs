using System.Reflection;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialUnitMetadataTests
{
    [TestMethod]
    [DynamicData(nameof(Measurements))]
    public void Measurement_HasCorrectBaseUnit(Type type, string member, BaseUnit expected)
    {
        var metadata = type.GetProperty(member)!.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>();
        metadata.ShouldNotBeNull($"{type.Name}.{member}");
        metadata.BaseUnit.ShouldBe(expected);
    }

    public static IEnumerable<object[]> Measurements()
    {
        foreach (var name in new[] { "Length", "Width" })
            yield return [typeof(AdditionalDataTexture), name, BaseUnit.Millimeter];
        foreach (var type in new[] { typeof(EdgebandTypeAllocationRequest), typeof(EdgebandTypeAllocationUpdate) })
        foreach (var name in new[] { "AllocatedLength", "UsedLength" })
            yield return [type, name, BaseUnit.Meter];
        foreach (var type in new[] { typeof(MaterialManagerRequestBoardType), typeof(MaterialManagerUpdateBoardType) })
        {
            foreach (var member in new[] { "Length", "Width", "Thickness" })
                yield return [type, member, BaseUnit.Millimeter];
            yield return [type, "Density", BaseUnit.KilogramPerCubicMeter];
        }

        foreach (var type in new[] { typeof(MaterialManagerRequestEdgebandType), typeof(MaterialManagerUpdateEdgebandType) })
        {
            yield return [type, "DefaultLength", BaseUnit.Meter];
            yield return [type, "TotalLengthAvailableWarningLimit", BaseUnit.Meter];
            foreach (var member in new[] { "Height", "Thickness", "FunctionLayerThickness", "ProtectionFilmThickness" })
                yield return [type, member, BaseUnit.Millimeter];
            yield return [type, "Airtec", BaseUnit.Bar];
        }
    }

    [TestMethod]
    public void UnitlessAndUnsupportedFields_HaveNoConversionMetadata()
    {
        foreach (var row in new[]
                 {
                     (typeof(MaterialManagerRequestBoardType), "Costs"),
                     (typeof(MaterialManagerRequestBoardType), "TotalQuantityAvailableWarningLimit"),
                     (typeof(MaterialManagerRequestEdgebandType), "Lasertec"),
                     (typeof(BoardTypeAllocationRequest), "Quantity")
                 })
            row.Item1.GetProperty(row.Item2)!.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>().ShouldBeNull();
    }
}