using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.Base.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shouldly;

namespace HomagConnect.Base.Tests.Extensions;

[TestClass]
public class PropertyValidationTests
{
    [TestMethod]
    [DataRow(null, true)]
    [DataRow(0.01, false)]
    [DataRow(0.099, false)]
    [DataRow(0.1, true)]
    [DataRow(999.9, true)]
    [DataRow(1000.0, false)]
    public void ThicknessInterface_UsesCorrectedMetricMinimum(double? thickness, bool expectedValid)
    {
        var item = new FullDimensions { Thickness = thickness };
        var results = new List<ValidationResult>();

        PropertyValidation.TryValidateProperty(thickness,
            new ValidationContext(item) { MemberName = nameof(FullDimensions.Thickness) }, results).ShouldBe(expectedValid);
        PropertyValidation.TryValidateObject(item, new ValidationContext(item), new List<ValidationResult>())
            .ShouldBe(expectedValid);
        DataAnnotationsValidator.TryValidateObjectRecursive(item, out _).ShouldBe(expectedValid);
    }

    [TestMethod]
    [DataRow(0.0, false)]
    [DataRow(0.1, true)]
    [DataRow(19999.9, true)]
    [DataRow(20000.0, false)]
    public void InterfaceRange_ValidatesOnlyTheSelectedProperty(double length, bool expectedValid)
    {
        var item = new Dimensions { Width = 20000 };
        var results = new List<ValidationResult>();

        PropertyValidation.TryValidateProperty(length,
            new ValidationContext(item) { MemberName = nameof(Dimensions.Length) }, results).ShouldBe(expectedValid);
        results.Count.ShouldBe(expectedValid ? 0 : 1);
        if (!expectedValid)
        {
            results.Single().MemberNames.ShouldBe([nameof(Dimensions.Length)]);
        }
    }

    [TestMethod]
    public void OptionalInterfaceDimension_AllowsNull()
    {
        var item = new Dimensions();
        PropertyValidation.TryValidateProperty(null,
            new ValidationContext(item) { MemberName = nameof(Dimensions.Length) },
            new List<ValidationResult>()).ShouldBeTrue();
        PatchBuilder<Dimensions>.For().Set(item => item.Length, (double?)null).Build()
            .ShouldContainKey("length");
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow(0.1)]
    [DataRow(11.0)]
    [DataRow(20000.0)]
    public void Patch_EnforcesClassAndInterfaceRules(double? length)
    {
        Should.Throw<ValidationException>(() =>
            PatchBuilder<RestrictedDimensions>.For().Set(item => item.Length, length));
    }

    [TestMethod]
    public void Patch_AcceptsIntersectionOfClassAndInterfaceRules()
    {
        PatchBuilder<RestrictedDimensions>.For().Set(item => item.Length, (double?)5).Build()["length"].ShouldBe(5.0);
    }

    [TestMethod]
    public void RecursiveValidation_UsesInheritedInterfaceRules()
    {
        DataAnnotationsValidator.TryValidateObjectRecursive(new Dimensions { Length = 20000 }, out var results)
            .ShouldBeFalse();
        results.ShouldContain(result => result.MemberNames.Contains(nameof(Dimensions.Length)));
    }

    [TestMethod]
    public void FlatValidation_ChecksInterfacesWithoutTraversingNestedObjects()
    {
        var item = new NestedDimensions { Length = 100, Child = new Dimensions { Length = 20000 } };
        PropertyValidation.TryValidateObject(item, new ValidationContext(item), new List<ValidationResult>())
            .ShouldBeTrue();
        DataAnnotationsValidator.TryValidateObjectRecursive(item, out _).ShouldBeFalse();

        item.Length = 20000;
        var results = new List<ValidationResult>();
        PropertyValidation.TryValidateObject(item, new ValidationContext(item), results).ShouldBeFalse();
        results.Single().MemberNames.ShouldBe([nameof(Dimensions.Length)]);
    }

    public interface IBoardDimensions : IDimensionPropertiesLengthWidth
    {
    }

    public class Dimensions : IBoardDimensions
    {
        public double? Length { get; set; }

        public double? Width { get; set; }
    }

    public class RestrictedDimensions : IBoardDimensions
    {
        [Required]
        [Range(1.0, 10.0)]
        public double? Length { get; set; }

        public double? Width { get; set; }
    }

    public class FullDimensions : IDimensionProperties
    {
        public double? Length { get; set; }

        public double? Width { get; set; }

        public double? Thickness { get; set; }
    }

    public class NestedDimensions : Dimensions
    {
        public Dimensions? Child { get; set; }
    }
}
