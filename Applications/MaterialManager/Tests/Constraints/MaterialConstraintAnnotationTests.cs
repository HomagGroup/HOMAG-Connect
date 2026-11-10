using System.ComponentModel.DataAnnotations;
using System.Reflection;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Constraints;

[TestClass]
public class MaterialConstraintAnnotationTests
{
    [TestMethod]
    [DataRow(nameof(BoardType.Length))]
    [DataRow(nameof(BoardType.Width))]
    [DataRow(nameof(BoardType.Thickness))]
    public void BoardDimensions_DeclareSharedRulesOnlyOnTheInterface(string member)
    {
        typeof(IDimensionProperties).IsAssignableFrom(typeof(BoardType)).ShouldBeTrue();
        var property = typeof(BoardType).GetProperty(member)!;
        property.GetCustomAttribute<RangeAttribute>().ShouldBeNull();
        property.GetCustomAttribute<ValueDependsOnUnitSystemAttribute>().ShouldBeNull();
        if (member == nameof(BoardType.Thickness))
        {
            property.GetCustomAttribute<RequiredAttribute>().ShouldBeNull();
        }
        else
        {
            property.GetCustomAttribute<RequiredAttribute>().ShouldNotBeNull();
        }
        property.GetCustomAttribute<DisplayAttribute>().ShouldBeNull();
    }

    [TestMethod]
    [DataRow(nameof(BoardType.Length), 0.09, false)]
    [DataRow(nameof(BoardType.Length), 0.1, true)]
    [DataRow(nameof(BoardType.Length), 19999.9, true)]
    [DataRow(nameof(BoardType.Length), 20000.0, false)]
    [DataRow(nameof(BoardType.Width), 0.09, false)]
    [DataRow(nameof(BoardType.Width), 0.1, true)]
    [DataRow(nameof(BoardType.Width), 19999.9, true)]
    [DataRow(nameof(BoardType.Width), 20000.0, false)]
    [DataRow(nameof(BoardType.Thickness), 0.09, false)]
    [DataRow(nameof(BoardType.Thickness), 0.1, true)]
    [DataRow(nameof(BoardType.Thickness), 999.9, true)]
    [DataRow(nameof(BoardType.Thickness), 1000.0, false)]
    public void BoardDimensions_RecursiveValidationEnforcesInterfaceRange(string member, double value, bool expectedValid)
    {
        var board = new BoardType
        {
            BoardCode = "board", MaterialCode = "material", Length = 2800, Width = 2070, Thickness = 19
        };
        typeof(BoardType).GetProperty(member)!.SetValue(board, value);

        DataAnnotationsValidator.TryValidateObjectRecursive(board, out var results).ShouldBe(expectedValid);
        if (expectedValid)
        {
            results.ShouldBeEmpty();
        }
        else
        {
            results.ShouldContain(result => result.MemberNames.Contains(member));
        }
    }

    public static IEnumerable<object[]> BoardRanges()
    {
        foreach (var type in new[] { typeof(MaterialManagerRequestBoardType), typeof(MaterialManagerUpdateBoardType), typeof(BoardType) })
        {
            yield return [type, "Length", 0.1, 19999.9];
            yield return [type, "Width", 0.1, 19999.9];
            yield return [type, "Thickness", 0.1, 999.9];
            yield return [type, "Density", 0.1, double.PositiveInfinity];
            yield return [type, "Costs", 0.0, double.PositiveInfinity];
        }
    }

    [TestMethod]
    [DynamicData(nameof(BoardRanges))]
    public void BoardRanges_MatchMetricDefinitions(Type type, string member, double minimum, double maximum)
    {
        var property = type.GetProperty(member)!;
        var range = PropertyValidation.GetValidationAttributes(type, property).OfType<RangeAttribute>().SingleOrDefault();
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

    [TestMethod]
    public void MaterialCreationAndAllocation_RequirednessMatchesBackend()
    {
        var defaultLength = typeof(MaterialManagerRequestEdgebandType).GetProperty(nameof(MaterialManagerRequestEdgebandType.DefaultLength))!;
        defaultLength.PropertyType.ShouldBe(typeof(double?));
        defaultLength.GetCustomAttribute<RequiredAttribute>().ShouldBeNull();

        typeof(BoardTypeAllocationRequest).GetProperty(nameof(BoardTypeAllocationRequest.Source))!
            .GetCustomAttribute<RequiredAttribute>().ShouldBeNull();
        typeof(EdgebandTypeAllocationRequest).GetProperty(nameof(EdgebandTypeAllocationRequest.AllocatedLength))!
            .GetCustomAttribute<RequiredAttribute>().ShouldNotBeNull();
        typeof(BoardTypeAllocationRequest).GetProperty(nameof(BoardTypeAllocationRequest.Workstation))!
            .GetCustomAttribute<RequiredAttribute>().ShouldBeNull();
        typeof(EdgebandTypeAllocationRequest).GetProperty(nameof(EdgebandTypeAllocationRequest.Workstation))!
            .GetCustomAttribute<RequiredAttribute>().ShouldBeNull();

        var boardAllocation = new BoardTypeAllocationRequest
        {
            BoardTypeCode = "board",
            CreatedBy = "user",
            Name = "allocation",
            Quantity = 1,
            Workstation = null
        };
        Validator.TryValidateObject(boardAllocation, new ValidationContext(boardAllocation), null, true).ShouldBeTrue();

        var edgebandAllocation = new EdgebandTypeAllocationRequest
        {
            EdgebandCode = "edge",
            Order = "order",
            CreatedBy = "user",
            AllocatedLength = 10,
            Workstation = null
        };
        Validator.TryValidateObject(edgebandAllocation, new ValidationContext(edgebandAllocation), null, true).ShouldBeTrue();
    }

    public static IEnumerable<object[]> EdgebandAndAllocationRanges()
    {
        foreach (var type in new[] { typeof(MaterialManagerRequestEdgebandType), typeof(MaterialManagerUpdateEdgebandType), typeof(EdgebandType) })
        {
            yield return [type, "DefaultLength", 0.0, 2000.0];
            yield return [type, "Height", 0.1, 999.9];
            yield return [type, "Thickness", 0.01, 99.99];
            yield return [type, "Costs", 0.0, double.PositiveInfinity];
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