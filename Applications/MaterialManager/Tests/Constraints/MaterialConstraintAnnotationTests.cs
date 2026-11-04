using System.ComponentModel.DataAnnotations;
using System.Reflection;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialConstraintAnnotationTests
{
    public static IEnumerable<object[]> BoardRanges()
    {
        foreach (var type in new[] { typeof(MaterialManagerRequestBoardType), typeof(MaterialManagerUpdateBoardType), typeof(BoardType) })
        {
            yield return [type, "Length", 0.1, 19999.9];
            yield return [type, "Width", 0.1, 19999.9];
            yield return [type, "Thickness", 0.1, 999.9];
            yield return [type, "Density", 0.1, double.PositiveInfinity];
            yield return [type, "Costs", 0.1, 99999.99];
        }
    }

    [TestMethod]
    [DynamicData(nameof(BoardRanges))]
    public void BoardRanges_MatchMetricDefinitions(Type type, string member, double minimum, double maximum)
    {
        var property = type.GetProperty(member)!;
        var range = property.GetCustomAttribute<RangeAttribute>();
        range.ShouldNotBeNull($"{type.Name}.{member}");
        Convert.ToDouble(range.Minimum).ShouldBe(minimum);
        Convert.ToDouble(range.Maximum).ShouldBe(maximum);
        range.IsValid(minimum).ShouldBeTrue();
        range.IsValid(minimum - 0.01).ShouldBeFalse();
        range.IsValid(double.NaN).ShouldBeFalse();
        if (!double.IsPositiveInfinity(maximum))
        {
            range.IsValid(maximum).ShouldBeTrue();
            range.IsValid(maximum + 0.01).ShouldBeFalse();
            range.IsValid(double.PositiveInfinity).ShouldBeFalse();
        }
    }

    [TestMethod]
    public void BoardText_BoundariesAndWarningCharacters()
    {
        var request = MaterialValidationTestSupport.Board();
        request.Comments = new string('x', 300);
        request.BoardCode = "warning#code";
        Validator.ValidateObject(request, new ValidationContext(request), true);
        request.Comments += "x";
        Should.Throw<ValidationException>(() =>
            Validator.ValidateObject(request, new ValidationContext(request), true)).ShouldBeOfType<ValidationException>();
    }

    [TestMethod]
    public void BoardUpdate_AllOptionalPropertiesMayBeOmitted()
    {
        var results = new List<ValidationResult>();
        var update = new MaterialManagerUpdateBoardType();
        Validator.TryValidateObject(update, new ValidationContext(update), results, true).ShouldBeTrue();
    }

    public static IEnumerable<object[]> EdgebandAndAllocationRanges()
    {
        foreach (var type in new[] { typeof(MaterialManagerRequestEdgebandType), typeof(MaterialManagerUpdateEdgebandType), typeof(EdgebandType) })
        {
            yield return [type, "DefaultLength", 0.0, 2000.0];
            yield return [type, "Height", 0.1, 999.9];
            yield return [type, "Thickness", 0.01, 99.99];
            yield return [type, "Costs", 0.1, 99999.99];
            yield return [type, "TotalLengthAvailableWarningLimit", 0.0, 999999.99];
            foreach (var name in new[] { "Airtec", "Lasertec", "FunctionLayerThickness", "ProtectionFilmThickness" })
                yield return [type, name, 0.0, double.PositiveInfinity];
        }

        foreach (var type in new[] { typeof(EdgebandTypeAllocationRequest), typeof(EdgebandTypeAllocationUpdate) })
            yield return [type, "AllocatedLength", 0.0, 99999.0];
        foreach (var type in new[] { typeof(BoardTypeAllocationRequest), typeof(BoardTypeAllocationUpdate) })
            yield return [type, "Quantity", 1.0, (double)int.MaxValue];
    }

    [TestMethod]
    [DynamicData(nameof(EdgebandAndAllocationRanges))]
    public void RemainingRanges_MatchMappedDefinitions(Type type, string member, double minimum, double maximum)
    {
        var range = type.GetProperty(member)!.GetCustomAttribute<RangeAttribute>();
        range.ShouldNotBeNull($"{type.Name}.{member}");
        Convert.ToDouble(range.Minimum).ShouldBe(minimum);
        Convert.ToDouble(range.Maximum).ShouldBe(maximum);
        range.IsValid(minimum).ShouldBeTrue();
        range.IsValid(minimum - 1).ShouldBeFalse();
        if (!double.IsPositiveInfinity(maximum))
        {
            range.IsValid(maximum).ShouldBeTrue();
            if (range.OperandType != typeof(int))
                range.IsValid(maximum + 1).ShouldBeFalse();
        }
    }

    public static IEnumerable<object[]> TextRules()
    {
        foreach (var type in new[] { typeof(MaterialManagerRequestBoardType), typeof(MaterialManagerUpdateBoardType), typeof(BoardType) })
        {
            yield return [type, "BoardCode", 50];
            yield return [type, "MaterialCode", 50];
            yield return [type, "EmbossingTop", 50];
            yield return [type, "EmbossingBottom", 50];
        }

        foreach (var type in new[] { typeof(MaterialManagerRequestEdgebandType), typeof(MaterialManagerUpdateEdgebandType), typeof(EdgebandType) })
        {
            yield return [type, "EdgebandCode", 50];
            yield return [type, "DecorEmbossingCode", 50];
            yield return [type, "TechnologyMacro", 50];
        }

        foreach (var type in new[]
                 {
                     typeof(MaterialManagerRequestBoardType), typeof(MaterialManagerUpdateBoardType),
                     typeof(BoardType), typeof(MaterialManagerRequestEdgebandType), typeof(MaterialManagerUpdateEdgebandType),
                     typeof(EdgebandType)
                 })
        {
            foreach (var member in new[] { "ManufacturerName", "DecorCode", "DecorName", "ArticleNumber", "Gtin", "DecorId" })
                yield return [type, member, 50];
            yield return [type, "ProductName", 200];
            yield return [type, "Comments", 300];
        }
    }

    [TestMethod]
    [DynamicData(nameof(TextRules))]
    public void TextRules_EnforceMaximumWithoutBlockingWarnings(Type type, string name, int maximum)
    {
        var rule = type.GetProperty(name)!.GetCustomAttribute<StringLengthAttribute>();
        rule.ShouldNotBeNull($"{type.Name}.{name}");
        rule.MaximumLength.ShouldBe(maximum);
        rule.IsValid(new string('#', maximum)).ShouldBeTrue();
        rule.IsValid(new string('x', maximum + 1)).ShouldBeFalse();
        rule.IsValid(null).ShouldBeTrue();
    }

    [TestMethod]
    public void TextureDimensions_PreserveExistingTypesWithoutNewRangeRequirements()
    {
        foreach (var name in new[] { "Length", "Width" })
        {
            var property = typeof(AdditionalDataTexture).GetProperty(name)!;
            property.PropertyType.ShouldBe(typeof(double));
            property.GetCustomAttribute<RangeAttribute>().ShouldBeNull();
        }
    }
}