using System.ComponentModel.DataAnnotations;
using System.Reflection;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;

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
        Assert.IsNotNull(range, $"{type.Name}.{member}");
        Assert.AreEqual(minimum, Convert.ToDouble(range.Minimum));
        Assert.AreEqual(maximum, Convert.ToDouble(range.Maximum));
        Assert.IsTrue(range.IsValid(minimum));
        Assert.IsFalse(range.IsValid(minimum - 0.01));
        Assert.IsFalse(range.IsValid(double.NaN));
        if (!double.IsPositiveInfinity(maximum))
        {
            Assert.IsTrue(range.IsValid(maximum));
            Assert.IsFalse(range.IsValid(maximum + 0.01));
            Assert.IsFalse(range.IsValid(double.PositiveInfinity));
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
        Assert.ThrowsExactly<ValidationException>(() =>
            Validator.ValidateObject(request, new ValidationContext(request), true));
    }

    [TestMethod]
    public void BoardUpdate_AllOptionalPropertiesMayBeOmitted()
    {
        var results = new List<ValidationResult>();
        var update = new MaterialManagerUpdateBoardType();
        Assert.IsTrue(Validator.TryValidateObject(update, new ValidationContext(update), results, true));
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
        Assert.IsNotNull(range, $"{type.Name}.{member}");
        Assert.AreEqual(minimum, Convert.ToDouble(range.Minimum));
        Assert.AreEqual(maximum, Convert.ToDouble(range.Maximum));
        Assert.IsTrue(range.IsValid(minimum));
        Assert.IsFalse(range.IsValid(minimum - 1));
        if (!double.IsPositiveInfinity(maximum))
        {
            Assert.IsTrue(range.IsValid(maximum));
            if (range.OperandType != typeof(int))
                Assert.IsFalse(range.IsValid(maximum + 1));
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
        Assert.IsNotNull(rule, $"{type.Name}.{name}");
        Assert.AreEqual(maximum, rule.MaximumLength);
        Assert.IsTrue(rule.IsValid(new string('#', maximum)));
        Assert.IsFalse(rule.IsValid(new string('x', maximum + 1)));
        Assert.IsTrue(rule.IsValid(null));
    }

    [TestMethod]
    public void TextureDimensions_PreserveExistingTypesWithoutNewRangeRequirements()
    {
        foreach (var name in new[] { "Length", "Width" })
        {
            var property = typeof(AdditionalDataTexture).GetProperty(name)!;
            Assert.AreEqual(typeof(double), property.PropertyType);
            Assert.IsNull(property.GetCustomAttribute<RangeAttribute>());
        }
    }
}