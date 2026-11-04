using System.ComponentModel.DataAnnotations;
using System.Reflection;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;

using Shouldly;

namespace HomagConnect.MaterialAssist.Tests.Constraints;

[TestClass]
public class MaterialConstraintAnnotationTests
{
    [TestMethod]
    [DynamicData(nameof(Ranges))]
    public void MappedRange_AcceptsInclusiveBoundaries(Type type, string name, double minimum, double maximum)
    {
        var rule = PropertyValidation.GetValidationAttributes(type, type.GetProperty(name)!)
            .OfType<RangeAttribute>().SingleOrDefault();
        rule.ShouldNotBeNull();
        Convert.ToDouble(rule.Minimum).ShouldBe(minimum, $"{type.Name}.{name}");
        Convert.ToDouble(rule.Maximum).ShouldBe(maximum, $"{type.Name}.{name}");
        rule.IsValid(minimum).ShouldBeTrue();
        rule.IsValid(maximum).ShouldBeTrue();
        rule.IsValid(minimum - 1).ShouldBeFalse();
        rule.IsValid(maximum + 1).ShouldBeFalse();
    }

    [TestMethod]
    public void OptionalUpdates_DoNotAcquireCreationRequiredness()
    {
        foreach (var update in new object[] { new MaterialAssistUpdateBoardEntity(), new MaterialAssistUpdateEdgebandEntity() })
            Validator.ValidateObject(update, new ValidationContext(update), true);
    }

    public static IEnumerable<object[]> Ranges()
    {
        foreach (var type in new[]
                 {
                     typeof(MaterialAssistRequestOffcutEntity), typeof(MaterialAssistRequestTemplateEntity),
                     typeof(MaterialAssistUpdateBoardEntity), typeof(MaterialAssistStoreBoardEntity)
                 })
        foreach (var name in new[] { "Length", "Width" })
            yield return [type, name, 0.1, 19999.9];
        foreach (var type in new[]
                 {
                     typeof(MaterialAssistRequestBoardEntity), typeof(MaterialAssistRequestEdgebandEntity),
                     typeof(MaterialAssistUpdateBoardEntity), typeof(MaterialAssistUpdateEdgebandEntity)
                 })
            yield return [type, "Quantity", 1.0, 9999.0];
        foreach (var type in new[] { typeof(MaterialAssistRequestEdgebandEntity), typeof(MaterialAssistUpdateEdgebandEntity) })
        {
            yield return [type, "Length", 0.0, 2000.0];
            yield return [type, "CurrentThickness", 0.01, 99.99];
        }

        yield return [typeof(MaterialAssistStoreEdgebandEntity), "Length", 0.0, 2000.0];
    }
}